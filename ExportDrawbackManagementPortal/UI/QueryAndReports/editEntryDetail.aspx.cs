using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_editEntryDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }

    private void GridViewBind()
    {
        EntryAdapter ea = new EntryAdapter();
        DataSet ds = ea.getListsAll();
        show(ds);

    }
    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int index = GridView1.SelectedIndex;
        if (index < 0)
        {
            Label1.Text = "请先点击某行的编辑按钮";
            return;
        }
        T_EntryList item = new T_EntryList();
        item.OwnerName = owner_name.Text.Trim();
        item.DDate = DateTime.Parse(d_date.Text.Trim());
        item.AgentName = agent_name.Text.Trim();
        item.EntryId = entry_id.Text.Trim();
        item.GName = txt_g_name.Text.Trim();
        item.CodeTs = txt_code_ts.Text;
        item.GNo = Int32.Parse(txt_g_no.Text.Trim());
        item.GQty = Decimal.Parse(txt_g_qty.Text.Trim());
        item.GUnit = txt_g_unit.Text;
        item.TradeCurr = txt_trade_curr.Text;
        item.DeclPrice = Decimal.Parse(txt_decl_price.Text.Trim());
        item.DeclTotal = Decimal.Parse(txt_decl_total.Text.Trim());
        item.DrawbackRate = Decimal.Parse(txt_drawback_rate.Text.Trim());
        item.Id = Int32.Parse(UserInfoAdapter.CurrentUser.PersonId);
        item.Operator = UserInfoAdapter.CurrentUser.Name;

        EntryAdapter ea = new EntryAdapter();
        try
        {
            ea.update(item);
            GridView1.SelectedIndex = -1;
            GridViewBind();
            clean();
            Label1.Text = "更新成功";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

     protected void btnQuery_Click(object sender, EventArgs e)
    {
         EntryAdapter ea = new EntryAdapter();
         T_EntryList item = new T_EntryList();
         item.EntryId = txtEntryId.Text.Trim();
         DataSet ds = ea.queryEntryList(item);
         show(ds);
         clean();
    }
    private void clean()
    {
        owner_name.Text = "";
        d_date.Text = "";
        agent_name.Text = "";
        entry_id.Text = "";

        txt_g_name.Text = "";
        txt_code_ts.Text = "";
        txt_g_no.Text = "";
        txt_g_qty.Text = "";
        txt_g_unit.Text = "";
        txt_trade_curr.Text = "";
        txt_decl_price.Text = "";
        txt_decl_total.Text = "";
        txt_drawback_rate.Text = "";

        GridView1.SelectedIndex = -1;
       
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridView1.SelectedIndex;
        owner_name.Text = GridView1.SelectedRow.Cells[3].Text;
        d_date.Text = GridView1.SelectedRow.Cells[4].Text;
        agent_name.Text = GridView1.SelectedRow.Cells[5].Text;
        entry_id.Text = GridView1.SelectedRow.Cells[1].Text;

        txt_g_name.Text = GridView1.SelectedRow.Cells[6].Text;
        txt_code_ts.Text = GridView1.SelectedRow.Cells[12].Text;
        txt_g_no.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_g_qty.Text = GridView1.SelectedRow.Cells[7].Text;
        txt_g_unit.Text = GridView1.SelectedRow.Cells[8].Text;
        txt_trade_curr.Text = GridView1.SelectedRow.Cells[9].Text;
        txt_decl_price.Text = GridView1.SelectedRow.Cells[10].Text;
        txt_decl_total.Text = GridView1.SelectedRow.Cells[11].Text;
        txt_drawback_rate.Text = GridView1.SelectedRow.Cells[13].Text;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string[] estr = e.CommandArgument.ToString().Split(',');
            string entry_id = estr[0];
            int g_no = Convert.ToInt32(estr[1]);
            EntryAdapter ea = new EntryAdapter();
            try
            {
                ea.delete(entry_id, g_no);
                GridView1.SelectedIndex = -1;
                GridViewBind();
                clean();
                Label1.Text = "删除成功";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
}