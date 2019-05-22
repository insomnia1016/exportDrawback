using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class ExceptionProcessModule: IHttpModule
{
    #region IHttpModule 成员

    public void Dispose()
    {
        
    }

    public void Init(HttpApplication context)
    {
        context.Error += new EventHandler(context_Error);
    }

    /// <summary>
    /// 读取UserException信息。
    /// </summary>
    private void ReadUserExceptionInfo(SoapException soapEx)
    {
        string userExMessage = string.Empty;
        string userExTypeString = string.Empty;
        //match user exception class
        System.Text.RegularExpressions.MatchCollection mc =
        Regex.Matches(soapEx.Message, "---> ([^:]+):");
        if (mc.Count >= 1)
        {
            userExTypeString = mc[0].Groups[1].Value;
            //match user exception message
            mc = Regex.Matches(soapEx.Message, "---> [^:]+:(.*)\n");
            if (mc.Count > 0) userExMessage = mc[0].Groups[1].Value;
        }
    }

    void context_Error(object sender, EventArgs e)
    {        
        string str = Resources.Resource.ErrorPage;
        HttpContext context = HttpContext.Current;
        IHttpHandler handler = context.CurrentHandler;
        if (!(handler is Page))
        {
            return;
        }
        Exception ex = FindRealException(context.Error);
        string detail = GetExceptionDetail(ex);
        string msg = ex.Message;
        if (ex is SoapException)
        {
            SoapException sex = ex as SoapException;
            string userExMessage = "Web服务发生错误";
            string userExTypeString = string.Empty;
            //match user exception class
            System.Text.RegularExpressions.MatchCollection mc =
            Regex.Matches(sex.Message, "---> ([^:]+):");
            if (mc.Count >= 1)
            {
                userExTypeString = mc[0].Groups[1].Value;
                //match user exception message
                mc = Regex.Matches(sex.Message, "---> [^:]+:(.*)\n");
                if (mc.Count > 0) userExMessage = mc[0].Groups[1].Value;
            }
            msg = userExMessage;
            if (userExTypeString == "System.ApplicationException")
            {
                detail = msg;
            }
            else
            {
                detail = ex.Message;
            }
        }
        else if (ex is ApplicationException)
        {
            detail = msg;
        }
        else
        {
            LogEntry logEntry = new LogEntry(detail, "General", 3, 9191, System.Diagnostics.TraceEventType.Error, msg, null);
            Logger.Write(logEntry);
        }

        str = str.Replace("#Msg", "<p>" + HttpUtility.HtmlEncode(msg) + "</p>");
        str = str.Replace("#DetailInfo", detail);
        HttpResponse response = context.Response;
        response.ContentEncoding = Encoding.GetEncoding("gb2312");
        response.Clear();
        response.Write(str);
        response.End();
        context.ClearError();            
    }

    private System.Exception FindRealException(System.Exception ex)
    {
        System.Exception lastEx = ex;

        while (ex is System.Web.HttpUnhandledException || ex is System.Web.HttpException || ex is System.Reflection.TargetInvocationException)
        {
            lastEx = ex;
            ex = ex.InnerException;
        }

        if (ex == null)
            ex = lastEx;

        return ex;
    }
    private string GetExceptionDetail(System.Exception ex)
    {
        StringBuilder strB = new StringBuilder(1024);

        while (ex != null)
        {
            strB.AppendFormat("<p class=\"INNER_TITLE\">{0}:{1}</p>", ex.GetType().FullName, ex.Message);
            strB.AppendFormat("<p>{0}</p>", HttpUtility.HtmlEncode(ex.StackTrace).Replace("\r\n", "<br />"));

            ex = ex.InnerException;
        }

        return strB.ToString();
    }

    #endregion
}

