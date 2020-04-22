using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_DepositList : System.Web.UI.Page
{
    private bool changed = false;
    protected List<string> SelectedItems
    {
        get { return ViewState["selecteditems"] != null ? (List<string>)ViewState["selecteditems"] : null; }
        set { ViewState["selecteditems"] = value; }
    }
    public DataSet ds { get; set; }
    public int ItemID { get; set; }
    public int currencyID { get; set; }
    public string type { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        string custName = Request.QueryString["custName"].ToString();
        currencyID = Int32.Parse(Request.QueryString["id"].ToString());
        type = Request.QueryString["type"].ToString();
        if (!string.IsNullOrEmpty(custName))
        {
            ItemID = getCusIDByName(custName);
        }
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(custName))
            {
                GridViewBind(ItemID, currencyID, type);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>alert('请先输入客户信息！');window.close();</script>");

            }
        }
    }
    private int getCusIDByName(string name)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        int id = raa.getIDByName(name);
        return id;
    }
    private void GridViewBind(int id, int currencyID, string type)
    {
        DepositAdapter da = new DepositAdapter();
        DataSet ds = da.getUnCheckDepositLists(id, currencyID, type);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        //deposit_id,customer,amount_all,agenter,agent_date
        ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("deposit_id");
        dt.Columns.Add("amount_all", typeof(Decimal));
        dt.Columns.Add("receive_amount_for", typeof(Decimal));
        dt.Columns.Add("unreceive_amount_for", typeof(Decimal));
        dt.Columns.Add("check_amount_for", typeof(Decimal));
        dt.Columns.Add("currencyID");
        dt.Columns.Add("agenter");
        dt.Columns.Add("check_status", typeof(Int32));
        dt.Columns.Add("agent_date", typeof(DateTime));

        if (!changed)
            GetSelectedItem();
        foreach (string id in (List<string>)this.SelectedItems)
        {
            string[] item = id.Split('$');
            DataRow dr = dt.NewRow();
            dr["deposit_id"] = item[0];
            dr["amount_all"] = Decimal.Parse(item[1].Trim());
            dr["agenter"] = item[2];
            dr["agent_date"] = DateTime.Parse(item[3]);
            dr["check_status"] = Int32.Parse(item[4]);
            dr["receive_amount_for"] = Decimal.Parse(item[5].Trim()==""?"0":item[5].Trim());
            dr["unreceive_amount_for"] = Decimal.Parse(item[6].Trim() == "" ? "0" : item[6].Trim());
            dr["check_amount_for"] = Decimal.Parse(item[7].Trim() == "" ? "0" : item[7].Trim());
            dr["currencyID"] = item[8].Trim();
            dt.Rows.Add(dr);
        }

        ds.Tables.Add(dt);
        Session[UserInfoAdapter.CurrentUser.Name + "Depos"] = ds;
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.__doPostBack('ctl00$ContentPlaceHolder1$Button1','');window.close();</script>");
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.location.reload(true);window.close();</script>");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int check_status = Int32.Parse(e.Row.Cells[9].Text);
            if (check_status == 0)
            {
                e.Row.Cells[9].Text = "未抵扣";
            }
            else
            {
                e.Row.Cells[9].Text = "已抵扣";
            }
            string StrCurrencyID = e.Row.Cells[6].Text;
            if (!string.IsNullOrEmpty(StrCurrencyID))
            {
                int currencyID = Int32.Parse(StrCurrencyID);
                e.Row.Cells[6].Text = ca.getCurrencyByID(currencyID);
               
            }

            if (e.Row.RowIndex > -1 && this.SelectedItems != null)
            {
                CheckBox cbx = (CheckBox)e.Row.FindControl("cbItem");
                int i = e.Row.RowIndex;
                string id = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}${8}",
                this.GridView1.DataKeys[i].Values[0].ToString(),
                this.GridView1.DataKeys[i].Values[1].ToString(),
                this.GridView1.DataKeys[i].Values[2].ToString(),
                this.GridView1.DataKeys[i].Values[3].ToString(),
                this.GridView1.DataKeys[i].Values[4].ToString(),
                this.GridView1.DataKeys[i].Values[5].ToString(),
                this.GridView1.DataKeys[i].Values[6].ToString(),
                this.GridView1.DataKeys[i].Values[7].ToString(),
                this.GridView1.DataKeys[i].Values[8].ToString()
                );
                if (SelectedItems.Contains(id))
                    cbx.Checked = true;
                else
                    cbx.Checked = false;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewBind(ItemID, currencyID, type);
    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        GetSelectedItem();
        changed = true;
    }
    private void GetSelectedItem()
    {
        List<string> selecteditems = null;
        if (this.SelectedItems == null)
        {
            selecteditems = new List<string>();
        }
        else
        {
            selecteditems = this.SelectedItems;
        }


        //获取选择的记录
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)this.GridView1.Rows[i].FindControl("cbItem");

            string id = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}${8}",
                this.GridView1.DataKeys[i].Values[0].ToString(),
                this.GridView1.DataKeys[i].Values[1].ToString(),
                this.GridView1.DataKeys[i].Values[2].ToString(),
                this.GridView1.DataKeys[i].Values[3].ToString(),
                this.GridView1.DataKeys[i].Values[4].ToString(),
                this.GridView1.DataKeys[i].Values[5].ToString(),
                this.GridView1.DataKeys[i].Values[6].ToString(),
                this.GridView1.DataKeys[i].Values[7].ToString(),
                this.GridView1.DataKeys[i].Values[8].ToString()
                );

            if (selecteditems.Contains(id) && !cbx.Checked)
                selecteditems.Remove(id);
            if (!selecteditems.Contains(id) && cbx.Checked)
                selecteditems.Add(id);
        }
        this.SelectedItems = selecteditems;


    }
}