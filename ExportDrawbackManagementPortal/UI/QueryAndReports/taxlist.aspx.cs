using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_taxlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlDataBind();
            show();
        }


    }

    private void ddlDataBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DropDownList1.DataSource = ca.getTaxReturnState();
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "code";
        DropDownList1.DataBind();
    }

    private void show()
    {
        TaxListAdapter tla = new TaxListAdapter();
        GridView1.DataSource = tla.getTaxList();
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectIndex = GridView1.SelectedIndex;
        string stateName = GridView1.SelectedRow.Cells[18].Text;
        string stateCode = getStateCode(stateName);
        DropDownList1.SelectedValue = stateCode;

        CalendarBox1.Text = translate(GridView1.SelectedRow.Cells[20].Text.Trim());
        CalendarBox2.Text = translate(GridView1.SelectedRow.Cells[19].Text.Trim());
        txt_tax_return_no.Text = translate(GridView1.SelectedRow.Cells[21].Text);
        showStyle(stateCode);

    }
    private void init()
    {
        CalendarBox1.Text = CalendarBox2.Text = txt_tax_return_no.Text = "";
        DropDownList1.SelectedIndex = 0;
    }
    private string translate(string str)
    {
        if (str == "&nbsp;") return "";
        return str.Trim();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        showStyle(ddl.SelectedValue);
    }

    private void showStyle(string value)
    {
        switch (value)
        {
            case "D":
                shenbao.Style.Add("display", "none");//显示
                tuishui.Style.Add("display", "block");//隐藏
                break;
            case "P":
                tuishui.Style.Add("display", "none");
                shenbao.Style.Add("display", "block");
                break;
            default:
                tuishui.Style.Add("display", "none");
                shenbao.Style.Add("display", "none");
                break;
        }
    }
    protected void updateUser_Click(object sender, EventArgs e)
    {
        T_TaxList taxlist = new T_TaxList();
        HiddenField hdf = GridView1.SelectedRow.Cells[0].FindControl("hdfId") as HiddenField;
        taxlist.Id = Int32.Parse(hdf.Value);
        string state = DropDownList1.SelectedValue;
        taxlist.StateCode = state;
        switch (state)
        {
            case "P":
                if (string.IsNullOrEmpty(CalendarBox1.Text))
                {
                    Label1.Text = "退税申报日期不能为空";
                    return;
                }
                else
                {
                    taxlist.TaxRetutnDDate = DateTime.Parse(CalendarBox1.Text);
                }
                if (string.IsNullOrEmpty(txt_tax_return_no.Text))
                {
                    Label1.Text = "申报批次号不能为空";
                    return;
                }
                else
                {
                    taxlist.TaxReturnNo = txt_tax_return_no.Text;
                }
                break;
            case "D":
                if (string.IsNullOrEmpty(CalendarBox2.Text))
                {
                    Label1.Text = "退税日期不能为空";
                    return;
                }
                else
                {
                    taxlist.TaxReturnDate = DateTime.Parse(CalendarBox2.Text);
                }
                break;
            default:
                taxlist.TaxReturnDate = null;
                taxlist.TaxReturnNo = "";
                taxlist.TaxRetutnDDate = null;
                break;
        }

        TaxListAdapter tla = new TaxListAdapter();
        try
        {
            tla.UpdateState(taxlist);
            Label1.Text = "更新成功";
            GridView1.SelectedIndex = -1;
            init();
            show();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }


    }

    private string getStateCode(string name)
    {
        CommonAdapter ca = new CommonAdapter();
        return ca.getTaxReturnStateCodeByName(name);
    }
    private string getStateName(string code)
    {
        CommonAdapter ca = new CommonAdapter();
        return ca.getTaxReturnStateNameByState(code);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string code = e.Row.Cells[18].Text.Trim();
            e.Row.Cells[18].Text = getStateName(code);
        }
    }
}