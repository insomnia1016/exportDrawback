using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_QueryAndReports_department : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBand();
        }
    }

    private void GridViewBand()
    {
        DepartmentAdapter da = new DepartmentAdapter();
        DataSet ds = da.getDepartments();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int index = GridView1.SelectedIndex;
        GridViewRow row = GridView1.SelectedRow;
        HiddenField1.Value = (row.Cells[0].FindControl("hdfId") as HiddenField).Value;
        this.txt_code.Text = row.Cells[1].Text;
        this.txt_name.Text = row.Cells[2].Text;
    }
   

    private void init()
    {
        this.txt_code.Text = "";
        this.txt_name.Text = "";
        HiddenField1.Value = "";
        GridView1.SelectedIndex = -1;
    }
    protected void updateUser_Click(object sender, EventArgs e)
    {
        DepartmentAdapter da = new DepartmentAdapter();
        T_Department item = new T_Department();
        item.Code = this.txt_code.Text.Trim();
        item.Name = this.txt_name.Text.Trim();
        if (string.IsNullOrEmpty(item.Code))
        {
            Label1.Text = "部门代码不能为空";
            return;
        }
        if (string.IsNullOrEmpty(item.Name))
        {
            Label1.Text = "部门名称不能为空";
            return;
        }
        if (string.IsNullOrEmpty(HiddenField1.Value))
        {
            //新增
            try
            {
                da.addDepartment(item);
                Label1.Text = "新增成功";
                init();
                GridViewBand();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
        else
        {
            //更新
            item.Id = Int32.Parse(HiddenField1.Value);
            try
            {
                da.updateDepartment(item);
                Label1.Text = "保存成功";
                init();
                GridViewBand();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

    }
    protected void benDelete_Click(object sender, EventArgs e)
    {
        Button btn_del = sender as Button;
        GridViewRow row = btn_del.Parent as GridViewRow;
        int id = Int32.Parse((row.Cells[0].FindControl("hdfId") as HiddenField).Value);
        try
        {
            DepartmentAdapter da = new DepartmentAdapter();
            da.DeleteDepartmentById(id);
            Label1.Text = "保存成功";
            init();
            GridViewBand();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
}