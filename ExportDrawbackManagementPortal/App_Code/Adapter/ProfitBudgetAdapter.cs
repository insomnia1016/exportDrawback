using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// ProfitBudgetAdapter 的摘要说明
/// </summary>
public class ProfitBudgetAdapter : BaseAdapter<IProfitBudgetManager>, IProfitBudgetManager
{
	public ProfitBudgetAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public DataSet getSEOrderInfo(T_ProfitBudget item)
	{
		return Manager.getSEOrderInfo(item);
	}


	public string getDeptNameById(string dept_id)
	{
		return Manager.getDeptNameById(dept_id);
	}

	public string getEmpNameById(string emp_id)
	{
		return Manager.getEmpNameById(emp_id);
	}


	public void addProfitBudgetHead(T_ProfitBudget item)
	{
		Manager.addProfitBudgetHead(item);
	}

	public void addProfitBudgetList(List<T_ProfitBudgetList> lists)
	{
		Manager.addProfitBudgetList(lists);
	}


	public DataSet getProfitBudgetSummary()
	{
		return Manager.getProfitBudgetSummary();
	}


	public DataSet getProfitBudgetByID(int FInterID, string sale_bill_no)
	{
		return Manager.getProfitBudgetByID(FInterID, sale_bill_no);
	}


	public void updateProfitBudgetHead(T_ProfitBudget item)
	{
		Manager.updateProfitBudgetHead(item);
	}

	public void updateProfitBudgetList(List<T_ProfitBudgetList> lists)
	{
		Manager.updateProfitBudgetList(lists);
	}


	public void deleteBySaleBillNo(string sale_bill_no)
	{
		Manager.deleteBySaleBillNo(sale_bill_no);
	}


	public void audit(string sale_bill_no, bool flag)
	{
		Manager.audit(sale_bill_no, flag);
	}


	public DataSet getProfitBudgetSummaryByID(string sale_bill_no)
	{
		return Manager.getProfitBudgetSummaryByID(sale_bill_no);
	}


    public DataSet getProfitBudgetList(T_ProfitBudgetList item)
    {
        return Manager.getProfitBudgetList(item);
    }


    public void updateList(List<T_ProfitBudgetList> lists)
    {
        Manager.updateList(lists);
    }


    public DataSet getProfitBudgetList(string sale_bill_no)
    {
        return Manager.getProfitBudgetList(sale_bill_no);
    }


    public void AddUnAccountingQtyBecauseDelete(List<T_ProfitBudgetList> lists)
    {
        Manager.AddUnAccountingQtyBecauseDelete(lists);
    }
}