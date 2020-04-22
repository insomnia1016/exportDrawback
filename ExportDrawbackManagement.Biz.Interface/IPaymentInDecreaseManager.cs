using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface IPaymentInDecreaseManager
    {
        void insertInDecreaseList(DataTable dt);
        void addInDecreaseHead(T_PaymentIndecreaseHead head);
        DataSet getListsAll();
        void delete(string bill_no, int g_no);
        void deleteHead(string bill_no);
        DataSet getInDecreaseHeadByBillNo(string bill_no);
        void updateList(T_PaymentIndecreaseList item);
        void updateHead(T_PaymentIndecreaseHead item);
        DataSet getInDecreaseInfoByBillNo(string bill_no);
        void updateHeadAmountAll(string bill_no, decimal amount_all);
        string getLastBillNo(string key);
        DataSet getUnCheckInDecreaseLists(int CustID, int currencyID);
        void updateHeadCheckStatus(List<T_PaymentReceiptList> lists);
        void updateHeadCheckStatus(DataSet ds);
    }
}
