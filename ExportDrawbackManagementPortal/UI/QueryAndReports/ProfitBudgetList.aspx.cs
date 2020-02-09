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
        item.AuditState = false;

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
            list.DeptId = Int32.Parse(GridView1.Rows[i].Cells[1].Text);
            list.EmpId = Int32.Parse(GridView1.Rows[i].Cells[2].Text);
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
            string profit = (GridView1.Rows[i].Cells[18].FindControl("lbl_profit") as Label).Text;
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
        if(volume>0 && capacity>0)
        calculateProfit(row, exchange_rate, buy_price, sale_price, accessory_price, estimate_freight_charge, tax_rate, sale_rate, buy_rate, currency, volume, capacity, return_rate);
    }

    private void calculateProfit(GridViewRow row,
        decimal exchange_rate,
        decimal buy_price,
        decimal sale_price,
        decimal accessory_price,
        decimal estimate_freight_charge,
        decimal tax_rate,
        bool sale_rate,
        bool buy_rate,
        string currency,
        decimal volume,
        decimal capacity,
        decimal return_tax = (decimal)0.13)
    {
        //计算利润率
        decimal profit = 0;

        //销售价格是美金
        if (currency == "USD")
        {
            if (buy_rate)//采购价格人民币含税
            {
                profit = 1 - (((buy_price + (accessory_price + estimate_freight_charge) / (1 - tax_rate)) * (1 - return_tax / (decimal)1.13) + 2500 / (28 / volume * capacity)) / (sale_price * exchange_rate));
            }
            else
            {
                profit = 1 - ((((buy_price + accessory_price + estimate_freight_charge) / (1 - tax_rate)) * (1 - return_tax / (decimal)1.13) + 2500 / (28 / volume * capacity)) / (sale_price * exchange_rate));
            }
        }
        else//销售价格是人民币
        {
            //销售价格人民币含税
            if (sale_rate)
            {
                //采购价格人民币含税
                if (buy_rate)
                {
                    profit = (sale_price * exchange_rate - buy_price - accessory_price - estimate_freight_charge) / (sale_price * exchange_rate);
                }
                else//采购价格人民币不含税
                {
                    profit = (sale_price * exchange_rate - buy_price / (1 - tax_rate) - accessory_price - estimate_freight_charge) / (sale_price * exchange_rate);
                }
            }
            else//销售价格人民币不含税
            {
                profit = (sale_price * exchange_rate - buy_price - accessory_price - estimate_freight_charge) / (sale_price * exchange_rate);
            }
        }

        Label lb = row.Cells[18].FindControl("lbl_profit") as Label;
        lb.Text = profit.ToString("f3");
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
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //翻译是否含税
            bool sale_rate = e.Row.Cells[6].Text.Trim() == "True" ? true : false;
            if (sale_rate) e.Row.Cells[6].Text = "是";
            else e.Row.Cells[6].Text = "否";
            bool buy_rate = e.Row.Cells[8].Text.Trim() == "True" ? true : false;
            if (buy_rate) e.Row.Cells[8].Text = "是";
            else e.Row.Cells[8].Text = "否";

            //翻译业务部门和业务员
            ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
            string dept_id = e.Row.Cells[1].Text.Trim();
            string emp_id = e.Row.Cells[2].Text.Trim();
            if (!string.IsNullOrEmpty(dept_id))
            {
                string dept_name = pba.getDeptNameById(dept_id);
                if (!string.IsNullOrEmpty(dept_name))
                {
                    e.Row.Cells[1].Text = dept_name;
                }

            }
            if (!string.IsNullOrEmpty(emp_id))
            {
                string emp_name = pba.getEmpNameById(emp_id);
                if (!string.IsNullOrEmpty(emp_name))
                {
                    e.Row.Cells[2].Text = emp_name;
                }
            }
        }

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string state = e.Row.Cells[4].Text.ToLower();
            if (state == "true")
            {
                e.Row.Cells[4].Text = "通过";
                Button btnEdit = e.Row.Cells[0].FindControl("btnEdit") as Button;
                Button btnDelete = e.Row.Cells[0].FindControl("btnDelete") as Button;
                btnEdit.Enabled = btnDelete.Enabled = false;
            }
            else
            {
                e.Row.Cells[4].Text = "不通过";
            }
        }


    }
    private void check(GridViewRow row)
    {
        if (string.IsNullOrEmpty((row.Cells[5].FindControl("txt_exchange_rate") as TextBox).Text))
        {
            throw new Exception("汇率不能为空！");
        }
        else
        {
            if (Decimal.Parse((row.Cells[5].FindControl("txt_exchange_rate") as TextBox).Text) == 0)
            {
                throw new Exception("汇率不能为0！");
            }
        }
        if (string.IsNullOrEmpty((row.Cells[17].FindControl("txt_tax_rate") as TextBox).Text))
        {
            throw new Exception("税点不能为空！");
        }
        if (string.IsNullOrEmpty((row.Cells[16].FindControl("txt_capacity") as TextBox).Text))
        {
            throw new Exception("入数不能为空！");
        }
        else
        {
            if (Decimal.Parse((row.Cells[16].FindControl("txt_capacity") as TextBox).Text) == 0)
            {
                throw new Exception("入数不能为0！");
            }
        }
        if (string.IsNullOrEmpty((row.Cells[14].FindControl("lbl_volume") as Label).Text))
        {
            throw new Exception("体积不能为空！");
        }
        else
        {
            if (Decimal.Parse((row.Cells[14].FindControl("lbl_volume") as Label).Text) == 0)
            {
                throw new Exception("体积不能为0！");
            }
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
        decimal volume = length * height * height / 1000000;

        Label lbl_volume = row.Cells[14].FindControl("lbl_volume") as Label;
        lbl_volume.Text = volume.ToString("f3");
       
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

            if (volume > 0 && capacity > 0)
            {
                calculateProfit(row, exchange_rate, buy_price, sale_price, accessory_price, estimate_freight_charge, tax_rate, sale_rate, buy_rate, currency, volume, capacity, return_rate);
                Label2.Text = "";
                Label2.Visible = false;
            }

        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        sale_bill_no  = GridView2.SelectedRow.Cells[1].Text;
        this.txt_extra_charges.Text = GridView2.SelectedRow.Cells[2].Text;
        finterid = Int32.Parse((GridView2.SelectedRow.Cells[0].FindControl("hdf_finter_id") as HiddenField).Value);
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

        //this.lbl_sale_bill_no.Text = "";
        this.txt_extra_charges.Text = "";
        this.submit.Enabled = false;
        Label2.Text = "";
        Label2.Visible = false;
        GridView2.SelectedIndex = -1;

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        Button bt = sender as Button;
        string args = bt.CommandArgument.ToString();
        GridViewRow row = bt.Parent.Parent as GridViewRow;
        string sale_bill_no = row.Cells[1].Text.ToString();
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        if (args == "Y")
        {
            pba.audit(sale_bill_no, true);
        }
        else
        {
            pba.audit(sale_bill_no, false);
        }

        GridViewBind();

    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        string queryStr = this.txt_sale_bill_no.Text.Trim();
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        if (String.IsNullOrEmpty(queryStr))
        {
            show(GridView2, pba.getProfitBudgetSummary());

        }
        else
        {

            show(GridView2, pba.getProfitBudgetSummaryByID(queryStr));
        }
    }
}