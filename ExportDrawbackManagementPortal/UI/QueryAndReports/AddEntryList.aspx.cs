using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_AddEntryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label1.Text = "";
            //decl_total.Attributes.Add("readonly", "true");  
        }
        
    }
    private void clean()
    {
        owner_name.Text = "";
        d_date.Text = "";
        agent_name.Text = "";
        entry_id.Text = "";
        g_no.Text = "";
        g_name.Text = "";
        g_qty.Text = "";
        g_unit.Text = "";
        decl_price.Text = "";
        decl_total.Text = "";
        code_ts.Text = "";
        drawback_rate.Text = "";
    }
    protected void add_Click(object sender, EventArgs e)
    {
        T_EntryList entity = new T_EntryList();
        entity.OwnerName = owner_name.Text.Trim();
        entity.DDate = DateTime.Parse(d_date.Text.Trim());
        entity.AgentName = agent_name.Text.Trim();
        entity.EntryId = entry_id.Text.Trim();
        entity.GNo = long.Parse(g_no.Text.Trim());
        entity.GName = g_name.Text.Trim();
        entity.GQty = decimal.Parse(g_qty.Text.Trim());
        entity.GUnit = g_unit.Text.Trim();
        entity.DeclPrice = decimal.Parse(decl_price.Text.Trim());
        entity.DeclTotal = decimal.Parse(decl_total.Text.Trim());
        entity.CodeTs = code_ts.Text.Trim();
        entity.DrawbackRate = decimal.Parse(drawback_rate.Text.Trim());
        entity.Id =Int32.Parse(UserInfoAdapter.CurrentUser.PersonId);
        entity.Operator = UserInfoAdapter.CurrentUser.Name;
        EntryAdapter ea = new EntryAdapter();

        try
        {
            ea.addEntryList(entity);
            clean();
            Label1.Text = "哟，小伙子，不错，被你录入成功了";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

     }
}