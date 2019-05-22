<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="ExportDrawbackManagement.Framework.Common" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.Caching" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ExportDrawbackManagement.Biz.Entity" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        try
        {
            //System.Configuration.ConfigurationManager.AppSettings["EntityBasePath"] = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["EntityBasePath"]);
            //
            //string strPath = HttpContext.Current.Request.PhysicalApplicationPath + "\\visitcount.txt";

            ////注意，如果上面不判断路径是否存在，文件将被创建到C:\Windows\system32文件夹中
            //if (File.Exists(strPath))
            //{
            //    StreamReader sr = File.OpenText(strPath);
            //    ExportDrawbackManagementContext.Visitcounter = Convert.ToInt32(sr.ReadToEnd());
            //    sr.Close();
            //}
            //else
            //{
            //    StreamWriter sw = File.CreateText(strPath);
            //    ExportDrawbackManagementContext.Visitcounter = 0;
            //    sw.Write(ExportDrawbackManagementContext.Visitcounter);
            //    sw.Close();
            //}
        }
        catch (Exception ex)
        {
            throw ex;
            //lbMessage2.Text = "创建文件失败！原因：" + ex.Message;
        }

    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。

        if (Session["CurrentUser"]==null)
        {
            Response.Redirect("Default.aspx");
        }
    }

    void Application_PostAuthenticateRequest(object sender, EventArgs e)
    {
        
    }
</script>

