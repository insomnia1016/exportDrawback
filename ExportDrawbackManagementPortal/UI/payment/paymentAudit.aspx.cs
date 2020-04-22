using System;
using System.Data;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_payment_paymentAudit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }
    private void GridViewBind()
    {
        PaymentAdapter raa = new PaymentAdapter();
        DataSet ds = raa.getReceiptAuditHeads();
        show(ds);

    }
    private void show(DataSet ds)
    {
        GridView3.DataSource = ds;
        GridView3.DataBind();
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            string receipt_id = args[0];
            string receipt_type = args[1];
            PaymentAdapter raa = new PaymentAdapter();
            PaymentRequestAdapter ida = new PaymentRequestAdapter();
            PaymentDepositAdapter da = new PaymentDepositAdapter();
            PaymentInDecreaseAdapter pia = new PaymentInDecreaseAdapter();
            try
            {
                DataSet ds = raa.getInDecreaseList(receipt_id);
                pia.updateHeadCheckStatus(ds);
              
                ds = raa.getDepositList(receipt_id);
               
                if (receipt_type == "B")
                {
                    da.updateHeadIsPayed(ds);
                }
                else
                {
                    da.updateHeadCheckStatus(ds);//更新定金单的check_status值；
                }
                ds = raa.getRequestList(receipt_id);
                ida.updateHeadCheckStatus(ds);

                raa.deleteToDone(receipt_id);
                raa.deleteReceipt(receipt_id);
                GridView3.SelectedIndex = -1;
                GridViewBind();

                Label1.Text = "删除成功";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
    }


    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        GridViewBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string receipt_id = query_receipt_id.Text;
        string customer_name = txt_customer.Text;
        string start_time = CalendarBox1.Text;
        string end_time = CalendarBox2.Text;
        PaymentAdapter raa = new PaymentAdapter();
        show(raa.queryReceiptHeads(receipt_id, customer_name, start_time,end_time));
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PaymentTypeAdapter raa = new PaymentTypeAdapter();
            string type = e.Row.Cells[3].Text;
            e.Row.Cells[3].Text = raa.getTypeNameByType(type);
            CommonAdapter ca = new CommonAdapter();
            int currencyID = Int32.Parse(e.Row.Cells[6].Text);
            e.Row.Cells[6].Text = ca.getCurrencyByID(currencyID);
            //0-未核销,1-部分核销,2－完全核销
            int check_status = Int32.Parse(e.Row.Cells[12].Text);
            switch (check_status)
            {
                case 1:
                    e.Row.Cells[12].Text = "部分核销";
                    break;
                case 2:
                    e.Row.Cells[12].Text = "完全核销";
                    break;
                default:
                    e.Row.Cells[12].Text = "未核销";
                    break;
            }
            string auditstatus = e.Row.Cells[11].Text.ToLower();
            if (auditstatus == "3")
            {
                e.Row.Cells[11].Text = "通过";
                Button btnDelete = e.Row.Cells[0].FindControl("btnDelete") as Button;
                btnDelete.Enabled = false;
            }
            else if (auditstatus == "2")
            {
                e.Row.Cells[11].Text = "不通过";
            }
            else if (auditstatus == "1")
            {
                e.Row.Cells[11].Text = "待审核";
            }
            else
            {
                e.Row.Cells[11].Text = "未提交";

            }
            int CheckerID = Int32.Parse(e.Row.Cells[8].Text);
            e.Row.Cells[8].Text = ca.getEmpNameByID(CheckerID);


        }
    }
    protected void btn_audit_Click(object sender, EventArgs e)
    {
        Button bt = sender as Button;
        string args = bt.CommandArgument.ToString();
        GridViewRow row = bt.Parent.Parent as GridViewRow;
        HyperLink thisData = row.Cells[1].Controls[0] as HyperLink;
        string receipt_id = thisData.Text;
        PaymentAdapter raa = new PaymentAdapter();
        T_PaymentReceiptHead head = new T_PaymentReceiptHead();
        head.ReceiptId = receipt_id;

        T_AccountLog account_log = new T_AccountLog();
        account_log.ReceiptId = receipt_id;
        account_log.AccountId = (bt.Parent.FindControl("hdf_account_id") as HiddenField).Value;
        account_log.Operater = UserInfoAdapter.CurrentUser.Name;
        account_log.OperateTime = DateTime.Now;
        account_log.Amount = Decimal.Parse(row.Cells[5].Text);

        AccountLogAdapter ala = new AccountLogAdapter();

        if (args == "Y")
        {
            account_log.Amount *= -1;
            head.AuditStatus = 3;//通过
            //更新账户金额
        }
        else
        {
            head.AuditStatus = 2;//不通过
        }
        ala.insert(account_log);
        ala.Add(account_log);
        raa.updateReceiptHeadAuditStatus(head);
        GridViewBind();
    }
}