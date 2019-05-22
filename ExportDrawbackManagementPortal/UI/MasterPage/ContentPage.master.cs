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

    //protected override void OnPreRender(EventArgs e)
    //{
    //    string signNo = Request["SignNo"];
    //    if (!IsHasOperateLog || string.IsNullOrEmpty(signNo))
    //    {
    //        hlOperateLog.Visible = false;
    //    }
    //    else
    //    {
    //        hlOperateLog.NavigateUrl = string.Format("javascript:ShowOperateLog('{0}');", signNo);
    //    }
    //    base.OnPreRender(e);
    //}
}
