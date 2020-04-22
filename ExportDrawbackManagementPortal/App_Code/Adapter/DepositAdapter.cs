using ExportDrawbackManagement.Biz.Interface;
using System.Collections.Generic;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;


/// <summary>
/// DepositAdapter 的摘要说明
/// </summary>
public class DepositAdapter : BaseAdapter<IDepositManager>, IDepositManager
{
	public DepositAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public string getLastDepositID(string key)
	{
		return Manager.getLastDepositID(key);
	}


	public DataSet getUnCheckSEOrderList(int CustID, int currencyID)
	{
		return Manager.getUnCheckSEOrderList(CustID, currencyID);
	}


	public void addDepositHead(T_DepositHead head)
	{
		Manager.addDepositHead(head);
	}

	public void insertDepositList(List<T_DepositList> lists)
	{
		Manager.insertDepositList(lists);
	}


	public void addToDone(List<T_DepositList> lists)
	{
		Manager.addToDone(lists);
	}


	public DataSet getListsAll()
	{
		return Manager.getListsAll();
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


	public DataSet getDepositHeads(string deposit_id)
	{
		return Manager.getDepositHeads(deposit_id);
	}


	public void deleteToDone(string key)
	{
		Manager.deleteToDone(key);
	}


	public DataSet getDoneDepositID(string customer_name)
	{
		return Manager.getDoneDepositID(customer_name);
	}


    public DataSet getUnCheckDepositLists(int CustID, int currencyID, string type)
	{
		return Manager.getUnCheckDepositLists(CustID, currencyID,type);
	}


	public void updateHeadCheckStatus(List<T_ReceiptList> lists)
	{
		Manager.updateHeadCheckStatus(lists);
	}


	public void updateHeadCheckStatus(DataSet ds)
	{
		Manager.updateHeadCheckStatus(ds);
	}


    public void updateHeadIsPayed(List<T_ReceiptList> lists)
    {
        Manager.updateHeadIsPayed(lists);
    }

    public void updateHeadIsPayed(DataSet ds)
    {
        Manager.updateHeadIsPayed(ds);
    }
}