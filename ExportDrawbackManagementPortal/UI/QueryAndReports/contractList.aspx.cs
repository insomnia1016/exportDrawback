using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_QueryAndReports_contractList : System.Web.UI.Page
{
    CommonAdapter ca = new CommonAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ContractAdapter ca = new ContractAdapter();
            show(ca.getContractSummary());
            
        }
    }
    /// <summary>
    /// 绑定GridView1
    /// </summary>
    private void show(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void query_Click(object sender, EventArgs e)
    {
        T_ContractHead head = new T_ContractHead();
        if (!string.IsNullOrEmpty(txt_contract_id.Text))
        {
            head.ContractId = txt_contract_id.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_entry_id.Text.Trim()))
        {
            head.baoguandanhao = txt_entry_id.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_xufang.Text.Trim()))
        {
            head.Xufang = txt_xufang.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_xufang_jingbanren.Text.Trim()))
        {
            head.XufangJingbanren = txt_xufang_jingbanren.Text.Trim();
        }
        if (!string.IsNullOrEmpty(CalendarBox1.Text.Trim()))
        {
            head.startTime = DateTime.Parse(CalendarBox1.Text.Trim());
        }
        if (!string.IsNullOrEmpty(CalendarBox2.Text.Trim()))
        {
            head.endTime = DateTime.Parse(CalendarBox2.Text.Trim());
        }

        ContractAdapter contractA = new ContractAdapter();
        show(contractA.queryContractSummary(head));


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string code = e.Row.Cells[4].Text;
            e.Row.Cells[4].Text = ca.getDeliveryModeNameByCode(code);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ContractAdapter ca = new ContractAdapter();
        show(ca.getContractSummary());
    }
}