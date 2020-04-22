using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_ProfitBudget_detail : System.Web.UI.Page
{
   

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
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        DataSet head = pba.getProfitBudgetSummaryByID(id);
        lblt_sale_bill_no.Text = id;
        lbl_extra_charges.Text = Decimal.Parse(head.Tables[0].Rows[0]["extra_charges"].ToString()).ToString("f2");
       
        DataSet list = pba.getProfitBudgetList(id);
        gridviewBind(list);
    }

    private void gridviewBind(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_dept_id.Text = getDeptName(ds.Tables[0].Rows[0]["dept_id"].ToString());
            lbl_emp.Text = getEmpName(ds.Tables[0].Rows[0]["emp_id"].ToString());

        }
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //翻译是否含税
            bool sale_rate = e.Row.Cells[6].Text.Trim() == "1" ? true : false;
            if (sale_rate) e.Row.Cells[6].Text = "是";
            else e.Row.Cells[6].Text = "否";
            bool buy_rate = e.Row.Cells[8].Text.Trim() == "1" ? true : false;
            if (buy_rate) e.Row.Cells[8].Text = "是";
            else e.Row.Cells[8].Text = "否";
            
        }
       
    }

    private string getDeptName(string dept_id)
    {
        string result = dept_id;
        ProfitBudgetAdapter pba = new ProfitBudgetAdapter();
        if (!string.IsNullOrEmpty(dept_id))
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
        if (!string.IsNullOrEmpty(emp_id))
        {
            string emp_name = pba.getEmpNameById(emp_id);
            if (!string.IsNullOrEmpty(emp_name))
            {
                result = emp_name;
            }
        }
        return result;
    }

}