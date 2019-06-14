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
    EntryAdapter ea = new EntryAdapter();
    public DataSet ds { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        updateUser.Attributes.Add("onclick", "this.form.target='_blank'"); 
        if(!IsPostBack){
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
        catch(Exception ex)
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
       


        CheckBox cb;
        foreach (GridViewRow item in GridView1.Rows)
        {
            cb = (CheckBox)item.FindControl("cbItem");
            if (cb.Checked)
            {
                int index = item.RowIndex;
                DataRow dr = dt.NewRow();
                dr["entry_id"] = GridView1.Rows[index].Cells[1].Text;
                dr["g_no"] = GridView1.Rows[index].Cells[2].Text;
                dr["g_name"] = GridView1.Rows[index].Cells[6].Text;
                dr["g_qty"] = GridView1.Rows[index].Cells[7].Text;
                dr["g_unit"] = GridView1.Rows[index].Cells[8].Text;
                dt.Rows.Add(dr);
                
            }
        }
        DataRow dr1 = dt.NewRow();
       
        dr1["g_name"] = "合计：";
        dr1["g_qty"] = DBNull.Value;
        dr1["g_unit"] = "";
        dr1["entry_id"] = "";
        dr1["g_no"] = "";
        dt.Rows.Add(dr1);
        ds.Tables.Add(dt);
        Server.Transfer("contract_template.aspx");
       
        
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
}