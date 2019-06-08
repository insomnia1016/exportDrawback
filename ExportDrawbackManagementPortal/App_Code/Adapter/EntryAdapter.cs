using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;
/// <summary>
/// EntryAdapter 的摘要说明
/// </summary>
public class EntryAdapter:BaseAdapter<IEntryManager>,IEntryManager
{
	public EntryAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public void addEntryList(T_EntryList entity)
	{
		Manager.addEntryList(entity);
	}


	public DataSet getEntryList()
	{
		return Manager.getEntryList();
	}


	public void invoice(List<T_ContractList> lists)
	{
		Manager.invoice(lists);
	}


	public DataSet getListsAll()
	{
		return Manager.getListsAll();
	}


	public DataSet queryEntryList(T_EntryList item)
	{
		return Manager.queryEntryList(item);
	}
}