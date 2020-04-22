using System;
using System.Data;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_Setting_accountlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCurrencyBind();
            show();

        }
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CommonAdapter ca = new CommonAdapter();
            int currencyID = 1;
            if (!string.IsNullOrEmpty(e.Row.Cells[4].Text) && e.Row.Cells[4].Text != "&nbsp;")
            {
                currencyID = Int32.Parse(e.Row.Cells[4].Text);
            }

            e.Row.Cells[4].Text = ca.getCurrencyByID(currencyID);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridView1.SelectedIndex;
        GridViewRow gvr = GridView1.SelectedRow;
        txt_account_id.Text = gvr.Cells[3].Text;
        txt_account_name.Text = gvr.Cells[1].Text;
        txt_opening_bank.Text = gvr.Cells[2].Text;
        HiddenField1.Value = gvr.Cells[3].Text;
        txt_amount.Text = gvr.Cells[5].Text;
        CommonAdapter ca = new CommonAdapter();
        ddl_currency.SelectedValue = ca.getIDByCurrency(gvr.Cells[4].Text).ToString();
    }
    private void show()
    {
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getAccounts();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void save_Click(object sender, EventArgs e)
    {
        //表头
        T_Account item = new T_Account();
        item.AccountName = txt_account_name.Text.Trim();
        if (string.IsNullOrEmpty(item.AccountName))
        {
            Label1.Text = "账户名不能为空";
            return;
        }
        item.AccountId = txt_account_id.Text.Trim();
        if (string.IsNullOrEmpty(item.AccountId))
        {
            Label1.Text = "账号不能为空";
            return;
        }
        item.OpeningBank = txt_opening_bank.Text.Trim();
        if (string.IsNullOrEmpty(item.OpeningBank))
        {
            Label1.Text = "开户行不能为空";
            return;
        }
        item.CurrencyID = Int32.Parse(ddl_currency.SelectedValue);
        string amount = txt_amount.Text.Trim();
        if (string.IsNullOrEmpty(amount))
        {
            item.Amount = 0;
        }
        else item.Amount = Decimal.Parse(amount);
        

        try
        {

            AccountAdapter aa = new AccountAdapter();
            if (string.IsNullOrEmpty(HiddenField1.Value))
            {
                aa.insertAccount(item);
            }
            else
            {
                aa.update(item);
            }
            clean();
            GridView1.SelectedIndex = -1;
            show();
            Label1.Text = "哟，小伙子，不错，被你录入成功了";

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string account_id = e.CommandArgument.ToString();
           
            AccountAdapter aa = new AccountAdapter();
            try
            {
                aa.delete(account_id);
                GridView1.SelectedIndex = -1;
                show();
                clean();
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
        txt_account_id.Text = "";
        txt_account_name.Text = "";
        txt_opening_bank.Text = "";
        txt_amount.Text = "";
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}