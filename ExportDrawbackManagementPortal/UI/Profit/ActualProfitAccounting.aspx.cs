using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_Profit_ActualProfitAccounting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            show();
        }
    }

    private void show()
    {
        ProfitAccountingAdapter paa = new ProfitAccountingAdapter();
        DataSet ds = paa.getProfitBudgetSummary();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void save_Click(object sender, EventArgs e)
    {
        try
        {
            check();
            T_ActualProfitAccounting item = new T_ActualProfitAccounting();
            item.SaleBillNo = txt_sale_bill_no.Text.Trim();
            item.ActualAmount = Decimal.Parse(txt_actual_amount.Text.Trim());
            item.ReturnTax = Decimal.Parse(txt_return_tax.Text.Trim());
            item.ActualPay = Decimal.Parse(txt_actual_pay.Text.Trim());
            item.ExtraCharges = Decimal.Parse(txt_extra_charges.Text.Trim());
            item.Commission = Decimal.Parse(txt_commission.Text.Trim());
            item.ActualProfitAmount = Decimal.Parse(lbl_actual_profit_amount.Text.Trim());
            item.ActualProfit = Decimal.Parse(lbl_actual_profit.Text.Trim());
            item.AuditStatus = false;

           
            ProfitAccountingAdapter paa = new ProfitAccountingAdapter();
            paa.IsActualAudit(item.SaleBillNo, true);

            ActualProfitAccountingAdapter apaa = new ActualProfitAccountingAdapter();

            apaa.addData(item);
            Label1.Text = "保存成功";
            clean();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }

    private void clean()
    {
        txt_sale_bill_no.Text = "";
        txt_actual_amount.Text = "";
        txt_commission.Text = "";
        txt_extra_charges.Text = "";
        txt_actual_pay.Text = "";
        txt_return_tax.Text = "";
        lbl_actual_profit_amount.Text = "";
        lbl_actual_profit.Text = "";

        GridView1.SelectedIndex = -1;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string check_status = e.Row.Cells[4].Text;
            if (check_status == "True")
            {
                e.Row.Cells[4].Text = "通过";
            }
            else
            {
                e.Row.Cells[4].Text = "不通过";
                Button btnEdit = e.Row.Cells[0].FindControl("btnEdit") as Button;
                btnEdit.Enabled = false;
            }
        }
    }
    private void check()
    {
        if (string.IsNullOrEmpty(txt_sale_bill_no.Text.Trim()))
        {
            throw new Exception("销售发单号不能为空");
        }
        if (string.IsNullOrEmpty(txt_actual_amount.Text.Trim()))
        {
            throw new Exception("实际结汇收入不能为空");
        }
        if (string.IsNullOrEmpty(txt_return_tax.Text.Trim()))
        {
            throw new Exception("实际退税不能为空");
        }
        if (string.IsNullOrEmpty(txt_actual_pay.Text.Trim()))
        {
            throw new Exception("采购产品费用不能为空");
        }
        if (string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
        {
            throw new Exception("额外费用不能为空");
        }
        if (string.IsNullOrEmpty(txt_commission.Text.Trim()))
        {
            throw new Exception("提成不能为空");
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        int index = GridView1.SelectedIndex;
        GridViewRow grv = GridView1.SelectedRow;
        string sale_bill_no = (grv.Cells[1].Controls[0] as HyperLink).Text;
        ProfitAccountingAdapter paa = new ProfitAccountingAdapter();
        DataSet ds = paa.getActualProfit(sale_bill_no);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txt_sale_bill_no.Text = sale_bill_no;
            decimal actual_amount = Decimal.Parse(dr["actual_amount"].ToString());
            txt_actual_amount.Text = actual_amount.ToString("f2");
            decimal commission = decimal.Parse(dr["commission"].ToString());
            txt_commission.Text = commission.ToString("f3");
            decimal extra_charges = decimal.Parse(dr["extra_charges"].ToString());
            txt_extra_charges.Text = extra_charges.ToString("f2");
            decimal actual_pay = decimal.Parse(dr["actual_pay"].ToString());
            txt_actual_pay.Text = actual_pay.ToString("f2");
            //退税
            TaxListAdapter tla = new TaxListAdapter();
            decimal return_tax = tla.getTaxReturnTotal(sale_bill_no);
            txt_return_tax.Text = return_tax.ToString("f2");
            decimal actual_profit_amount = actual_amount - actual_pay;
            lbl_actual_profit_amount.Text = (actual_profit_amount - extra_charges - commission + return_tax).ToString("f2");

            decimal actual_profit = (actual_profit_amount - extra_charges - commission + return_tax) / actual_amount;
            lbl_actual_profit.Text = actual_profit.ToString("f3");

            save.Enabled = true;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
    protected void txt_value_TextChanged(object sender, EventArgs e)
    {
        try
        {
            check();
            decimal actual_amount = Decimal.Parse(txt_actual_amount.Text.Trim());
            decimal commission = Decimal.Parse(txt_commission.Text.Trim());
            decimal extra_charges = Decimal.Parse(txt_extra_charges.Text.Trim());
            decimal actual_pay = Decimal.Parse(txt_actual_pay.Text.Trim());
            decimal return_tax = Decimal.Parse(txt_return_tax.Text.Trim());
            decimal actual_profit_amount = actual_amount - commission - extra_charges - actual_pay + return_tax;
            decimal actual_profit = (actual_profit_amount / actual_amount);
            lbl_actual_profit_amount.Text = actual_profit_amount.ToString();
            lbl_actual_profit.Text = actual_profit.ToString("f3");

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }
}