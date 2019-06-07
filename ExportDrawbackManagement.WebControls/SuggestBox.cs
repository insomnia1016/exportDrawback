using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SuggestBox runat=server></{0}:SuggestBox>")]
    public class SuggestBox :  TextBox
    {
        protected override void OnPreRender(EventArgs e)
        {            
            base.OnPreRender(e);
            this.Page.ClientScript.RegisterClientScriptInclude("_suggest", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.JS.jquerysuggest.js"));

            string script = string.Format("$(document).ready(function(){{$(\"#{0}\").suggest(\"{1}\",{{mustMatch:{2},delay:{3}{4}}});}});\n",
                this.ClientID, this.RequestURL, IsMustMatch ? "true" : "false", TimeOut, string.IsNullOrEmpty(ExtParamFunc) ? string.Empty : string.Format(",extParaFunc:function(){{return {0};}}", ExtParamFunc));
            
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "startup-suggest" + this.ClientID, script, true);
            this.Page.ClientScript.RegisterOnSubmitStatement(this.Page.GetType(), "checkonsubmit", "return  CheckAll()");
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }

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
            string cssUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Css.jquerysuggest.css");

            HtmlLink cssLink = new HtmlLink();

            cssLink.Href = cssUrl;
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "Stylesheet");
            Page.Header.Controls.Add(cssLink);
        }

        /// <summary>
        /// 请求的URL
        /// </summary>
        [Bindable(true)]
        [Category("Action")]
        [DefaultValue("")]
        [Localizable(true)]
        public string RequestURL 
        {
            get 
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ViewState["RequestURL"])))
                    return ViewState["RequestURL"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["RequestURL"] = value;
            }
        }

        /// <summary>
        /// 是否绝对匹配
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        [Localizable(true)]
        public bool IsMustMatch
        {
            get
            {
                if (ViewState["IsMustMatch"] == null)
                {
                    return true;
                }
                return (bool)ViewState["IsMustMatch"];
            }
            set
            {
                ViewState["IsMustMatch"] = value;
            }
        }

        /// <summary>
        /// 时间间隔
        /// </summary>
        [Bindable(true)]
        [Category("Action")]
        [DefaultValue(200)]
        [Localizable(true)]
        public int TimeOut
        {
            get
            {
                if (ViewState["TimeOut"] == null)
                {
                    return 100;
                }
                else
                {
                    return Convert.ToInt32(ViewState["TimeOut"]);
                }
            }
            set
            {
                ViewState["TimeOut"] = value;
            }
        }

        /// <summary>
        /// 扩展js function
        /// </summary>
        [Bindable(true)]
        [Category("Action")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ExtParamFunc
        {
            get
            {
                if (ViewState["ExtParamFunc"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToString(ViewState["ExtParamFunc"]);
                }
            }
            set
            {
                ViewState["ExtParamFunc"] = value;
            }
        }
    }
}
