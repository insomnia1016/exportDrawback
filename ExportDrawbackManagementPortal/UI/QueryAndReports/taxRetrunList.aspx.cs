using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_QueryAndReports_taxRetrunList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlDataBind();
            TaxListAdapter tla = new TaxListAdapter();
            show(tla.getTaxList());
        }
    }
    private void ddlDataBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getTaxReturnState();
        DataRow dr = ds.Tables[0].NewRow();
        dr["name"] = "请选择";
        dr["code"] = "";
        ds.Tables[0].Rows.InsertAt(dr, 0);
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "code";
        DropDownList1.DataBind();
    }

    protected void query_Click(object sender, EventArgs e)
    {
        getQueryData();
    }

    private void getQueryData()
    {
        T_TaxList item = new T_TaxList();
        if (!string.IsNullOrEmpty(txt_owner_name.Text.Trim()))
        {
            item.OwnerName = txt_owner_name.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_entry_id.Text.Trim()))
        {
            item.EntryId = txt_entry_id.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_g_name.Text.Trim()))
        {
            item.GName = txt_g_name.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_code_ts.Text.Trim()))
        {
            item.CodeTs = txt_code_ts.Text.Trim();
        }
        if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
        {
            item.StateCode = DropDownList1.SelectedValue;
        }
        if (!string.IsNullOrEmpty(CalendarBox1.Text.Trim()))
        {
            item.startTime = DateTime.Parse(CalendarBox1.Text.Trim());
        }
        if (!string.IsNullOrEmpty(CalendarBox2.Text.Trim()))
        {
            item.endTime = DateTime.Parse(CalendarBox2.Text.Trim());
        }

        TaxListAdapter tla = new TaxListAdapter();
        show(tla.queryTaxList(item));
    }
    /// <summary>
    /// 绑定GridView1
    /// </summary>
    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
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
            string code = e.Row.Cells[17].Text.Trim();
            e.Row.Cells[17].Text = getStateName(code);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getQueryData();
    }
}