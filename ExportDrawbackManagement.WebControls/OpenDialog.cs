using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

[assembly: WebResource("WebControls.JS.DialogScript.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("WebControls.Images.DialogImages.btn_bg.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.d_bg.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.wait.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_01.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_02.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_05.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_06.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_07.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_08.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_09.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_10.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_11.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_13.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.bg_18.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.DialogImages.buttonbg.gif", "image/gif")]

namespace WebControls
{
    /// <summary>
    /// 弹出框控件
    /// </summary>
    [ToolboxData("<{0}:OpenDialog runat=server></{0}:OpenDialog>"), DefaultProperty("Title")]
    [Designer("WebControls.Designer, WebControls")]
    public class OpenDialog : WebControl, INamingContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            EnsureChildControls();
            base.OnPreRender(e);
            Page.ClientScript.RegisterClientScriptResource(this.GetType(), "WebControls.JS.DialogScript.js");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }

        #region 方法

        /// <summary>
        /// 类似Alert，弹出提示信息框
        /// </summary>
        /// <param name="title">弹出框标题</param>
        /// <param name="content">弹出框内容（若contentType参数为Text则内容为文本；为Page则内容为页面；为Html则内容为Html代码）</param>
        /// <param name="isRefresh">是否刷新本页面</param>
        /// <param name="contentType">内容类型（Text为文本；Page为页面；Html为程序员自己编辑的HTML代码）</param>
        /// <param name="cwidth">弹出框宽度</param>
        /// <param name="cheight">弹出框高度</param>
        public void Alert(string title, string content, bool isRefresh, ContentType contentType, int cwidth, int cheight)
        {

            string str = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_01.gif");
            string str2 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_02.gif");
            string str5 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_05.gif");
            string str6 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_06.gif");
            string str13 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_13.gif");
            string str7 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_07.gif");
            string str8 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_08.gif");
            string str9 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_09.gif");
            string str10 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_10.gif");
            string str11 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_11.gif");
            string strBtn = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.buttonbg.gif");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("MyAlert('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},{16});", title, content, isRefresh.ToString().ToLower(), str, str2, str5, str6, str13, str7, str8, str9, str10, str11, strBtn, contentType.GetHashCode(), cwidth, cheight);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "js", sb.ToString(), true);
        }

        /// <summary>
        /// 类似Alert，弹出提示信息框，关闭后跳转页面
        /// </summary>
        /// <param name="title">弹出框标题</param>
        /// <param name="content">弹出框内容（若contentType参数为Text则内容为文本；为Page则内容为页面；为Html则内容为Html代码）</param>
        /// <param name="url">跳转页面</param>
        /// <param name="contentType">内容类型（Text为文本；Page为页面；Html为程序员自己编辑的HTML代码）</param>
        /// <param name="cwidth">弹出框宽度</param>
        /// <param name="cheight">弹出框高度</param>
        public void AlertUrl(string title, string content, string url, ContentType contentType, int cwidth, int cheight)
        {

            string str = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_01.gif");
            string str2 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_02.gif");
            string str5 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_05.gif");
            string str6 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_06.gif");
            string str13 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_13.gif");
            string str7 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_07.gif");
            string str8 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_08.gif");
            string str9 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_09.gif");
            string str10 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_10.gif");
            string str11 = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.bg_11.gif");
            string strBtn = Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.DialogImages.buttonbg.gif");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("MyAlertUrl('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},{16});", title, content, url, str, str2, str5, str6, str13, str7, str8, str9, str10, str11, strBtn, contentType.GetHashCode(), cwidth, cheight);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "js", sb.ToString(), true);
        }

        #endregion
    }

    /// <summary>
    /// 内容枚举
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// 内容为文本
        /// </summary>
        Text = 1,
        /// <summary>
        /// 内容为页面
        /// </summary>
        Page = 2,
        /// <summary>
        /// 内容为HTML代码
        /// </summary>
        Html = 3
    }
}
