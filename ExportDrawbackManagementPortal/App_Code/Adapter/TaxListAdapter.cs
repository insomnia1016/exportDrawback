using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;
/// <summary>
/// TaxListAdapter 的摘要说明
/// </summary>
public class TaxListAdapter:BaseAdapter<ITaxListManager>,ITaxListManager
{
	public TaxListAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}



	public void generateTaxList(decimal bl)
	{
		Manager.generateTaxList(bl);
	}


	public DataSet getTaxList()
	{
		return Manager.getTaxList();
	}


    public void UpdateState(T_TaxList item)
    {
        Manager.UpdateState(item);
    }
}