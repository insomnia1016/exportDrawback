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
using System.Web.Configuration;

public partial class WebConfigEncrypt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        ConfigurationSection section = config.GetSection("connectionStrings");
        section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider"); ;
        config.Save();
    }
    protected void btnUnencrypt_Click(object sender, EventArgs e)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        ConfigurationSection section = config.GetSection("connectionStrings");
        section.SectionInformation.UnprotectSection();
        config.Save();
    }
}
