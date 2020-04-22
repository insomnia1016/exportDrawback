using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_QueryAndReports_ICSaleList : System.Web.UI.Page
{
    public decimal amountAll { get; set; }
    public decimal receiveAmountAll { get; set; }
    public decimal unreceiveAmountAll { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            show();
        }
    }

    private void show()
    {
        string fbillno = txt_fbillno_id.Text.Trim();
        string start_time = cb_start_time.Text;
        string end_time = cb_end_time.Text;
        string customer = txt_customer.Text.Trim();
        string check_status = ddl_check_status.SelectedValue;
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds1 = raa.getICSaleList(start_time, end_time, fbillno, customer, check_status);
        DataSet ds2 = raa.getICSaleListFromKindDee(start_time, end_time, fbillno, customer, check_status);
        DataTable ds = sub(ds2, ds1);
        ds.Merge(ds1.Tables[0]);
        ds = order(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    private DataTable sub(DataSet ds1, DataSet ds2)
    {
        var tempExcept = from r in ds1.Tables[0].AsEnumerable()
                         where
                         !(from rr in ds2.Tables[0].AsEnumerable() select rr.Field<string>("FBillNo")).Contains(
                         r.Field<string>("FBillNo"))
                         select r;
        if (tempExcept.Count() == 0)
        {
            return null;
        }
        else
        {
            return tempExcept.CopyToDataTable();

        }

    }
    private DataTable order(DataTable dt)
    {
        var tempDt = from r in dt.AsEnumerable()
                     orderby r.Field<string>("receipt_id") descending, r.Field<DateTime>("FDate") descending
                     select r;
        if (tempDt.Count() == 0)
        {
            return null;
        }
        else
        {
            return tempDt.CopyToDataTable();

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int currencyID = Int32.Parse(e.Row.Cells[7].Text);
            e.Row.Cells[7].Text = ca.getCurrencyByID(currencyID);
            int check_status = Int32.Parse(e.Row.Cells[8].Text);
            decimal amount = Decimal.Parse(e.Row.Cells[4].Text);
            amountAll += amount;
            decimal receiveAmount = Decimal.Parse(e.Row.Cells[5].Text);
            receiveAmountAll += receiveAmount;
            decimal unreceiveAmount = Decimal.Parse(e.Row.Cells[6].Text);
            unreceiveAmountAll += unreceiveAmount;
            switch (check_status)
            {
                case 1:
                    e.Row.Cells[8].Text = "部分核销";
                    break;
                case 2:
                    e.Row.Cells[8].Text = "完全核销";
                    break;
                default:
                    e.Row.Cells[8].Text = "未核销";
                    break;
            }

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = amountAll.ToString();//单据金额
            e.Row.Cells[5].Text = receiveAmountAll.ToString();//已核销金额
            e.Row.Cells[6].Text = unreceiveAmountAll.ToString();//未核销金额
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        show();

    }
}