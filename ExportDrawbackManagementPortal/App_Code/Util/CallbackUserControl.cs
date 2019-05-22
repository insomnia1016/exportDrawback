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
/// CallbackUserControl 的摘要说明
/// </summary>
public abstract class CallbackUserControl : GeneralUserControl,ICallbackContainer, ICallbackEventHandler
{
    public CallbackUserControl()
    {

    }

    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsCallback)
        {
            string script = Page.ClientScript.GetCallbackEventReference(this, "arg", ReceiveServerDataFunctionName, "context", ClientErrorCallback, UseAsync);
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            strb.Append("function ");
            strb.Append(CallServerFunctionName);
            strb.AppendLine("(arg,context)");
            strb.AppendLine("{");
            strb.AppendLine(script);
            strb.AppendLine("}");

            strb.Append("function ");
            strb.Append(ReceiveServerDataFunctionName);
            strb.AppendLine("(arg,context)");
            strb.AppendLine("{");
            strb.AppendLine(GetReceiveServerDataScript());
            strb.AppendLine("}");
            string callbackScript = strb.ToString();
            AddScriptBlock(KeyName + ".CallbackFunctions", callbackScript);
        }
        base.OnLoad(e);
    }

    bool _useAsync = true;
    /// <summary>
    /// 是否使用异步执行回调
    /// </summary>
    public bool UseAsync
    { get { return _useAsync; } set { _useAsync = value; } }

    string _clientErrorCallback = null;
    /// <summary>
    /// 回调出错处理函数
    /// arg, context
    /// </summary>
    public string ClientErrorCallback
    {
        get { return string.IsNullOrEmpty(_clientErrorCallback) ? null : _clientErrorCallback; }
        set { _clientErrorCallback = value; }
    }


    string callbacArgument;

    public virtual string KeyName
    {
        get
        {
            return this.ClientID;
        }
    }

    protected override void CreateChildControls()
    {

        base.CreateChildControls();
    }

    /// <summary>
    /// 回调服务器函数名称
    /// arg,context
    /// </summary>
    public virtual string CallServerFunctionName
    { get { return KeyName + "_CallServer"; } }

    /// <summary>
    /// 接受到服务器回调回应的函数名称
    /// arg,context
    /// </summary>
    public virtual string ReceiveServerDataFunctionName
    { get { return KeyName + "_ReceiveServerData"; } }

    /// <summary>
    /// 返回客户端接受到回调返回的参数时处理的脚本
    /// 参数arg , context
    /// </summary>
    /// <returns></returns>
    protected abstract string GetReceiveServerDataScript();

    /// <summary>
    /// 根据客户端回调的参数返回需要的字符串
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    protected abstract string GetCallbackResult(string arg);

    #region ICallbackEventHandler 成员
    /// <summary>
    /// 返回回调结果
    /// </summary>
    /// <returns></returns>
    public string GetCallbackResult()
    {       
        return GetCallbackResult(callbacArgument);
    }

    /// <summary>
    /// 产生回调事件
    /// </summary>
    /// <param name="eventArgument"></param>
    public void RaiseCallbackEvent(string eventArgument)
    {
        callbacArgument = eventArgument;
    }

    #endregion

    #region ICallbackContainer 成员

    public string GetCallbackScript(IButtonControl buttonControl, string argument)
    {
        return "test";
    }

    #endregion
}
