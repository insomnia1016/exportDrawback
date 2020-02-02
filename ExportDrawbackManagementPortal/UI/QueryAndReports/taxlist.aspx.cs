using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_taxlist : System.Web.UI.Page
{

    private bool changed = false;
    protected List<string> SelectedItems
    {
        get { return ViewState["selecteditems"] != null ? (List<string>)ViewState["selecteditems"] : null; }
        set { ViewState["selecteditems"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlDataBind();
            show();
        }
    }
    private void GetSelectedItem()
    {
        List<string> selecteditems = null;
        if (this.SelectedItems == null)
        {
            selecteditems = new List<string>();
        }
        else
        {
            selecteditems = this.SelectedItems;
        }

        //获取选择的记录
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)this.GridView1.Rows[i].FindControl("cbItem");

            string id = this.GridView1.DataKeys[i].Values[0].ToString();

            if (selecteditems.Contains(id) && !cbx.Checked)
                selecteditems.Remove(id);
            if (!selecteditems.Contains(id) && cbx.Checked)
                selecteditems.Add(id);
        }
        this.SelectedItems = selecteditems;

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
    
    private void init()
    {
        CalendarBox1.Text = CalendarBox2.Text = txt_tax_return_no.Text = "";
        DropDownList1.SelectedIndex = 0;
        this.SelectedItems = null;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)this.GridView1.Rows[i].FindControl("cbItem");
            cbx.Checked = false;
        }
    }
    private string translate(string str)
    {
        if (str == "&nbsp;") return "";
        return str.Trim();
    }
    
    protected void updateUser_Click(object sender, EventArgs e)
    {
        T_TaxList taxlist = new T_TaxList();
       
        if (!changed)
            GetSelectedItem();
        string[] ids = this.SelectedItems.ToArray();
        if (ids.Length == 0)
        {
            Label1.Text = "请先至少选择一行数据！";
            return;
        }
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
            tla.UpdateState(taxlist,ids);
            Label1.Text = "更新成功";
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
        if (e.Row.RowIndex > -1 && this.SelectedItems != null)
        {
            CheckBox cbx = (CheckBox)e.Row.FindControl("cbItem");
            string id = this.GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
            if (SelectedItems.Contains(id))
                cbx.Checked = true;
            else
                cbx.Checked = false;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        GetSelectedItem();
        changed = true;
    }
    protected void btn_Query_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_entryId.Text.Trim()))
        {
            show();
        }
        else
        {
            string entryId = txt_entryId.Text.Trim();
            TaxListAdapter tla = new TaxListAdapter();
            GridView1.DataSource = tla.getTaxListByEntryId(entryId);
            GridView1.DataBind();
        }
    }
}