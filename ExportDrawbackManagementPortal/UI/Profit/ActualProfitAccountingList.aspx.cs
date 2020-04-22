using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_Profit_ActualProfitAccountingList : System.Web.UI.Page
{
    public string sale_bill_no { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            show();
        }
    }

    private void show()
    {
        sale_bill_no = txt_bill_no.Text;
        ActualProfitAccountingAdapter apaa = new ActualProfitAccountingAdapter();
        GridView1.DataSource = apaa.getData(sale_bill_no);
        GridView1.DataBind();
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

            ActualProfitAccountingAdapter apaa = new ActualProfitAccountingAdapter();

            apaa.updateData(item);
            Label1.Text = "更新成功";
            clean();
            show();
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
            string audit_status = e.Row.Cells[9].Text;
            if (audit_status == "False")
            {
                e.Row.Cells[9].Text = "不通过";

            }
            else
            {
                e.Row.Cells[9].Text = "通过";
                Button btnSelect = e.Row.Cells[0].FindControl("btnEdit") as Button;
                btnSelect.Enabled = false;
                Button btnDelete = e.Row.Cells[0].FindControl("btnDelete") as Button;
                btnDelete.Enabled = false;
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        GridViewRow grv = GridView1.SelectedRow;
        HyperLink thisData = grv.Cells[1].Controls[0] as HyperLink;
        txt_sale_bill_no.Text = thisData.Text;
        decimal actual_amount = Decimal.Parse(grv.Cells[2].Text);
        txt_actual_amount.Text = actual_amount.ToString();
        decimal commission = decimal.Parse(grv.Cells[6].Text);
        txt_commission.Text = commission.ToString();
        decimal extra_charges = decimal.Parse(grv.Cells[5].Text);
        txt_extra_charges.Text = extra_charges.ToString();
        decimal actual_pay = decimal.Parse(grv.Cells[4].Text);
        txt_actual_pay.Text = actual_pay.ToString();

        decimal return_tax = decimal.Parse(grv.Cells[3].Text);
        txt_return_tax.Text = return_tax.ToString();
        decimal actual_profit_amount = decimal.Parse(grv.Cells[7].Text);
        lbl_actual_profit_amount.Text = actual_profit_amount.ToString();

        decimal actual_profit = decimal.Parse(grv.Cells[8].Text);
        lbl_actual_profit.Text = actual_profit.ToString();

        save.Enabled = true;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        show();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string sale_bill_no = e.CommandArgument.ToString();

            ActualProfitAccountingAdapter aa = new ActualProfitAccountingAdapter();
            ProfitAccountingAdapter paa = new ProfitAccountingAdapter();
            try
            {
                paa.IsActualAudit(sale_bill_no, false);
                aa.delete(sale_bill_no);
                GridView1.SelectedIndex = -1;
                show();
                clean();
                Label1.Text = "删除成功";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}