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
using System.Xml;
using ExportDrawbackManagement.Framework.Common;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_Common_WelcomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnTest_Click(object sender, EventArgs e)
    {
       
    }
    protected override void OnPreRenderComplete(EventArgs e)
    {
        if (UserInfoAdapter.CurrentUser != null)
        {

            Label1.Text = string.Format("{0}<b>,您好!</b>　　",
                 UserInfoAdapter.CurrentUser.Name);
        }
        else
        {

            Label1.Text = "";
        }
        base.OnPreRender(e);
        
    }
}
