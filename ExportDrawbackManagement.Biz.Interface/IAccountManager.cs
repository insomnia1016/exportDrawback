using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IAccountManager
    {
       void insertAccount(T_Account item);
       DataSet getAccounts();
       void delete(string account_id);
       void update(T_Account item);
       DataSet getSettlementList();
       DataSet getSettlementListForDDL();
       DataSet getAccountInfoByBankAndCurrency(string opening_bank, int currencyID);
       void updateLists(List<T_Account> lists);
       void log(T_SettlementLog item);
       DataSet getAccounts(int currencyID);
    }
}
