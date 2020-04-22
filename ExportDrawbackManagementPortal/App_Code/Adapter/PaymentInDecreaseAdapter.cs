using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;


/// <summary>
/// InDecreaseAdapter 的摘要说明
/// </summary>
public class PaymentInDecreaseAdapter : BaseAdapter<IPaymentInDecreaseManager>, IPaymentInDecreaseManager
{
    public PaymentInDecreaseAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public void insertInDecreaseList(DataTable dt)
	{
		Manager.insertInDecreaseList(dt);
	}

	public void addInDecreaseHead(T_PaymentIndecreaseHead head)
	{
		Manager.addInDecreaseHead(head);
	}

	public DataSet getListsAll()
	{
		return Manager.getListsAll();
	}

	public void delete(string bill_no, int g_no)
	{
		Manager.delete(bill_no, g_no);
	}


	public DataSet getInDecreaseHeadByBillNo(string bill_no)
	{
		return Manager.getInDecreaseHeadByBillNo(bill_no);
	}


    public void updateList(T_PaymentIndecreaseList item)
	{
		Manager.updateList(item);
	}

	public void updateHead(T_PaymentIndecreaseHead item)
	{
		Manager.updateHead(item);
	}


	public DataSet getInDecreaseInfoByBillNo(string bill_no)
	{
		return Manager.getInDecreaseInfoByBillNo(bill_no);
	}


	public void updateHeadAmountAll(string bill_no, decimal amount_all)
	{
		Manager.updateHeadAmountAll(bill_no, amount_all);
	}


	public void deleteHead(string bill_no)
	{
		Manager.deleteHead(bill_no);
	}


	public string getLastBillNo(string key)
	{
		return Manager.getLastBillNo(key);
	}


    public DataSet getUnCheckInDecreaseLists(int CustID, int currencyID)
    {
        return Manager.getUnCheckInDecreaseLists(CustID, currencyID);
    }


    public void updateHeadCheckStatus(List<T_PaymentReceiptList> lists)
    {
        Manager.updateHeadCheckStatus(lists);
    }


    public void updateHeadCheckStatus(DataSet ds)
    {
        Manager.updateHeadCheckStatus(ds);
    }



}