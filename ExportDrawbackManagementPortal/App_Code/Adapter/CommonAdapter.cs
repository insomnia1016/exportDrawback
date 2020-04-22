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


	public DataSet getCurrency()
	{
		return Manager.getCurrency();
	}


	public DataSet getCustomersBySearchKeyName(string name)
	{
		return Manager.getCustomersBySearchKeyName(name);
	}


    public string getCurrencyByID(int id)
    {
        return Manager.getCurrencyByID(id);
    }


    public DataSet getDepartment()
    {
        return Manager.getDepartment();
    }

    public DataSet getEmp()
    {
        return Manager.getEmp();
    }


    public int getIDByName(string name)
    {
        return Manager.getIDByName(name);
    }


    public int getIDByCurrency(string name)
    {
        return Manager.getIDByCurrency(name);
    }


    public string getEmpNameByID(int id)
    {
        return Manager.getEmpNameByID(id);
    }

    public string getDepartmentNameByID(int id)
    {
        return Manager.getDepartmentNameByID(id);
    }


    public DataSet getSuppliersBySearchKeyName(string name)
    {
        return Manager.getSuppliersBySearchKeyName(name);
    }
}