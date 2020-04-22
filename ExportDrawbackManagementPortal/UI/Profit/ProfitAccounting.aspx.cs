using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Linq;
using System.Drawing;
public partial class UI_Profit_ProfitAccounting : System.Web.UI.Page
{
    public static string sale_bill_no { get; set; }
    public static int finterid { get; set; }
    decimal commissionforAll = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            sale_bill_no = "";
            finterid = 0;
            GridView1EmptyBind();
        }
        
    }

    private string getDeptName(string dept_id)
    {
        string result = dept_id;
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        if (!string.IsNullOrEmpty(dept_id) && dept_id != "&nbsp;")
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
        if (!string.IsNullOrEmpty(emp_id) && emp_id != "&nbsp;")
        {
            string emp_name = pba.getEmpNameById(emp_id);
            if (!string.IsNullOrEmpty(emp_name))
            {
                result = emp_name;
            }
        }
        return result;
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


            
            string currency = e.Row.Cells[4].Text.Trim();
            if (string.IsNullOrEmpty(currency) || currency == "&nbsp;")
            {
                e.Row.Cells[21].BackColor = Color.Red;
                e.Row.Enabled = false;
                submit.Enabled = false;
            }
            else
            {
                decimal exchange_rate = Decimal.Parse((e.Row.Cells[5].FindControl("txt_exchange_rate") as TextBox).Text);
                decimal buy_price = Decimal.Parse(e.Row.Cells[7].Text.Trim());
                decimal sale_price = Decimal.Parse(e.Row.Cells[3].Text.Trim());
                decimal accessory_price = 0;
                if (!string.IsNullOrEmpty(((TextBox)e.Row.Cells[12].FindControl("txt_accessory_price")).Text.Trim()))
                    accessory_price = Decimal.Parse(((TextBox)e.Row.Cells[12].FindControl("txt_accessory_price")).Text.Trim());
                decimal estimate_freight_charge = 0;
                if (!string.IsNullOrEmpty((e.Row.Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text))
                    estimate_freight_charge = Decimal.Parse((e.Row.Cells[15].FindControl("txt_estimate_freight_charge") as TextBox).Text);
                decimal tax_rate = Decimal.Parse((e.Row.Cells[17].FindControl("txt_tax_rate") as TextBox).Text);
                decimal volume = Decimal.Parse((e.Row.Cells[14].FindControl("lbl_volume") as Label).Text);
                decimal capacity = Decimal.Parse((e.Row.Cells[16].FindControl("txt_capacity") as TextBox).Text);
                decimal return_rate = 0;
                if (!string.IsNullOrEmpty((e.Row.Cells[18].FindControl("txt_return_rate") as TextBox).Text))
                    return_rate = Decimal.Parse((e.Row.Cells[18].FindControl("txt_return_rate") as TextBox).Text);
                decimal profit = Common.calculateProfit(exchange_rate, buy_price, sale_price,accessory_price, estimate_freight_charge,tax_rate, sale_rate, buy_rate,currency, volume, capacity, return_rate);
                Label lb = e.Row.Cells[19].FindControl("lbl_profit") as Label;
                lb.Text = profit.ToString("f3");

                decimal sale_qty = Decimal.Parse(e.Row.Cells[9].Text);
                decimal commission = Common.calculateCommission(profit, sale_price, sale_qty, exchange_rate);
                commissionforAll += commission;
                Label lb_commission = e.Row.Cells[20].FindControl("lbl_commission") as Label;
                lb_commission.Text = commission.ToString("f3");

            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[20].Text = commissionforAll.ToString("f3");
        }

    }
    private void GridView1EmptyBind()
    {
        DataTable dt = getNewTable();

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }

    private static DataTable getNewTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FInterID",typeof(Int32));
        dt.Columns.Add("Sale_FEntryID", typeof(Int32));
        dt.Columns.Add("Buy_FEntryID", typeof(Int32));
        dt.Columns.Add("FItemID", typeof(Int32));
        dt.Columns.Add("sale_bill_no",typeof(String));
        dt.Columns.Add("buy_bill_no", typeof(String));
        dt.Columns.Add("dept_id", typeof(Int32));
        dt.Columns.Add("emp_id", typeof(Int32));
        dt.Columns.Add("sale_price", typeof(Decimal));
        dt.Columns.Add("currency", typeof(String));
        dt.Columns.Add("exchange_rate", typeof(Decimal));
        dt.Columns.Add("buy_price", typeof(Decimal));
        dt.Columns.Add("sale_qty", typeof(Decimal));
        dt.Columns.Add("buy_qty", typeof(Decimal));
        dt.Columns.Add("accessory", typeof(String));
        dt.Columns.Add("accessory_price", typeof(Decimal));
        dt.Columns.Add("length", typeof(Decimal));
        dt.Columns.Add("width", typeof(Decimal));
        dt.Columns.Add("height", typeof(Decimal));
        dt.Columns.Add("capacity", typeof(Decimal));
        dt.Columns.Add("estimate_freight_charge", typeof(Decimal));
        dt.Columns.Add("tax_rate", typeof(Decimal));
        dt.Columns.Add("sale_rate", typeof(Boolean));
        dt.Columns.Add("buy_rate", typeof(Boolean));
        dt.Columns.Add("return_rate", typeof(Decimal));
        dt.Columns.Add("profit", typeof(Decimal));
        dt.Columns.Add("volume", typeof(Decimal));
        dt.Columns.Add("FOrderBillNo", typeof(String));
        dt.Columns.Add("FName", typeof(String));
        dt.Columns.Add("FNumber", typeof(String));
        return dt;
    }
   
    protected void query_Click(object sender, EventArgs e)
    {
        Label2.Text = "";
        Label2.Visible = false;
        ProfitAccountingAdapter pba = new ProfitAccountingAdapter();
        T_ProfitAccounting item = new T_ProfitAccounting();
        sale_bill_no = this.txt_sale_bill_no.Text.Trim();
        item.SaleBillNo = sale_bill_no;
        DataSet ds1 = pba.getSEOrderInfo(item);
        finterid = Int32.Parse(ds1.Tables[0].Rows[0][0].ToString());
        var list = ds1.Tables[0].AsEnumerable().Select<DataRow, int>(x => Convert.ToInt32(x["FOrderInterID"])).ToList<int>().Distinct().ToList<int>();
        DataSet ds2 = pba.getProfitBudgetList(list);
        txt_extra_charges_lists_all.Text = pba.getExtraCharges(sale_bill_no);
        var query = from rds1 in ds1.Tables[0].AsEnumerable()
                    join rds2 in ds2.Tables[0].AsEnumerable()
                    on new
                    {
                        FInterID = rds1.Field<Int32>("FOrderInterID"),
                        Sale_FEntryID = rds1.Field<Int32>("FOrderEntryID"),
                        Buy_FEntryID = rds1.Field<Int32>("Buy_FEntryID")
                    }
                    equals new
                    {
                        FInterID = rds2.Field<Int32>("FInterID"),
                        Sale_FEntryID = rds2.Field<Int32>("Sale_FEntryID"),
                        Buy_FEntryID = rds2.Field<Int32>("Buy_FEntryID")
                    }
                    into temp  from tt in temp.DefaultIfEmpty()
                    select new
                    {
                        FInterID = rds1.Field<Int32>("FInterID"),
                        Sale_FEntryID = rds1.Field<Int32>("Sale_FEntryID"),
                        Buy_FEntryID = rds1.Field<Int32>("Buy_FEntryID"),
                        FItemID = rds1.Field<Int32>("FItemID"),
                        sale_bill_no = rds1.Field<String>("sale_bill_no"),
                        buy_bill_no = rds1.Field<String>("buy_bill_no"),
                        dept_id = tt == null ? 0 : tt.Field<Int32>("dept_id"),
                        emp_id = tt == null ? 0 : tt.Field<Int32>("emp_id"),
                        sale_price = rds1.Field<Decimal>("sale_price"),
                        currency = tt == null ? "" : tt.Field<String>("currency"),
                        exchange_rate = tt == null ? 0 : tt.Field<Decimal>("exchange_rate"),
                        buy_price = tt == null ? 0 : tt.Field<Decimal>("buy_price"),
                        sale_qty = rds1.Field<Decimal>("sale_qty"),
                        buy_qty = tt == null ? 0 : tt.Field<Decimal>("buy_qty"),
                        accessory = tt == null ? "" : tt.Field<String>("accessory"),
                        accessory_price = tt == null ? 0 : tt.Field<Decimal>("accessory_price"),
                        length = tt == null ? 0 : tt.Field<Decimal>("length"),
                        width = tt == null ? 0 : tt.Field<Decimal>("width"),
                        height = tt == null ? 0 : tt.Field<Decimal>("height"),
                        capacity = tt == null ? 0 : tt.Field<Decimal>("capacity"),
                        estimate_freight_charge = tt == null ? 0 : tt.Field<Decimal>("estimate_freight_charge"),
                        tax_rate = tt == null ? 0 : tt.Field<Decimal>("tax_rate"),
                        sale_rate = tt == null ? false : tt.Field<Boolean>("sale_rate"),
                        buy_rate = tt == null ? false : tt.Field<Boolean>("buy_rate"),
                        return_rate = tt == null ? 0 : tt.Field<Decimal>("return_rate"),
                        profit = tt == null ? 0 : tt.Field<Decimal>("profit"),
                        volume = tt == null ? 0 : tt.Field<Decimal>("volume"),
                        FOrderBillNo = rds1.Field<String>("FOrderBillNo"),
                        FName = tt == null ? "" : tt.Field<String>("FName"),
                        FNumber = tt == null ? "" : tt.Field<String>("FNumber")
                    };
        DataTable dt = getNewTable();
        foreach (var obj in query)
        {
            dt.Rows.Add(obj.FInterID,
                        obj.Sale_FEntryID,
                        obj.Buy_FEntryID,
                        obj.FItemID,
                        obj.sale_bill_no,
                        obj.buy_bill_no,
                        obj.dept_id,
                        obj.emp_id,
                        obj.sale_price,
                        obj.currency,
                        obj.exchange_rate,
                        obj.buy_price,
                        obj.sale_qty,
                        obj.buy_qty,
                        obj.accessory,
                        obj.accessory_price,
                        obj.length,
                        obj.width,
                        obj.height,
                        obj.capacity,
                        obj.estimate_freight_charge,
                        obj.tax_rate,
                        obj.sale_rate,
                        obj.buy_rate,
                        obj.return_rate,
                        obj.profit,
                        obj.volume,
                        obj.FOrderBillNo,
                        obj.FName,
                        obj.FNumber);
        }
        show(dt);
    }

    protected void txt_volume_TextChanged(object sender, EventArgs e)
    {
        Label2.Text = "";
        Label2.Visible = false; 
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

            
            decimal commission = Common.calculateCommission(profit, sale_price, sale_qty, exchange_rate);
            Label lb_commission = row.Cells[20].FindControl("lbl_commission") as Label;
            lb_commission.Text = commission.ToString("f3");

        }

    }
    /// <summary>
    /// 绑定GridView1
    /// </summary>
    private void show(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            txt_dept_id.Text = getDeptName(dt.Rows[0]["dept_id"].ToString());
            txt_emp.Text = getEmpName(dt.Rows[0]["emp_id"].ToString());
            this.submit.Enabled = true;
        }
        else
        {
            this.submit.Enabled = false;
        }
        GridView1.DataSource = dt;
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

        decimal profit = Common.calculateProfit(exchange_rate, buy_price, sale_price, accessory_price, estimate_freight_charge, tax_rate, sale_rate, buy_rate, currency, volume, capacity, return_rate);
        Label lb = row.Cells[19].FindControl("lbl_profit") as Label;
        lb.Text = profit.ToString("f3");

        decimal commission = Common.calculateCommission(profit, sale_price, sale_qty, exchange_rate);
        Label lb_commission = row.Cells[20].FindControl("lbl_commission") as Label;
        lb_commission.Text = commission.ToString("f3");

    }



    protected void submit_Click(object sender, EventArgs e)
    {

        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();

        //收集利润核算表头信息
        T_ProfitAccounting item = new T_ProfitAccounting();
        item.UpdateTime = DateTime.Now;
        decimal extra_charges = 0;
        if (!string.IsNullOrEmpty(txt_extra_charges.Text.Trim()))
            extra_charges += Decimal.Parse(txt_extra_charges.Text.Trim());

        if (!string.IsNullOrEmpty(txt_extra_charges_lists_all.Text.Trim()))
            extra_charges += Decimal.Parse(txt_extra_charges_lists_all.Text.Trim());
        item.ExtraCharges = extra_charges;
        item.FInterID = finterid;
        item.SaleBillNo = sale_bill_no;
        item.AuditState = false;
        item.IsActualAudit = false;

        //收集利润核算表体信息
        List<T_ProfitAccountingList> lists = new List<T_ProfitAccountingList>();
        List<T_ProfitBudgetList> pbls = new List<T_ProfitBudgetList>();
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            T_ProfitAccountingList list = new T_ProfitAccountingList();
            T_ProfitBudgetList pbl = new T_ProfitBudgetList();

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
            Label lbCommission = GridView1.Rows[i].Cells[20].FindControl("lbl_commission") as Label;
            list.Commission = Decimal.Parse(lbCommission.Text);
            list.SEOrderID = GridView1.Rows[i].Cells[21].Text;
            lists.Add(list);

            pbl.BuyFentryid = list.BuyFentryid;
            pbl.SaleBillNo = list.SEOrderID;
            pbl.BuyBillNo = list.BuyBillNo;

            DataSet ds = pba.getProfitBudgetList(pbl);
            decimal un_accounting_qty = Decimal.Parse(ds.Tables[0].Rows[0]["un_accounting_qty"].ToString());
            pbl.UnAccountingQty = un_accounting_qty - list.SaleQty;
            if (un_accounting_qty - list.SaleQty == 0)
            {
                pbl.CheckStatus = true;
            }
            else
            {
                pbl.CheckStatus = false;
            }
            pbls.Add(pbl);
            
        }
        try
        {
            //更新check_status字段为true，如果表体相关记录check_status字段全为true
            //var query1 = (from tt in pbls
            //            where tt.CheckStatus == true
            //             select tt.SaleBillNo).Distinct().ToList();
            //var query2 = (from tt in pbls
            //              where tt.CheckStatus == false
            //              select tt.SaleBillNo).Distinct().ToList();
            //var query = query1.Except(query2).ToList();



            ProfitAccountingAdapter paa = new ProfitAccountingAdapter();
            paa.addProfitBudgetHead(item);
            paa.addProfitBudgetList(lists);

            pba.updateList(pbls);

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