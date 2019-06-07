using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebControls
{

    /// <summary>
    /// 分页导航条样式
    /// </summary>
    public enum PageStype
    {
        /// <summary>
        /// Digg
        /// </summary>
        Digg,
        /// <summary>
        /// Yahoo
        /// </summary>
        Yahoo,
     //   Meneame,
        /// <summary>
        /// Flickr
        /// </summary>
        Flickr,
        /// <summary>
        /// Sabrosus
        /// </summary>
        Sabrosus,
        /// <summary>
        /// Scott
        /// </summary>
        Scott,
      //  Quotes,
      //  Black,
      //  Black2,
       // BlackRed,
      //  Grayr,
        //Yellow,
       // Jogger,
       // Starcraft2,
        //Tres,
       // Megas512,
       // Technorati,
       // Youtube,
       // Msdn,
        /// <summary>
        /// Badoo
        /// </summary>
        Badoo,
       // Manu,
       // GreenBlack,
       // Viciao,
        //Yahoo2
    }

    /// <summary>
    /// 页码对齐方式
    /// </summary>
    public enum PageTextAlign
    {
        /// <summary>
        /// 左
        /// </summary>
        left,
        /// <summary>
        /// 居中
        /// </summary>
        center,
        /// <summary>
        /// 右
        /// </summary>
        right
    }

    public static class PageStyleCSS
    {
        public static string GetCSS(PageStype style)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("div.pagingbar{font-size: 12px;font-family: Verdana, Arial, Helvetica, sans-serif;}");
            switch (style)
            {
                case PageStype.Digg:
                    sb.Append("DIV.digg{padding-right: 3px;padding-left: 3px;padding-bottom: 3px;margin: 3px;padding-top: 3px;text-align: center;}DIV.digg A{border-right: #aaaadd 1px solid;padding-right: 5px;border-top: #aaaadd 1px solid;padding-left: 5px;padding-bottom: 2px;margin: 2px;border-left: #aaaadd 1px solid;color: #000099;padding-top: 2px;border-bottom: #aaaadd 1px solid;text-decoration: none;}DIV.digg A:hover{border-right: #000099 1px solid;border-top: #000099 1px solid;border-left: #000099 1px solid;color: #000;border-bottom: #000099 1px solid;}DIV.digg A:active{border-right: #000099 1px solid;border-top: #000099 1px solid;border-left: #000099 1px solid;color: #000;border-bottom: #000099 1px solid;}DIV.digg SPAN.current{border-right: #000099 1px solid;padding-right: 5px;border-top: #000099 1px solid;padding-left: 5px;font-weight: bold;padding-bottom: 2px;margin: 2px;border-left: #000099 1px solid;color: #fff;padding-top: 2px;border-bottom: #000099 1px solid;background-color: #000099;}DIV.digg SPAN.disabled{border-right: #eee 1px solid;padding-right: 5px;border-top: #eee 1px solid;padding-left: 5px;padding-bottom: 2px;margin: 2px;border-left: #eee 1px solid;color: #ddd;padding-top: 2px;border-bottom: #eee 1px solid;}");
                    break;
                case PageStype.Badoo:
                    sb.Append("DIV.badoo{padding-right: 0px;padding-left: 0px;font-size: 13px;padding-bottom: 10px;color: #48b9ef;padding-top: 10px;font-family: Arial, Helvetica, sans-serif;background-color: #fff;text-align: center;}DIV.badoo A{border-right: #f0f0f0 2px solid;padding-right: 5px;border-top: #f0f0f0 2px solid;padding-left: 5px;padding-bottom: 2px;margin: 0px 2px;border-left: #f0f0f0 2px solid;color: #48b9ef;padding-top: 2px;border-bottom: #f0f0f0 2px solid;text-decoration: none;}DIV.badoo A:hover{border-right: #ff5a00 2px solid;border-top: #ff5a00 2px solid;border-left: #ff5a00 2px solid;color: #ff5a00;border-bottom: #ff5a00 2px solid;}DIV.badoo A:active{border-right: #ff5a00 2px solid;border-top: #ff5a00 2px solid;border-left: #ff5a00 2px solid;color: #ff5a00;border-bottom: #ff5a00 2px solid;}DIV.badoo SPAN.current{border-right: #ff5a00 2px solid;padding-right: 5px;border-top: #ff5a00 2px solid;padding-left: 5px;font-weight: bold;padding-bottom: 2px;border-left: #ff5a00 2px solid;color: #fff;padding-top: 2px;border-bottom: #ff5a00 2px solid;background-color: #ff6c16;}DIV.badoo SPAN.disabled{display: none;}");
                    break;
                case PageStype.Yahoo:
                    sb.Append("DIV.yahoo{padding-right: 3px;padding-left: 3px;padding-bottom: 3px;margin: 3px;padding-top: 3px;text-align: center;}DIV.yahoo A{border-right: #fff 1px solid;padding-right: 5px;border-top: #fff 1px solid;padding-left: 5px;padding-bottom: 2px;margin: 2px;border-left: #fff 1px solid;color: #000099;padding-top: 2px;border-bottom: #fff 1px solid;text-decoration: underline;}DIV.yahoo A:hover{border-right: #000099 1px solid;border-top: #000099 1px solid;border-left: #000099 1px solid;color: #000;border-bottom: #000099 1px solid;}DIV.yahoo A:active{border-right: #000099 1px solid;border-top: #000099 1px solid;border-left: #000099 1px solid;color: #f00;border-bottom: #000099 1px solid;}DIV.yahoo SPAN.current{border-right: #fff 1px solid;padding-right: 5px;border-top: #fff 1px solid;padding-left: 5px;font-weight: bold;padding-bottom: 2px;margin: 2px;border-left: #fff 1px solid;color: #000;padding-top: 2px;border-bottom: #fff 1px solid;background-color: #fff;}DIV.yahoo SPAN.disabled{border-right: #eee 1px solid;padding-right: 5px;border-top: #eee 1px solid;padding-left: 5px;padding-bottom: 2px;	margin: 2px;border-left: #eee 1px solid;color: #ddd;padding-top: 2px;border-bottom: #eee 1px solid;}");
                    break;
                case PageStype.Flickr:
                    sb.Append("DIV.flickr{padding-right: 3px;padding-left: 3px;padding-bottom: 3px;margin: 3px;padding-top: 3px;text-align: center;}DIV.flickr A{border-right: #dedfde 1px solid;padding-right: 6px;background-position: 50% bottom;border-top: #dedfde 1px solid;padding-left: 6px;padding-bottom: 2px;border-left: #dedfde 1px solid;color: #0061de;margin-right: 3px;padding-top: 2px;border-bottom: #dedfde 1px solid;text-decoration: none;}DIV.flickr A:hover{border-right: #000 1px solid;border-top: #000 1px solid;background-image: none;border-left: #000 1px solid;color: #fff;border-bottom: #000 1px solid;background-color: #0061de;}DIV.meneame A:active{border-right: #000 1px solid;border-top: #000 1px solid;background-image: none;border-left: #000 1px solid;color: #fff;border-bottom: #000 1px solid;background-color: #0061de;}DIV.flickr SPAN.current{padding-right: 6px;padding-left: 6px;font-weight: bold;padding-bottom: 2px;color: #ff0084;margin-right: 3px;padding-top: 2px;}DIV.flickr SPAN.disabled{padding-right: 6px;padding-left: 6px;padding-bottom: 2px;color: #adaaad;margin-right: 3px;padding-top: 2px;");
                    break;
                case PageStype.Sabrosus:
                    sb.Append("DIV.sabrosus{padding-right: 3px;padding-left: 3px;padding-bottom: 3px;margin: 3px;padding-top: 3px;text-align: center;}DIV.sabrosus A{border-right: #9aafe5 1px solid;padding-right: 5px;border-top: #9aafe5 1px solid;padding-left: 5px;padding-bottom: 2px;border-left: #9aafe5 1px solid;color: #2e6ab1;margin-right: 2px;padding-top: 2px;border-bottom: #9aafe5 1px solid;text-decoration: none;}DIV.sabrosus A:hover{border-right: #2b66a5 1px solid;border-top: #2b66a5 1px solid;border-left: #2b66a5 1px solidcolor: #000;border-bottom: #2b66a5 1px solid;background-color: lightyellow;}DIV.pagination A:active{border-right: #2b66a5 1px solid;border-top: #2b66a5 1px solid;border-left: #2b66a5 1px solid;color: #000;border-bottom: #2b66a5 1px solid;background-color: lightyellow;}DIV.sabrosus SPAN.current{border-right: navy 1px solid;padding-right: 5px;border-top: navy 1px solid;padding-left: 5px;font-weight: bold;padding-bottom: 2px;border-left: navy 1px solid;color: #fff;margin-right: 2px;padding-top: 2px;border-bottom: navy 1px solid;background-color: #2e6ab1;}DIV.sabrosus SPAN.disabled{border-right: #929292 1px solid;padding-right: 5px;border-top: #929292 1px solid;padding-left: 5px;padding-bottom: 2px;border-left: #929292 1px solid;color: #929292;	margin-right: 2px;padding-top: 2px;border-bottom: #929292 1px solid;}");
                    break;
                case PageStype.Scott:
                    sb.Append("DIV.scott{padding-right: 3px;padding-left: 3px;padding-bottom: 3px;margin: 3px;padding-top: 3px;text-align: center;}DIV.scott A{border-right: #ddd 1px solid;padding-right: 5px;border-top: #ddd 1px solid;padding-left: 5px;padding-bottom: 2px;border-left: #ddd 1px solid;color: #88af3f;margin-right: 2px;padding-top: 2px;border-bottom: #ddd 1px solid;text-decoration: none;}DIV.scott A:hover{border-right: #85bd1e 1px solid;border-top: #85bd1e 1px solid;border-left: #85bd1e 1px solid;color: #638425;border-bottom: #85bd1e 1px solid;background-color: #f1ffd6;}DIV.scott A:active{border-right: #85bd1e 1px solid;border-top: #85bd1e 1px solid;border-left: #85bd1e 1px solid;color: #638425;border-bottom: #85bd1e 1px solid;background-color: #f1ffd6;}DIV.scott SPAN.current{border-right: #b2e05d 1px solid;padding-right: 5px;border-top: #b2e05d 1px solid;padding-left: 5px;font-weight: bold;padding-bottom: 2px;border-left: #b2e05d 1px solid;color: #fff;margin-right: 2px;padding-top: 2px;border-bottom: #b2e05d 1px solid;background-color: #b2e05d;}DIV.scott SPAN.disabled{border-right: #f3f3f3 1px solid;padding-right: 5px;border-top: #f3f3f3 1px solid;padding-left: 5px;padding-bottom: 2px;border-left: #f3f3f3 1px solid;color: #ccc;margin-right: 2px;padding-top: 2px;border-bottom: #f3f3f3 1px solid;}");
                    break;



            }
            return sb.ToString();
        }
    }
}
