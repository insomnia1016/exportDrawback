using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// ActualProfitAccountingAdapter 的摘要说明
/// </summary>
public class ActualProfitAccountingAdapter : BaseAdapter<IActualProfitAccountingManager>, IActualProfitAccountingManager
{
	public ActualProfitAccountingAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet getData(string sale_bill_no)
    {
        return Manager.getData(sale_bill_no);
    }


    public void addData(T_ActualProfitAccounting item)
    {
        Manager.addData(item);
    }


    public void updateData(T_ActualProfitAccounting item)
    {
        Manager.updateData(item);
    }


    public void audit(string sale_bill_no, bool flag)
    {
        Manager.audit(sale_bill_no, flag);
    }


    public void delete(string sale_bill_no)
    {
        Manager.delete(sale_bill_no);
    }
}