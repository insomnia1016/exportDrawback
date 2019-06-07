using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;

[assembly: WebResource("WebControls.Images.commandbg.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.commandbg2.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.commandbg3.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.commandbg4.gif", "image/gif")]
[assembly: WebResource("WebControls.Images.dot.gif", "image/gif")]
[assembly: WebResource("WebControls.Css.hint.css", "text/html")]
namespace WebControls
{
    /// <summary>
    /// 提示信息控件
    /// </summary>
    [DefaultEvent("Click"), DefaultProperty("Text"), ToolboxData("<{0}:Hint runat=server></{0}:Hint>")]
    public class Hint : System.Web.UI.WebControls.WebControl
    {

        #region Property HintImageUrl

        /// <summary>
        /// 图片地址
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HintImageUrl
        {
            get
            {
                if (base.ViewState["hintimageurl"] != null)
                {
                    return (String)base.ViewState["hintimageurl"];
                }
                else
                {
                    return "../images";
                }
            }
            set
            {
                base.ViewState["hintimageurl"] = value;
            }
        }

        #endregion

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            RegisterCSS();
        }
        /// <summary>
        /// 注册CSS
        /// </summary>
        private void RegisterCSS()
        {
            string cssUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Css.hint.css");

            HtmlLink cssLink = new HtmlLink();

            cssLink.Href = cssUrl;
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "Stylesheet");
            Page.Header.Controls.Add(cssLink);
        }

        /// <summary>
        /// 输出html,在浏览器中显示控件
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            string commandbgUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.commandbg.gif");
            string commandbgUrl2 = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.commandbg2.gif");
            string commandbgUrl3 = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.commandbg3.gif");
            string commandbgUrl4 = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.commandbg4.gif");
            string dotUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Images.dot.gif");

            StringBuilder sb = new StringBuilder();
                   
            sb.Append("<!--提示层部分开始-->");

            sb.Append("<span id=\"hintdivup\" style=\"display:none; position:absolute;z-index:500;\">\r\n");
		    sb.Append("<div style=\"position:absolute; visibility: visible; width: 271px;z-index:501;\">\r\n");
            sb.Append("<p><img src=\"" + commandbgUrl + "\" /></p>\r\n");
            //sb.Append("<div><img src=\"" + commandbgUrl + "\" /></div>\r\n");
            sb.Append("<div class=\"messagetext\"><img src=\"" + dotUrl + "\" /><span id=\"hintinfoup\" ></span></div>\r\n");
            sb.Append("<p><img src=\"" + commandbgUrl2 + "\" /></p>\r\n");
		    sb.Append("</div>\r\n");
            sb.Append("<iframe id=\"hintiframeup\" style=\"position:absolute;z-index:100;width:266px;scrolling:no;\" frameborder=\"0\"></iframe>\r\n");
	        sb.Append("</span>\r\n");


            sb.Append("<span id=\"hintdivdown\" style=\"display:none; position:absolute;z-index:500;\">\r\n");
            sb.Append("<div style=\"position:absolute; visibility: visible; width: 271px;z-index:501;\">\r\n");
            sb.Append("<p><img src=\"" + commandbgUrl3 + "\" /></p>\r\n");
            sb.Append("<div class=\"messagetext\"><img src=\"" + dotUrl + "\" /><span id=\"hintinfodown\" ></span></div>\r\n");
            sb.Append("<p><img src=\"" + commandbgUrl4 + "\" /></p>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("<iframe id=\"hintiframedown\" style=\"position:absolute;z-index:100;width:266px;scrolling:no;\" frameborder=\"0\"></iframe>\r\n");
            sb.Append("</span>\r\n");

            sb.Append("<!--提示层部分结束-->\r\n");

            output.Write(sb.ToString());
        }

    }
}
