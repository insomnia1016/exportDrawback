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

public partial class UI_CommonControls_DatePicker : GeneralUserControl
{
    public UI_CommonControls_DatePicker()
    {
        //AddScriptInclude("DatePicker", "../../Scripts/js/DatePicker.js");
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
}
