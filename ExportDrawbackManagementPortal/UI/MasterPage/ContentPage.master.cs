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

public partial class UI_MasterPage_ContentPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.Text = Page.Title;

    }

    //bool isHasOperateLog = false;

    //public bool IsHasOperateLog
    //{
    //    get { return isHasOperateLog; }
    //    set { isHasOperateLog = value; }
    //}

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["CurrentUser"] == null)
                if (!Common.LoginCheck())
                {
                    throw new Exception("未登录访问出错，将跳转");
                }

            base.OnPreRender(e);
        }
        catch (Exception ex)
        {
            if (ex.Message == "未登录访问出错，将跳转")
            {
                Response.Redirect("../../login.aspx");
            }

        }
    }
}
