using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IReceiptAuditManager
    {
       DataSet getUnCheckReceiptLists(int CustID, int currencyID);
       int getIDByName(string name);
       DataSet getCustInfoByName(string name);
       string getLastReceiptID(string key);
       void insertReceiptList(List<T_ReceiptList> lists);
       void addReceiptHead(T_ReceiptHead head);
       void addToDone(List<T_ReceiptList> lists);
       DataSet getDoneBillNos(string customer_name);
       DataSet getReceiptAuditHeads();
       string getTypeNameByCode(string code);
       DataSet getReceiptHeads(string receipt_id);
       void deleteReceipt(string receipt_id);
       DataSet getReceiptList(string receipt_id);
       DataSet getInDecreaseList(string receipt_id);
       void updateToDone(List<T_ReceiptList> lists);
       void updateReceiptHead(T_ReceiptHead head);
       void updateReceiptList(List<T_ReceiptList> lists);
       void deleteToDone(string receipt_id);
       void updateReceiptHeadAuditStatus(T_ReceiptHead head);
       DataSet getDepositList(string receipt_id);
       DataSet getICSaleList(string start_time, string end_time, string FBillNo, string customer_name,string check_status);
       DataSet getICSaleListFromKindDee(string start_time, string end_time, string FBillNo, string customer_name, string check_status);
       DataSet queryReceiptHeads(string receipt_id, string customer_name = "", string start_time = "", string end_time = "");
    }
}
