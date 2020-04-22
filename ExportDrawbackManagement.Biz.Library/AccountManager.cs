using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
    class AccountManager : BaseManager<T_Account>, IAccountManager
    {

        public void insertAccount(T_Account item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[account]
                               ([account_id]
                               ,[account_name]
                               ,[opening_bank]
                               ,[currencyID]
                               ,[amount])
                         VALUES
                               (@account_id
                               ,@account_name
                               ,@opening_bank
                               ,@currencyID
                               ,@amount)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@currencyID", DbType.Int32, item.CurrencyID);
                    db.AddInParameter(cmd, "@amount", DbType.Decimal, item.Amount);
                    db.AddInParameter(cmd, "@account_name", DbType.String, item.AccountName);
                    db.AddInParameter(cmd, "@opening_bank", DbType.String, item.OpeningBank);
                    db.AddInParameter(cmd, "@account_id", DbType.String, item.AccountId);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataSet getAccounts()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[account]";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public DataSet getAccounts(int currencyID)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[account] WHERE [currencyID]=@currencyID ";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@currencyID", DbType.Int32, currencyID);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public DataSet getSettlementList()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT *
                            FROM account t
                            WHERE
                            (
                                SELECT COUNT(*)
                                FROM account
                                WHERE account_name = t.account_name
                                      AND opening_bank = t.opening_bank
                            )   > 1;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public DataSet getSettlementListForDDL()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT  account_name,opening_bank
                             FROM [dbo].[account]
                            GROUP BY account_name,opening_bank
                            HAVING COUNT(*)>1";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public DataSet getAccountInfoByBankAndCurrency(string opening_bank, int currencyID)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT  *
                            FROM    [dbo].[account]
                            WHERE opening_bank =@opening_bank
                            AND currencyID=@currencyID";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@opening_bank", DbType.String, opening_bank);
                db.AddInParameter(cmd, "@currencyID", DbType.Int32, currencyID);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public void delete(string account_id)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE FROM [dbo].[account] WHERE account_id = @account_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@account_id", DbType.String, account_id);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        

        public void update(T_Account item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[account]
                           SET [account_name] = @account_name
                              ,[opening_bank] = @opening_bank
                              ,[currencyID] = @currencyID
                              ,[amount] = @amount
                         WHERE [account_id] = @account_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@currencyID", DbType.Int32, item.CurrencyID);
                    db.AddInParameter(cmd, "@amount", DbType.Decimal, item.Amount);
                    db.AddInParameter(cmd, "@account_name", DbType.String, item.AccountName);
                    db.AddInParameter(cmd, "@opening_bank", DbType.String, item.OpeningBank);
                    db.AddInParameter(cmd, "@account_id", DbType.String, item.AccountId);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("更新账户数据失败,请检查人品");
                }
            }
        }
        public void updateLists(List<T_Account> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[account]
                           SET [account_name] = @account_name
                              ,[opening_bank] = @opening_bank
                              ,[currencyID] = @currencyID
                              ,[amount] = @amount
                         WHERE [account_id] = @account_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_Account list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@currencyID", DbType.Int32, list.CurrencyID);
                        db.AddInParameter(cmd, "@amount", DbType.Decimal, list.Amount);
                        db.AddInParameter(cmd, "@account_name", DbType.String, list.AccountName);
                        db.AddInParameter(cmd, "@opening_bank", DbType.String, list.OpeningBank);
                        db.AddInParameter(cmd, "@account_id", DbType.String, list.AccountId);
                        db.ExecuteNonQuery(cmd);
                    }

                }
                catch
                {
                    throw new Exception("更新账户数据失败,请检查人品");
                }
            }
        }

        public void log(T_SettlementLog item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[settlement_log]
                                   ([out_account_id]
                                   ,[out_account_name]
                                   ,[out_opening_bank]
                                   ,[out_currencyID]
                                   ,[out_amount]
                                   ,[out_settlement_amount]
                                   ,[in_account_id]
                                   ,[in_account_name]
                                   ,[in_opening_bank]
                                   ,[in_currencyID]
                                   ,[in_amount]
                                   ,[in_settlement_amount]
                                   ,[operate_time]
                                   ,[operater]
                                   ,[exchange_rate])
                             VALUES
                                   (@out_account_id
                                   ,@out_account_name
                                   ,@out_opening_bank
                                   ,@out_currencyID
                                   ,@out_amount
                                   ,@out_settlement_amount
                                   ,@in_account_id
                                   ,@in_account_name
                                   ,@in_opening_bank
                                   ,@in_currencyID
                                   ,@in_amount
                                   ,@in_settlement_amount
                                   ,@operate_time
                                   ,@operater
                                   ,@exchange_rate) ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@out_account_id", DbType.String, item.OutAccountId);
                    db.AddInParameter(cmd, "@out_account_name", DbType.String, item.OutAccountName);
                    db.AddInParameter(cmd, "@out_opening_bank", DbType.String, item.OutOpeningBank);
                    db.AddInParameter(cmd, "@out_currencyID", DbType.Int32, item.OutCurrencyid);
                    db.AddInParameter(cmd, "@out_amount", DbType.Decimal, item.OutAmount);
                    db.AddInParameter(cmd, "@out_settlement_amount", DbType.Decimal, item.OutSettlementAmount);
                    db.AddInParameter(cmd, "@in_account_id", DbType.String, item.InAccountId);
                    db.AddInParameter(cmd, "@in_account_name", DbType.String, item.InAccountName);
                    db.AddInParameter(cmd, "@in_opening_bank", DbType.String, item.InOpeningBank);
                    db.AddInParameter(cmd, "@in_currencyID", DbType.Int32, item.InCurrencyid);
                    db.AddInParameter(cmd, "@in_amount", DbType.Decimal, item.InAmount);
                    db.AddInParameter(cmd, "@in_settlement_amount", DbType.Decimal, item.InSettlementAmount);
                    db.AddInParameter(cmd, "@operate_time", DbType.DateTime, item.OperateTime);
                    db.AddInParameter(cmd, "@operater", DbType.String, item.Operater);
                    db.AddInParameter(cmd, "@exchange_rate", DbType.Decimal, item.ExchangeRate);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("记录操作日志数据失败,请检查人品");
                }
            }
        }
    }
}
