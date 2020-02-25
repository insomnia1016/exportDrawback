using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_ReceiptAdd : System.Web.UI.Page
{
    public DataSet ds { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlReceiptTypeBind();
            ddlCurrencyBind();
            GridViewEmptyBind();

        }
        else
        {
            UserInfo user = UserInfoAdapter.CurrentUser;
            ds = (DataSet)Session[user.Name];
            Session[user.Name] = null;
            show(ds);
        }
       
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
        ddl_currency.DataValueField = "FNumber";
        ddl_currency.DataSource = ds;
        ddl_currency.DataBind();
    }
    private void show(DataSet ds)
    {

        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();
    }

    private void GridViewEmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("FInterID");
        dt.Columns.Add("FBillNo");
        dt.Columns.Add("FDate");
        dt.Columns.Add("FAmountFor");
        dt.Columns.Add("FReceiveAmountFor");
        dt.Columns.Add("FUnReceiveAmountFor");
        dt.Columns.Add("FCurrencyID");
        dt.Columns.Add("FNote");

        if (dt.Rows.Count == 0)
        {
            for (int i = 0; i < 19; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
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
             }
            
         }
    }
}