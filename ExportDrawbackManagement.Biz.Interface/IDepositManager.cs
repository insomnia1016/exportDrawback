using ExportDrawbackManagement.Biz.Entity;
using System.Collections.Generic;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IDepositManager
    {
       string getLastDepositID(string key);
       DataSet getUnCheckSEOrderList(int CustID, int currencyID);
       void addDepositHead(T_DepositHead head);
       void insertDepositList(List<T_DepositList> lists);
       void addToDone(List<T_DepositList> lists);
       DataSet getListsAll();
       DataSet getDepositHeadByKey(string key);
       DataSet getDepositListByKey(string key);
       void delete(string key);
       DataSet getDepositHeads(string deposit_id);
       void deleteToDone(string key);
       DataSet getDoneDepositID(string customer_name);
       DataSet getUnCheckDepositLists(int CustID, int currencyID, string type);
       void updateHeadCheckStatus(List<T_ReceiptList> lists);
       void updateHeadCheckStatus(DataSet ds);
       void updateHeadIsPayed(List<T_ReceiptList> lists);
       void updateHeadIsPayed(DataSet ds);
    }
}
