using System;
using System.Data;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Collections.Generic;

public partial class UI_Setting_settlement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCurrencyBind();
            ddlOpeningBankBind();
            show();

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CommonAdapter ca = new CommonAdapter();
            int currencyID = 1;
            if (!string.IsNullOrEmpty(e.Row.Cells[3].Text) && e.Row.Cells[3].Text != "&nbsp;")
            {
                currencyID = Int32.Parse(e.Row.Cells[3].Text);
            }

            e.Row.Cells[3].Text = ca.getCurrencyByID(currencyID);
        }
    }

    private void ddlOpeningBankBind()
    {
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getSettlementListForDDL();
        DataRow dr = ds.Tables[0].NewRow();
        dr["opening_bank"] = "请选择";
        dr["account_name"] = "请选择";

        ds.Tables[0].Rows.InsertAt(dr,0);
        ddl_opening_bank.DataTextField = "opening_bank";
        ddl_opening_bank.DataValueField = "opening_bank";
        ddl_opening_bank.DataSource = ds;
        ddl_opening_bank.DataBind();

        DropDownList2.DataTextField = "opening_bank";
        DropDownList2.DataValueField = "opening_bank";
        DropDownList2.DataSource = ds;
        DropDownList2.DataBind();
    }
    private void show()
    {
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getSettlementList();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void save_Click(object sender, EventArgs e)
    {
       
         T_SettlementLog log = new T_SettlementLog();

        decimal out_amount = Decimal.Parse(TextBox5.Text.Trim());
        decimal in_amount = Decimal.Parse(TextBox6.Text.Trim());
        decimal exchange_rate = Decimal.Parse(txt_exchange_rate.Text.Trim());

        log.InSettlementAmount = in_amount;
        log.OutSettlementAmount = out_amount;
        log.ExchangeRate = exchange_rate;


        List<T_Account> lists = new List<T_Account>();
        T_Account list = new T_Account();
        list.AccountId = txt_account_id.Text.Trim();
        list.OpeningBank = ddl_opening_bank.SelectedValue;
        list.AccountName = txt_account_name.Text.Trim();
        list.Amount = Decimal.Parse(txt_amount.Text.Trim()) - out_amount;
        list.CurrencyID = Int32.Parse(ddl_currency.SelectedValue);

        log.OutAccountId = list.AccountId;
        log.OutOpeningBank = list.OpeningBank;
        log.OutAccountName = list.AccountName;
        log.OutAmount = Decimal.Parse(txt_amount.Text.Trim());
        log.OutCurrencyid = list.CurrencyID;

        lists.Add(list);

        list = new T_Account();
        list.AccountId = TextBox2.Text.Trim();
        list.OpeningBank = DropDownList2.SelectedValue;
        list.AccountName = TextBox3.Text.Trim();
        list.Amount = Decimal.Parse(TextBox4.Text.Trim()) + in_amount;
        list.CurrencyID = Int32.Parse(DropDownList1.SelectedValue);

        log.InAccountId = list.AccountId;
        log.InOpeningBank = list.OpeningBank;
        log.InAccountName = list.AccountName;
        log.InAmount = Decimal.Parse(TextBox4.Text.Trim());
        log.InCurrencyid = list.CurrencyID;

        lists.Add(list);

        log.OperateTime = DateTime.Now;
        log.Operater = UserInfoAdapter.CurrentUser.Name;
      
        

        try
        {
            AccountAdapter aa = new AccountAdapter();
            aa.updateLists(lists);
            aa.log(log);
            show();
            clean();
            Label1.Text = "结汇成功";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
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

        DropDownList1.DataTextField = "FName";
        DropDownList1.DataValueField = "FCurrencyID";
        DropDownList1.DataSource = ds;
        DropDownList1.DataBind();
    }
   
    private void clean()
    {
        txt_account_id.Text = "";
        txt_account_name.Text = "";
        txt_amount.Text = "";
        txt_exchange_rate.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        ddl_opening_bank.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;

    }


    protected void ddl_opening_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList2.SelectedValue = ddl_opening_bank.SelectedValue;
        ddl_currency.SelectedValue = "1000";
        DropDownList1.SelectedValue = "1";
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getAccountInfoByBankAndCurrency(ddl_opening_bank.SelectedValue, 1000);
        if(ds.Tables[0].Rows.Count>0){
            txt_account_id.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            txt_account_name.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            txt_amount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }
        ds = aa.getAccountInfoByBankAndCurrency(ddl_opening_bank.SelectedValue, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            TextBox2.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            TextBox4.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }
        
    }
    protected void ddl_currency_SelectedIndexChanged(object sender, EventArgs e)
    {
        string opening_bank = ddl_opening_bank.SelectedValue;
        DropDownList ddl = sender as DropDownList;
        if (string.IsNullOrEmpty(opening_bank))
        {
            return;
        }
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getAccountInfoByBankAndCurrency(opening_bank, Int32.Parse(ddl.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_account_id.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            txt_account_name.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            txt_amount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }
        if (ddl.SelectedValue == "1000")
        {
            DropDownList1.SelectedValue="1";
        }
        else if (ddl.SelectedValue == "1")
        {
            DropDownList1.SelectedValue = "1000";
        }

        ds = aa.getAccountInfoByBankAndCurrency(ddl_opening_bank.SelectedValue, Int32.Parse(DropDownList1.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            TextBox2.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            TextBox4.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string opening_bank = ddl_opening_bank.SelectedValue;
        DropDownList ddl = sender as DropDownList;
        if (string.IsNullOrEmpty(opening_bank))
        {
            return;
        }
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getAccountInfoByBankAndCurrency(opening_bank, Int32.Parse(ddl.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            TextBox2.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            TextBox4.Text = ds.Tables[0].Rows[0]["amount"].ToString();
           
        }
        if (ddl.SelectedValue == "1000")
        {
            ddl_currency.SelectedValue = "1";
        }
        else if (ddl.SelectedValue == "1")
        {
            ddl_currency.SelectedValue = "1000";
        }

        ds = aa.getAccountInfoByBankAndCurrency(ddl_opening_bank.SelectedValue, Int32.Parse(ddl_currency.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_account_id.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            txt_account_name.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            txt_amount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_opening_bank.SelectedValue= DropDownList2.SelectedValue;
        ddl_currency.SelectedValue = "1000";
        DropDownList1.SelectedValue = "1";
        AccountAdapter aa = new AccountAdapter();
        DataSet ds = aa.getAccountInfoByBankAndCurrency(ddl_opening_bank.SelectedValue, 1000);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_account_id.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            txt_account_name.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            txt_amount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }
        ds = aa.getAccountInfoByBankAndCurrency(ddl_opening_bank.SelectedValue, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            TextBox2.Text = ds.Tables[0].Rows[0]["account_id"].ToString();
            TextBox3.Text = ds.Tables[0].Rows[0]["account_name"].ToString();
            TextBox4.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        }
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        string exchange_rate = txt_exchange_rate.Text.Trim();
        if (string.IsNullOrEmpty(exchange_rate))
        {
            return;
        }
        decimal out_amount = Decimal.Parse(TextBox5.Text.Trim());
        decimal in_amount = out_amount * Decimal.Parse(exchange_rate);
        TextBox6.Text = in_amount.ToString();

    }
    protected void txt_exchange_rate_TextChanged(object sender, EventArgs e)
    {
        decimal exchange_rate = Decimal.Parse(txt_exchange_rate.Text.Trim());
        string out_amount = TextBox5.Text.Trim();
        if (string.IsNullOrEmpty(out_amount))
        {
            return;
        }
        decimal in_amount = exchange_rate * Decimal.Parse(out_amount);
        TextBox6.Text = in_amount.ToString();
    }
}