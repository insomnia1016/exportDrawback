using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_Profit_ProfitDetail : System.Web.UI.Page
{
    decimal commissionforAll = 0;
    public static string sale_bill_no { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sale_bill_no = Request.QueryString["id"].ToString();
            show();

        }
    }

    private void show()
    {
        ProfitAccountingAdapter pba = new ProfitAccountingAdapter();
        DataSet ds = pba.getProfitBudgetByID(0, sale_bill_no);
        lbl_extra_charges.Text = decimal.Parse(ds.Tables[0].Rows[0]["extra_charges"].ToString()).ToString("f2");
        txt_dept_id.Text = getDeptName(ds.Tables[0].Rows[0]["dept_id"].ToString());
        txt_emp.Text = getEmpName(ds.Tables[0].Rows[0]["emp_id"].ToString());
        GridView1.DataSource = ds;
        GridView1.DataBind();
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
            e.Row.Cells[1].Text = e.Row.Cells[1].Text == "&amp;nbsp;" ? string.Empty : e.Row.Cells[1].Text;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text == "&amp;nbsp;" ? string.Empty : e.Row.Cells[2].Text;
            decimal commission = Decimal.Parse(e.Row.Cells[20].Text);
            commissionforAll += commission;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[20].Text = commissionforAll.ToString("f3");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
}