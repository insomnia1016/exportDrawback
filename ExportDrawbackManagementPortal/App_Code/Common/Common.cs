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
            if (HttpContext.Current.Session["PersonId"] == null)
            {
                return false;
            }
            Int32 personId =Int32.Parse(HttpContext.Current.Session["PersonId"].ToString());

            CommonAdapter ca = new CommonAdapter();

            DataSet ds = ca.getUserInfoById(personId);
            DataRow dr = ds.Tables[0].Rows[0];
            UserInfo user = new UserInfo();
            
            user.Derpartment = dr["derpartment"].ToString().Trim();
            user.Name = dr["name"].ToString().Trim();
            user.PersonId = dr["person_id"].ToString().Trim();
            user.Roles = dr["roles"].ToString().Trim();
            user.Rank = dr["rank"].ToString().Trim();
            user.Username = dr["username"].ToString().Trim();
            
           
            ExportDrawbackManagementIdentity identity = new ExportDrawbackManagementIdentity(user);

            ExportDrawbackManagementPrincipal edPrincipal = new ExportDrawbackManagementPrincipal(identity, user.Roles.Split(',').ToArray());

            HttpContext.Current.Session["CurrentUser"] = edPrincipal;
            return true;

        }
        catch
        {
            throw new ZHNException("未登录访问出错，将跳转");
        }
    }
}
