using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class DropDownList : System.Web.UI.WebControls.DropDownList, IWebControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("webcontrolsscript",
                Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.JS.WebControlsScript.js"));

            string script = string.Empty;

            if (!string.IsNullOrEmpty(RegularExpressionString))
            {
                script += string.Format("$(document).ready(function(){{AddToVerifyArray($(\"#{0}\"),'{1}');}});\n", 
                    this.ClientID, this.RegularExpressionString);
            }
            if (!IsAllowNull)
            {
                script += string.Format("$(document).ready(function(){{AddToCheckNullArray($(\"#{0}\"));}});\n", this.ClientID);
            }
            if (IsReadOnly)
            {
                script += string.Format("$(document).ready(function(){{AddToDisabledArray($(\"#{0}\"));}});\n", this.ClientID);
            }
            if (!string.IsNullOrEmpty(script))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "startup" + this.ClientID, script, true);
            }

            this.Page.ClientScript.RegisterOnSubmitStatement(this.Page.GetType(), "checkonsubmit", "return CheckAll()");

            base.OnPreRender(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            //RenderChildren(writer);
            if (!IsAllowNull && StarAlign != Align.Empty)
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
                //如果页面不是回传的
                if (!this.Page.IsPostBack)
                {
                    if (this.SelectedItem != null)
                    {
                        return this.SelectedItem.Text;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //if (this.SelectedIndex == -1)
                    //{
                        //如果是回传,而且在前台被重新绑定过
                        return this.Context.Request.Form[this.ClientID];
                    //}
                    //else
                    //{
                        //return this.SelectedItem.Text;
                    //}
                }
            }
            set
            {
                this.ClearSelection();
                if (this.Items.FindByText(value) != null)
                {
                    this.Items.FindByText(value).Selected = true;
                }
                else if (this.Items.FindByValue(value) != null)
                {
                    this.Items.FindByValue(value).Selected = true;
                }
                else
                {
                    this.Items.Insert(0,value);
                    this.Items[0].Selected = true;
                }
            }
        }
        /// <summary>
        /// 必填星显示风格
        /// </summary>
        public Align StarAlign
        {
            get
            {
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

        public System.Web.UI.WebControls.DropDownList Control
        {
            get
            {
                return this;
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
    }
}