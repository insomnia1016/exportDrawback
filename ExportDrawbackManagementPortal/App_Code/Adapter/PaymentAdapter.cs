using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

/// <summary>
/// PaymentAdapter 的摘要说明
/// </summary>
public class PaymentAdapter:BaseAdapter<IPaymentManager>,IPaymentManager
{
	public PaymentAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public void insert(T_PaymentRequest item)
    {
        Manager.insert(item);
    }


    public void update(T_PaymentRequest item)
    {
        Manager.update(item);
    }


    public string getLastPaymentID(string key)
    {
        return Manager.getLastPaymentID(key);
    }


    public DataSet getPaymentByID(string payment_id)
    {
        return Manager.getPaymentByID(payment_id);
    }


    public DataSet getPayments(T_PaymentRequest item, string start_time, string end_time)
    {
        return Manager.getPayments(item, start_time, end_time);
    }


    public string getLastReceiptID(string key)
    {
        return Manager.getLastReceiptID(key);
    }


    public int getIDByName(string name)
    {
        return Manager.getIDByName(name);
    }


    public DataSet getSupplierInfoByName(string name)
    {
        return Manager.getSupplierInfoByName(name);
    }


    public DataSet getUnCheckReceiptLists(int FSupplyID, int currencyID)
    {
        return Manager.getUnCheckReceiptLists(FSupplyID, currencyID);
    }


    public DataSet getDoneBillNos(string customer_name)
    {
        return Manager.getDoneBillNos(customer_name);
    }


    public void addReceiptHead(T_PaymentReceiptHead head)
    {
        Manager.addReceiptHead(head);
    }


    public void insertReceiptList(List<T_PaymentReceiptList> lists)
    {
        Manager.insertReceiptList(lists);
    }


    public void addToDone(List<T_PaymentReceiptList> lists)
    {
        Manager.addToDone(lists);
    }


    public DataSet getReceiptAuditHeads()
    {
        return Manager.getReceiptAuditHeads();
    }


    public DataSet getInDecreaseList(string receipt_id)
    {
        return Manager.getInDecreaseList(receipt_id);
    }


    public DataSet getDepositList(string receipt_id)
    {
        return Manager.getDepositList(receipt_id);
    }


    public void deleteToDone(string receipt_id)
    {
        Manager.deleteToDone(receipt_id);
    }


    public void deleteReceipt(string receipt_id)
    {
        Manager.deleteReceipt(receipt_id);
    }


    public DataSet getReceiptHeads(string receipt_id)
    {
        return Manager.getReceiptHeads(receipt_id);
    }


    public void updateReceiptHeadAuditStatus(T_PaymentReceiptHead head)
    {
        Manager.updateReceiptHeadAuditStatus(head);
    }


    public DataSet getReceiptList(string receipt_id)
    {
        return Manager.getReceiptList(receipt_id);
    }


    public void updateReceiptHead(T_PaymentReceiptHead head)
    {
        Manager.updateReceiptHead(head);
    }


    public void updateReceiptList(List<T_PaymentReceiptList> lists)
    {
        Manager.updateReceiptList(lists);
    }


    public void updateToDone(List<T_PaymentReceiptList> lists)
    {
        Manager.updateToDone(lists);
    }
    public DataSet getICPurChaseList(string start_time, string end_time, string FBillNo, string customer_name, string check_status)
    {
        return Manager.getICPurChaseList(start_time, end_time, FBillNo, customer_name, check_status);
    }

    public DataSet getICPurChaseListFromKindDee(string start_time, string end_time, string FBillNo, string customer_name,string check_status)
    {
        return Manager.getICPurChaseListFromKindDee(start_time, end_time, FBillNo, customer_name,check_status);
    }


    public DataSet queryReceiptHeads(string receipt_id, string customer_name = "", string start_time = "", string end_time = "")
    {
        return Manager.queryReceiptHeads(receipt_id, customer_name, start_time, end_time);
    }


    public bool depositIsUsed(string deposit_id)
    {
        return Manager.depositIsUsed(deposit_id);
    }


    public bool inDecreaseIsUsed(string inDecrease_no)
    {
        return Manager.inDecreaseIsUsed(inDecrease_no);
    }

    public bool requestIsUsed(string request_id)
    {
        return Manager.requestIsUsed(request_id);
    }


    public DataSet getRequestList(string receipt_id)
    {
        return Manager.getRequestList(receipt_id);
    }
}