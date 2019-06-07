using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ExportDrawbackManagement.Framework.Common;
using ExportDrawbackManagement.Biz.Entity;



public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        if (authCookie != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
        {
            if (Common.LoginCheck())
                Response.Redirect("UI/security/default.aspx");
            else
            {
                Response.Redirect("login.aspx");
            }
        }
        else
        {
            Logon();
        }
    }
    private void Logon()
    {

        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, HttpContext.Current.User.Identity.Name, DateTime.Now, DateTime.Now.AddDays(1), false, "Test");
        string str = FormsAuthentication.Encrypt(ticket);
        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
        authCookie.Value = str;
        Response.SetCookie(authCookie);//产生cookie.
        
        //HttpContext.Current.Session["Test"] = "You can find me.";//产生Session.
        Response.Redirect(FormsAuthentication.DefaultUrl);
    }

}
