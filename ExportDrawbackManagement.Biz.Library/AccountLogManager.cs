using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Library
{
    class AccountLogManager:BaseManager<T_AccountLog>,IAccountLogManager
    {
        public void insert(T_AccountLog item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[account_log]
                               ([account_id]
                               ,[receipt_id]
                               ,[operate_time]
                               ,[operater]
                               ,[amount])
                         VALUES
                               (@account_id
                               ,@receipt_id
                               ,@operate_time
                               ,@operater
                               ,@amount)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@account_id", DbType.String, item.AccountId);
                    db.AddInParameter(cmd, "@receipt_id", DbType.String, item.ReceiptId);
                    db.AddInParameter(cmd, "@operate_time", DbType.DateTime, item.OperateTime);
                    db.AddInParameter(cmd, "@operater", DbType.String, item.Operater);
                    db.AddInParameter(cmd, "@amount", DbType.Decimal, item.Amount);
                    db.ExecuteNonQuery(cmd);
                }
                catch 
                {
                    throw  new Exception("账户操作日志记录失败");
                }
            }
        }
        public void Add(T_AccountLog item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[account]
                           SET [amount] += @amount
                         WHERE [account_id] = @account_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@amount", DbType.Decimal, item.Amount);
                    db.AddInParameter(cmd, "@account_id", DbType.String, item.AccountId);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("更新账户金额数据失败,请检查人品");
                }
            }
        }

        public DataSet getAccountLogs(string account_id,string start_time,string end_time)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT  *
                            FROM    [dbo].[account_log]
                            WHERE   account_id = @account_id ";
            if (!string.IsNullOrEmpty(start_time))
            {
                sql += " AND operate_time > @start_time ";
            }
            if (!string.IsNullOrEmpty(end_time))
            {
                sql += "  AND operate_time < @end_time ";
            }
            using (DbConnection cn = db.CreateConnection())
            {

                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@account_id", DbType.String, account_id);
                    if (!string.IsNullOrEmpty(start_time))
                    {
                        db.AddInParameter(cmd, "@start_time", DbType.DateTime, DateTime.Parse(start_time));
                    }
                    if (!string.IsNullOrEmpty(end_time))
                    {
                        db.AddInParameter(cmd, "@end_time", DbType.DateTime, DateTime.Parse(end_time));
                    }
                   return  db.ExecuteDataSet(cmd);
                }
                catch
                {
                    throw new Exception("获取账户操作数据失败,请检查人品");
                }
            }
        }

        public DataSet getAccount(string account_id)
        {

            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[account] WHERE account_id=@account_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@account_id", DbType.String, account_id);
                    return  db.ExecuteDataSet(cmd);
                }
                catch
                {
                    throw new Exception("获取账户信息失败");
                }
            }
        }
    }
}
