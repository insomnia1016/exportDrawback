using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;


/// <summary>
/// PaymentRequestAdapter 的摘要说明
/// </summary>
public class PaymentRequestAdapter : BaseAdapter<IPaymentRequestManager>, IPaymentRequestManager
{
	public PaymentRequestAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}




    public void updateHeadCheckStatus(List<T_PaymentReceiptList> lists)
    {
        Manager.updateHeadCheckStatus(lists);
    }

    public void updateHeadCheckStatus(DataSet ds)
    {
        Manager.updateHeadCheckStatus(ds);
    }


    public DataSet getUnCheckPaymentRequestLists()
    {
        return Manager.getUnCheckPaymentRequestLists();
    }


    public void delete(string key)
    {
        Manager.delete(key);
    }
}