using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_InDecreaseEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCurrencyBind();
            GridViewBind();
        }
    }
    private void GridViewBind()
    {
        InDecreaseAdapter ida = new InDecreaseAdapter();
        DataSet ds = ida.getListsAll();
        show(ds);

    }
    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int index = GridView1.SelectedIndex;
        if (index < 0)
        {
            Label1.Text = "请先点击某行的编辑按钮";
            return;
        }
        T_IndecreaseHead head = new T_IndecreaseHead();
        head.BillNo = bill_no.Text.Trim();
        head.Customer = customer.Text;
        head.CurrencyID = ddl_currency.SelectedValue;
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();

        DataSet ds = raa.getCustInfoByName(head.Customer);
        if (ds.Tables[0].Rows.Count > 0)
        {
            head.CustomerID = Int32.Parse(ds.Tables[0].Rows[0]["FNumber"].ToString());
            head.ItemID = Int32.Parse(ds.Tables[0].Rows[0]["FItemID"].ToString());
        }
        else
        {
            Label1.Text = "客户姓名请输入关键字后从下拉框选择！";
            return;
        }
        head.AgentDate = DateTime.Parse(agent_date.Text);
        head.Agenter = agent_name.Text;
        T_IndecreaseList list = new T_IndecreaseList();
        list.BillNo = bill_no.Text.Trim();
        list.GNo = Int32.Parse(txt_g_no.Text);
        list.Type = ddl_type.SelectedValue;
        list.Note = txt_note.Text;
        list.Name = txt_name.Text;
        list.Amount = Decimal.Parse(txt_amount.Text);
        list.ApplyDate = DateTime.Parse(cb_apply_date.Text);
        if (list.Type == "R")
        {
            head.AmountAll = Decimal.Parse(hdf_amount_all.Value) - Decimal.Parse(hdf_amount.Value) + Decimal.Parse(txt_amount.Text) * -1;

        }
        else
        {
            head.AmountAll = Decimal.Parse(hdf_amount_all.Value) - Decimal.Parse(hdf_amount.Value) + Decimal.Parse(txt_amount.Text);

        }

        InDecreaseAdapter ida = new InDecreaseAdapter();
        try
        {
            ida.updateHead(head);
            ida.updateList(list);
            GridView1.SelectedIndex = -1;
            GridViewBind();
            clean();
            Label1.Text = "更新成功";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        InDecreaseAdapter ida = new InDecreaseAdapter();
        DataSet ds;
        if (string.IsNullOrEmpty(query_bill_no.Text))
        {
            ds = ida.getListsAll();
        }
        else
        {
            ds = ida.getInDecreaseInfoByBillNo(query_bill_no.Text);
        }
         
        show(ds);
        clean();
    }
    private void clean()
    {
        customer.Text = "";
        bill_no.Text = "";
        agent_name.Text = "";
        agent_date.Text = "";

        txt_g_no.Text = "";
        txt_name.Text = "";
        txt_amount.Text = "";
        ddl_type.SelectedIndex = -1;
        cb_apply_date.Text = "";
        txt_note.Text = "";
        GridView1.SelectedIndex = -1;

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridView1.SelectedIndex;
        HyperLink thisData = GridView1.SelectedRow.Cells[1].Controls[0] as HyperLink;
        string str_bill_no = thisData.Text;
        bill_no.Text = str_bill_no;
        InDecreaseAdapter ida = new InDecreaseAdapter();
        try
        {
            DataSet ds = ida.getInDecreaseHeadByBillNo(str_bill_no);
            customer.Text = ds.Tables[0].Rows[0]["customer"].ToString();
            agent_name.Text = ds.Tables[0].Rows[0]["agenter"].ToString();
            string str_agent_date = ds.Tables[0].Rows[0]["agent_date"].ToString();
            agent_date.Text = DateTime.Parse(str_agent_date).ToString("yyyy-MM-dd HH:mm:ss");
            hdf_amount_all.Value = ds.Tables[0].Rows[0]["amount_all"].ToString();
            ddl_currency.SelectedValue = ds.Tables[0].Rows[0]["currencyID"].ToString();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

        txt_g_no.Text = GridView1.SelectedRow.Cells[2].Text;
        txt_name.Text = GridView1.SelectedRow.Cells[3].Text;
        hdf_amount.Value = txt_amount.Text = GridView1.SelectedRow.Cells[4].Text;
        string typeName = GridView1.SelectedRow.Cells[5].Text;
        if (typeName == "扣    款") ddl_type.SelectedValue = "R";
        else ddl_type.SelectedValue = "B";
       
        if (ddl_type.SelectedValue == "R")
        {
            hdf_amount.Value = (Decimal.Parse(txt_amount.Text) * (-1)).ToString();
        }
        string str_apply_date = GridView1.SelectedRow.Cells[6].Text;
        cb_apply_date.Text = DateTime.Parse(str_apply_date).ToString("yyyy-MM-dd HH:mm:ss");
        txt_note.Text = GridView1.SelectedRow.Cells[7].Text;

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
            string[] estr = e.CommandArgument.ToString().Split(',');
            string bill_no = estr[0];
            int g_no = Convert.ToInt32(estr[1]);
            decimal amount = Decimal.Parse(estr[2]);
            string type = estr[3];
            InDecreaseAdapter ida = new InDecreaseAdapter();
            try
            {
                updateHeadAmountAllForDeleteList(bill_no, amount, type);
                ida.delete(bill_no, g_no);
                GridView1.SelectedIndex = -1;
                GridViewBind();
                clean();
                Label1.Text = "删除成功";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
    }
    private void updateHeadAmountAllForDeleteList(string bill_no, decimal amount, string type)
    {
        InDecreaseAdapter ida = new InDecreaseAdapter();
        DataSet ds = ida.getInDecreaseHeadByBillNo(bill_no);
        decimal amount_all = Decimal.Parse(ds.Tables[0].Rows[0]["amount_all"].ToString());
        if (type == "R")
        {
            amount_all -= amount * -1;
        }
        else
        {
            amount_all -= amount;
        }
        if (amount_all == 0)
        {
            ida.deleteHead(bill_no);
        }
        else
        {
            ida.updateHeadAmountAll(bill_no, amount_all);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string amount = GridView1.Rows[e.RowIndex].Cells[4].Text.ToString();
        //string type = GridView1.Rows[e.RowIndex].Cells[5].Text.ToString(); 
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridViewBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ReceiptAdapter ra = new ReceiptAdapter();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hnfCheckStatus = e.Row.Cells[0].FindControl("hnf_check_status") as HiddenField;
            int check_status = Int32.Parse(hnfCheckStatus.Value);
            if (check_status == 2)
            {
                Button btn_delete = e.Row.Cells[0].FindControl("btnDelete") as Button;
                btn_delete.Enabled = false;
                btnSave.Enabled = false;
            }
            string type = e.Row.Cells[5].Text;
            if (type == "R")
            {
                e.Row.Cells[5].Text = "扣    款";
            }
            else
            {
                e.Row.Cells[5].Text = "补    款";
            }
        }
    }
}