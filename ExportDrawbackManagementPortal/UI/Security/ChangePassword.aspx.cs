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
using System.Threading;

public partial class UI_Security_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSetPin_Click(object sender, EventArgs e)
    {
        string oldPswd = txtOldPin.Text.Trim();
        string newPswd = txtNewPin.Text.Trim();
        Int32 persion_id = Int32.Parse(HttpContext.Current.Session["PersonId"].ToString());
        string msg;
        UsersAdapter ua = new UsersAdapter();
        ua.changePassword(persion_id, oldPswd, newPswd, out msg);

        msgLbl.Text = msg;

    }
}
