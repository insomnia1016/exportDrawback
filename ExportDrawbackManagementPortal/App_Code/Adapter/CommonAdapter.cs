using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using System.Data;
/// <summary>
/// CommonAdapter 的摘要说明
/// </summary>
public class CommonAdapter : BaseAdapter<ICommonManager>, ICommonManager
{
	public CommonAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}



	public DataSet getUserInfoById(int person_id)
	{
		return Manager.getUserInfoById(person_id);
	}


	public DataSet getCustomers()
	{
		return Manager.getCustomers();
	}


	public DataSet getDeliveryMode()
	{
		return Manager.getDeliveryMode();
	}


	public DataSet getTaxReturnState()
	{
		return Manager.getTaxReturnState();
	}


	public string getTaxReturnStateNameByState(string code)
	{
		return Manager.getTaxReturnStateNameByState(code);
	}


	public string getTaxReturnStateCodeByName(string name)
	{
		return Manager.getTaxReturnStateCodeByName(name);
	}


	public string getDeliveryModeNameByCode(string code)
	{
		return Manager.getDeliveryModeNameByCode(code);
	}





	public DataSet getXuFang()
	{
		return Manager.getXuFang();
	}
}