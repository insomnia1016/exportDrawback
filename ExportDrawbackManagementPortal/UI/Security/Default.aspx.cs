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
using System.Security.Principal;

public partial class UI_Security_Default : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~/App_Data/Menus.xml"));
        menuFuns.Items.Clear();
        foreach (XmlNode mainMenuNode in doc.SelectNodes(string.Format("/Root/MenuItems/MenuItem")))
        {
            AddToMenu(mainMenuNode, menuFuns.Items, 1);
        }
    }

    string GetAttrValue(XmlNode node, string attrName)
    {
        XmlAttribute attr = node.Attributes[attrName];
        if (attr == null)
            return "";
        return attr.Value;
    }
   
    void AddToMenu(XmlNode node, MenuItemCollection menuItems, int level)
    {
        string funcstr = GetAttrValue(node, "Roles");
        if (!string.IsNullOrEmpty(funcstr))
        {
            string[] funcs = funcstr.Split(',');//菜单栏的权限，用逗号分隔
            if (!IsInRoles(funcs))
            {
                return;
            }
        }
        MenuItem menuItem = new MenuItem();
        menuItem.Text = GetAttrValue(node, "DisplayName");
        menuItem.NavigateUrl = GetAttrValue(node, "Url");
        menuItem.ImageUrl = GetAttrValue(node, "ImageUrl");
        menuItem.Target = GetAttrValue(node, "Target");
        if (string.IsNullOrEmpty(menuItem.Target))
            menuItem.Target = "contentFrame";
        menuItem.ToolTip = GetAttrValue(node, "Title");

        string visibleStr = GetAttrValue(node, "Visible");
        bool visible = true;
        if (!string.IsNullOrEmpty(visibleStr))
            bool.TryParse(visibleStr, out visible);
        if (!visible)
            return;

        //加载菜单图片
        if (!string.IsNullOrEmpty(menuItem.ImageUrl))
        {
            if (level == 0)
            {
                menuItem.ImageUrl = "";
                menuItem.Text = "------" + menuItem.Text.ToString();
            }
        }else
            menuItem.Text = "------" + menuItem.Text.ToString();


        menuItems.Add(menuItem);

        if (menuItem.NavigateUrl == "#" || string.IsNullOrEmpty(menuItem.NavigateUrl))
        {
            menuItem.NavigateUrl = "";
            menuItem.Selectable = false;
        }
        else
        {
            if (menuItem.Target != "_blank")
            {
                if (menuItem.NavigateUrl.IndexOf('?') > 0)//有带querystring的url
                    menuItem.NavigateUrl += string.Format("&ValuePath={0}&MenuTitle={1}", Server.UrlEncode(menuItem.ValuePath), Server.UrlEncode(menuItem.ToolTip));
                else
                    menuItem.NavigateUrl += string.Format("?ValuePath={0}&MenuTitle={1}", Server.UrlEncode(menuItem.ValuePath), Server.UrlEncode(menuItem.ToolTip));
            }
        }

        foreach (XmlNode mainMenuNode in node.SelectNodes(string.Format("./MenuItems/MenuItem")))
        {
            AddToMenu(mainMenuNode, menuItem.ChildItems, ++level);
        }
    }
    /// <summary>
    /// 是否包含所有角色
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    private bool IsInRoles(params string[] roles)
    {
        //原先的代码
        //foreach (string role in roles)
        //{
        //    if (!IsInRole(role))
        //        return false;
        //}
        //return true;
        //只要有一个角色满足就可以
        foreach (string role in roles)
        {
            if (IsInRole(role))
                return true;
        }
        return false;
    }
    /// <summary>
    /// 是否包含角色
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    private bool IsInRole(string role)
    {

        //string tt = UserAuthAdapter.CurrentUser.Roles;
        try
        {
            string[] _Roles = UserInfoAdapter.CurrentUser.Roles.Split(',');
            if (_Roles != null && !string.IsNullOrEmpty(role))
            {
                foreach (string r in _Roles)
                {
                    if (r.Equals(role, StringComparison.OrdinalIgnoreCase) || r.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            
        }
        catch (Exception ex)
        {
            if (ex.Message == "未登录访问出错，将跳转")
            {
                Response.Redirect("../../login.aspx");
            }

        }

        return false;
    }

    /// <summary>
    /// 是否包含任一角色
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    private bool IsInAnyRoles(params string[] roles)
    {
        foreach (string role in roles)
        {
            if (IsInRole(role))
                return true;
        }
        return false;
    }
    protected void menuFuns_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
}


