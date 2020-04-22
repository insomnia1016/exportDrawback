using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_payment_payment_inDecrease_add : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            ddlCurrencyBind();
            bill_no.Text = LoadLastBillNo();
            agent_name.Text = UserInfoAdapter.CurrentUser.Name;
            agent_date.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
    private string LoadLastBillNo()
    {
        string search =  DateTime.Now.ToString("yyyyMMdd");
        PaymentInDecreaseAdapter ida = new PaymentInDecreaseAdapter();
        return ida.getLastBillNo(search);
    }
    
    /// <summary>
    /// 绑定repeater的数据源
    /// </summary>
    private void BindData()
    {
        DataTable dt = DefineDataTableSchema(hfRptColumns.Value);
        LoadEmptyData(dt);

        rptTest.DataSource = dt;
        rptTest.DataBind();
    }
    /// <summary>
    /// 根据repeater相对应的列名，定义数据源datatable的schema
    /// </summary>
    /// <param name="columns">列名</param>
    /// <returns></returns>
    public DataTable DefineDataTableSchema(string columns)
    {
        DataTable dt = new DataTable();
        string[] columnsAry = columns.Split(',');
        foreach (string str in columnsAry)
        {
            dt.Columns.Add(str);
        }
        return dt;
    }
    /// <summary>
    /// 生成空数据
    /// </summary>
    /// <param name="dt"></param>
    private void LoadEmptyData(DataTable dt)
    {
        //默认显示一行空数据
        DataRow row = dt.NewRow();
        dt.Rows.Add(row);
    }
    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        //首先，恢复数据源
        DataTable dt = DefineDataTableSchema(hfRptColumns.Value);
        foreach (RepeaterItem item in rptTest.Items)
        {
            DataRow newRow = dt.NewRow();
            newRow["bill_no"] = bill_no.Text;
            newRow["g_no"] = ((TextBox)item.FindControl("txt_g_no")).Text;
            newRow["name"] = ((TextBox)item.FindControl("txt_name")).Text;
            newRow["amount"] = ((TextBox)item.FindControl("txt_amount")).Text;
            newRow["type"] = ((DropDownList)item.FindControl("ddl_type")).SelectedValue;
            newRow["apply_date"] = ((TextBox)item.FindControl("cb_apply_date")).Text;
            newRow["note"] = ((TextBox)item.FindControl("txt_note")).Text;
            dt.Rows.Add(newRow);
        }

        //添加一行
        DataRow row = dt.NewRow();
        dt.Rows.Add(row);
        rptTest.DataSource = dt;
        rptTest.DataBind();
    }
    protected void add_Click(object sender, EventArgs e)
    {
        //表头
        T_PaymentIndecreaseHead head = new T_PaymentIndecreaseHead();
        head.AgentDate = DateTime.Parse(agent_date.Text);
        head.BillNo = bill_no.Text;
        head.Customer = customer.Text;
        head.Agenter = agent_name.Text;
        head.CheckStatus = 0;
        head.CurrencyID = ddl_currency.SelectedValue;
        decimal amount_all = 0;
        PaymentAdapter raa = new PaymentAdapter();

        DataSet ds = raa.getSupplierInfoByName(head.Customer);
        if (ds.Tables[0].Rows.Count > 0)
        {
            head.CustomerID = Int32.Parse(ds.Tables[0].Rows[0]["FNumber"].ToString());
            head.ItemID = Int32.Parse(ds.Tables[0].Rows[0]["FItemID"].ToString());
        }
        else
        {
            Label1.Text = "供应商姓名请输入关键字后从下拉框选择！";
            return;
        }
        //表体
        DataTable dt = DefineDataTableSchema(hfRptColumns.Value);
        foreach (RepeaterItem item in rptTest.Items)
        {
            DataRow newRow = dt.NewRow();

            string name = ((TextBox)item.FindControl("txt_name")).Text;
            if (string.IsNullOrEmpty(name))
            {
                //如果项号是空，则过滤此条记录
                continue;
            }
            string amount = ((TextBox)item.FindControl("txt_amount")).Text;
            if (string.IsNullOrEmpty(amount))
            {
                //如果项号是空，则过滤此条记录
                continue;
            }
            string apply_date = ((TextBox)item.FindControl("cb_apply_date")).Text;
            if (string.IsNullOrEmpty(apply_date))
            {
                //如果项号是空，则过滤此条记录
                continue;
            }
            string type = ((DropDownList)item.FindControl("ddl_type")).SelectedValue;
            if (string.IsNullOrEmpty(type))
            {
                //如果项号是空，则过滤此条记录
                continue;
            }
            newRow["g_no"] = ((TextBox)item.FindControl("txt_g_no")).Text;
            newRow["name"] = name;
            newRow["amount"] = amount;
            newRow["apply_date"] = apply_date;
            newRow["type"] = type;
            if (type == "R")
            {
                amount_all += Decimal.Parse(amount) * -1;
            }
            else
            {
                amount_all += Decimal.Parse(amount);
            }
            newRow["note"] = ((TextBox)item.FindControl("txt_note")).Text; ;
            newRow["bill_no"] = bill_no.Text;
            dt.Rows.Add(newRow);
        }

        head.AmountAll = amount_all;
        try
        {
            PaymentInDecreaseAdapter ida = new PaymentInDecreaseAdapter();
            ida.insertInDecreaseList(dt);
            ida.addInDecreaseHead(head);
            clean();
            Label1.Text = "哟，小伙子，不错，被你录入成功了";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void rptTest_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddlType = (DropDownList)e.Item.FindControl("ddl_type");
            ddlType.Items.Add(new ListItem("扣    款", "R"));
            ddlType.Items.Add(new ListItem("补    款", "B"));
            DataRowView rowv = (DataRowView)e.Item.DataItem;
            ddlType.SelectedValue = rowv[3].ToString();
        }
    }
    private void clean()
    {
        customer.Text = "";
        bill_no.Text = LoadLastBillNo();
        agent_date.Text = "";
        agent_name.Text = "";
        BindData();
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
}