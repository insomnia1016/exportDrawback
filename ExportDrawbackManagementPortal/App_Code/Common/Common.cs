using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using ExportDrawbackManagement.Framework.Common;
using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Library;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 一些公用的方法
/// </summary>
public class Common
{
    public Common()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

          
    public static bool LoginCheck()
    {
        try
        {
            if (HttpContext.Current.Session["PersonId"] == null)
            {
                return false;
            }
            Int32 personId =Int32.Parse(HttpContext.Current.Session["PersonId"].ToString());

            CommonAdapter ca = new CommonAdapter();

            DataSet ds = ca.getUserInfoById(personId);
            DataRow dr = ds.Tables[0].Rows[0];
            UserInfo user = new UserInfo();
            
            user.Derpartment = dr["derpartment"].ToString().Trim();
            user.Name = dr["name"].ToString().Trim();
            user.PersonId = dr["person_id"].ToString().Trim();
            user.Roles = dr["roles"].ToString().Trim();
            user.Rank = dr["rank"].ToString().Trim();
            user.Username = dr["username"].ToString().Trim();
            
           
            ExportDrawbackManagementIdentity identity = new ExportDrawbackManagementIdentity(user);

            ExportDrawbackManagementPrincipal edPrincipal = new ExportDrawbackManagementPrincipal(identity, user.Roles.Split(',').ToArray());

            HttpContext.Current.Session["CurrentUser"] = edPrincipal;
            return true;

        }
        catch
        {
            throw new ZHNException("未登录访问出错，将跳转");
        }
    }

    public static decimal calculateProfit(
        decimal exchange_rate,
        decimal buy_price,
        decimal sale_price,
        decimal accessory_price,
        decimal estimate_freight_charge,
        decimal tax_rate,
        bool sale_rate,
        bool buy_rate,
        string currency,
        decimal volume,
        decimal capacity,
        decimal return_tax = (decimal)0.13)
    {
        //计算利润率
        decimal profit = 0;

        //销售价格是美金
        if (currency == "USD")
        {
            if (buy_rate)//采购价格人民币含税
            {
                profit = 1 - (((buy_price + (accessory_price + estimate_freight_charge) / (1 - tax_rate)) * (1 - return_tax / (decimal)1.13) + 2500 / (28 / volume * capacity)) / (sale_price * exchange_rate));
            }
            else
            {
                profit = 1 - ((((buy_price + accessory_price + estimate_freight_charge) / (1 - tax_rate)) * (1 - return_tax / (decimal)1.13) + 2500 / (28 / volume * capacity)) / (sale_price * exchange_rate));
            }
        }
        else//销售价格是人民币
        {
            //销售价格人民币含税
            if (sale_rate)
            {
                //采购价格人民币含税
                if (buy_rate)
                {
                    profit = (sale_price * exchange_rate - buy_price - accessory_price - estimate_freight_charge) / (sale_price * exchange_rate);
                }
                else//采购价格人民币不含税
                {
                    profit = (sale_price * exchange_rate - buy_price / (1 - tax_rate) - accessory_price - estimate_freight_charge) / (sale_price * exchange_rate);
                }
            }
            else//销售价格人民币不含税
            {
                profit = (sale_price * exchange_rate - buy_price - accessory_price - estimate_freight_charge) / (sale_price * exchange_rate);
            }
        }

        return profit;
    }

    public static decimal calculateCommission(decimal profit,decimal sale_price,decimal sale_qty,decimal exchange_rate)
    {
        return sale_price * sale_qty * exchange_rate * profit * 0.008m / 0.15m;
    }

}
