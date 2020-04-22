using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_QueryAndReports_contract : System.Web.UI.Page
{
    private bool changed = false;
    protected List<string> SelectedItems
    {
        get { return ViewState["selecteditems"] != null ? (List<string>)ViewState["selecteditems"] : null; }
        set { ViewState["selecteditems"] = value; }
    }


    EntryAdapter ea = new EntryAdapter();
    public DataSet ds { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        updateUser.Attributes.Add("onclick", "this.form.target='_blank'");
        if (!IsPostBack)
        {
            show();
        }
    }

    private void show()
    {
        try
        {
            DataSet ds1 = ea.getEntryList();
            GridView1.DataSource = ds1;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    protected void updateUser_Click(object sender, EventArgs e)
    {


        ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("entry_id");
        dt.Columns.Add("g_no");
        dt.Columns.Add("g_name");
        dt.Columns.Add("g_qty");
        dt.Columns.Add("g_unit");
        dt.Columns.Add("sale_bill_no");

        if (!changed)
            GetSelectedItem();
        foreach (string id in (List<string>)this.SelectedItems)
        {
            string[] item = id.Split('$');
            DataRow dr = dt.NewRow();
            dr["entry_id"] = item[0];
            dr["g_no"] = item[1];
            dr["g_name"] = item[2];
            dr["g_qty"] = item[3];
            dr["g_unit"] = item[4];
            dr["sale_bill_no"] = item[5];
            dt.Rows.Add(dr);
        }

        DataRow dr1 = dt.NewRow();

        dr1["g_name"] = "合计：";
        dr1["g_qty"] = DBNull.Value;
        dr1["g_unit"] = "";
        dr1["entry_id"] = "";
        dr1["g_no"] = "";
        dr1["sale_bill_no"] = "";
        dt.Rows.Add(dr1);
        ds.Tables.Add(dt);
        Server.Transfer("contract_template.aspx");


    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
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

            string id = string.Format("{0}${1}${2}${3}${4}${5}",
               this.GridView1.DataKeys[i].Values[0].ToString(),
               this.GridView1.DataKeys[i].Values[1].ToString(),
               this.GridView1.DataKeys[i].Values[2].ToString(),
               this.GridView1.DataKeys[i].Values[3].ToString(),
               this.GridView1.DataKeys[i].Values[4].ToString(),
               this.GridView1.DataKeys[i].Values[5].ToString()
               );

            if (selecteditems.Contains(id) && !cbx.Checked)
                selecteditems.Remove(id);
            if (!selecteditems.Contains(id) && cbx.Checked)
                selecteditems.Add(id);
        }
        this.SelectedItems = selecteditems;


    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        GetSelectedItem();
        changed = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1 && this.SelectedItems != null)
        {
            CheckBox cbx = (CheckBox)e.Row.FindControl("cbItem");
            int i = e.Row.RowIndex;
            string id = string.Format("{0}${1}${2}${3}${4}${5}",
            this.GridView1.DataKeys[i].Values[0].ToString(),
            this.GridView1.DataKeys[i].Values[1].ToString(),
            this.GridView1.DataKeys[i].Values[2].ToString(),
            this.GridView1.DataKeys[i].Values[3].ToString(),
            this.GridView1.DataKeys[i].Values[4].ToString(),
            this.GridView1.DataKeys[i].Values[5].ToString()
            );
            if (SelectedItems.Contains(id))
                cbx.Checked = true;
            else
                cbx.Checked = false;
        }
    }
}