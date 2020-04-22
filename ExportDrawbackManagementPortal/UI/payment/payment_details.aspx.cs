using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_payment_payment_details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string payment_id = Request.QueryString["id"].ToString();
            PaymentAdapter pa = new PaymentAdapter();
            DataSet ds = pa.getPaymentByID(payment_id);
            WebDataBind(ds);
        }
    }

    private void WebDataBind(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count == 0) return;
        DataRow dr = ds.Tables[0].Rows[0];
        lbl_d_date.Text = DateTime.Parse(dr["payment_date"].ToString()).ToString("yyyy年MM月dd日");
        lbl_payee_unit.Text = dr["payee_unit"].ToString();
        string amount = dr["amount"].ToString();
        lbl_amount_num.Text = dr["amount"].ToString().Substring(0, amount.Length-3);
        lbl_amount.Text = MoneyToUpper(dr["amount"].ToString());
        lbl_opening_bank.Text = dr["payee_opening_bank"].ToString();
        lbl_account_id.Text = dr["payee_account_id"].ToString();
        lbl_icsale_id.Text = dr["customer_bill_no"].ToString();
        lbl_payment_explain.Text = dr["payment_explain"].ToString();
        lbl_poorder_id.Text = dr["factory_bill_no"].ToString();
        lbl_goods_model.Text = dr["goods_model"].ToString();
        lbl_note.Text = dr["note"].ToString();
        lbl_dept_name.Text = dr["dept_name"].ToString();
        lbl_emp_name.Text = dr["emp_name"].ToString();
    }



    /// <summary>
    /// 金额转换成中文大写金额
    /// </summary>
    /// <param name="LowerMoney">eg:10.74</param>
    /// <returns></returns>
    public static string MoneyToUpper(string LowerMoney)
    {
        string functionReturnValue = null;
        bool IsNegative = false; // 是否是负数
        if (LowerMoney.Trim().Substring(0, 1) == "-")
        {
            // 是负数则先转为正数
            LowerMoney = LowerMoney.Trim().Remove(0, 1);
            IsNegative = true;
        }
        string strLower = null;
        string strUpart = null;
        string strUpper = null;
        int iTemp = 0;
        // 保留两位小数 123.489→123.49　　123.4→123.4
        LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
        if (LowerMoney.IndexOf(".") > 0)
        {
            if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
            {
                LowerMoney = LowerMoney + "0";
            }
        }
        else
        {
            LowerMoney = LowerMoney + ".00";
        }
        strLower = LowerMoney;
        iTemp = 1;
        strUpper = "";
        while (iTemp <= strLower.Length)
        {
            switch (strLower.Substring(strLower.Length - iTemp, 1))
            {
                case ".":
                    strUpart = "圆";
                    break;
                case "0":
                    strUpart = "零";
                    break;
                case "1":
                    strUpart = "壹";
                    break;
                case "2":
                    strUpart = "贰";
                    break;
                case "3":
                    strUpart = "叁";
                    break;
                case "4":
                    strUpart = "肆";
                    break;
                case "5":
                    strUpart = "伍";
                    break;
                case "6":
                    strUpart = "陆";
                    break;
                case "7":
                    strUpart = "柒";
                    break;
                case "8":
                    strUpart = "捌";
                    break;
                case "9":
                    strUpart = "玖";
                    break;
            }

            switch (iTemp)
            {
                case 1:
                    strUpart = strUpart + "分";
                    break;
                case 2:
                    strUpart = strUpart + "角";
                    break;
                case 3:
                    strUpart = strUpart + "";
                    break;
                case 4:
                    strUpart = strUpart + "";
                    break;
                case 5:
                    strUpart = strUpart + "拾";
                    break;
                case 6:
                    strUpart = strUpart + "佰";
                    break;
                case 7:
                    strUpart = strUpart + "仟";
                    break;
                case 8:
                    strUpart = strUpart + "万";
                    break;
                case 9:
                    strUpart = strUpart + "拾";
                    break;
                case 10:
                    strUpart = strUpart + "佰";
                    break;
                case 11:
                    strUpart = strUpart + "仟";
                    break;
                case 12:
                    strUpart = strUpart + "亿";
                    break;
                case 13:
                    strUpart = strUpart + "拾";
                    break;
                case 14:
                    strUpart = strUpart + "佰";
                    break;
                case 15:
                    strUpart = strUpart + "仟";
                    break;
                case 16:
                    strUpart = strUpart + "万";
                    break;
                default:
                    strUpart = strUpart + "";
                    break;
            }

            strUpper = strUpart + strUpper;
            iTemp = iTemp + 1;
        }

        strUpper = strUpper.Replace("零拾", "零");
        strUpper = strUpper.Replace("零佰", "零");
        strUpper = strUpper.Replace("零仟", "零");
        strUpper = strUpper.Replace("零零零", "零");
        strUpper = strUpper.Replace("零零", "零");
        strUpper = strUpper.Replace("零角零分", "整");
        strUpper = strUpper.Replace("零分", "整");
        strUpper = strUpper.Replace("零角", "零");
        strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
        strUpper = strUpper.Replace("亿零万零圆", "亿圆");
        strUpper = strUpper.Replace("零亿零万", "亿");
        strUpper = strUpper.Replace("零万零圆", "万圆");
        strUpper = strUpper.Replace("零亿", "亿");
        strUpper = strUpper.Replace("零万", "万");
        strUpper = strUpper.Replace("零圆", "圆");
        strUpper = strUpper.Replace("零零", "零");

        // 对壹圆以下的金额的处理
        if (strUpper.Substring(0, 1) == "圆")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "零")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "角")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "分")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "整")
        {
            strUpper = "零圆整";
        }
        functionReturnValue = strUpper;

        if (IsNegative == true)
        {
            return "负" + functionReturnValue;
        }
        else
        {
            return functionReturnValue;
        }
    }
}