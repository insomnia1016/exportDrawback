using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// ProfitAccountingAdapter 的摘要说明
/// </summary>
public class ProfitAccountingAdapter : BaseAdapter<IProfitAccountingManager>, IProfitAccountingManager
{
	public ProfitAccountingAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
	public DataSet getSEOrderInfo(T_ProfitAccounting item)
	{
		return Manager.getSEOrderInfo(item);
	}
	public DataSet getProfitBudgetList(List<int> lists)
	{
		return Manager.getProfitBudgetList(lists);
	}
	public void addProfitBudgetHead(T_ProfitAccounting item)
	{
		Manager.addProfitBudgetHead(item);
	}
	public void addProfitBudgetList(List<T_ProfitAccountingList> lists)
	{
		Manager.addProfitBudgetList(lists);
	}
	public void updateProfitBudgetList(List<T_ProfitAccountingList> lists)
	{
		Manager.updateProfitBudgetList(lists);
	}
	public void updateProfitBudgetHead(T_ProfitAccounting item)
	{
		Manager.updateProfitBudgetHead(item);
	}
	public DataSet getProfitBudgetByID(int FInterIID, string sale_bill_no)
	{
		return Manager.getProfitBudgetByID(FInterIID, sale_bill_no);
	}
	public void deleteBySaleBillNo(string sale_bill_no)
	{
		Manager.deleteBySaleBillNo(sale_bill_no);
	}
	public void audit(string sale_bill_no, bool flag)
	{
		Manager.audit(sale_bill_no, flag);
	}
	public DataSet getProfitBudgetSummary()
	{
	   return Manager.getProfitBudgetSummary();
	}
	public DataSet getProfitBudgetSummaryByID(string sale_bill_no)
	{
		return Manager.getProfitBudgetSummaryByID(sale_bill_no);
	}


	public DataSet getActualProfit(string sale_bill_no)
	{
		return Manager.getActualProfit(sale_bill_no);
	}


	public void IsActualAudit(string sale_bill_no, bool flag)
	{
		Manager.IsActualAudit(sale_bill_no, flag);
	}


    public string getExtraCharges(string sale_bill_no)
    {
        return Manager.getExtraCharges(sale_bill_no);
    }
}