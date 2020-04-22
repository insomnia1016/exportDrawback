using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// PaymentTypeAdapter 的摘要说明
/// </summary>
public class PaymentTypeAdapter : BaseAdapter<IPaymentTypeManager>, IPaymentTypeManager
{
	public PaymentTypeAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet getTypes()
    {
        return Manager.getTypes();
    }

   

    public void updateType(T_PaymentType item)
    {
        Manager.updateType(item);
    }

    public void addType(T_PaymentType item)
    {
        Manager.addType(item);
    }

    public void DeleteTypeById(int id)
    {
        Manager.DeleteTypeById(id);
    }

    public string getTypeNameByType(string code)
    {
        return Manager.getTypeNameByType(code);
    }
}