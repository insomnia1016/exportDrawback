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

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
  
    }

    protected override void OnPreRender(EventArgs e)
    {
        if (UserInfoAdapter.CurrentUser!= null)
        {
            pnlOpInfo.Visible = true;
            UserInfo user = UserInfoAdapter.CurrentUser;
            lblOpInfo.Text = string.Format("<b>操作员:</b>{0}　　<b>工号:</b>{1}　　", 
                user.displayName, user.person_id);
        }
        else
        {
            pnlOpInfo.Visible = false;
            lblOpInfo.Text = "";
        }
        base.OnPreRender(e);
    }
}
