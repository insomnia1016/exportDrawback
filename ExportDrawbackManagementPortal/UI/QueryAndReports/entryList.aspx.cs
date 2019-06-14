using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_entryList : System.Web.UI.Page
{
    EntryAdapter ea = new EntryAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = ea.getListsAll();
            show(ds);
        }

    }

    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void query_Click(object sender, EventArgs e)
    {
        getQueryData();
       
    }

    private void getQueryData()
    {
        T_EntryList item = new T_EntryList();
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
        if (!string.IsNullOrEmpty(txt_agent_name.Text.Trim()))
        {
            item.AgentName = txt_agent_name.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_operator.Text.Trim()))
        {
            item.Operator = txt_operator.Text.Trim();
        }
        if (!string.IsNullOrEmpty(CalendarBox1.Text.Trim()))
        {
            item.startTime = DateTime.Parse(CalendarBox1.Text.Trim());
        }
        if (!string.IsNullOrEmpty(CalendarBox2.Text.Trim()))
        {
            item.endTime = DateTime.Parse(CalendarBox2.Text.Trim());
        }
        try
        {
            show(ea.queryEntryList(item));
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getQueryData();
    }
}