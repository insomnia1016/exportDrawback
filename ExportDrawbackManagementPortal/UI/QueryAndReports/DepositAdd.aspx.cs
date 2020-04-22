using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_QueryAndReports_DepositAdd : System.Web.UI.Page
{
    public DataSet ds { get; set; }
    decimal amountAll = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            init();
        }
        else
        {
            UserInfo user = UserInfoAdapter.CurrentUser;

            if (Session[user.Name + "SEOrder"] != null)
            {
                ds = (DataSet)Session[user.Name + "SEOrder"];
                Session[user.Name + "SEOrder"] = null;
                show(ds);

            }
        }
        txt_deposit_id.Text = LoadLastDepositId();
        amountAll = Decimal.Parse(GridView1.FooterRow.Cells[3].Text);
        agent_date.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        CommonAdapter ca = new CommonAdapter();
        int selectedValue = ca.getIDByName(UserInfoAdapter.CurrentUser.Name);
        ddl_agenter.SelectedValue  = selectedValue.ToString();
    }

    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    private void init()
    {
        ddlCurrencyBind();
        ddlAgenterBind();
        GridViewEmptyBind();
    }
    private string LoadLastDepositId()
    {
        string search = "DJD" + DateTime.Now.ToString("yyyyMMdd");
        DepositAdapter da = new DepositAdapter();
        return da.getLastDepositID(search);
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
    private void GridViewEmptyBind()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("FBillNo");
        dt.Columns.Add("FAmount", typeof(Decimal));
        dt.Columns.Add("FDate", typeof(DateTime));
        dt.Columns.Add("FNote");

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
    private string getCusIDByName(string name)
    {
        ReceiptAuditAdapter raa = new ReceiptAuditAdapter();
        DataSet ds = raa.getCustInfoByName(name);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["FNumber"].ToString();
        }
        else
        {
            return string.Empty;
        }

    }
    protected void add_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        //表头
        T_DepositHead head = new T_DepositHead();
        head.DepositId = txt_deposit_id.Text;
        head.Customer = customer.Text;
        CommonAdapter ca = new CommonAdapter();
        head.Agenter = ca.getEmpNameByID(Int32.Parse(ddl_agenter.SelectedValue));
        head.CheckStatus = 0;
        head.CurrencyID = ddl_currency.SelectedValue;
        head.AmountAll = Decimal.Parse(txt_amount_all.Text);
        head.UnreceiveAmountFor = Decimal.Parse(txt_amount_all.Text);
        string str_agent_date = agent_date.Text;
        if (!string.IsNullOrEmpty(str_agent_date))
        {
            head.AgentDate = DateTime.Parse(str_agent_date);
        }
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
        //表体
        List<T_DepositList> lists = new List<T_DepositList>();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            T_DepositList list = new T_DepositList();
            list.DepositId = head.DepositId;
            list.GNo = Int32.Parse((GridView1.Rows[i].Cells[0].FindControl("lbl_gno") as Label).Text);
            string fbillno = (GridView1.Rows[i].Cells[1].FindControl("txt_bill_no") as TextBox).Text;
            if (string.IsNullOrEmpty(fbillno))
            {
                continue;
            }
            list.FBillNo = fbillno;
            list.Fdate = DateTime.Parse((GridView1.Rows[i].Cells[2].FindControl("lbl_fdate") as Label).Text);
            list.Amount = Decimal.Parse((GridView1.Rows[i].Cells[3].FindControl("lbl_amountfor") as Label).Text);
            list.Note = (GridView1.Rows[i].Cells[4].FindControl("txt_note") as TextBox).Text;
           
            lists.Add(list);

        }
        List<T_DepositList> addtodonelists = addBillNoToDone(lists, head);

        try
        {
            //保存数据
            DepositAdapter da = new DepositAdapter();
            da.addDepositHead(head);
            da.insertDepositList(lists);

            //TODO:更新销售发票的check_status,增减单的check_status
            da.addToDone(addtodonelists);
            Label1.Text = "哟，小伙子，不错，被你录入成功了";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    private List<T_DepositList> addBillNoToDone(List<T_DepositList> lists, T_DepositHead head)
    {
        List<T_DepositList> newlists = new List<T_DepositList>();
        foreach (T_DepositList list in lists)
        {
           list.CustomerName = head.Customer;
           list.CustomerCode = head.CustomerID;
           newlists.Add(list);
        }
        return newlists;
    }
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblAmountAll = e.Row.Cells[3].FindControl("lbl_amountfor") as Label;
            if (!string.IsNullOrEmpty(lblAmountAll.Text))
            {
                amountAll += Decimal.Parse(lblAmountAll.Text);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "汇总";
            e.Row.Cells[3].Text = amountAll.ToString();//单据金额
        }
    }
}