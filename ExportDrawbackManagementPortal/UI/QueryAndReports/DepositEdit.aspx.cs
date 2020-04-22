using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_QueryAndReports_DepositEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init();
        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //定金表头
        int index = GridView2.SelectedIndex;
        string str_deposit_id = GridView2.SelectedRow.Cells[1].Text;
        txt_deposit_id.Text = str_deposit_id;
        DepositAdapter da = new DepositAdapter();
        DataSet ds = da.getDepositHeadByKey(str_deposit_id);
        customer.Text = ds.Tables[0].Rows[0]["customer"].ToString();
        txt_amount_all.Text = ds.Tables[0].Rows[0]["amount_all"].ToString();
        CommonAdapter ca = new CommonAdapter();
        ddl_agenter.SelectedValue = ca.getIDByName(ds.Tables[0].Rows[0]["agenter"].ToString()).ToString();
        string str_agent_date = ds.Tables[0].Rows[0]["agent_date"].ToString();
        agent_date.Text = DateTime.Parse(str_agent_date).ToString("yyyy-MM-dd HH:mm:ss");
        ddl_currency.SelectedValue = ds.Tables[0].Rows[0]["currencyID"].ToString();

        //定金表体
        ds = da.getDepositListByKey(str_deposit_id);
        show(ds, GridView1);
        
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string deposit_id = e.CommandArgument.ToString();
            DepositAdapter da = new DepositAdapter();
            try
            {
                da.delete(deposit_id);
                da.deleteToDone(deposit_id);
                GridViewBind();
                clean();
                GridViewEmptyBind();
                Label1.Text = "删除成功";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
    }

    private void clean()
    {
        customer.Text = "";
        txt_deposit_id.Text = "";
        agent_date.Text = "";
        GridView2.SelectedIndex = -1;

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GridViewBind();

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string currencyId = e.Row.Cells[3].Text;
            CommonAdapter ca = new CommonAdapter();
            e.Row.Cells[3].Text = ca.getCurrencyByID(Int32.Parse(currencyId));

            HiddenField hnfCheckStatus = e.Row.Cells[0].FindControl("hnf_check_status") as HiddenField;
            int check_status = Int32.Parse(hnfCheckStatus.Value);
            if (check_status == 2)
            {
                Button btn_delete = e.Row.Cells[0].FindControl("btnDelete") as Button;
                btn_delete.Enabled = false;
            }
        }
    }
    private void GridViewEmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("g_no");
        dt.Columns.Add("FBillNo");
        dt.Columns.Add("Fdate", typeof(DateTime));
        dt.Columns.Add("amount", typeof(Decimal));
        dt.Columns.Add("note");

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
    protected void Button2_Click(object sender, EventArgs e)
    {
   
        string deposit_id = query_deposit_id.Text;
        DepositAdapter da = new DepositAdapter();
        show(da.getDepositHeads(deposit_id), GridView2);
        GridView2.SelectedIndex = -1;
    }
    
    private void init()
    {
        ddlCurrencyBind();
        ddlAgenterBind();
        GridViewEmptyBind();
        GridViewBind();
    }
    private void ddlAgenterBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds = ca.getEmp();
        ddl_agenter.DataTextField = "FName";
        ddl_agenter.DataValueField = "FItemID";
        ddl_agenter.DataSource = ds;
        ddl_agenter.DataBind();
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
    private void GridViewBind()
    {
        DepositAdapter da = new DepositAdapter();
        DataSet ds = da.getListsAll();
        show(ds,GridView2);

    }
    private void show(DataSet ds,GridView gv)
    {
        gv.DataSource = ds;
        gv.DataBind();
    }
}