using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.ComponentModel;
[assembly: WebResource("WebControls.JS.jqueryvalidat.js", "text/javascript")]
namespace WebControls
{
    public class TextBox : System.Web.UI.WebControls.TextBox, IWebControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("webcontrolsscript",
                Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.JS.WebControlsScript.js"));
            this.Page.ClientScript.RegisterClientScriptInclude("webvalidatscript",
                Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.JS.jqueryvalidat.js"));

            string script = string.Empty;

            if (!string.IsNullOrEmpty(RegularExpressionString))
            {
                script += string.Format("$(document).ready(function(){{AddToVerifyArray($(\"#{0}\"),'{1}');}});\n", 
                    this.ClientID, this.RegularExpressionString);
            }

            if (!IsAllowNull)
            {
                script += string.Format("$(document).ready(function(){{AddToCheckNullArray($(\"#{0}\"));}});\n", 
                    this.ClientID);
            }

            if (IsReadOnly)
            {
                script += string.Format("$(document).ready(function(){{AddToDisabledArray($(\"#{0}\"));}});\n", this.ClientID);
            }

            if (!string.IsNullOrEmpty(script))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "startup" + this.ClientID, script, true);
            }

            //this.Attributes.Add("onmouseover", "showhintinfo(this, 0,0,'测试','email格式错误','20','up')");
            //this.Attributes.Add("onmouseout", "hidehintinfo()");
            if (RequiredType != DataType.Empty)
            {
                if (this.Attributes["onblur"] != null)
                {
                    this.Attributes.Add("onblur", string.Format("ValidatData('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}',{8});{9}",this.ClientID
                        ,RequiredType.ToString().ToLower(),HintLeftOffSet,HintTopOffSet,HintTitle,HintInfo,HintHeight,HintShowType.ToString(), HintTimeOut,this.Attributes["onblur"]));
                }
                else
                {
                    this.Attributes.Add("onblur", string.Format("ValidatData('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}',{8});",this.ClientID
                        , RequiredType.ToString().ToLower(), HintLeftOffSet, HintTopOffSet, HintTitle, HintInfo, HintHeight, HintShowType.ToString(), HintTimeOut));
                }
            }

            //this.Page.ClientScript.RegisterOnSubmitStatement(this.Page.GetType(), "checkonsubmit", "return  CheckAll()");

