using System;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class RadioButtonList : System.Web.UI.WebControls.RadioButtonList, IWebControl
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
                script += string.Format("$(document).ready(function(){{AddToCheckNullArray($(\"#{0}\"));}});\n",
                    this.ClientID);
            }

            if (!string.IsNullOrEmpty(script))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "startup" + this.ClientID, script, true);
            }

            if (IsReadOnly)
            {
                for(int i = 0; i < this.Items.Count; i ++)
                {
                    this.Items[i].Attributes.Add("disabled", "true");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "enable_" + this.ClientID + ClientIDSeparator.ToString() + i.ToString(),
                        "$(document).ready(function(){{AddToDisabledArray($(\"#" + this.ClientID + ClientIDSeparator.ToString() + i.ToString() + "\").parent());}});\n", true);
                }
            }

            //this.Page.ClientScript.RegisterOnSubmitStatement(this.Page.GetType(), "checkonsubmit", "return  CheckAll()");

            base.OnPreRender(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            //RenderChildren(writer);
            if (!IsAllowNull && StarAlign != Align.Empty)
            {
                if (StarAlign == Align.Left)
                {
                    writer.Write("<span style = \"color:#FF0000; float:left;\">*</span>");
                    base.Render(writer);
                }
                else if (StarAlign == Align.Right)
                {
                    base.Attributes.Add("style", "float:left;");
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
                return this.SelectedValue;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    foreach (string s in value.Split(','))
                    {
                        if (this.Items.FindByText(s) != null)
                        {
                            this.Items.FindByText(s).Selected = true;
                        }
                        else if (this.Items.FindByValue(s) != null)
                        {
                            this.Items.FindByValue(s).Selected = true;
                        }
                        else
                        {
                            this.Items.Insert(0, s);
                            this.Items[0].Selected = true;
                        }
                    }
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

        public System.Web.UI.WebControls.RadioButtonList Control
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
