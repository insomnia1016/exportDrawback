using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using ExportDrawbackManagement.Framework.Common;
using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Library;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 一些公用的方法
/// </summary>
public class Common
{
    public Common()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
          
    public static bool LoginCheck()
    {
        try
        {
            UserInfo user = new UserInfo();

            
           
            ExportDrawbackManagementIdentity identity = new ExportDrawbackManagementIdentity(user);
                        
            ExportDrawbackManagementPrincipal edPrincipal = new ExportDrawbackManagementPrincipal(identity, user.UserRight.ToArray());

            HttpContext.Current.Session["CurrentUser"] = edPrincipal;
            return true;

        }
        catch
        {
            throw new ZHNException("用户无法认证，登录失败！");
        }
    }
}
