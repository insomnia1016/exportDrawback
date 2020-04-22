using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// ReceiptAdapter 的摘要说明
/// </summary>
public class ReceiptAdapter : BaseAdapter<IReceiptManager>, IReceiptManager
{
	public ReceiptAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

	public DataSet getReceipts()
	{
		return Manager.getReceipts();
	}

	public void addReceipt(T_Receipt item)
	{
		Manager.addReceipt(item);
	}

	public void updateReceipt(T_Receipt item)
	{
		Manager.updateReceipt(item);
	}

	public void DeleteReceiptById(int id)
	{
		Manager.DeleteReceiptById(id);
	}


	public string getReceiptNameByType(string code)
	{
		return Manager.getReceiptNameByType(code);
	}
}