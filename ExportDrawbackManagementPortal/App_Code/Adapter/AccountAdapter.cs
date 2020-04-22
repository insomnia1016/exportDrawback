using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// AccountAdapter 的摘要说明
/// </summary>
public class AccountAdapter : BaseAdapter<IAccountManager>, IAccountManager
{
	public AccountAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}



    public void insertAccount(T_Account item)
    {
        Manager.insertAccount(item);
    }

    public DataSet getAccounts()
    {
        return Manager.getAccounts();
    }

    public void delete(string account_id)
    {
        Manager.delete(account_id);
    }

    public void update(T_Account item)
    {
        Manager.update(item);
    }


    public DataSet getSettlementList()
    {
        return Manager.getSettlementList();
    }


    public DataSet getSettlementListForDDL()
    {
        return Manager.getSettlementListForDDL();
    }


    public DataSet getAccountInfoByBankAndCurrency(string opening_bank, int currencyID)
    {
        return Manager.getAccountInfoByBankAndCurrency(opening_bank, currencyID);
    }


    public void updateLists(List<T_Account> lists)
    {
        Manager.updateLists(lists);
    }


    public void log(T_SettlementLog item)
    {
        Manager.log(item);
    }

    public DataSet getAccounts(int currencyID)
    {
        return Manager.getAccounts(currencyID);
    }
}