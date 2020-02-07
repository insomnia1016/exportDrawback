using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_QueryAndReports_ProfitBudgetList : System.Web.UI.Page
{
    public static decimal profit_all { get; set; }
    public static string sale_bill_no { get; set; }
    public static int finterid { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            GridViewBind();
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        //收集利润预算表头信息
        T_ProfitBudget item = new T_ProfitBudget();
        item.UpdateTime = DateTime.Now;
        decimal extra_charges = 0;
        if (!string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
            extra_charges = Decimal.Parse(txt_extra_charges.Text.Trim());
        item.ExtraCharges = extra_charges;
        item.FInterID = finterid;
        item.SaleBillNo = sale_bill_no;
        item.ProfitAll = profit_all;
        item.AuditState = false;

        //收集利润预算表体信息
        List<T_ProfitBudgetList> lists = new List<T_ProfitBudgetList>();
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            T_ProfitBudgetList list = new T_ProfitBudgetList();
            list.FInterID = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[10].FindControl("hdf_finter_id")).Value);
            list.SaleFentryid = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[10].FindControl("hdf_sale_fentry_id")).Value);
            list.BuyFentryid = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[10].FindControl("hdf_buy_fentry_id")).Value);
            list.FItemID = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[10].FindControl("hdf_fitem_id")).Value);
            list.SaleBillNo = sale_bill_no;
            list.BuyBillNo = GridView1.Rows[i].Cells[0].Text;
            list.DeptId = Int32.Parse(GridView1.Rows[i].Cells[1].Text);
            list.EmpId = Int32.Parse(GridView1.Rows[i].Cells[2].Text);
            list.SalePrice = Decimal.Parse(GridView1.Rows[i].Cells[3].Text);
            list.ExchangeRate = Decimal.Parse(((TextBox)GridView1.Rows[i].Cells[4].FindControl("txt_exchange_rate")).Text);
            list.BuyPrice = Decimal.Parse(GridView1.Rows[i].Cells[5].Text);
            list.SaleQty = Decimal.Parse(GridView1.Rows[i].Cells[6].Text);
            list.BuyQty = Decimal.Parse(GridView1.Rows[i].Cells[7].Text);
            list.Accessory = (GridView1.Rows[i].Cells[8].FindControl("txt_accessory") as TextBox).Text;
            string accessory_price = (GridView1.Rows[i].Cells[9].FindControl("txt_accessory_price") as TextBox).Text;
            if (!String.IsNullOrEmpty(accessory_price))
            {
                list.AccessoryPrice = Decimal.Parse(accessory_price);
            }
            else
            {
                list.AccessoryPrice = 0;
            }
            list.Profit = Decimal.Parse((GridView1.Rows[i].Cells[10].FindControl("lbl_profit") as Label).Text);
            lists.Add(list);

        }
        try
        {
            ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
            pba.updateProfitBudgetHead(item);
            pba.updateProfitBudgetList(lists);
            
            Label2.Text = "保存成功！";
            Label2.Visible = true;
            GridViewBind();
            GridView2.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message;
            Label2.Visible = true;
            return;
        }
    }
    protected void txt_exchange_rate_TextChanged(object sender, EventArgs e)
    {
        //计算每一行的含税金额
        TextBox tb = sender as TextBox;
        decimal exchange_rate = 0;
        if (!string.IsNullOrEmpty(tb.Text.Trim()))
            exchange_rate = Decimal.Parse(tb.Text.Trim());

        GridViewRow row = tb.Parent.Parent as GridViewRow;
        decimal sale_qty = Decimal.Parse(row.Cells[6].Text.Trim());
        decimal buy_qty = Decimal.Parse(row.Cells[7].Text.Trim());
        decimal buy_price = Decimal.Parse(row.Cells[5].Text.Trim());
        decimal sale_price = Decimal.Parse(row.Cells[3].Text.Trim());
        decimal extra_charges = 0;
        if (!string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
            extra_charges = Decimal.Parse(txt_extra_charges.Text.Trim());
        decimal accessory_price = 0;
        if (!string.IsNullOrEmpty(((TextBox)row.Cells[9].FindControl("txt_accessory_price")).Text.Trim()))
            accessory_price = Decimal.Parse(((TextBox)row.Cells[9].FindControl("txt_accessory_price")).Text.Trim());
        calculateProfit(row, exchange_rate, sale_qty, buy_qty, buy_price, sale_price, accessory_price);
    }
    protected void txt_accessory_price_TextChanged(object sender, EventArgs e)
    {
        //计算每一行的含税金额
        TextBox tb = sender as TextBox;
        decimal accessory_price = 0;
        if (!string.IsNullOrEmpty(tb.Text.Trim()))
            accessory_price = Decimal.Parse(tb.Text.Trim());

        GridViewRow row = tb.Parent.Parent as GridViewRow;
        decimal sale_qty = Decimal.Parse(row.Cells[6].Text.Trim());
        decimal buy_qty = Decimal.Parse(row.Cells[7].Text.Trim());
        decimal buy_price = Decimal.Parse(row.Cells[5].Text.Trim());
        decimal sale_price = Decimal.Parse(row.Cells[3].Text.Trim());
        decimal exchange_rate = 0;
        if (!string.IsNullOrEmpty(((TextBox)row.Cells[4].FindControl("txt_exchange_rate")).Text.Trim()))
            exchange_rate = Decimal.Parse(((TextBox)row.Cells[4].FindControl("txt_exchange_rate")).Text.Trim());
        decimal extra_charges = 0;
        if (!string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
            extra_charges = Decimal.Parse(txt_extra_charges.Text.Trim());
        calculateProfit(row, exchange_rate, sale_qty, buy_qty, buy_price, sale_price, accessory_price);
    }
    private void calculateProfit(GridViewRow row, decimal exchange_rate, decimal sale_qty, decimal buy_qty, decimal buy_price, decimal sale_price, decimal accessory_price)
    {
        decimal profit = sale_qty * sale_price * exchange_rate - buy_qty * (buy_price + accessory_price);
        Label lb = row.Cells[10].FindControl("lbl_profit") as Label;
        lb.Text = profit.ToString("f2");

        profit_all = 0;
        //计算合计：
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Label inner_profit = GridView1.Rows[i].Cells[10].FindControl("lbl_profit") as Label;
            profit = Decimal.Parse(inner_profit.Text.ToString());
            profit_all += profit;
        }
        Label lbl_profit_all = GridView1.FooterRow.Cells[10].FindControl("lbl_profit_all") as Label;
        lbl_profit_all.Text = "总计：" + profit_all.ToString("f2");
    }
    /// <summary>
    /// 绑定GridView1
    /// </summary>
    private void show(GridView gv, DataSet ds)
    {
        if (gv == GridView1)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.submit.Enabled = true;
            }
            else
            {
                this.submit.Enabled = false;
            }
        }
        gv.DataSource = ds;
        gv.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = "总计：" + profit_all;
        }

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        sale_bill_no = this.lbl_sale_bill_no.Text = GridView2.SelectedRow.Cells[1].Text;
        this.txt_extra_charges.Text = GridView2.SelectedRow.Cells[2].Text;
        finterid = Int32.Parse((GridView2.SelectedRow.Cells[0].FindControl("hdf_finter_id") as HiddenField).Value);
        profit_all = Decimal.Parse(GridView2.SelectedRow.Cells[4].Text);
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        DataSet ds = pba.getProfitBudgetByID(finterid, sale_bill_no);

        show(GridView1, ds);

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sale_bill_no = e.CommandArgument.ToString();

            ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
            try
            {
                pba.deleteBySaleBillNo(sale_bill_no);
                GridViewBind();
                clean();
                GridView1.DataSource = "";
                GridView1.DataBind();
                Label2.Visible = true;
                Label2.Text = "删除成功";
            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
            }

        }
    }
    private void GridViewBind()
    {
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        show(GridView2, pba.getProfitBudgetSummary());

    }
    private void clean()
    {

        this.lbl_sale_bill_no.Text = "";
        this.txt_extra_charges.Text = "";
        this.submit.Enabled = false;
        Label2.Text = "";
        Label2.Visible = false;
        GridView2.SelectedIndex = -1;

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}