using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;


public partial class UI_payment_payment_request_list : System.Web.UI.Page
{
    private bool changed = false;
    protected List<string> SelectedItems
    {
        get { return ViewState["selecteditems"] != null ? (List<string>)ViewState["selecteditems"] : null; }
        set { ViewState["selecteditems"] = value; }
    }
    public DataSet ds { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
                GridViewBind();
           
        }
    }
    private int getCusIDByName(string name)
    {
        PaymentAdapter raa = new PaymentAdapter();
        int id = raa.getIDByName(name);
        return id;
    }
    private void GridViewBind()
    {
        PaymentRequestAdapter ida = new PaymentRequestAdapter();
        DataSet ds = ida.getUnCheckPaymentRequestLists();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        //payment_id,payee_unit,amount,emp_name,payment_date,isUserd
        ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("payment_id");
        dt.Columns.Add("payee_unit");
        dt.Columns.Add("amount", typeof(Decimal));
        dt.Columns.Add("emp_name");
        dt.Columns.Add("payment_date", typeof(DateTime));

        if (!changed)
            GetSelectedItem();
        foreach (string id in (List<string>)this.SelectedItems)
        {
            string[] item = id.Split('$');
            DataRow dr = dt.NewRow();
            dr["payment_id"] = item[0];
            dr["payee_unit"] = item[1];
            dr["amount"] = Decimal.Parse(item[2].Trim());
            dr["emp_name"] = item[3];
            dr["payment_date"] = DateTime.Parse(item[4]);
            dt.Rows.Add(dr);
        }
       
        ds.Tables.Add(dt);
        Session[UserInfoAdapter.CurrentUser.Name+"Reque"] = ds;
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.__doPostBack('ctl00$ContentPlaceHolder1$Button1','');window.close();</script>");
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script language='javascript' type='text/javascript'>window.opener.location.reload(true);window.close();</script>");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CommonAdapter ca = new CommonAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            
            if (e.Row.RowIndex > -1 && this.SelectedItems != null)
            {
                CheckBox cbx = (CheckBox)e.Row.FindControl("cbItem");
                int i = e.Row.RowIndex;
                string id = string.Format("{0}${1}${2}${3}${4}",
                this.GridView1.DataKeys[i].Values[0].ToString(),
                this.GridView1.DataKeys[i].Values[1].ToString(),
                this.GridView1.DataKeys[i].Values[2].ToString(),
                this.GridView1.DataKeys[i].Values[3].ToString(),
                this.GridView1.DataKeys[i].Values[4].ToString()
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
        GridViewBind();
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

            string id = string.Format("{0}${1}${2}${3}${4}",
               this.GridView1.DataKeys[i].Values[0].ToString(),
               this.GridView1.DataKeys[i].Values[1].ToString(),
               this.GridView1.DataKeys[i].Values[2].ToString(),
               this.GridView1.DataKeys[i].Values[3].ToString(),
               this.GridView1.DataKeys[i].Values[4].ToString()
               );

            if (selecteditems.Contains(id) && !cbx.Checked)
                selecteditems.Remove(id);
            if (!selecteditems.Contains(id) && cbx.Checked)
                selecteditems.Add(id);
        }
        this.SelectedItems = selecteditems;


    }
}