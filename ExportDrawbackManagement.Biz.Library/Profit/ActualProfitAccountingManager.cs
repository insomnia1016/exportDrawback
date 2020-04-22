using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
    class ActualProfitAccountingManager : BaseManager<T_ActualProfitAccounting>, IActualProfitAccountingManager
    {
        public DataSet getData(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @" SELECT  * FROM [dbo].[ActualProfitAccounting]
                            WHERE  1=1   ";
            if (!string.IsNullOrEmpty(sale_bill_no))
            {
                sql += " and sale_bill_no = @sale_bill_no;";
            }
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    if (!string.IsNullOrEmpty(sale_bill_no))
                    {
                        db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
                    } 
                   return  db.ExecuteDataSet(cmd);

                }

            }
            catch
            {
                throw new Exception("获取实际利润核算表数据失败。");
            }
        }
        public void updateData(T_ActualProfitAccounting item)
        {
            Database db = Dao.GetDatabase();
            string sql = @" UPDATE [dbo].[ActualProfitAccounting]
                               SET [actual_amount] = @actual_amount
                                  ,[return_tax] = @return_tax
                                  ,[actual_pay] = @actual_pay
                                  ,[extra_charges] = @extra_charges
                                  ,[commission] = @commission
                                  ,[actual_profit_amount] = @actual_profit_amount
                                  ,[actual_profit] = @actual_profit
                             WHERE [sale_bill_no] = @sale_bill_no ";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, item.SaleBillNo);
                    db.AddInParameter(cmd, "@actual_amount", DbType.Decimal, item.ActualAmount);
                    db.AddInParameter(cmd, "@return_tax", DbType.Decimal, item.ReturnTax);
                    db.AddInParameter(cmd, "@actual_pay", DbType.Decimal, item.ActualPay);
                    db.AddInParameter(cmd, "@extra_charges", DbType.Decimal, item.ExtraCharges);
                    db.AddInParameter(cmd, "@commission", DbType.Decimal, item.Commission);
                    db.AddInParameter(cmd, "@actual_profit_amount", DbType.Decimal, item.ActualProfitAmount);
                    db.AddInParameter(cmd, "@actual_profit", DbType.Decimal, item.ActualProfit);
                    db.ExecuteNonQuery(cmd);

                }

            }
            catch
            {
                throw new Exception("更新实际利润核算表数据失败。");
            }
        }
        public void addData(T_ActualProfitAccounting item)
        {
            Database db = Dao.GetDatabase();
            string sql = @" INSERT INTO [dbo].[ActualProfitAccounting]
                                   ([sale_bill_no]
                                   ,[actual_amount]
                                   ,[return_tax]
                                   ,[actual_pay]
                                   ,[extra_charges]
                                   ,[commission]
                                   ,[actual_profit_amount]
                                   ,[actual_profit]
                                   ,[audit_status])
                             VALUES
                                   (@sale_bill_no
                                   ,@actual_amount
                                   ,@return_tax
                                   ,@actual_pay
                                   ,@extra_charges
                                   ,@commission
                                   ,@actual_profit_amount
                                   ,@actual_profit
                                   ,@audit_status) ";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, item.SaleBillNo);
                    db.AddInParameter(cmd, "@actual_amount", DbType.Decimal, item.ActualAmount);
                    db.AddInParameter(cmd, "@return_tax", DbType.Decimal, item.ReturnTax);
                    db.AddInParameter(cmd, "@actual_pay", DbType.Decimal, item.ActualPay);
                    db.AddInParameter(cmd, "@extra_charges", DbType.Decimal, item.ExtraCharges);
                    db.AddInParameter(cmd, "@commission", DbType.Decimal, item.Commission);
                    db.AddInParameter(cmd, "@actual_profit_amount", DbType.Decimal, item.ActualProfitAmount);
                    db.AddInParameter(cmd, "@actual_profit", DbType.Decimal, item.ActualProfit);
                    db.AddInParameter(cmd, "@audit_status", DbType.Boolean, item.AuditStatus);
                    db.ExecuteNonQuery(cmd);

                }

            }
            catch
            {
                throw new Exception("新增实际利润核算表数据失败。");
            }
        }
        public void audit(string sale_bill_no, bool flag)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE  [dbo].[ActualProfitAccounting]
							SET	   [audit_status] = @audit_status
							WHERE   sale_bill_no = @sale_bill_no;";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
                    db.AddInParameter(cmd, "@audit_status", DbType.Boolean, flag);
                    db.ExecuteNonQuery(cmd);

                }

            }
            catch
            {
                throw new Exception("审批实际利润核算表数据失败。");
            }
        }

        public void delete(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE FROM [dbo].[ActualProfitAccounting] WHERE sale_bill_no = @sale_bill_no ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
    }
}
