using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// CallbackPicker 的摘要说明
/// </summary>
public abstract class CallbackListPicker:CallbackUserControl
{
    public CallbackListPicker()
    {
        AddScriptInclude("Picker", "../../Scripts/js/Picker.js");
        AddScriptInclude("ListPicker", "../../Scripts/js/ListPicker.js");
    }

    protected override void OnLoad(EventArgs e)
    {

        base.OnLoad(e);        
    }

    bool _isForceCheck = true;
    public bool IsForceCheck
    { get { return _isForceCheck; } set { _isForceCheck = value; } }

    Unit _width = new Unit("250px");
    public Unit Width
    { get { return _width; } set { _width = value; } }

    void RegisterClientScript()
    {
            if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), KeyName))
            {
                string startupScript = string.Format(@"
var {0} = new ListPicker('{0}',{0}_Panel,null,{1},{2});
g_CurrentPickers.push({0});
{0}.LoadData();
", KeyName, CallServerFunctionName, IsForceCheck.ToString().ToLower());
                Page.ClientScript.RegisterStartupScript(this.GetType(), KeyName, startupScript,true);
            }
    }

    protected override void CreateChildControls()
    {
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.ID = string.Format("Panel");
        div.Style.Add(HtmlTextWriterStyle.TextAlign,"left");
        div.Style.Add(HtmlTextWriterStyle.Width,Width.ToString());
        div.Style.Add(HtmlTextWriterStyle.Position,"absolute");
        div.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
        div.Style.Add(HtmlTextWriterStyle.BorderWidth,"1px");
        div.Style.Add(HtmlTextWriterStyle.Padding,"1px 1px 1px 1px");
        div.Style.Add(HtmlTextWriterStyle.Display,"none");
        div.InnerHtml = "<div style='vertical-align: middle; text-align: center;'>正在加载数据．．．</div>";
        base.Controls.Add(div);
        base.CreateChildControls();
        if (!Page.IsCallback)
        {            
            RegisterClientScript();            
        }        
    }

    public virtual string PickerPanelName
    { get { return KeyName + "_Panel"; } }

    protected override string GetReceiveServerDataScript()
    {
        return KeyName + ".ReceiveServerData(arg,context)";
    }
    
}
