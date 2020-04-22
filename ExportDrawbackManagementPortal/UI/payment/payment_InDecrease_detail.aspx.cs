using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;
using System.IO;

public partial class UI_payment_payment_InDecrease_detail : System.Web.UI.Page
{
    decimal amountforAll = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            setData(id);
        }
    }
    private void setData(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return;
        }
        PaymentInDecreaseAdapter ida = new PaymentInDecreaseAdapter();
        DataSet head = ida.getInDecreaseHeadByBillNo(id);
        lbl_customer.Text = head.Tables[0].Rows[0]["customer"].ToString();
        lbl_indecrease_id.Text = id;
        if(!string.IsNullOrEmpty(head.Tables[0].Rows[0]["currencyID"].ToString()))
        {
            int currency = Int32.Parse(head.Tables[0].Rows[0]["currencyID"].ToString());
            CommonAdapter ca = new CommonAdapter();
            lbl_currency.Text = ca.getCurrencyByID(currency);
        }
        
        lbl_agenter.Text = head.Tables[0].Rows[0]["agenter"].ToString();
        lbl_agente_date.Text = head.Tables[0].Rows[0]["agent_date"].ToString();

        DataSet list = ida.getInDecreaseInfoByBillNo(id);
        gridviewBind(list);
    }
    private void gridviewBind(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }


   
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[2].Text;
            if (type == "R")
            {
                e.Row.Cells[2].Text = "扣款";
            }
            else
            {
                e.Row.Cells[2].Text = "补款";
            }
            if (!string.IsNullOrEmpty(e.Row.Cells[1].Text))
            {
                amountforAll += Decimal.Parse(e.Row.Cells[1].Text);
            }
            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "总计";
            e.Row.Cells[1].Text = amountforAll.ToString();//销售发票中金额
        }
    }
}