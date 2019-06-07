using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_company : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init();
            show();
        }

    }

    private void show()
    {
        CompanyAdapter ca = new CompanyAdapter();
        GridView1.DataSource = ca.getCompanyInfo();
        GridView1.DataBind();
    }

    protected void benDelete_Click(object sender, EventArgs e)
    {
        CompanyAdapter ca = new CompanyAdapter();
        int id = Int32.Parse(((HiddenField)((Button)sender).Parent.FindControl("hdfId")).Value);
        try
        {
            ca.delete(id);

            Label1.Text = "删除成功";
            show();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void updateUser_Click(object sender, EventArgs e)
    {
        CompanyAdapter ca = new CompanyAdapter();
        T_Customers company = new T_Customers();
        company.CompanyName = txtCompany.Text.Trim();
        if (string.IsNullOrEmpty(company.CompanyName))
        {
            Label1.Text = "公司名称不能为空";
            return;
        }
        company.Address = txtAddress.Text.Trim();
        company.Tel = txtTel.Text.Trim();
        company.Jingban = txtJingban.Text.Trim();
        company.Fadingdaibiaoren = txtFadingdaibiaoren.Text.Trim();
        company.Dailiren = txtDailiren.Text.Trim();
        if (!string.IsNullOrEmpty(HiddenField1.Value))
        {
            company.Id = Int32.Parse(HiddenField1.Value);
            try
            {
                ca.Save(company);
                Label1.Text = "保存成功";
                init();
                show();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
        else
        {
            try
            {
                ca.insert(company);
                Label1.Text = "新增成功";
                init();
                show();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
    }
    private void init()
    {
        txtCompany.Text = "";
        txtAddress.Text = "";
        txtTel.Text = "";
        txtJingban.Text = "";
        txtFadingdaibiaoren.Text = "";
        txtDailiren.Text = "";
        HiddenField1.Value = "";
        GridView1.SelectedIndex = -1;
    }

    protected void clean_Click(object sender, EventArgs e)
    {
        init();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectIndex = GridView1.SelectedIndex;
        txtCompany.Text = guolu(GridView1.SelectedRow.Cells[1].Text);
        txtAddress.Text = guolu(GridView1.SelectedRow.Cells[2].Text);
        txtTel.Text = guolu(GridView1.SelectedRow.Cells[3].Text);
        txtJingban.Text = guolu(GridView1.SelectedRow.Cells[4].Text);
        txtFadingdaibiaoren.Text = guolu(GridView1.SelectedRow.Cells[5].Text);
        txtDailiren.Text = guolu(GridView1.SelectedRow.Cells[6].Text);
        HiddenField1.Value = ((HiddenField)GridView1.SelectedRow.FindControl("hdfId")).Value;
        Label1.Text = "";
    }

    private string guolu(string str)
    {
        str = str.Trim();
        if (string.IsNullOrEmpty(str) || str == "&nbsp;")
        {
            return "";
        }
        return str;
    }
}