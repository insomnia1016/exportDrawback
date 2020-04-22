using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_QueryAndReports_addEntryDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            
           
        }
    }

   
    private void clean()
    {
        owner_name.Text = "";
        d_date.Text = "";
        agent_name.Text = "";
        entry_id.Text = "";
        BindData();
    }
    /// <summary>
    /// 绑定repeater的数据源
    /// </summary>
    private void BindData()
    {
        DataTable dt = DefineDataTableSchema(hfRptColumns.Value);
        LoadEmptyData(dt);
        
        rptTest.DataSource = dt;
        rptTest.DataBind();
    }
    /// <summary>
    /// 根据repeater相对应的列名，定义数据源datatable的schema
    /// </summary>
    /// <param name="columns">列名</param>
    /// <returns></returns>
    public DataTable DefineDataTableSchema(string columns)
    {
        DataTable dt = new DataTable();
        string[] columnsAry = columns.Split(',');
        foreach (string str in columnsAry)
        {
            dt.Columns.Add(str);
        }
        return dt;
    }

    /// <summary>
    /// 生成空数据
    /// </summary>
    /// <param name="dt"></param>
    private void LoadEmptyData(DataTable dt)
    {
        //默认显示一行空数据
        DataRow row = dt.NewRow();
        dt.Rows.Add(row);
    }
    protected void add_Click(object sender, EventArgs e)
    {


        DataTable dt = DefineDataTableSchema("owner_name,d_date,agent_name,sale_bill_no,entry_id,g_name,code_ts,g_no,g_qty,g_unit,trade_curr,decl_price,decl_total,drawback_rate,id,operator");
        foreach (RepeaterItem item in rptTest.Items)
        {
            DataRow newRow = dt.NewRow();

            string g_no = ((TextBox)item.FindControl("txt_g_no")).Text;
            if (string.IsNullOrEmpty(g_no))
            {
                //如果项号是空，则过滤此条记录
                continue;
            }
            newRow["g_no"] = g_no;
            newRow["owner_name"] = owner_name.Text.Trim();
            newRow["d_date"] = d_date.Text.Trim();
            newRow["agent_name"] = agent_name.Text.Trim();
            newRow["entry_id"] = entry_id.Text.Trim();
            newRow["sale_bill_no"] = txt_sale_bill_no.Text.Trim();
            string g_name = ((TextBox)item.FindControl("txt_g_name")).Text;
            if (string.IsNullOrEmpty(g_name))
            {
                //如果商品名称是空，则过滤此条记录
                continue;
            }
            newRow["g_name"] = g_name;
            newRow["code_ts"] = ((TextBox)item.FindControl("txt_code_ts")).Text;
            string g_qty = ((TextBox)item.FindControl("txt_g_qty")).Text;
            if (string.IsNullOrEmpty(g_qty)) continue;
            newRow["g_qty"] = g_qty;
            newRow["g_unit"] = ((TextBox)item.FindControl("txt_g_unit")).Text;
            newRow["trade_curr"] = ((TextBox)item.FindControl("txt_trade_curr")).Text;
            string decl_price = ((TextBox)item.FindControl("txt_decl_price")).Text;
            if (string.IsNullOrEmpty(decl_price)) continue;
            newRow["decl_price"] = decl_price;
            string decl_total = ((TextBox)item.FindControl("txt_decl_total")).Text;
            if (string.IsNullOrEmpty(decl_total)) continue;
            newRow["decl_total"] = decl_total;
            string drawback_rate = ((TextBox)item.FindControl("txt_drawback_rate")).Text;
            if (string.IsNullOrEmpty(drawback_rate)) continue;
            newRow["drawback_rate"] = drawback_rate;
            newRow["id"] = UserInfoAdapter.CurrentUser.PersonId;
            newRow["operator"] = UserInfoAdapter.CurrentUser.Name;

            dt.Rows.Add(newRow);
        }

        EntryAdapter ea = new EntryAdapter();
        try
        {
            ea.insertEntryList(dt);
            clean();
            Label1.Text = "哟，小伙子，不错，被你录入成功了";
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }


    }

    #region 添加一行
    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        //首先，恢复数据源
        DataTable dt = DefineDataTableSchema(hfRptColumns.Value);
        foreach (RepeaterItem item in rptTest.Items)
        {
            DataRow newRow = dt.NewRow();
            newRow["g_name"] = ((TextBox)item.FindControl("txt_g_name")).Text;
            newRow["code_ts"] = ((TextBox)item.FindControl("txt_code_ts")).Text;
            newRow["g_no"] = ((TextBox)item.FindControl("txt_g_no")).Text;
            newRow["g_qty"] = ((TextBox)item.FindControl("txt_g_qty")).Text;
            newRow["g_unit"] = ((TextBox)item.FindControl("txt_g_unit")).Text;
            newRow["trade_curr"] = ((TextBox)item.FindControl("txt_trade_curr")).Text;
            newRow["decl_price"] = ((TextBox)item.FindControl("txt_decl_price")).Text;
            newRow["decl_total"] = ((TextBox)item.FindControl("txt_decl_total")).Text;
            newRow["drawback_rate"] = ((TextBox)item.FindControl("txt_drawback_rate")).Text;
            dt.Rows.Add(newRow);
        }

        //添加一行
        DataRow row = dt.NewRow();
        dt.Rows.Add(row);
        rptTest.DataSource = dt;
        rptTest.DataBind();
    }
    protected void txt_g_qty_Click(object sender, EventArgs e)
    {
        RepeaterItem rm = (sender as TextBox).Parent as RepeaterItem;
        string g_qty = ((TextBox)rm.FindControl("txt_g_qty")).Text.Trim();
        string decl_price = ((TextBox)rm.FindControl("txt_decl_price")).Text.Trim();
        if (string.IsNullOrEmpty(g_qty) || string.IsNullOrEmpty(decl_price))
        {
            return;
        }
        else
        {
            decimal qty = Decimal.Parse(g_qty);
            decimal price = Decimal.Parse(decl_price);
            decimal total = Math.Round(qty * price, 2);
            ((TextBox)rm.FindControl("txt_decl_total")).Text = total.ToString();
        }
    }
    #endregion
}