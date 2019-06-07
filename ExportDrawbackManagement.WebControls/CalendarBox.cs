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
    [ToolboxData("<{0}:CalendarBox runat=server></{0}:CalendarBox>")]
    public class CalendarBox : TextBox
    {
        private bool _isDateFormat = false;
        /// <summary>
        /// 是否是日期格式
        /// </summary>
        public bool IsDateFormat
        {
            get
            {
                return _isDateFormat;
            }
            set
            {
                if (value)
                {
                    FormatString = "yyyy-MM-dd";
                }
                this.Attributes.Add("onblur", "this.value=FormatDate(this.value);");
                _isDateFormat = value;
            }
        }

        private bool _isTimeFormat = false;
        /// <summary>
        /// 是否是时间格式
        /// </summary>
        public bool IsTimeFormat
        {
            get
            {
                return _isTimeFormat;
            }
            set
            {
                if (value)
                {
                    FormatString = "yyyy-MM-dd HH:mm:ss";
                    this.Attributes.Add("onblur","this.value=FormatDate(this.value);");
                }
                _isTimeFormat = value;
            }
        }

        private string _formatString;
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormatString
        {
            get
            {
                return _formatString;
            }
            set
            {
             
                _formatString = value;
            }
        }
        private bool _isShowButton = false;
        /// <summary>
        /// 是否显示图标
        /// </summary>
        public bool IsShowButton
        {
            get
            {
                return _isShowButton;
            }
            set
            {
                if (value == false)
                {

                }
                _isShowButton = value;
            }
        }

        /// <summary>
        /// 资源路径 当前控件所在页面的相对路径
        /// </summary>
        public string ResourcePath { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Attributes.Add("onkeyup", "this.value=this.value.replace(/[^\\d\\-\\/\\s:]/g,'')");
            if (IsShowButton == false)
            {
                this.Attributes.Add("onclick", string.Format("WdatePicker({0})", "{" + string.Format("{0}", string.Format("el:'{0}',errDealMode:0,dateFmt:'{1}'", this.ClientID, FormatString)) + "}"));
            }
        }



        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (string.IsNullOrEmpty(FormatString))
            {
                if (IsDateFormat)
                {
                    FormatString = "yyyy-MM-dd";
                }
                else if (IsTimeFormat)
                {
                    FormatString = "yyyy-MM-dd HH:mm:ss";
                }
            }
            if (IsShowButton)
            {
                string imgPicker = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "WebControls.Calendar.skin.datePicker.gif");

                string button = string.Format("<img onClick=\"WdatePicker({0})\" src=\"" + imgPicker + "\" width=\"16\" height=\"24\" align=\"absbottom\" style=\"cursor:pointer\" />", "{" + string.Format("{0}", string.Format("el:'{0}',errDealMode:0,dateFmt:'{1}'", this.ClientID, FormatString)) + "}");
                writer.Write(button);
            }



        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (string.IsNullOrEmpty(ResourcePath))
            {
                ResourcePath = "Calendar";
            }
            if (!ResourcePath.EndsWith("/"))
            {
                ResourcePath = ResourcePath + "/";
            }

            StringBuilder clientScript = new StringBuilder();
            clientScript.Append(" function FormatDate(strDate) {\r\n");
            clientScript.Append(@" var strDate=strDate.replace(/(^\s+|\s+$)/g,'');");
            clientScript.Append("\r\n");
            clientScript.Append(@" var reg = /^(\d{4,4})[-\/\\]{0,1}(\d{1,2})[-\/\\]{0,1}(\d{1,2})\s{0,1}(\d{1,2})[:]{0,1}(\d{1,2})[:]{0,1}(\d{1,2})$/;");
            clientScript.Append("\r\n");
            clientScript.Append(@" var r = strDate.match(reg);");
            clientScript.Append("\r\n");
            clientScript.Append(@"  if(r==null){");
            clientScript.Append("\r\n");
            clientScript.Append(@"  reg= /^(\d{4,4})[-\/\\]{0,1}(\d{1,2})[-\/\\]{0,1}(\d{1,2})$/;");
            clientScript.Append("\r\n");
            clientScript.Append(@" var r = strDate.match(reg);");
            clientScript.Append("\r\n");
            clientScript.Append(" if(r==null)return \"\";");
            clientScript.Append("\r\n");
            clientScript.Append("  return r[1]+\"-\"+r[2]+\"-\"+r[3];\r\n");
            clientScript.Append("  } else{\r\n");
            clientScript.Append("  return r[1]+\"-\"+r[2]+\"-\"+r[3]+\" \"+r[4]+\":\"+r[5]+\":\"+r[6];\r\n");
            clientScript.Append("    }}\r\n");

            this.Page.ClientScript.RegisterClientScriptInclude("WdatePicker.js", string.Format("{0}WdatePicker.js", ResourcePath));
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "_controlFormat", clientScript.ToString(), true);
        }




    }
}
