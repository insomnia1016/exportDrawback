using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebControls
{
    [DefaultProperty("PageSize")]
    [ToolboxData("<{0}:DicRepeater runat=server></{0}:DicRepeater>")]
    public class DicRepeater : Repeater
    {
      

        [Description("分页样式"), Category("Behavior"), DefaultValue(PageStype.Badoo)]
        public PageStype PageStyle { get; set; }

        [Description("记录总数"), Category("Behavior"), DefaultValue(0)]
        public int TotalCount { get; set; }

        [Description("每页数量"), Category("Behavior"), DefaultValue(20)]
        public int PageSize { get; set; }

        [Description("数据源 list or datatable 格式"), Category("Behavior")]
        public object objDataSource { get; set; }

        [Description("左边显示连接数"), Category("Behavior"), DefaultValue(1)]
        public int LeftSize { get; set; }

        [Description("页码对齐方式"), Category("Behavior"), DefaultValue(PageTextAlign.left)]
        public PageTextAlign PageTextAlign { get; set; }

   

        public override void DataBind()
        {
            if (objDataSource != null)
            {
                this.DataSource = objDataSource;
            }
            base.DataBind();
            
            
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex 
        {
            get
            {
                int pageIndex = 1;
                if (!int.TryParse(HttpContext.Current.Request["pageIndex"], out pageIndex))
                    pageIndex = 1;
                return pageIndex;
            }
        }

       
         /// <summary>
        /// 链接页面地址
        /// </summary>
        public string Url
        {
            get
            {
                return GetPageUrlLink(HttpContext.Current.Request.Url, TotalCount);
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (TotalCount == 0)
                {
                    int totalCount = 0;
                    if (!int.TryParse(HttpContext.Current.Request["totalCount"], out totalCount))
                        totalCount = 0;
                    if (totalCount != 0)
                        TotalCount = totalCount;
                }
                return TotalCount % PageSize == 0 ? TotalCount / PageSize : (TotalCount / PageSize + 1);
            }
        }


       

        public DicRepeater()
            : base()
        {
            PageSize = 20;
            LeftSize = 4;
            TotalCount = 20;
            PageStyle = PageStype.Badoo;
            this.EnableViewState = false;
        }

        /// <summary>
        /// 获取分页链接路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPageUrlLink(Uri url, int totalCount)
        {
            if (url == null)
                return string.Empty;
            string link = url.ToString();
            string unionChar = "?";
            if (link.IndexOf("?") > -1)
            {
                unionChar = "&";
            }

            if (link.ToLower().IndexOf("totalcount") <= 0)
            {
                link = link + unionChar + "totalCount=" + totalCount.ToString();
                unionChar = "&";
            }

            if (link.ToLower().IndexOf("pageindex=") > -1)
            {
                link = Regex.Replace(link, @"pageindex=(\d{1,})", "pageIndex={0}", RegexOptions.IgnoreCase);
            }
            else
            {
                link = link + unionChar + "pageIndex={0}";
            }

            return link;
        }


        /// <summary>
        /// 分页条
        /// </summary>
        /// <returns></returns>
        public string BuildLinkHtml()
        {

            //需要输出的HTML
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<style>{1}</style><div id=\"pagingbar\" class=\"pagingbar\" style=\"width:100%; text-align:{0};\"><div style=\"width:100%; text-align:{0};\" class=\"", PageTextAlign.ToString(), PageStyleCSS.GetCSS(PageStyle));
            html.Append(PageStyle.ToString().ToLower());
            html.Append("\">");

            #region 输出上一页
            if (PageIndex <= 1)
            {
                html.Append("<span class=\"disabled\">< Prev </span>");
            }
            else
            {
                html.Append("<a href=\"");
                html.Append(string.Format(Url, PageIndex - 1));
                html.Append("\" ");
                html.Append(">< Prev </a>");
            }
            #endregion

            //总长度
            int barSize = LeftSize * 2 + 1;

            #region 输出中间
            //页数小于0
            //if (PageCount <= 0)
            //{
            //    return "<center><span>页数为0！！</span></center>";
            //}

            //总页数小于正常显示的条数 则全部显示出来
            if (PageCount <= barSize)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (PageIndex == i)
                    {
                        html.Append("<span class=\"current\">");
                        html.Append(i);
                        html.Append("</span>");
                    }
                    else
                    {
                        html.Append("<a href=\"");
                        html.Append(string.Format(Url, i));
                        html.Append("\" ");
                        html.Append(">");
                        html.Append(i);
                        html.Append("</a>");
                    }
                }
            }
            else
            {
                if (PageIndex < LeftSize + 1)
                {
                    for (int i = 1; i <= barSize; i++)
                    {
                        if (PageIndex == i)
                        {
                            html.Append("<span class=\"current\">");
                            html.Append(i);
                            html.Append("</span>");
                        }
                        else
                        {
                            html.Append("<a href=\"");
                            html.Append(string.Format(Url, i));
                            html.Append("\" ");
                            html.Append(">");
                            html.Append(i);
                            html.Append("</a>");
                        }
                    }

                    html.Append("...<a href=\"");
                    html.Append(string.Format(Url, PageCount));
                    html.Append("\" ");
                    html.Append(">");
                    html.Append(PageCount);
                    html.Append("</a>");
                }
                else
                {
                    html.Append("<a href=\"");
                    html.Append(string.Format(Url, 1));
                    html.Append("\" ");
                    html.Append(">");
                    html.Append(1);
                    html.Append("</a>...");


                    if (PageIndex < PageCount - LeftSize)
                    {
                        for (int i = PageIndex - LeftSize; i < PageIndex; i++)
                        {
                            if (i == 1)
                            {
                                continue;
                            }

                            html.Append("<a href=\"");
                            html.Append(string.Format(Url, i));
                            html.Append("\" ");
                            html.Append(">");
                            html.Append(i);
                            html.Append("</a>");
                        }
                        for (int i = PageIndex; i <= PageIndex + LeftSize; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<span class=\"current\">");
                                html.Append(i);
                                html.Append("</span>");
                            }
                            else
                            {
                                html.Append("<a href=\"");
                                html.Append(string.Format(Url, i));
                                html.Append("\" ");
                                html.Append(">");
                                html.Append(i);
                                html.Append("</a>");
                            }
                        }
                        html.Append("...<a href=\"");
                        html.Append(string.Format(Url, PageCount));
                        html.Append("\" ");
                        html.Append(">");
                        html.Append(PageCount);
                        html.Append("</a>");

                    }
                    else
                    {
                        for (int i = PageCount - LeftSize - LeftSize; i <= PageCount; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<span class=\"current\">");
                                html.Append(i);
                                html.Append("</span>");
                            }
                            else
                            {
                                html.Append("<a href=\"");
                                html.Append(string.Format(Url, i));
                                html.Append("\" ");
                                html.Append(">");
                                html.Append(i);
                                html.Append("</a>");
                            }
                        }
                    }

                }

            }
            #endregion

            #region 输出下一页
            if (PageIndex >= PageCount)
            {
                html.Append("<span class=\"disabled\"> Next > </span>");
            }
            else
            {
                html.Append("<a href=\"");
                html.Append(string.Format(Url, PageIndex + 1));
                html.Append("\" ");
                html.Append("> Next > </a>");
            }
            #endregion

            html.Append("&nbsp;&nbsp;</div></div>");
            return html.ToString();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write(BuildLinkHtml());
        }
       
    }
}
