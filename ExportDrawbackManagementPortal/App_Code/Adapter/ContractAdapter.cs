using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;
/// <summary>
/// ContractAdapter 的摘要说明
/// </summary>
public class ContractAdapter:BaseAdapter<IContractManager>,IContractManager
{
	public ContractAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public void addContractList(List<T_ContractList> lists)
	{
		Manager.addContractList(lists);
	}

	public void addContractHead(T_ContractHead head)
	{
		Manager.addContractHead(head);
	}


	public DataSet getContractSummary()
	{
		return Manager.getContractSummary();
	}


	public DataSet queryContractSummary(T_ContractHead head)
	{
		return Manager.queryContractSummary(head);
	}


	public T_ContractHead getContractDetail(string id)
	{
		return Manager.getContractDetail(id);
	}
}