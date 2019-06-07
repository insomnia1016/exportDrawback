using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
        }
    }
    protected void img_clicked(object sender, ImageClickEventArgs e)
    {
        T_Users entity = new T_Users();
        entity.Username = username.Text.Trim();
        entity.Password = password1.Text.Trim();
        if (entity.Password.Length < 6)
        {
            errorTxt.Text = "密码长度不能少于6位";
            return;
        }
        entity.Name = name.Text.Trim();
        entity.Derpartment = derparment.Text.Trim() ?? "";

        UsersAdapter ua = new UsersAdapter();
        try
        {
           string person_id = ua.register(entity);
            //自动登录
           HttpContext.Current.Session["PersonId"] = person_id;
           Common.LoginCheck();

            Response.Redirect("UI/security/default.aspx");
        }
        catch(Exception ex)
        {
            errorTxt.Text = ex.Message;
        }
    }
}