            base.OnPreRender(e);
        }

        /// <summary>
        /// 代表的栏位名
        /// </summary>
        public string ControlName
        {
            get
            {
                string str = (string)this.ViewState["ColumnName"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ColumnName"] = value;
            }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get
            {
                string str = (string)this.ViewState["Description"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Description"] = value;
            }
        }
        /// <summary>
        /// 只读
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                if (ViewState["IsReadOnly"] == null)
                {
                    return false;
                }
                return (bool)ViewState["IsReadOnly"];
            }
            set
            {
                if (value)
                {
                    this.Attributes.Add("disabled", "true");
                }
                else
                {
                    this.Attributes.Remove("disabled");
                }

                ViewState["IsReadOnly"] = value;
            }
        }
        /// <summary>
        /// 允许为空
        /// </summary>
        public bool IsAllowNull
        {
            get
            {
                if (ViewState["IsAllowNull"] == null)
                {
                    return true;
                }
                return (bool)ViewState["IsAllowNull"];
            }
            set
            {
                ViewState["IsAllowNull"] = value;
            }
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        public string RegularExpressionString
        {
            get
            {
                string str = (string)this.ViewState["RegularExpressionString"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RegularExpressionString"] = value;
            }
        }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName
        {
            get
            {
                string str = (string)this.ViewState["DisplayName"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DisplayName"] = value;
            }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
        /// <summary>
        /// 可访问
        /// </summary>
        public bool ValueVisible
        {
            get
            {
                return this.Visible;
            }
            set
            {
                this.Visible = value;
            }
        }

        /// <summary>
        /// 必填星显示风格
        /// </summary>
        public Align StarAlign 
        {
            get {
                if (ViewState["StarAlign"] != null)
                {
                    return (Align)ViewState["StarAlign"];
                }
                else
                    return Align.Empty;
            }

            set
            {
                ViewState["StarAlign"] = value;
            }
        }     

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //if (!IsAllowNull)
            //{
            //    System.Web.UI.WebControls.Label star = new System.Web.UI.WebControls.Label();
            //    star.Text = "*";
            //    star.ForeColor = System.Drawing.Color.Red;
            //    this.Controls.Add(star);
            //}
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            //RenderChildren(writer);
            if (!IsAllowNull&&StarAlign!=Align.Empty)
            {
                if (StarAlign == Align.Left)
                {
                    writer.Write("<span style = \"color:#FF0000\">*</span>");
                    base.Render(writer);
                }
                else if (StarAlign == Align.Right)
                {
                    base.Render(writer);
                    writer.Write("<span style = \"color:#FF0000\">*</span>");
                }
            }
            else
            {
                base.Render(writer);
            }      
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        public DataType RequiredType
        {
            get
            {
                if (ViewState["RequiredType"] == null)
                    return DataType.Empty;
                else
                    return (DataType)ViewState["RequiredType"];
            }
            set
            {
                ViewState["RequiredType"] = value;
            }
        }

        #region 提示框标题

        /// <summary>
        /// 提示框标题
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HintTitle
        {
            get {
                if (ViewState["HintTitle"] != null)
                {
                    return (string)ViewState["HintTitle"];
                }
                else
                return string.Empty; 
            }
            set { ViewState["HintTitle"] = value; }
        }

        /// <summary>
        /// 提示框内容
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string HintInfo
        {
            get
            {
                if (ViewState["HintInfo"] != null)
                {
                    return (string)ViewState["HintInfo"];
                }
                else
                    return string.Empty;
            }
            set { ViewState["HintInfo"] = value; }
        }

        /// <summary>
        /// 提示框左侧偏移量
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(0)]
        public int HintLeftOffSet
        {
            get
            {
                if (ViewState["HintLeftOffSet"] != null)
                {
                    return (int)ViewState["HintInfo"];
                }
                else
                    return 0;
            }
            set { ViewState["HintLeftOffSet"] = value; }
        }

        /// <summary>
        /// 提示框顶部偏移量
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(0)]
        public int HintTopOffSet
        {
            get
            {
                if (ViewState["HintTopOffSet"] != null)
                {
                    return (int)ViewState["HintTopOffSet"];
                }
                else
                    return 0;
            }
            set { ViewState["HintTopOffSet"] = value; }
        }

        /// <summary>
        /// 提示框风格,up(上方显示)或down(下方显示)
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("up")]
        public ShowType HintShowType
        {
            get
            {
                if (ViewState["HintShowType"] != null)
                {
                    return (ShowType)ViewState["HintShowType"];
                }
                else
                    return ShowType.up;
            }
            set { ViewState["HintShowType"] = value; }
        }



        /// <summary>
        /// 提示框高度
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(130)]
        public int HintHeight
        {
            get
            {
                if (ViewState["HintHeight"] != null)
                {
                    return (int)ViewState["HintHeight"];
                }
                else
                    return 50;
            }
            set { ViewState["HintHeight"] = value; }
        }

        /// <summary>
        /// 提示过期时间
        /// </summary>
        public int HintTimeOut
        {
            get 
            {
                if (ViewState["HintTimeOut"] != null)
                    return (int)ViewState["HintTimeOut"];
                else
                    return 2000;
            }

            set
            {
                ViewState["HintTimeOut"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 提示信息显示位置
        /// </summary>
        public enum ShowType 
        { 
            /// <summary>
            /// 上
            /// </summary>
            up,
            /// <summary>
            /// 下
            /// </summary>
            down
        }
    }
}
