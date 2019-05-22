using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// GeneralUserControl 的摘要说明
/// </summary>
public class GeneralUserControl:UserControl
{
    public GeneralUserControl()
    {
        AddScriptInclude("CommonFunction", "../../Scripts/js/CommonFunction.js");
        AddScriptInclude("prototype", "../../Scripts/js/prototype-1.4.0.js");
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!Page.IsCallback)
        {
            RegisterScriptInclude();
            RegisterScriptBlock();
        }
    }

    /// <summary>
    /// 脚本包含集合
    /// </summary>
    List<KeyValuePair<string, string>> ScriptIncludes = new List<KeyValuePair<string, string>>();

    List<KeyValuePair<string, string>> ScriptBlocks = new List<KeyValuePair<string, string>>();

    /// <summary>
    /// 添加脚本包含
    /// </summary>
    /// <param name="key"></param>
    /// <param name="filePath"></param>
    public void AddScriptInclude(string key, string filePath)
    {
        if (!ScriptIncludes.Contains(new KeyValuePair<string, string>(key, filePath))) 
            ScriptIncludes.Add(new KeyValuePair<string, string>(key, filePath));
    }


    /// <summary>
    /// 注册客户端脚本文件包含
    /// </summary>
    void RegisterScriptInclude()
    {
        foreach (KeyValuePair<string,string> pair in ScriptIncludes)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(typeof(GeneralUserControl), pair.Key))
            {
                Page.ClientScript.RegisterClientScriptInclude(typeof(GeneralUserControl), pair.Key, pair.Value);
            }
        }
    }

    /// <summary>
    /// 添加脚本块
    /// </summary>
    /// <param name="key"></param>
    /// <param name="filePath"></param>
    public void AddScriptBlock(string key, string script)
    {
        ScriptBlocks.Add(new KeyValuePair<string, string>(key, script));
    }

    /// <summary>
    /// 注册客户端脚本块
    /// </summary>
    void RegisterScriptBlock()
    {
        foreach (KeyValuePair<string, string> pair in ScriptBlocks)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), pair.Key))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), pair.Key, pair.Value,true);
            }
        }
    }
}
