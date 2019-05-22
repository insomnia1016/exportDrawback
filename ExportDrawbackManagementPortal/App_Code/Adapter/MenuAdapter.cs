using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ExportDrawbackManagement.Framework.Common;
using System.Xml;

/// <summary>
/// MenuAdapter 的摘要说明
/// </summary>
[System.ComponentModel.DataObject] 
public class MenuAdapter
{
    public MenuAdapter()
    {

    }

    string GetAttributeValue(XmlNode node, string attributeName)
    {
        XmlAttribute attr = node.Attributes[attributeName];
        if (attr == null)
            return null;
        else
            return attr.Value;
    }

    public DataSet GetMainMenus()
    {
        string[] colNames = new string[] {"Name","Url","Title","DisplayName" };
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds.Tables.Add(dt);
        foreach (string col in colNames)
        {
            dt.Columns.Add(col);
        }

        if (ExportDrawbackManagementContext.Current.User != null)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("~/Functions.xml"));
            XmlNodeList nodes = doc.SelectNodes("/Functions/Function[@Type='Menu']");

            foreach (XmlNode node in nodes)
            {
                string name = GetAttributeValue(node, "Name");
                if (!((ExportDrawbackManagementPrincipal)HttpContext.Current.Session["CurrentUser"]).IsInRole(name.ToUpper()))
                {
                    continue;
                }
                DataRow newRow = dt.NewRow();
                foreach (string col in colNames)
                {
                    newRow[col] =  GetAttributeValue(node,col);
                }                
                dt.Rows.Add(newRow);
            }
        }
        return ds;
    }
}
