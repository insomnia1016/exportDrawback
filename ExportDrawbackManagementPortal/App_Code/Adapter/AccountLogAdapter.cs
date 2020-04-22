using ExportDrawbackManagement.Biz.Interface;
using System.Collections.Generic;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// AccountLogAdapter 的摘要说明
/// </summary>
public class AccountLogAdapter : BaseAdapter<IAccountLogManager>, IAccountLogManager
{
	public AccountLogAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public void insert(T_AccountLog item)
    {
        Manager.insert(item);
    }


    public void Add(T_AccountLog item)
    {
        Manager.Add(item);
    }


    public DataSet getAccountLogs(string account_id, string start_time, string end_time)
    {
        return Manager.getAccountLogs(account_id, start_time, end_time);
    }


    public DataSet getAccount(string account_id)
    {
        return Manager.getAccount(account_id);
    }
}