using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_payment_typeManager : System.Web.UI.Page
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
        PaymentTypeAdapter da = new PaymentTypeAdapter();
        DataSet ds = da.getTypes();
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
        PaymentTypeAdapter da = new PaymentTypeAdapter();
        T_PaymentType item = new T_PaymentType();
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
                da.addType(item);
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
                da.updateType(item);
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
        GridViewRow row = btn_del.Parent.Parent as GridViewRow;
        int id = Int32.Parse((row.Cells[0].FindControl("hdfId") as HiddenField).Value);
        try
        {
            PaymentTypeAdapter da = new PaymentTypeAdapter();
            da.DeleteTypeById(id);
            Label1.Text = "保存成功";
            init();
            GridViewBand();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string code = e.Row.Cells[1].Text;
            if (code == "A" || code == "B")
            {
                Button btnEdit = e.Row.Cells[0].FindControl("btnEdit") as Button;
                btnEdit.Enabled = false;
                Button btnDelete = e.Row.Cells[0].FindControl("benDelete") as Button;
                btnDelete.Enabled = false;
            }
        }
    }
}