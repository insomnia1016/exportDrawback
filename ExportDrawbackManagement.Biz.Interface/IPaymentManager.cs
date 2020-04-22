using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IPaymentManager
    {
       void insert(T_PaymentRequest item);
       void update(T_PaymentRequest item);
       string getLastPaymentID(string key);
       string getLastReceiptID(string key);
       DataSet getPaymentByID(string payment_id);
       DataSet getPayments(T_PaymentRequest item, string start_time, string end_time);
       int getIDByName(string name);
       DataSet getSupplierInfoByName(string name);
       DataSet getUnCheckReceiptLists(int FSupplyID, int currencyID);
       DataSet getDoneBillNos(string customer_name);
       void addReceiptHead(T_PaymentReceiptHead head);
       void insertReceiptList(List<T_PaymentReceiptList> lists);
       void addToDone(List<T_PaymentReceiptList> lists);
       DataSet getReceiptAuditHeads();
       DataSet getInDecreaseList(string receipt_id);
       DataSet getDepositList(string receipt_id);
       DataSet getRequestList(string receipt_id);
       void deleteToDone(string receipt_id);
       void deleteReceipt(string receipt_id);
       DataSet getReceiptHeads(string receipt_id);
       void updateReceiptHeadAuditStatus(T_PaymentReceiptHead head);
       void updateReceiptHead(T_PaymentReceiptHead head);
       void updateReceiptList(List<T_PaymentReceiptList> lists);
       DataSet getReceiptList(string receipt_id);
       void updateToDone(List<T_PaymentReceiptList> lists);
       DataSet getICPurChaseList(string start_time, string end_time, string FBillNo, string customer_name, string check_status);
       DataSet getICPurChaseListFromKindDee(string start_time, string end_time, string FBillNo, string customer_name, string check_status);
       DataSet queryReceiptHeads(string receipt_id, string customer_name = "", string start_time = "", string end_time = "");
       bool depositIsUsed(string deposit_id);
       bool inDecreaseIsUsed(string inDecrease_no);
       bool requestIsUsed(string request_id);
    }
}
