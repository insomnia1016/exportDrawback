using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_payment_paymentAdd : System.Web.UI.Page
{
    public DataSet ds { get; set; }
    decimal amountforAll = 0;
    decimal payamountforAll = 0;
    decimal unpayamountforAll = 0;
    decimal checkamountforAll = 0;
    decimal RequestAmountForAll = 0;
    decimal IndecreaseAmountForAll = 0;
    decimal DepositAmountForAll = 0;
    decimal DepositCheckAmountForAll = 0;
    decimal DepositPayamountforAll = 0;
    decimal DepositUnPayamountforAll = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init();
            txt_recetpt_no.Text = LoadLastReceiptID();
            d_date.Text = check_date.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            CommonAdapter ca = new CommonAdapter();
            int selectedValue = ca.getIDByName(UserInfoAdapter.CurrentUser.Name);
            ddl_preparer.SelectedValue = ddl_checker.SelectedValue = selectedValue.ToString();

        }
        else
        {
            UserInfo user = UserInfoAdapter.CurrentUser;
            if (Session[user.Name + "Pay"] != null)
            {
                ds = (DataSet)Session[user.Name + "Pay"];
                Session[user.Name + "Pay"] = null;
                show(ds, GridView1);

            }
            if (Session[user.Name + "Reque"] != null)
            {
                ds = (DataSet)Session[user.Name + "Reque"];
                Session[user.Name + "Reque"] = null;
                show(ds, GridView4);
                initAmount();
            }
            if (Session[user.Name + "PayInDec"] != null)
            {
                ds = (DataSet)Session[user.Name + "PayInDec"];
                Session[user.Name + "PayInDec"] = null;
                show(ds, GridView2);
                initAmount();
            }
            if (Session[user.Name + "PayDepos"] != null)
            {
                ds = (DataSet)Session[user.Name + "PayDepos"];
                Session[user.Name + "PayDepos"] = null;
                show(ds, GridView3);
            }
           
            

        }


    }

    private void initAmount()
    {
        RequestAmountForAll = Decimal.Parse(GridView4.FooterRow.Cells[3].Text);
        checkamountforAll = Decimal.Parse(GridView1.FooterRow.Cells[6].Text);
        DepositCheckAmountForAll = Decimal.Parse(GridView3.FooterRow.Cells[6].Text);
        IndecreaseAmountForAll = Decimal.Parse(GridView2.FooterRow.Cells[3].Text);
        calculateAmount();
    }

    private void calculateAmount()
    {
        decimal receipt_charge = 0;
        if (!string.IsNullOrEmpty(txt_receipt_charge.Text))
        {
            receipt_charge = Decimal.Parse(txt_receipt_charge.Text);
        }
        string receipt_type = ddl_receipt_type.SelectedValue;
        decimal amount = 0;
        switch (receipt_type)
        {
            case "A":
                amount = checkamountforAll - DepositCheckAmountForAll + IndecreaseAmountForAll + receipt_charge;
                break;
            case "B":
                amount = DepositCheckAmountForAll + receipt_charge;
                break;
            default:
                amount = RequestAmountForAll + receipt_charge;
                break;
        }

        txt_amount.Text = amount.ToString();
    }

    private void DDLBind()
    {
        ddlReceiptTypeBind();
        ddlCurrencyBind();
        ddlPreparerBind();
        ddlCheckerBind();
        ddlAccount(1);
    }

    private void ddlAccount(int currencyID)
    {

        AccountAdapter aa = new AccountAdapter();

        DataSet ds = aa.getAccounts(currencyID);

        ddl_account.DataTextField = "account_id";
        ddl_account.DataValueField = "account_id";
        ddl_account.DataSource = ds;
        ddl_account.DataBind();
    }

    private void ddlCheckerBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getEmp();
        ddl_checker.DataTextField = "FName";
        ddl_checker.DataValueField = "FItemID";
        ddl_checker.DataSource = ds;
        ddl_checker.DataBind();
    }

    private void ddlPreparerBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getEmp();
        ddl_preparer.DataTextField = "FName";
        ddl_preparer.DataValueField = "FItemID";
        ddl_preparer.DataSource = ds;
        ddl_preparer.DataBind();
    }


    private string LoadLastReceiptID()
    {
        string search = "FKDJ" + DateTime.Now.Year.ToString().Substring(2, 2);

        PaymentAdapter raa = new PaymentAdapter();
        return raa.getLastReceiptID(search);
    }
    private void ddlReceiptTypeBind()
    {
        PaymentTypeAdapter ra = new PaymentTypeAdapter();
        DataSet ds = ra.getTypes();
        ddl_receipt_type.DataTextField = "name";
        ddl_receipt_type.DataValueField = "code";
        ddl_receipt_type.DataSource = ds;
        ddl_receipt_type.DataBind();
    }

    private void ddlCurrencyBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getCurrency();
        ddl_currency.DataTextField = "FName";
        ddl_currency.DataValueField = "FCurrencyID";
        ddl_currency.DataSource = ds;
        ddl_currency.DataBind();
    }
    private void show(DataSet ds, GridView gv)
    {
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (gv == GridView1)
            {
                GridView1EmptyBind();
                GridView1.FooterRow.Cells[6].Text = "0";
            }
            else if (gv == GridView4)
            {
                GridView4EmptyBind();
                GridView4.FooterRow.Cells[3].Text = "0";
            }
            else if (gv == GridView3)
            {
                GridView3EmptyBind();
                GridView3.FooterRow.Cells[6].Text = "0";
            }
            else
            {
                GridView2EmptyBind();
                GridView2.FooterRow.Cells[3].Text = "0";
            }
        }
        else
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
        if (gv == GridView3)
        {
            txt_deposit_checkamountfor_TextChanged(null, null);
        }

    }

    private void GridView1EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("FInterID");
        dt.Columns.Add("FBillNo");
        dt.Columns.Add("FDate");
        dt.Columns.Add("FPurchaseAmountFor");
        dt.Columns.Add("FPayAmountFor");
        dt.Columns.Add("FUnPayAmountFor");
        dt.Columns.Add("FCurrencyID");
        dt.Columns.Add("FNote");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }
    private void GridView2EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("bill_no");
        dt.Columns.Add("customer");
        dt.Columns.Add("amount_all");
        dt.Columns.Add("agenter");
        dt.Columns.Add("agent_date");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView2.DataSource = dt;
        this.GridView2.DataBind();
    }
    private void GridView4EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("payment_id");
        dt.Columns.Add("payee_unit");
        dt.Columns.Add("amount");
        dt.Columns.Add("emp_name");
        dt.Columns.Add("payment_date");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView4.DataSource = dt;
        this.GridView4.DataBind();
    }
    private void GridView3EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("deposit_id");
        dt.Columns.Add("customer");
        dt.Columns.Add("amount_all");
        dt.Columns.Add("agenter");
        dt.Columns.Add("agent_date");
        dt.Columns.Add("pay_amount_for");
        dt.Columns.Add("unpay_amount_for");
        dt.Columns.Add("check_amount_for");
        dt.Columns.Add("currencyID");
        dt.Columns.Add("note");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView3.DataSource = dt;
        this.GridView3.DataBind();
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCurrency = e.Row.Cells[7].FindControl("lbl_fcurrencyid") as Label;
            if (!string.IsNullOrEmpty(lblCurrency.Text))
            {
                int currencyID = Int32.Parse(lblCurrency.Text);
                lblCurrency.Text = ca.getCurrencyByID(currencyID);
                Label lblAmountFor = e.Row.Cells[3].FindControl("lbl_amountfor") as Label;
                amountforAll += Decimal.Parse(lblAmountFor.Text);
                Label lblReceiveAmountFor = e.Row.Cells[4].FindControl("lbl_fpayamountfor") as Label;
                payamountforAll += Decimal.Parse(lblReceiveAmountFor.Text);
                Label lblUnReceiveAmountFor = e.Row.Cells[5].FindControl("lbl_funpayamountfor") as Label;
                unpayamountforAll += Decimal.Parse(lblUnReceiveAmountFor.Text);
            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = amountforAll.ToString();//采购发票中金额
            e.Row.Cells[4].Text = payamountforAll.ToString();//已支付金额
            e.Row.Cells[5].Text = unpayamountforAll.ToString();//未支付金额
            e.Row.Cells[6].Text = checkamountforAll.ToString();//单据金额

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblIndecreaseAmountForAll = e.Row.Cells[3].FindControl("lbl_amount_all") as Label;
            if (!string.IsNullOrEmpty(lblIndecreaseAmountForAll.Text))
            {
                IndecreaseAmountForAll += Decimal.Parse(lblIndecreaseAmountForAll.Text);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = IndecreaseAmountForAll.ToString();//单据金额
        }
    }
    protected void updateUser_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        T_PaymentReceiptHead head = new T_PaymentReceiptHead();
        head.CustomerName = txt_customer.Text;
        
        head.ReceiptId = txt_recetpt_no.Text;
        head.ReceiptDate = DateTime.Parse(d_date.Text);
        head.ReceiptType = ddl_receipt_type.SelectedValue;
        //货款时供应商才从金蝶里读取
        if (head.ReceiptType == "A" || head.ReceiptType == "B")
        {
            if (!string.IsNullOrEmpty(head.CustomerName))
            {
                head.CustomerCode = getCusIDByName(head.CustomerName);
                if (string.IsNullOrEmpty(head.CustomerCode))
                {
                    Label1.Text = "输入的公司名称不对，请输入关键字然后从下拉框中选择！";
                    return;
                }
            }
        }
        head.AccountId = ddl_account.SelectedValue;
        if (!String.IsNullOrEmpty(txt_amount.Text) && Decimal.Parse(txt_amount.Text) > 0)
        {
            head.Amount = Decimal.Parse(txt_amount.Text);
        }
        else
        {
            Label1.Text = "该付款单金额不能为0。";
            return;
        }
        head.Currency = ddl_currency.SelectedValue;
        if (!string.IsNullOrEmpty(txt_receipt_charge.Text))
        {
            head.ReceiptCharge = Decimal.Parse(txt_receipt_charge.Text);

        }

        head.FPreparer = Int32.Parse(ddl_preparer.SelectedValue);
        head.FChecker = Int32.Parse(ddl_checker.SelectedValue);
        head.FCheckDate = DateTime.Parse(check_date.Text);
        head.Note = txt_note.Text;
        head.FCheckStatus = (int)2;
        head.AuditStatus = 1;

        CommonAdapter ca = new CommonAdapter();
        List<T_PaymentReceiptList> lists = new List<T_PaymentReceiptList>();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            T_PaymentReceiptList list = new T_PaymentReceiptList();
            list.ReceiptId = head.ReceiptId;
            list.ReceiptNo = Int32.Parse((GridView1.Rows[i].Cells[0].FindControl("lbl_gno") as Label).Text);
            string fbillno = (GridView1.Rows[i].Cells[1].FindControl("txt_bill_no") as TextBox).Text;
            if (string.IsNullOrEmpty(fbillno))
            {
                continue;
            }
            list.FBillNo = fbillno;
            list.FDate = DateTime.Parse((GridView1.Rows[i].Cells[2].FindControl("lbl_fdate") as Label).Text);
            list.FPurchaseAmountFor = Decimal.Parse((GridView1.Rows[i].Cells[3].FindControl("lbl_amountfor") as Label).Text);
            list.FPayAmountFor = Decimal.Parse((GridView1.Rows[i].Cells[4].FindControl("lbl_fpayamountfor") as Label).Text);
            list.FUnPayAmountFor = Decimal.Parse((GridView1.Rows[i].Cells[5].FindControl("lbl_funpayamountfor") as Label).Text);
            string fcheckamountfor = (GridView1.Rows[i].Cells[6].FindControl("txt_fcheckamountfor") as TextBox).Text;
            if (string.IsNullOrEmpty(fcheckamountfor))
            {
                Label1.Text = "核销金额不能为空";
                return;
            }
            list.FCheckAmountFor = Decimal.Parse(fcheckamountfor);
            list.FCurrencyID = ca.getIDByCurrency((GridView1.Rows[i].Cells[7].FindControl("lbl_fcurrencyid") as Label).Text);
            list.FNote = (GridView1.Rows[i].Cells[8].FindControl("txt_note") as TextBox).Text;
            if (list.FUnPayAmountFor - list.FCheckAmountFor == 0)
            {
                list.FCheckStatus = 2;
            }
            else
            {
                list.FCheckStatus = 1;
                head.FCheckStatus = 1;
            }
            lists.Add(list);

        }
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            T_PaymentReceiptList list = new T_PaymentReceiptList();
            list.ReceiptId = head.ReceiptId;

            list.ReceiptNo = GridView1.Rows.Count + Int32.Parse((GridView2.Rows[i].Cells[0].FindControl("lbl_gno") as Label).Text);
            string indecreaseno = (GridView2.Rows[i].Cells[1].FindControl("txt_bill_no") as TextBox).Text;
            if (string.IsNullOrEmpty(indecreaseno))
            {
                continue;
            }
            list.IndecreaseNo = indecreaseno;
            list.FPurchaseAmountFor = Decimal.Parse((GridView2.Rows[i].Cells[3].FindControl("lbl_amount_all") as Label).Text);
            lists.Add(list);
        }
       
        List<T_PaymentReceiptList> listsFromDeposit = new List<T_PaymentReceiptList>();
        foreach (GridViewRow gvr in GridView3.Rows)
        {
            T_PaymentReceiptList list = new T_PaymentReceiptList();
            list.ReceiptId = head.ReceiptId;
            list.ReceiptNo = GridView1.Rows.Count + GridView4.Rows.Count + Int32.Parse((gvr.Cells[0].FindControl("lbl_gno") as Label).Text);
            string deposit_id = (gvr.Cells[1].FindControl("txt_deposit_id") as TextBox).Text;
            if (string.IsNullOrEmpty(deposit_id))
            {
                continue;
            }
            list.DepositId = deposit_id;
            list.FDate = DateTime.Parse((gvr.Cells[2].FindControl("lbl_agent_date") as Label).Text);
            list.FNote = (gvr.Cells[9].FindControl("txt_note") as TextBox).Text;
            list.FCurrencyID = ca.getIDByCurrency((gvr.Cells[7].FindControl("lbl_fcurrencyid") as Label).Text);
            list.FPurchaseAmountFor = Decimal.Parse((gvr.Cells[3].FindControl("lbl_amount_all") as Label).Text);
            list.FPayAmountFor = Decimal.Parse((gvr.Cells[4].FindControl("lbl_fpayamountfor") as Label).Text);
            list.FUnPayAmountFor = Decimal.Parse((gvr.Cells[5].FindControl("lbl_funpayamountfor") as Label).Text);
            string str_deposit_checkamountfor = (gvr.Cells[6].FindControl("txt_deposit_checkamountfor") as TextBox).Text;
            if (string.IsNullOrEmpty(str_deposit_checkamountfor))
            {
                Label1.Text = "核销金额不能为空";
                return;
            }
            list.FCheckAmountFor = Decimal.Parse(str_deposit_checkamountfor);
            if (list.FUnPayAmountFor - list.FCheckAmountFor == 0)
            {
                list.FCheckStatus = 2;
            }
            else
            {
                list.FCheckStatus = 1;
                head.FCheckStatus = 1;
            }
            listsFromDeposit.Add(list);
        }
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            T_PaymentReceiptList list = new T_PaymentReceiptList();
            list.ReceiptId = head.ReceiptId;

            list.ReceiptNo = GridView1.Rows.Count + Int32.Parse((GridView4.Rows[i].Cells[0].FindControl("lbl_gno") as Label).Text);
            string requestID = (GridView4.Rows[i].Cells[1].FindControl("txt_bill_no") as TextBox).Text;
            if (string.IsNullOrEmpty(requestID))
            {
                continue;
            }
            list.RequestID = requestID;
            list.FPurchaseAmountFor = Decimal.Parse((GridView4.Rows[i].Cells[3].FindControl("lbl_amount_all") as Label).Text);
            lists.Add(list);
        }
        List<T_PaymentReceiptList> addtodonelists = addBillNoToDone(lists, head);
        List<T_PaymentReceiptList> checkStatusLists = addInDecreaseNoToDone(lists);
        try
        {
            //保存数据
            PaymentAdapter raa = new PaymentAdapter();
            PaymentRequestAdapter pra = new PaymentRequestAdapter();
            PaymentDepositAdapter da = new PaymentDepositAdapter();
            PaymentInDecreaseAdapter ida = new PaymentInDecreaseAdapter();
            raa.addReceiptHead(head);
            raa.insertReceiptList(lists);
            raa.insertReceiptList(listsFromDeposit);

            raa.addToDone(addtodonelists);//把采购发票号加入已做列表中
            if (ddl_receipt_type.SelectedValue == "B")
            {
                da.updateHeadIsPayed(listsFromDeposit);
            }
            else
            {
                da.updateHeadCheckStatus(listsFromDeposit);//更新定金单的check_status值；
            }

            ida.updateHeadCheckStatus(checkStatusLists);//更新增减单的check_status值为2；
            pra.updateHeadCheckStatus(lists);//更新付款通知书的isUserd值为1(true)；
            Label1.Text = "哟，小伙子，不错，被你录入成功了";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }



    private List<T_PaymentReceiptList> addInDecreaseNoToDone(List<T_PaymentReceiptList> lists)
    {
        List<T_PaymentReceiptList> newlists = new List<T_PaymentReceiptList>();
        foreach (T_PaymentReceiptList list in lists)
        {
            if (!string.IsNullOrEmpty(list.IndecreaseNo))
            {

                newlists.Add(list);
            }
        }
        return newlists;
    }

    private List<T_PaymentReceiptList> addBillNoToDone(List<T_PaymentReceiptList> lists, T_PaymentReceiptHead head)
    {
        List<T_PaymentReceiptList> newlists = new List<T_PaymentReceiptList>();
        foreach (T_PaymentReceiptList list in lists)
        {
            if (list.FCheckStatus > 0)
            {
                list.CustomerName = head.CustomerName;
                list.CustomerCode = head.CustomerCode;
                newlists.Add(list);
            }
        }
        return newlists;
    }

    private void init()
    {
        DDLBind();
        GridView1EmptyBind();
        GridView2EmptyBind();
        GridView3EmptyBind();
        GridView4EmptyBind();
    }
    private string getCusIDByName(string name)
    {
        PaymentAdapter raa = new PaymentAdapter();
        DataSet ds = raa.getSupplierInfoByName(name);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["FNumber"].ToString();
        }
        else
        {
            return string.Empty;
        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblRequestAmountForAll = e.Row.Cells[3].FindControl("lbl_amount_all") as Label;
            if (!string.IsNullOrEmpty(lblRequestAmountForAll.Text))
            {
                RequestAmountForAll += Decimal.Parse(lblRequestAmountForAll.Text);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = RequestAmountForAll.ToString();//单据金额
        }
    }
    protected void txt_receipt_charge_TextChanged(object sender, EventArgs e)
    {
        RequestAmountForAll = Decimal.Parse(GridView4.FooterRow.Cells[3].Text);
        DepositAmountForAll = Decimal.Parse(GridView3.FooterRow.Cells[6].Text);
        checkamountforAll = Decimal.Parse(GridView1.FooterRow.Cells[6].Text);
        IndecreaseAmountForAll = Decimal.Parse(GridView2.FooterRow.Cells[3].Text);
        calculateAmount();
    }
    protected void txt_fcheckamountfor_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_fcheckamountfor;
        decimal fcheckamountfor = 0;
        RequestAmountForAll = Decimal.Parse(GridView4.FooterRow.Cells[3].Text);
        DepositAmountForAll = Decimal.Parse(GridView3.FooterRow.Cells[6].Text);
        IndecreaseAmountForAll = Decimal.Parse(GridView2.FooterRow.Cells[3].Text);
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            txt_fcheckamountfor = gvr.Cells[6].FindControl("txt_fcheckamountfor") as TextBox;
            if (!string.IsNullOrEmpty(txt_fcheckamountfor.Text))
            {
                fcheckamountfor += decimal.Parse(txt_fcheckamountfor.Text);
            }
        }
        
        checkamountforAll = fcheckamountfor;
        calculateAmount();
        GridView1.FooterRow.Cells[6].Text = fcheckamountfor.ToString();
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCurrency = e.Row.Cells[7].FindControl("lbl_fcurrencyid") as Label;
            if (!string.IsNullOrEmpty(lblCurrency.Text))
            {
                int currencyID = Int32.Parse(lblCurrency.Text);
                lblCurrency.Text = ca.getCurrencyByID(currencyID);
                Label lblDepositAmountForAll = e.Row.Cells[3].FindControl("lbl_amount_all") as Label;
                if (!string.IsNullOrEmpty(lblDepositAmountForAll.Text))
                {
                    DepositAmountForAll += Decimal.Parse(lblDepositAmountForAll.Text);
                }

                Label lblPayAmountFor = e.Row.Cells[4].FindControl("lbl_fpayamountfor") as Label;
                DepositPayamountforAll += Decimal.Parse(lblPayAmountFor.Text);
                Label lblUnPayAmountFor = e.Row.Cells[5].FindControl("lbl_funpayamountfor") as Label;
                DepositUnPayamountforAll += Decimal.Parse(lblUnPayAmountFor.Text);
            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = DepositAmountForAll.ToString();//采购订单中金额
            e.Row.Cells[4].Text = DepositPayamountforAll.ToString();//已支付金额
            e.Row.Cells[5].Text = DepositUnPayamountforAll.ToString();//未支付金额
            e.Row.Cells[6].Text = DepositCheckAmountForAll.ToString();//单据金额

        }
    }
    protected void txt_deposit_checkamountfor_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_deposit_checkamountfor;
        decimal fdeposit_checkamountfor = 0;
        RequestAmountForAll = Decimal.Parse(GridView4.FooterRow.Cells[3].Text);
        IndecreaseAmountForAll = Decimal.Parse(GridView2.FooterRow.Cells[3].Text);
        checkamountforAll = Decimal.Parse(GridView1.FooterRow.Cells[6].Text);
        foreach (GridViewRow gvr in GridView3.Rows)
        {
            txt_deposit_checkamountfor = gvr.Cells[6].FindControl("txt_deposit_checkamountfor") as TextBox;
            if (!string.IsNullOrEmpty(txt_deposit_checkamountfor.Text))
            {
                
                fdeposit_checkamountfor += decimal.Parse(txt_deposit_checkamountfor.Text);

            } 
            if (ddl_receipt_type.SelectedValue != "B")
            {
                txt_deposit_checkamountfor.Enabled = false;
            }
        }
        
        DepositCheckAmountForAll = fdeposit_checkamountfor;
        calculateAmount();
        GridView3.FooterRow.Cells[6].Text = fdeposit_checkamountfor.ToString();
    }


    protected void ddl_currency_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1EmptyBind();
        GridView2EmptyBind();
        GridView3EmptyBind();
        GridView4EmptyBind();
        GridView1.FooterRow.Cells[6].Text = "0";
        GridView2.FooterRow.Cells[3].Text = "0";
        GridView3.FooterRow.Cells[6].Text = "0";
        GridView4.FooterRow.Cells[3].Text = "0";
        int currencyID = Int32.Parse(ddl_currency.SelectedValue);
        ddlAccount(currencyID);
    }


    protected void ddl_receipt_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1EmptyBind();
        GridView2EmptyBind();
        GridView3EmptyBind();
        GridView4EmptyBind();
        GridView1.FooterRow.Cells[6].Text = "0";
        GridView2.FooterRow.Cells[3].Text = "0";
        GridView3.FooterRow.Cells[6].Text = "0";
        GridView4.FooterRow.Cells[3].Text = "0";
        int currencyID = Int32.Parse(ddl_currency.SelectedValue);
        ddlAccount(currencyID);
        txt_amount.Text = "0";
        string receipt_type = ddl_receipt_type.SelectedValue;
        switch (receipt_type)
        {
            case "A":
                grid1.Visible = true;
                grid2.Visible = true;
                grid3.Visible = false;
                grid4.Visible = true;
                break;
            case "B":
                grid1.Visible = false;
                grid2.Visible = false;
                grid3.Visible = false;
                grid4.Visible = true;
                break;
            default:
                grid1.Visible = false;
                grid2.Visible = false;
                grid3.Visible = true;
                grid4.Visible = false;
                break;
        }

    }
}