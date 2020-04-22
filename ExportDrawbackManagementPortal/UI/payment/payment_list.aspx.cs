using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_payment_payment_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            query_Click(null, null);
        }
    }
    private void show(DataSet ds)
    {
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
    protected void query_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

    private void GridViewBind()
    {
        T_PaymentRequest item = new T_PaymentRequest();
        item.PaymentId = txt_payment_id.Text;
        item.PayeeUnit = txt_payee_unit.Text;
        item.PayeeOpeningBank = txt_payee_opening_bank.Text;
        item.PayeeAccountId = txt_payee_account_id.Text;
        item.CustomerBillNo = txt_icsale_id.Text;
        item.FactoryBillNo = txt_poorder_id.Text;
        item.GoodsModel = txt_goods_model.Text;
        item.EmpName = txt_emp_name.Text;
        string start_time = CalendarBox1.Text;
        string end_time = CalendarBox2.Text;

        PaymentAdapter pa = new PaymentAdapter();
        DataSet ds = pa.getPayments(item, start_time, end_time);
        show(ds);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex; 
        query_Click(null, null);
    }
    private void GridView1EmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("payment_id");
        dt.Columns.Add("payment_date");
        dt.Columns.Add("payee_unit");
        dt.Columns.Add("amount");
        dt.Columns.Add("payee_opening_bank");
        dt.Columns.Add("payee_account_id");
        dt.Columns.Add("customer_bill_no");
        dt.Columns.Add("factory_bill_no");
        dt.Columns.Add("goods_model");
        dt.Columns.Add("payment_explain");

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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string request_id = e.CommandArgument.ToString();
            PaymentRequestAdapter pra = new PaymentRequestAdapter();
            PaymentAdapter pa = new PaymentAdapter();

            try
            {
                bool result = pa.requestIsUsed(request_id);
                if (result)
                {
                    Label1.Text = "该付款通知书已经被使用，不能删除！";
                    return;
                }
                else
                {
                    pra.delete(request_id);
                    GridViewBind();
                    GridView1EmptyBind();
                    Label1.Text = "删除成功";
                }

            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}