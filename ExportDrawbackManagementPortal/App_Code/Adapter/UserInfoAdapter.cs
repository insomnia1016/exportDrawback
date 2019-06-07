using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Library;
using ExportDrawbackManagement.Framework.Common;
 

/// <summary>
///UserInfoAdapter 的摘要说明
/// </summary>
public class UserInfoAdapter
{
	public UserInfoAdapter()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

	public static UserInfo CurrentUser
	{
		get
		{
			try
			{
				if (HttpContext.Current.Session["CurrentUser"] == null)
                    if (!Common.LoginCheck())
                    {
                        throw new ZHNException("未登录访问出错，将跳转");
                    }
				return (UserInfo)((ExportDrawbackManagementPrincipal)HttpContext.Current.Session["CurrentUser"]).ExportDrawbackManagementIdentity.CurrUserAuth;
			}
			catch (Exception ex)
			{
				throw new ZHNException(ex.Message);
			}
		}
		set
		{
			ExportDrawbackManagementIdentity identity = new ExportDrawbackManagementIdentity(value);
			HttpContext.Current.Session["CurrentUser"] = new ExportDrawbackManagementPrincipal(identity, value.Roles.Split(',').ToArray());
		}
	}
}