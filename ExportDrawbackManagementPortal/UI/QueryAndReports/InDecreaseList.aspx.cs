using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;


public partial class UI_QueryAndReports_InDecreaseList : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        string custName = Request.QueryString["custName"].ToString();
        currencyID = Int32.Parse(Request.QueryString["id"].ToString());
        if (!string.IsNullOrEmpty(custName))
        {
            ItemID = getCusIDByName(custName);
        }
        if (!IsPostBack)
        {
           
            if (!string.IsNullOrEmpty(custName))
            {
                GridViewBind(ItemID, currencyID);
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
    private void GridViewBind(int id,int currencyID)
    {
        InDecreaseAdapter ida = new InDecreaseAdapter();
        DataSet ds = ida.getUnCheckInDecreaseLists(id, currencyID);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        //bill_no,customer,amount_all,agenter,agent_date
        ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("bill_no");
        dt.Columns.Add("customer");
        dt.Columns.Add("amount_all", typeof(Decimal));
        dt.Columns.Add("agenter");
        dt.Columns.Add("agent_date", typeof(DateTime));

        if (!changed)
            GetSelectedItem();
        foreach (string id in (List<string>)this.SelectedItems)
        {
            string[] item = id.Split('$');
            DataRow dr = dt.NewRow();
            dr["bill_no"] = item[0];
            dr["customer"] = item[1];
            dr["amount_all"] = Decimal.Parse(item[2].Trim());
            dr["agenter"] = item[3];
            dr["agent_date"] = DateTime.Parse(item[4]);
            dt.Rows.Add(dr);
        }
       
        ds.Tables.Add(dt);
        Session[UserInfoAdapter.CurrentUser.Name+"InDec"] = ds;
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.__doPostBack('ctl00$ContentPlaceHolder1$Button1','');window.close();</script>");
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.location.reload(true);window.close();</script>");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int check_status = Int32.Parse(e.Row.Cells[6].Text);
            if (check_status == 0)
            {
                e.Row.Cells[6].Text = "未抵扣";
            }
            else
            {
                e.Row.Cells[6].Text = "已抵扣";
            }
            
            
            if (e.Row.RowIndex > -1 && this.SelectedItems != null)
            {
                CheckBox cbx = (CheckBox)e.Row.FindControl("cbItem");
                int i = e.Row.RowIndex;
                string id = string.Format("{0}${1}${2}${3}${4}${5}",
                this.GridView1.DataKeys[i].Values[0].ToString(),
                this.GridView1.DataKeys[i].Values[1].ToString(),
                this.GridView1.DataKeys[i].Values[2].ToString(),
                this.GridView1.DataKeys[i].Values[3].ToString(),
                this.GridView1.DataKeys[i].Values[4].ToString(),
                this.GridView1.DataKeys[i].Values[5].ToString()
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
        GridViewBind(ItemID, currencyID);
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

            string id = string.Format("{0}${1}${2}${3}${4}${5}",
               this.GridView1.DataKeys[i].Values[0].ToString(),
               this.GridView1.DataKeys[i].Values[1].ToString(),
               this.GridView1.DataKeys[i].Values[2].ToString(),
               this.GridView1.DataKeys[i].Values[3].ToString(),
               this.GridView1.DataKeys[i].Values[4].ToString(),
               this.GridView1.DataKeys[i].Values[5].ToString()
               );

            if (selecteditems.Contains(id) && !cbx.Checked)
                selecteditems.Remove(id);
            if (!selecteditems.Contains(id) && cbx.Checked)
                selecteditems.Add(id);
        }
        this.SelectedItems = selecteditems;


    }
}