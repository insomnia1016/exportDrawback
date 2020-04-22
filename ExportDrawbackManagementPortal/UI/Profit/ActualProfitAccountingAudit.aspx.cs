using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;
public partial class UI_Profit_ActualProfitAccountingAudit : System.Web.UI.Page
{
    public string sale_bill_no { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            show();
        }
    }
    private void show()
    {
        sale_bill_no = txt_bill_no.Text;
        ActualProfitAccountingAdapter apaa = new ActualProfitAccountingAdapter();
        GridView1.DataSource = apaa.getData(sale_bill_no);
        GridView1.DataBind();
    }
    protected void audit_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string arg = btn.CommandArgument;
        GridViewRow gvr = btn.Parent.Parent as GridViewRow;
        HyperLink thisData = gvr.Cells[1].Controls[0] as HyperLink;
        string SaleBillNo = thisData.Text;
        ActualProfitAccountingAdapter apaa = new ActualProfitAccountingAdapter();
        if (arg == "true")
        {
            apaa.audit(SaleBillNo, true);
        }
        else
        {
            apaa.audit(SaleBillNo, false);
        }
        show();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string audit_status = e.Row.Cells[9].Text;
            if (audit_status == "False")
            {
                e.Row.Cells[9].Text = "不通过";

            }
            else
            {
                e.Row.Cells[9].Text = "通过";
            }
        }
    }
    protected void btn_query_Click(object sender, EventArgs e)
    {
        show();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
}