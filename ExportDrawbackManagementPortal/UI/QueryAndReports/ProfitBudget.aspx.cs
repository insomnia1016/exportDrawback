﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_ProfitBudget : System.Web.UI.Page
{
    public static string sale_bill_no { get; set; }
    public static int finterid { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            sale_bill_no = "";
            finterid = 0;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //翻译是否含税
            bool sale_rate = e.Row.Cells[6].Text.Trim() == "1" ? true : false;
            if (sale_rate) e.Row.Cells[6].Text = "是";
            else e.Row.Cells[6].Text = "否";
            bool buy_rate = e.Row.Cells[8].Text.Trim() == "1" ? true : false;
            if (buy_rate) e.Row.Cells[8].Text = "是";
            else e.Row.Cells[8].Text = "否";
            string buy_bill_no = e.Row.Cells[0].Text.Trim();
            if (string.IsNullOrEmpty(buy_bill_no) || buy_bill_no == "&nbsp;")
            {
                submit.Enabled = false;
            }
            
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        T_ProfitBudget item = new T_ProfitBudget();
        sale_bill_no = this.txt_sale_bill_no.Text.Trim();
        item.SaleBillNo = sale_bill_no;
        show(pba.getSEOrderInfo(item));
    }
    protected void query_Click(object sender, EventArgs e)
    {
        Label2.Text = "";
        Label2.Visible = false;

        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        
        T_ProfitBudget item = new T_ProfitBudget();
        sale_bill_no = this.txt_sale_bill_no.Text.Trim();

        //判断是否提交过
        DataSet ds = pba.getProfitBudgetSummaryByID(sale_bill_no);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Label2.Text = "该销售订单号已经提交过利润预算！";
            Label2.Visible = true;
            return;
        }

        item.SaleBillNo = sale_bill_no;
        ds = pba.getSEOrderInfo(item);
        if (ds.Tables[0].Rows.Count > 0)
        {
            finterid = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
            show(ds);
        }
       
    }

    protected void txt_volume_TextChanged(object sender, EventArgs e)
    {
        TextBox tb = sender as TextBox;
        GridViewRow row = tb.Parent.Parent as GridViewRow;
        decimal length = 0;
        if (!string.IsNullOrEmpty(((TextBox)row.Cells[13].FindControl("txt_length")).Text.Trim()))
            length = Decimal.Parse(((TextBox)row.Cells[13].FindControl("txt_length")).Text.Trim());
        decimal width = 0;
        if (!string.IsNullOrEmpty(((TextBox)row.Cells[13].FindControl("txt_width")).Text.Trim()))
            width = Decimal.Parse(((TextBox)row.Cells[13].FindControl("txt_width")).Text.Trim());
        decimal height = 0;
        if (!string.IsNullOrEmpty(((TextBox)row.Cells[13].FindControl("txt_height")).Text.Trim()))
            height = Decimal.Parse(((TextBox)row.Cells[13].FindControl("txt_height")).Text.Trim());
        decimal volume = length * width * height / 1000000;

        Label lbl_volume = row.Cells[14].FindControl("lbl_volume") as Label;
        lbl_volume.Text = volume.ToString("f3");
        if (volume > 0)
        {
            try
            {
                check(row);
            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
                Label2.Visible = true;
                return;
            }
            decimal exchange_rate = Decimal.Parse((row.Cells[5].FindControl("txt_exchange_rate") as TextBox).Text);
            decimal sale_qty = Decimal.Parse(row.Cells[9].Text.Trim());
            decimal buy_qty = Decimal.Parse(row.Cells[10].Text.Trim());
            decimal buy_price = Decimal.Parse(row.Cells[7].Text.Trim());
            decimal sale_price = Decimal.Parse(row.Cells[3].Text.Trim());
            decimal extra_charges = 0;
            if (!string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
                extra_charges = Decimal.Parse(txt_extra_charges.Text.Trim());
            decimal accessory_price = 0;
            if (!string.IsNullOrEmpty(((TextBox)row.Cells[12].FindControl("txt_accessory_price")).Text.Trim()))
                accessory_price = Decimal.Parse(((TextBox)row.Cells[12].FindControl("txt_accessory_price")).Text.Trim());
            bool sale_rate = row.Cells[6].Text == "是" ? true : false;
            bool buy_rate = row.Cells[8].Text == "是" ? true : false;
            decimal tax_rate = Decimal.Parse((row.Cells[17].FindControl("txt_tax_rate") as TextBox).Text);
            decimal estimate_freight_charge = 0;
            if (!string.IsNullOrEmpty((row.Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text))
                estimate_freight_charge = Decimal.Parse((row.Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text);
            decimal capacity = Decimal.Parse((row.Cells[16].FindControl("txt_capacity") as TextBox).Text);
            string currency = row.Cells[4].Text;
            decimal return_rate = 0;
            if (!string.IsNullOrEmpty((row.Cells[18].FindControl("txt_return_rate") as TextBox).Text))
                return_rate = Decimal.Parse((row.Cells[18].FindControl("txt_return_rate") as TextBox).Text);

            decimal profit = Common.calculateProfit(exchange_rate, buy_price, sale_price, accessory_price, estimate_freight_charge, tax_rate, sale_rate, buy_rate, currency, volume, capacity, return_rate);
            Label lb = row.Cells[19].FindControl("lbl_profit") as Label;
            lb.Text = profit.ToString("f3");

        }

    }

    private string getDeptName(string dept_id)
    {
        string result = dept_id;
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        if (!string.IsNullOrEmpty(dept_id))
        {
            string dept_name = pba.getDeptNameById(dept_id);
            if (!string.IsNullOrEmpty(dept_name))
            {
                result = dept_name;
            }
        }
        return result;
    }
    private string getEmpName(string emp_id)
    {
        string result = emp_id;
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        if (!string.IsNullOrEmpty(emp_id))
        {
            string emp_name = pba.getEmpNameById(emp_id);
            if (!string.IsNullOrEmpty(emp_name))
            {
                result = emp_name;
            }
        }
        return result;
    }
    /// <summary>
    /// 绑定GridView1
    /// </summary>
    private void show(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_dept_id.Text = getDeptName(ds.Tables[0].Rows[0]["dept_id"].ToString());
            txt_emp.Text = getEmpName(ds.Tables[0].Rows[0]["emp_id"].ToString());
            this.submit.Enabled = true;
        }
        else
        {
            this.submit.Enabled = false;
        }

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    private void check(GridViewRow row)
    {
        if (string.IsNullOrEmpty((row.Cells[5].FindControl("txt_exchange_rate") as TextBox).Text))
        {
            throw new Exception("汇率不能为空！");
        }
        if (string.IsNullOrEmpty((row.Cells[17].FindControl("txt_tax_rate") as TextBox).Text))
        {
            throw new Exception("税点不能为空！");
        }
        if (string.IsNullOrEmpty((row.Cells[16].FindControl("txt_capacity") as TextBox).Text))
        {
            throw new Exception("入数不能为空！");
        }
        if (string.IsNullOrEmpty((row.Cells[14].FindControl("lbl_volume") as Label).Text))
        {
            throw new Exception("体积不能为空或0！");
        }
    }
    protected void txt_Value_TextChanged(object sender, EventArgs e)
    {
        Label2.Text = "";
        Label2.Visible = false;


        //计算每一行的含税金额
        TextBox tb = sender as TextBox;
        GridViewRow row = tb.Parent.Parent as GridViewRow;
        try
        {
            check(row);
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message;
            Label2.Visible = true;
            return;
        }

        decimal exchange_rate = Decimal.Parse((row.Cells[5].FindControl("txt_exchange_rate") as TextBox).Text);
        decimal sale_qty = Decimal.Parse(row.Cells[9].Text.Trim());
        decimal buy_qty = Decimal.Parse(row.Cells[10].Text.Trim());
        decimal buy_price = Decimal.Parse(row.Cells[7].Text.Trim());
        decimal sale_price = Decimal.Parse(row.Cells[3].Text.Trim());
        decimal extra_charges = 0;
        if (!string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
            extra_charges = Decimal.Parse(txt_extra_charges.Text.Trim());
        decimal accessory_price = 0;
        if (!string.IsNullOrEmpty(((TextBox)row.Cells[12].FindControl("txt_accessory_price")).Text.Trim()))
            accessory_price = Decimal.Parse(((TextBox)row.Cells[12].FindControl("txt_accessory_price")).Text.Trim());
        bool sale_rate = row.Cells[6].Text == "是" ? true : false;
        bool buy_rate = row.Cells[8].Text == "是" ? true : false;
        decimal tax_rate = Decimal.Parse((row.Cells[17].FindControl("txt_tax_rate") as TextBox).Text);
        decimal estimate_freight_charge = 0;
        if (!string.IsNullOrEmpty((row.Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text))
            estimate_freight_charge = Decimal.Parse((row.Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text);
        decimal capacity = Decimal.Parse((row.Cells[16].FindControl("txt_capacity") as TextBox).Text);
        string currency = row.Cells[4].Text;
        decimal volume = Decimal.Parse((row.Cells[14].FindControl("lbl_volume") as Label).Text);
        decimal return_rate = 0;
        if (!string.IsNullOrEmpty((row.Cells[18].FindControl("txt_return_rate") as TextBox).Text))
            return_rate = Decimal.Parse((row.Cells[18].FindControl("txt_return_rate") as TextBox).Text);

        decimal profit = Common.calculateProfit(exchange_rate, buy_price, sale_price, 
            accessory_price, estimate_freight_charge, 
            tax_rate, sale_rate, buy_rate,
            currency, volume, capacity, return_rate);
        Label lb = row.Cells[19].FindControl("lbl_profit") as Label;
        lb.Text = profit.ToString("f3");
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
        item.AuditState = false;
        item.CheckStatus = false;

        //收集利润预算表体信息
        List<T_ProfitBudgetList> lists = new List<T_ProfitBudgetList>();
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            T_ProfitBudgetList list = new T_ProfitBudgetList();
            list.FInterID = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[18].FindControl("hdf_finter_id")).Value);
            list.SaleFentryid = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[18].FindControl("hdf_sale_fentry_id")).Value);
            list.BuyFentryid = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[18].FindControl("hdf_buy_fentry_id")).Value);
            list.FItemID = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[18].FindControl("hdf_fitem_id")).Value);
            list.SaleBillNo = sale_bill_no;
            list.BuyBillNo = GridView1.Rows[i].Cells[0].Text;
            list.FName = GridView1.Rows[i].Cells[1].Text;
            list.FNumber = GridView1.Rows[i].Cells[2].Text;
            list.DeptId = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[18].FindControl("hdf_dept_id")).Value);
            list.EmpId = Int32.Parse(((HiddenField)GridView1.Rows[i].Cells[18].FindControl("hdf_emp_id")).Value);
            list.SalePrice = Decimal.Parse(GridView1.Rows[i].Cells[3].Text);
            list.Currency = GridView1.Rows[i].Cells[4].Text;
            TextBox txt_exchange_rate = (TextBox)GridView1.Rows[i].Cells[5].FindControl("txt_exchange_rate");
            if (string.IsNullOrEmpty(txt_exchange_rate.Text))
                list.ExchangeRate = 0;
            else
                list.ExchangeRate = Decimal.Parse(txt_exchange_rate.Text);
            list.BuyPrice = Decimal.Parse(GridView1.Rows[i].Cells[7].Text);
            list.SaleQty = Decimal.Parse(GridView1.Rows[i].Cells[9].Text);
            list.BuyQty = Decimal.Parse(GridView1.Rows[i].Cells[10].Text);
            list.Accessory = (GridView1.Rows[i].Cells[11].FindControl("txt_accessory") as TextBox).Text;
            TextBox txt_accessory_price = GridView1.Rows[i].Cells[12].FindControl("txt_accessory_price") as TextBox;
            if (!String.IsNullOrEmpty(txt_accessory_price.Text))
                list.AccessoryPrice = Decimal.Parse(txt_accessory_price.Text);
            else
                list.AccessoryPrice = 0;
            string length = (GridView1.Rows[i].Cells[13].FindControl("txt_length") as TextBox).Text;
            if (string.IsNullOrEmpty(length))
                list.Length = 0;
            else
                list.Length = Decimal.Parse(length);
            string width = (GridView1.Rows[i].Cells[13].FindControl("txt_width") as TextBox).Text;
            if (string.IsNullOrEmpty(width))
                list.Width = 0;
            else
                list.Width = Decimal.Parse(width);
            string height = (GridView1.Rows[i].Cells[13].FindControl("txt_height") as TextBox).Text;
            if (string.IsNullOrEmpty(height))
                list.Height = 0;
            else
                list.Height = Decimal.Parse(height);
            string capacity = (GridView1.Rows[i].Cells[16].FindControl("txt_capacity") as TextBox).Text;
            if (string.IsNullOrEmpty(capacity))
                list.Capacity = 0;
            else
                list.Capacity = Decimal.Parse(capacity);
            string estimate_freight_charge = (GridView1.Rows[i].Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text;
            if (string.IsNullOrEmpty(estimate_freight_charge))
                list.EstimateFreightCharge = 0;
            else
                list.EstimateFreightCharge = Decimal.Parse(estimate_freight_charge);
            string tax_rate = (GridView1.Rows[i].Cells[17].FindControl("txt_tax_rate") as TextBox).Text;
            if (string.IsNullOrEmpty(tax_rate))
                list.TaxRate = 0;
            else
                list.TaxRate = Decimal.Parse(tax_rate);
            list.SaleRate = GridView1.Rows[i].Cells[6].Text == "是" ? true : false;
            list.BuyRate = GridView1.Rows[i].Cells[8].Text == "是" ? true : false;
            string profit = (GridView1.Rows[i].Cells[19].FindControl("lbl_profit") as Label).Text;
            if (string.IsNullOrEmpty(profit))
                list.Profit = 0;
            else
                list.Profit = Decimal.Parse(profit);
            string volume = (GridView1.Rows[i].Cells[14].FindControl("lbl_volume") as Label).Text;
            if (string.IsNullOrEmpty(volume))
                list.Volume = 0;
            else
                list.Volume = Decimal.Parse(volume);
            string return_rate = (GridView1.Rows[i].Cells[18].FindControl("txt_return_rate") as TextBox).Text;
            if (string.IsNullOrEmpty(return_rate))
                list.ReturnRate = 0;
            else
                list.ReturnRate = Decimal.Parse(return_rate);
            list.UnAccountingQty = list.SaleQty;
            list.CheckStatus = false;
            lists.Add(list);

        }
        try
        {
            ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
            pba.addProfitBudgetHead(item);
            pba.addProfitBudgetList(lists);

            Label2.Text = "保存成功！";
            Label2.Visible = true;

        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message;
            Label2.Visible = true;
            return;
        }

    }

}