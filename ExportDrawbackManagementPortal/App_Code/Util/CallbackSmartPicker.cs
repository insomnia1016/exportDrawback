using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Xml;
using System.Collections.Generic;

/// <summary>
/// CallbackSmartPicker 的摘要说明
/// </summary>
public abstract class CallbackSmartPicker : CallbackUserControl
{
    public CallbackSmartPicker()
    {
        AddScriptInclude("Picker", "../../Scripts/js/Picker.js");
        AddScriptInclude("SmartPicker", "../../Scripts/js/SmartPicker.js");
        ItemAttributes = new List<string>();
        //ItemAttributes.Add("ItemCode");
        //ItemAttributes.Add("ItemName");
    }

    Unit _width = new Unit("250px");
    public Unit Width
    { get { return _width; } set { _width = value; } }

    protected List<string> ItemAttributes;

    string GetAttrArrayString()
    {
        StringBuilder strb = new StringBuilder();
        strb.Append("new Array(");
        for (int i = 0; i < ItemAttributes.Count; i++)
        {
            strb.AppendFormat("'{0}'", ItemAttributes[i]);
            if (i < ItemAttributes.Count - 1)
                strb.Append(",");
        }
        strb.Append(")");
        return strb.ToString();
    }

    int _codeLength;

    public int CodeLength
    {
        get { return _codeLength; }
        set { _codeLength = value; }
    }

    void RegisterClientScript()
    {
        if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), KeyName))
        {
            string startupScript = string.Format(@"
var {0} = new SmartPicker('{0}',{1},{2},{3});
g_CurrentPickers.push({0});
{0}.LoadData();
", KeyName, CallServerFunctionName,CodeLength, GetAttrArrayString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), KeyName, startupScript, true);
        }
    }

    protected override void CreateChildControls()
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.ID = string.Format("Panel");
        div.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        div.Style.Add(HtmlTextWriterStyle.Width, Width.ToString());
        div.Style.Add(HtmlTextWriterStyle.Position, "absolute");
        div.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
        div.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
        div.Style.Add(HtmlTextWriterStyle.Padding, "1px 1px 1px 1px");
        div.Style.Add(HtmlTextWriterStyle.Display, "none");
        div.Style.Add(HtmlTextWriterStyle.BackgroundColor,"white");

        HtmlGenericControl divHeader = new HtmlGenericControl("div");
        divHeader.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
        divHeader.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
        divHeader.Style.Add(HtmlTextWriterStyle.BackgroundColor,"#d3d3d3");
        divHeader.InnerHtml = GetHeaderPanleInnerHtml();
        div.Controls.Add(divHeader);

        HtmlGenericControl divData = new HtmlGenericControl("div");
        divData.ID = "Data";
        divData.Style.Add(HtmlTextWriterStyle.Overflow, "auto");
        div.Controls.Add(divData);
        divData.InnerHtml = "<div style='vertical-align: middle; text-align: center;'>正在加载数据．．．</div>";
        
        base.Controls.Add(div);
        base.CreateChildControls();
        if (!Page.IsCallback)
        {
            RegisterClientScript();
        }
    }

    protected virtual string GetHeaderPanleInnerHtml()
    {
        return @"
    <span style='width:40px;'>代码|</span>
    <span style='margin-left:5px;'>名称</span>
";
    }

    protected override string GetReceiveServerDataScript()
    {
        return KeyName + ".ReceiveServerData(arg,context)";
    }

    protected override string GetCallbackResult(string arg)
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root = doc.CreateElement("Root");
        doc.AppendChild(root);
        XmlElement items = doc.CreateElement("Items");
        root.AppendChild(items);
        FillXmlData(items);
        return doc.OuterXml;
    }

    protected abstract void FillXmlData(XmlNode items);
    //{
    //    XmlDocument doc = items.OwnerDocument;
    //    for (int i = 0; i < 100; i++)
    //    {
    //        XmlElement item = doc.CreateElement("Item");
    //        items.AppendChild(item);
    //        item.SetAttribute("Code", i.ToString());
    //        item.SetAttribute("Name", i.ToString() + "Name");
    //    }
    //}

}
