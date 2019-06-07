using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Framework.Common;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        warning.Text = "";
    }

    protected void img_clicked(object sender, EventArgs e)
    {
        string username = this.username.Text.Trim();
        string password = this.password.Text.Trim();
        warning.Text = "";
        DataSet ds;
        UsersAdapter ua = new UsersAdapter();
        try
        {
            bool success = ua.login(username, password,out ds);
            if (success)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                int PersonId = Int32.Parse(dr["person_id"].ToString());
                HttpContext.Current.Session["PersonId"] = PersonId;

                if (Common.LoginCheck())
                {
                    Response.Write("<script language='javascript'>window.location='UI/security/default.aspx'</script>");
                    //Response.Redirect("UI/security/default.aspx");
                }

                
            }
            else
            {
                warning.Text = "用户名或密码错误，请重新尝试";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

       
    }
    protected void img2_clicked(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("register.aspx");
    }
}