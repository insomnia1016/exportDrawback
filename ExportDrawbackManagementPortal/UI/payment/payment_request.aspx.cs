using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;

public partial class UI_payment_payment_request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            d_date.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            txt_emp_name.Text = UserInfoAdapter.CurrentUser.Name;
            txt_dept_name.Text = UserInfoAdapter.CurrentUser.Derpartment;
            hdf_payment_id.Value = LoadLastPaymentId();
        }
        
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        T_PaymentRequest item = new T_PaymentRequest();
        item.PaymentId = hdf_payment_id.Value;
        item.PaymentDate = DateTime.ParseExact(d_date.Text, "yyyy年MM月dd日", System.Globalization.CultureInfo.CurrentCulture);
        item.PayeeUnit = txt_payee_unit.Text;
        item.Amount = Decimal.Parse(txt_amount.Text);
        item.PayeeOpeningBank = txt_opening_bank.Text;
        item.PayeeAccountId = txt_account_id.Text;
        item.CustomerBillNo = txt_icsale_id.Text;
        item.FactoryBillNo = txt_poorder_id.Text;
        item.GoodsModel = txt_goods_model.Text;
        item.PaymentExplain = txt_payment_explain.Text;
        item.Note = txt_note.Text;
        item.DeptName = txt_dept_name.Text;
        item.EmpName = txt_emp_name.Text;
        item.IsUserd = false;

        PaymentAdapter pa = new PaymentAdapter();
        try
        {
            pa.insert(item);
            Label1.Text = "保存成功";
            clean();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }

    private void clean()
    {
        hdf_payment_id.Value="";
        d_date.Text= DateTime.Now.ToString("yyyy年MM月dd日");
        txt_amount.Text = "";
        txt_opening_bank.Text = "";
        txt_account_id.Text = "";
        txt_icsale_id.Text = "";
        txt_poorder_id.Text = "";
        txt_goods_model.Text = "";
        txt_payment_explain.Text = "";
        txt_note.Text = "";
        txt_dept_name.Text = "";
        txt_emp_name.Text = "";
    }

    private string LoadLastPaymentId()
    {
        string search = "PAY" + DateTime.Now.ToString("yyyyMMdd");
        PaymentAdapter pa = new PaymentAdapter();
        return pa.getLastPaymentID(search);
    }
    protected void txt_amount_TextChanged(object sender, EventArgs e)
    {
        string amount = txt_amount.Text;
        if (string.IsNullOrEmpty(amount))
        {
            return;
        }
        lbl_amount.Text = MoneyToUpper(amount);
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