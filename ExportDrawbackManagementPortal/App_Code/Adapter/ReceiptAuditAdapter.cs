using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// ReceiptAuditAdapter 的摘要说明
/// </summary>
public class ReceiptAuditAdapter : BaseAdapter<IReceiptAuditManager>, IReceiptAuditManager
{
	public ReceiptAuditAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public DataSet getUnCheckReceiptLists(int empid)
	{
		return Manager.getUnCheckReceiptLists(empid);
	}
}