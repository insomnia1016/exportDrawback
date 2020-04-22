using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// ReceiptAuditAdapter 的摘要说明
/// </summary>
public class ReceiptAuditAdapter : BaseAdapter<IReceiptAuditManager>, IReceiptAuditManager
{
	public ReceiptAuditAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	


	public int getIDByName(string name)
	{
		return Manager.getIDByName(name);
	}

    public DataSet getUnCheckReceiptLists(int CustID, int currencyID)
	{
		return Manager.getUnCheckReceiptLists(CustID,currencyID);
	}




	public DataSet getCustInfoByName(string name)
	{
		return Manager.getCustInfoByName(name);
	}





	public string getLastReceiptID(string key)
	{
		return Manager.getLastReceiptID(key);
	}


	public void insertReceiptList(List<T_ReceiptList> lists)
	{
		Manager.insertReceiptList(lists);
	}


	public void addReceiptHead(T_ReceiptHead head)
	{
		Manager.addReceiptHead(head);
	}


	public void addToDone(List<T_ReceiptList> lists)
	{
		Manager.addToDone(lists);
	}


	public DataSet getDoneBillNos(string customer_name)
	{
		return Manager.getDoneBillNos(customer_name);
	}


	public DataSet getReceiptAuditHeads()
	{
		return Manager.getReceiptAuditHeads();
	}


	public string getTypeNameByCode(string code)
	{
		return Manager.getTypeNameByCode(code);
	}


	public DataSet getReceiptHeads(string receipt_id)
	{
		return Manager.getReceiptHeads(receipt_id);
	}


	public void deleteReceipt(string receipt_id)
	{
		Manager.deleteReceipt(receipt_id);
	}


	public DataSet getReceiptList(string receipt_id)
	{
		return Manager.getReceiptList(receipt_id);
	}


	public DataSet getInDecreaseList(string receipt_id)
	{
		return Manager.getInDecreaseList(receipt_id);
	}


    public void updateToDone(List<T_ReceiptList> lists)
    {
        Manager.updateToDone(lists);
    }

    public void updateReceiptHead(T_ReceiptHead head)
    {
        Manager.updateReceiptHead(head);
    }

    public void updateReceiptList(List<T_ReceiptList> lists)
    {
        Manager.updateReceiptList(lists);
    }


    public void deleteToDone(string receipt_id)
    {
        Manager.deleteToDone(receipt_id);
    }


    public void updateReceiptHeadAuditStatus(T_ReceiptHead head)
    {
        Manager.updateReceiptHeadAuditStatus(head);
    }


    public DataSet getDepositList(string receipt_id)
    {
        return Manager.getDepositList(receipt_id);
    }


    public DataSet getICSaleList(string start_time = "", string end_time = "", string FBillNo = "", string customer_name = "", string check_status="")
    {
        return Manager.getICSaleList(start_time, end_time, FBillNo, customer_name,check_status);
    }


    public DataSet getICSaleListFromKindDee(string start_time = "", string end_time = "", string FBillNo = "", string customer_name = "", string check_status="")
    {
        return Manager.getICSaleListFromKindDee(start_time, end_time, FBillNo, customer_name,check_status);
    }


    public DataSet queryReceiptHeads(string receipt_id, string customer_name = "", string start_time = "", string end_time = "")
    {
        return Manager.queryReceiptHeads(receipt_id, customer_name, start_time, end_time);
    }
}