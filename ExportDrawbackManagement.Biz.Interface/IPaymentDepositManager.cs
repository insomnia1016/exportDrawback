using ExportDrawbackManagement.Biz.Entity;
using System.Collections.Generic;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IPaymentDepositManager
    {
       string getLastDepositID(string key);
       DataSet getDoneDepositID(string customer_name);
       void addDepositHead(T_PaymentDepositHead head);
       void insertDepositList(List<T_PaymentDepositList> lists);
       void addToDone(List<T_PaymentDepositList> lists);
       DataSet getDepositHeadByKey(string key);
       DataSet getDepositListByKey(string key);
       void delete(string key);
       void deleteToDone(string key);
       DataSet getDepositHeads(string deposit_id);
       DataSet getListsAll();
       DataSet getUnCheckPOOrderList(int FSupplyID, int currencyID);
       DataSet getUnCheckDepositLists(int CustID, int currencyID, string type);
       void updateHeadCheckStatus(List<T_PaymentReceiptList> lists);
       void updateHeadCheckStatus(DataSet ds);
       void updateHeadIsPayed(List<T_PaymentReceiptList> lists);
       void updateHeadIsPayed(DataSet ds);
    }
}
