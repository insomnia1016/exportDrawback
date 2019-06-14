using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// CompanyAdapter 的摘要说明
/// </summary>
public class CompanyAdapter:BaseAdapter<ICompanyManager>,ICompanyManager
{
	public CompanyAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public T_Customers getCompanyInfoById(int id)
	{
		return Manager.getCompanyInfoById(id);
	}


	public DataSet getCompanyInfo()
	{
		return Manager.getCompanyInfo();
	}


	public void Save(T_Customers item)
	{
		Manager.Save(item);
	}

	public void insert(T_Customers item)
	{
		Manager.insert(item);
	}


	public void delete(int id)
	{
		Manager.delete(id);
	}


	public T_Customers getXuFangInfoById(int id)
	{
		return Manager.getXuFangInfoById(id);
	}
}