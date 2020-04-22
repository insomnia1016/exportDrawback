using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_QueryAndReports_receipt_detail : System.Web.UI.Page
{
    decimal amountforAll = 0;
    decimal receiveamountforAll = 0;
    decimal unreceiveamountforAll = 0;
    decimal checkamountforAll = 0;
    decimal IndecreaseAmountForAll = 0;
    decimal DepositAmountForAll = 0;
    decimal DepositCheckAmountForAll = 0;
    decimal DepositReceiveamountforAll = 0;
    decimal DepositUnreceiveamountforAll = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string receipt_id = Request.QueryString["id"].ToString();
            HeadBind(receipt_id);
            ListBind(receipt_id);
            InDecreaseBind(receipt_id);
            DepositBind(receipt_id);
            txt_recetpt_no.Text = receipt_id;
            DDLBind();
            string receipt_type = ddl_receipt_type.SelectedValue;
            switch (receipt_type)
            {
                case "A":
                    grid1.Visible = true;
                    grid2.Visible = true;
                    break;
                case "B":
                    grid1.Visible = false;
                    grid2.Visible = true;
                    break;
                default:
                    grid1.Visible = false;
                    grid2.Visible = false;
                    break;
            }
        }
       
    }
    private void DDLBind()
    {
        ddlReceiptTypeBind();
        ddlCurrencyBind();
        ddlDepartmentBind();
        ddlEmpBind();
        ddlPreparerBind();
        ddlCheckerBind();
    }
    private void HeadBind(string receipt_id)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds = raa.getReceiptHeads(receipt_id);
        if(ds.Tables[0].Rows.Count>0){
            DataRow dr = ds.Tables[0].Rows[0];
            txt_customer.Text = dr["customer_name"].ToString();
            d_date.Text = DateTime.Parse(dr["receipt_date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            string currencyID = dr["currency"].ToString();
            ddl_currency.SelectedValue = currencyID;
            txt_amount.Text = Decimal.Parse(dr["amount"].ToString()).ToString("0.00");
            if (!string.IsNullOrEmpty(dr["receipt_charge"].ToString()))
            {
                txt_receipt_charge.Text = Decimal.Parse(dr["receipt_charge"].ToString()).ToString("0.00");

            }
            else
            {
                txt_receipt_charge.Text = "";
            }
            ddl_receipt_type.SelectedValue = dr["receipt_type"].ToString();
            if (dr["receipt_type"].ToString() != "A" && dr["receipt_type"].ToString() != "B")
            {
                txt_amount.Enabled = true;
            }
            txt_account.Text = dr["account_id"].ToString();
            ddl_department.SelectedValue = dr["FDeptID"].ToString();
            ddl_emp.SelectedValue = dr["FEmpID"].ToString();
            ddl_preparer.SelectedValue = dr["FPreparer"].ToString();
            ddl_checker.SelectedValue = dr["FChecker"].ToString();
            check_date.Text = DateTime.Parse(dr["FCheckDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            //通过审核后不让其修改
            string audit_status = dr["audit_status"].ToString();
            txt_note.Text = dr["note"].ToString();
            if (!string.IsNullOrEmpty(audit_status) && audit_status == "3")
            {
                updateUser.Enabled = false;
            }
        }
    }

    private void ListBind(string receipt_id)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds = raa.getReceiptList(receipt_id);
        if (ds.Tables[0].Rows.Count == 0)
        {
            GridView1EmptyBind();
        }
        else
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }

    private void InDecreaseBind(string receipt_id)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds = raa.getInDecreaseList(receipt_id);
        if (ds.Tables[0].Rows.Count == 0)
        {
            GridView2EmptyBind();
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }
       
    }
    private void DepositBind(string receipt_id)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds = raa.getDepositList(receipt_id);
        if (ds.Tables[0].Rows.Count == 0)
        {
            GridView3EmptyBind();
        }
        else
        {
            GridView3.DataSource = ds;
            GridView3.DataBind();
        }

    }
    private void GridView1EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("receipt_no");
        dt.Columns.Add("FBillNo");
        dt.Columns.Add("FDate");
        dt.Columns.Add("FAmountFor");
        dt.Columns.Add("FReceiveAmountFor");
        dt.Columns.Add("FUnReceiveAmountFor");
        dt.Columns.Add("FCheckAmountFor");
        dt.Columns.Add("FCurrencyID");
        dt.Columns.Add("FNote");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        this.GridView1.Enabled = false;
    }
    private void GridView2EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("receipt_no");
        dt.Columns.Add("InDecrease_no");
        dt.Columns.Add("customer");
        dt.Columns.Add("FAmountFor");
        dt.Columns.Add("agenter");
        dt.Columns.Add("agent_date");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView2.DataSource = dt;
        this.GridView2.DataBind();
        GridView2.Enabled = false;
    }
    private void GridView3EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("receipt_no");
        dt.Columns.Add("Deposit_id");
        dt.Columns.Add("FDate");
        dt.Columns.Add("FAmountFor");
        dt.Columns.Add("FReceiveAmountFor");
        dt.Columns.Add("FUnReceiveAmountFor");
        dt.Columns.Add("FCheckAmountFor");
        dt.Columns.Add("FCurrencyID");
        dt.Columns.Add("agenter");
        dt.Columns.Add("FNote");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView3.DataSource = dt;
        this.GridView3.DataBind();
        GridView3.Enabled = false;
    }
    private string getCusIDByName(string name)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds = raa.getCustInfoByName(name);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["FNumber"].ToString();
        }
        else
        {
            return string.Empty;
        }

    }
    protected void updateUser_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        T_ReceiptHead head = new T_ReceiptHead();
        head.CustomerName = txt_customer.Text;
        head.CustomerCode = getCusIDByName(head.CustomerName);
        
        head.ReceiptId = txt_recetpt_no.Text;
        head.ReceiptDate = DateTime.Parse(d_date.Text);
        head.ReceiptType = ddl_receipt_type.SelectedValue;
        if (!String.IsNullOrEmpty(txt_amount.Text) && Decimal.Parse(txt_amount.Text) > 0)
        {
            head.Amount = Decimal.Parse(txt_amount.Text);
        }
        else
        {
            Label1.Text = "该收款单金额不能为0。";
            return;
        }
        head.Currency = ddl_currency.SelectedValue;
        if (!string.IsNullOrEmpty(txt_receipt_charge.Text))
        {
            head.ReceiptCharge = Decimal.Parse(txt_receipt_charge.Text);

        }
        head.FDeptID = Int32.Parse(ddl_department.SelectedValue);
        head.FEmpID = Int32.Parse(ddl_emp.SelectedValue);
        head.FPreparer = Int32.Parse(ddl_preparer.SelectedValue);
        head.FChecker = Int32.Parse(ddl_checker.SelectedValue);
        head.FCheckDate = DateTime.Parse(check_date.Text);
        head.FCheckStatus = (int)2;
        head.Note = txt_note.Text;
        CommonAdapter ca = new CommonAdapter();
        List<T_ReceiptList> lists = new List<T_ReceiptList>();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            T_ReceiptList list = new T_ReceiptList();
            list.ReceiptId = head.ReceiptId;
            string g_no = GridView1.Rows[i].Cells[0].Text;
            if (string.IsNullOrEmpty(g_no) || g_no == "&nbsp;")
            {
                break;
            }
            list.ReceiptNo = Int32.Parse(g_no);
            list.FBillNo = GridView1.Rows[i].Cells[1].Text;
            list.FDate = DateTime.Parse(GridView1.Rows[i].Cells[2].Text);
            list.FAmountFor = Decimal.Parse(GridView1.Rows[i].Cells[3].Text);
            list.FReceiveAmountFor = Decimal.Parse(GridView1.Rows[i].Cells[4].Text);
            list.FUnReceiveAmountFor = Decimal.Parse(GridView1.Rows[i].Cells[5].Text);
            string fcheckamountfor = (GridView1.Rows[i].Cells[6].FindControl("txt_fcheckamountfor") as TextBox).Text;
            if (string.IsNullOrEmpty(fcheckamountfor))
            {
                Label1.Text = "核销金额不能为空";
                return;
            }
            list.FCheckAmountFor = Decimal.Parse(fcheckamountfor);
            list.FCurrencyID = ca.getIDByCurrency(GridView1.Rows[i].Cells[7].Text);
            list.FNote = (GridView1.Rows[i].Cells[8].FindControl("txt_note") as TextBox).Text;
            if (list.FUnReceiveAmountFor - list.FCheckAmountFor == 0)
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
            T_ReceiptList list = new T_ReceiptList();
            list.ReceiptId = head.ReceiptId;
            string g_no = (GridView2.Rows[i].Cells[0].FindControl("lbl_gno") as Label).Text;
            if (string.IsNullOrEmpty(g_no) || g_no == "&nbsp;")
            {
                break;
            }
            list.ReceiptNo = Int32.Parse(g_no);
           
            list.IndecreaseNo = (GridView2.Rows[i].Cells[1].FindControl("txt_bill_no") as TextBox).Text;
            list.FAmountFor = Decimal.Parse((GridView2.Rows[i].Cells[3].FindControl("lbl_amount_all") as TextBox).Text);
            lists.Add(list);
        }
        List<T_ReceiptList> listsFromDeposit = new List<T_ReceiptList>();
        foreach (GridViewRow gvr in GridView3.Rows)
        {
            T_ReceiptList list = new T_ReceiptList();
            list.ReceiptId = head.ReceiptId;
            string g_no = (gvr.Cells[0].FindControl("lbl_gno") as Label).Text;
            if (string.IsNullOrEmpty(g_no) || g_no == "&nbsp;")
            {
                break;
            }
            list.ReceiptNo = GridView1.Rows.Count + GridView2.Rows.Count + Int32.Parse((gvr.Cells[0].FindControl("lbl_gno") as Label).Text);
            string deposit_id = (gvr.Cells[1].FindControl("txt_deposit_id") as TextBox).Text;
            if (string.IsNullOrEmpty(deposit_id))
            {
                continue;
            }
            list.DepositId = deposit_id;
            list.FDate = DateTime.Parse((gvr.Cells[2].FindControl("lbl_agent_date") as Label).Text);
            list.FNote = (gvr.Cells[9].FindControl("txt_note") as TextBox).Text;
            list.FCurrencyID = ca.getIDByCurrency((gvr.Cells[7].FindControl("lbl_fcurrencyid") as Label).Text);
            list.FAmountFor = Decimal.Parse((gvr.Cells[3].FindControl("lbl_amount_all") as Label).Text);
            list.FReceiveAmountFor = Decimal.Parse((gvr.Cells[4].FindControl("lbl_freceiveamountfor") as Label).Text);
            list.FUnReceiveAmountFor = Decimal.Parse((gvr.Cells[5].FindControl("lbl_funreceiveamountfor") as Label).Text);
            string str_deposit_checkamountfor = (gvr.Cells[6].FindControl("txt_deposit_checkamountfor") as TextBox).Text;
            if (string.IsNullOrEmpty(str_deposit_checkamountfor))
            {
                Label1.Text = "核销金额不能为空";
                return;
            }
            list.FCheckAmountFor = Decimal.Parse(str_deposit_checkamountfor);
            if (list.FUnReceiveAmountFor - list.FCheckAmountFor == 0)
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
        List<T_ReceiptList> addtodonelists = addBillNoToDone(lists, head);
        List<T_ReceiptList> checkStatusLists = addInDecreaseNoToDone(lists);
        try
        {
            //更新数据
            ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
            DepositAdapter da = new DepositAdapter();

            raa.updateReceiptHead(head);
            raa.updateReceiptList(lists);
            raa.updateReceiptList(listsFromDeposit);

            //TODO:更新销售发票的check_status,增减单的check_status
            raa.updateToDone(addtodonelists);
            if (ddl_receipt_type.SelectedValue == "B")
            {
                da.updateHeadIsPayed(listsFromDeposit);
            }
            else
            {
                da.updateHeadCheckStatus(listsFromDeposit);//更新定金单的check_status值；
            }
            Label1.Text = "更新成功了";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    private List<T_ReceiptList> addInDecreaseNoToDone(List<T_ReceiptList> lists)
    {
        List<T_ReceiptList> newlists = new List<T_ReceiptList>();
        foreach (T_ReceiptList list in lists)
        {
            if (!string.IsNullOrEmpty(list.IndecreaseNo))
            {

                newlists.Add(list);
            }
        }
        return newlists;
    }

    private List<T_ReceiptList> addBillNoToDone(List<T_ReceiptList> lists, T_ReceiptHead head)
    {
        List<T_ReceiptList> newlists = new List<T_ReceiptList>();
        foreach (T_ReceiptList list in lists)
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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!string.IsNullOrEmpty(e.Row.Cells[3].Text))
            {
                IndecreaseAmountForAll += Decimal.Parse(e.Row.Cells[3].Text);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = IndecreaseAmountForAll.ToString();//单据金额
        }
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!string.IsNullOrEmpty(e.Row.Cells[7].Text) && e.Row.Cells[7].Text != "&nbsp;")
            {
                int currencyID = Int32.Parse(e.Row.Cells[7].Text);
                e.Row.Cells[7].Text = ca.getCurrencyByID(currencyID);
                amountforAll += Decimal.Parse(e.Row.Cells[3].Text);
                receiveamountforAll += Decimal.Parse(e.Row.Cells[4].Text);
                unreceiveamountforAll += Decimal.Parse(e.Row.Cells[5].Text);
                TextBox tb_fcheckamountfor = e.Row.Cells[6].FindControl("txt_fcheckamountfor") as TextBox;
                checkamountforAll += Decimal.Parse(tb_fcheckamountfor.Text);
            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = amountforAll.ToString();//单据金额
            e.Row.Cells[4].Text = receiveamountforAll.ToString();//已核销金额
            e.Row.Cells[5].Text = unreceiveamountforAll.ToString();//未核销金额
            e.Row.Cells[6].Text = checkamountforAll.ToString();//单据金额

        }
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

    private void ddlEmpBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getEmp();
        ddl_emp.DataTextField = "FName";
        ddl_emp.DataValueField = "FItemID";
        ddl_emp.DataSource = ds;
        ddl_emp.DataBind();
    }
    private void initAmount()
    {
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
                amount = receipt_charge;
                break;
        }
        txt_amount.Text = amount.ToString();

    }
    private void ddlDepartmentBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getDepartment();
        ddl_department.DataTextField = "FName";
        ddl_department.DataValueField = "FItemID";
        ddl_department.DataSource = ds;
        ddl_department.DataBind();
    }
   
    private void ddlReceiptTypeBind()
    {
        ReceiptAdapter ra = new ReceiptAdapter();
        DataSet ds = ra.getReceipts();
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
    protected void txt_receipt_charge_TextChanged(object sender, EventArgs e)
    {
        IndecreaseAmountForAll = Decimal.Parse(GridView2.FooterRow.Cells[3].Text);
        DepositAmountForAll = Decimal.Parse(GridView3.FooterRow.Cells[6].Text);
        checkamountforAll = Decimal.Parse(GridView1.FooterRow.Cells[6].Text);
        calculateAmount();    
    }
    protected void txt_fcheckamountfor_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_fcheckamountfor;
        decimal fcheckamountfor = 0;
        IndecreaseAmountForAll = Decimal.Parse(GridView2.FooterRow.Cells[3].Text);
        DepositAmountForAll = Decimal.Parse(GridView3.FooterRow.Cells[6].Text);
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

                Label lblReceiveAmountFor = e.Row.Cells[4].FindControl("lbl_freceiveamountfor") as Label;
                DepositReceiveamountforAll += Decimal.Parse(lblReceiveAmountFor.Text);
                Label lblUnReceiveAmountFor = e.Row.Cells[5].FindControl("lbl_funreceiveamountfor") as Label;
                DepositUnreceiveamountforAll += Decimal.Parse(lblUnReceiveAmountFor.Text);
            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = DepositAmountForAll.ToString();//销售订单中金额
            e.Row.Cells[4].Text = DepositReceiveamountforAll.ToString();//已核销金额
            e.Row.Cells[5].Text = DepositUnreceiveamountforAll.ToString();//未核销金额
            e.Row.Cells[6].Text = DepositCheckAmountForAll.ToString();//单据金额

        }
    }
    protected void txt_deposit_checkamountfor_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_deposit_checkamountfor;
        decimal fdeposit_checkamountfor = 0;
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
}