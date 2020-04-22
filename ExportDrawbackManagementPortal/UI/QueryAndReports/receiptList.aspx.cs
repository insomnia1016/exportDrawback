using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;


public partial class UI_QueryAndReports_receiptList : System.Web.UI.Page
{
    private bool changed = false;
    protected List<string> SelectedItems
    {
        get { return ViewState["selecteditems"] != null ? (List<string>)ViewState["selecteditems"] : null; }
        set { ViewState["selecteditems"] = value; }
    }
    public DataSet ds { get; set; }
    public int ItemID { get; set; }
    public int ID { get; set; }
    public string custName { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        custName = Request.QueryString["custName"].ToString();
        ID = Int32.Parse(Request.QueryString["id"].ToString());
        if (!string.IsNullOrEmpty(custName))
        {
            ItemID = getCusIDByName(custName);
        }
        if (!IsPostBack)
        {
           

            if (!string.IsNullOrEmpty(custName))
            {

                GridViewBind(ItemID, custName, ID);

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
    private void GridViewBind(int id, string name, int currencyID)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds1 = raa.getUnCheckReceiptLists(id, currencyID);
        DataSet ds2 = raa.getDoneBillNos(name);
        DataTable dt = sub(ds1, ds2);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    private DataTable sub(DataSet ds1, DataSet ds2)
    {
        var tempExcept = from r in ds1.Tables[0].AsEnumerable()
                         where
                         !(from rr in ds2.Tables[0].AsEnumerable() select rr.Field<string>("FBillNo")).Contains(
                         r.Field<string>("FBillNo"))
                         select r;
        if (tempExcept.Count() == 0)
        {
            return null;
        }
        else
        {
            return tempExcept.CopyToDataTable();

        }

    }

    protected void btn_import_Click(object sender, EventArgs e)
    {
        //FInterID,FBillNo,FDate,FInvoiceAmountFor,FReceiveAmountFor,FUnReceiveAmountFor,FCurrencyID,FNote
        ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("FInterID", typeof(Int32));
        dt.Columns.Add("FBillNo");
        dt.Columns.Add("FDate", typeof(DateTime));
        dt.Columns.Add("FAmountFor", typeof(Decimal));
        dt.Columns.Add("FReceiveAmountFor", typeof(Decimal));
        dt.Columns.Add("FUnReceiveAmountFor", typeof(Decimal));
        dt.Columns.Add("FCurrencyID", typeof(Int32));
        dt.Columns.Add("FNote");

        if (!changed)
            GetSelectedItem();
        foreach (string id in (List<string>)this.SelectedItems)
        {
            string[] item = id.Split('$');
            DataRow dr = dt.NewRow();
            dr["FInterID"] = Int32.Parse(item[0]);
            dr["FBillNo"] = item[1];
            dr["FDate"] = DateTime.Parse(item[2].Trim());
            dr["FAmountFor"] = Decimal.Parse(item[3]);
            dr["FReceiveAmountFor"] = Decimal.Parse(item[4]);
            dr["FUnReceiveAmountFor"] = Decimal.Parse(item[5]);
            dr["FCurrencyID"] = Int32.Parse(item[6]);
            dr["FNote"] = item[7];
            dt.Rows.Add(dr);
        }
        //DataRow dr1 = dt.NewRow();

        //dr1["FInterID"] = 0;
        //dr1["FBillNo"] = "";
        //dr1["FDate"] = DateTime.MinValue;
        //dr1["FAmountFor"] = 0;
        //dr1["FReceiveAmountFor"] = 0;
        //dr1["FUnReceiveAmountFor"] = 0;
        //dr1["FCurrencyID"] = 0;
        //dr1["FNote"] = "";
        //dt.Rows.Add(dr1);
        ds.Tables.Add(dt);
        Session[UserInfoAdapter.CurrentUser.Name] = ds;
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.__doPostBack('ctl00$ContentPlaceHolder1$Button1','');window.close();</script>");
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.location.reload(true);window.close();</script>");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int currencyID = Int32.Parse(e.Row.Cells[6].Text);
            e.Row.Cells[6].Text = ca.getCurrencyByID(currencyID);

            if (e.Row.RowIndex > -1 && this.SelectedItems != null)
            {
                CheckBox cbx = (CheckBox)e.Row.FindControl("cbItem");
                int i = e.Row.RowIndex;
                string id = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}",
                this.GridView1.DataKeys[i].Values[0].ToString(),
                this.GridView1.DataKeys[i].Values[1].ToString(),
                this.GridView1.DataKeys[i].Values[2].ToString(),
                this.GridView1.DataKeys[i].Values[3].ToString(),
                this.GridView1.DataKeys[i].Values[4].ToString(),
                this.GridView1.DataKeys[i].Values[5].ToString(),
                this.GridView1.DataKeys[i].Values[6].ToString(),
                this.GridView1.DataKeys[i].Values[7].ToString()

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
        GridViewBind(ItemID, custName, ID);
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

            string id = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}",
               this.GridView1.DataKeys[i].Values[0].ToString(),
               this.GridView1.DataKeys[i].Values[1].ToString(),
               this.GridView1.DataKeys[i].Values[2].ToString(),
               this.GridView1.DataKeys[i].Values[3].ToString(),
               this.GridView1.DataKeys[i].Values[4].ToString(),
               this.GridView1.DataKeys[i].Values[5].ToString(),
               this.GridView1.DataKeys[i].Values[6].ToString(),
               this.GridView1.DataKeys[i].Values[7].ToString()

               );

            if (selecteditems.Contains(id) && !cbx.Checked)
                selecteditems.Remove(id);
            if (!selecteditems.Contains(id) && cbx.Checked)
                selecteditems.Add(id);
        }
        this.SelectedItems = selecteditems;


    }

}