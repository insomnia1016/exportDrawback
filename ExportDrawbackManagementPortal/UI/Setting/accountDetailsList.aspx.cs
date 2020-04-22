using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_Setting_accountDetailsList : System.Web.UI.Page
{
    public decimal AmountAll { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlAccount();
            GridView1EmptyBind();
        }
    }
    private void ddlAccount()
    {
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getAccounts();
        ddl_account.DataTextField = "account_id";
        ddl_account.DataValueField = "account_id";
        ddl_account.DataSource = ds;
        ddl_account.DataBind();
    }
    private void GridView1EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("account_id");
        dt.Columns.Add("receipt_id");
        dt.Columns.Add("operate_time");
        dt.Columns.Add("operater");
        dt.Columns.Add("amount");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = dt.NewRow();
                dr["amount"] = 0;
                dt.Rows.Add(dr);
            }
        }
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }
    protected void query_Click(object sender, EventArgs e)
    {
        HeadBind();
        ListBind();
    }
    private void HeadBind()
    {
        string account_id = ddl_account.SelectedValue;
        AccountLogAdapter ala = new AccountLogAdapter();
        DataSet ds = ala.getAccount(account_id);
        DataRow dr = ds.Tables[0].Rows[0];
        lbl_account_id.Text = dr["account_id"].ToString();
        lbl_account_name.Text = dr["account_name"].ToString();
        lbl_opening_bank.Text = dr["opening_bank"].ToString();
        CommonAdapter ca = new CommonAdapter();
        int currencyID = Int32.Parse(dr["currencyID"].ToString());
        lbl_currency.Text = ca.getCurrencyByID(currencyID);
        lbl_amount.Text = dr["amount"].ToString();
       
    }
    private void ListBind()
    {
        string account_id = ddl_account.SelectedValue;
        string start_time = CalendarBox1.Text;
        string end_time = CalendarBox2.Text;
        AccountLogAdapter ala = new AccountLogAdapter();
        DataSet ds = ala.getAccountLogs(account_id, start_time, end_time);
        if (ds.Tables[0].Rows.Count == 0)
        {
            GridView1EmptyBind();
            return;
        }
        show(ds);

    }
    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal amount = 0;
            if (!string.IsNullOrEmpty(e.Row.Cells[4].Text))
            {
                amount = Decimal.Parse(e.Row.Cells[4].Text);
            }
           
            AmountAll += amount;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = AmountAll.ToString();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ListBind();
    }
}