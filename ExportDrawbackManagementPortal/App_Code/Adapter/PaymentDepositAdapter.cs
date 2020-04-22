using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

/// <summary>
/// PaymentDepositAdapter 的摘要说明
/// </summary>
public class PaymentDepositAdapter : BaseAdapter<IPaymentDepositManager>, IPaymentDepositManager
{
	public PaymentDepositAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public string getLastDepositID(string key)
    {
        return Manager.getLastDepositID(key);
    }


    public DataSet getUnCheckPOOrderList(int CustID, int currencyID)
    {
        return Manager.getUnCheckPOOrderList(CustID, currencyID);
    }


    public DataSet getDoneDepositID(string customer_name)
    {
        return Manager.getDoneDepositID(customer_name);
    }


    public void addDepositHead(T_PaymentDepositHead head)
    {
        Manager.addDepositHead(head);
    }

    public void insertDepositList(List<T_PaymentDepositList> lists)
    {
        Manager.insertDepositList(lists);
    }


    public void addToDone(List<T_PaymentDepositList> lists)
    {
        Manager.addToDone(lists);
    }


    public DataSet getDepositHeadByKey(string key)
    {
        return Manager.getDepositHeadByKey(key);
    }


    public DataSet getDepositListByKey(string key)
    {
        return Manager.getDepositListByKey(key);
    }


    public void delete(string key)
    {
        Manager.delete(key);
    }

    public void deleteToDone(string key)
    {
        Manager.deleteToDone(key);
    }


    public DataSet getDepositHeads(string deposit_id)
    {
        return Manager.getDepositHeads(deposit_id);
    }


    public DataSet getListsAll()
    {
        return Manager.getListsAll();
    }


   


    public void updateHeadCheckStatus(List<T_PaymentReceiptList> lists)
    {
        Manager.updateHeadCheckStatus(lists);
    }


    public void updateHeadCheckStatus(DataSet ds)
    {
        Manager.updateHeadCheckStatus(ds);
    }


    public DataSet getUnCheckDepositLists(int CustID, int currencyID, string type)
    {
        return Manager.getUnCheckDepositLists(CustID, currencyID, type);
    }

    public void updateHeadIsPayed(List<T_PaymentReceiptList> lists)
    {
        Manager.updateHeadIsPayed(lists);
    }

    public void updateHeadIsPayed(DataSet ds)
    {
        Manager.updateHeadIsPayed(ds);
    }
}