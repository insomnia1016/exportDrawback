using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
    class PaymentRequestManager : BaseManager<T_PaymentRequest>, IPaymentRequestManager
    {
       

        public DataSet getUnCheckPaymentRequestLists()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT  *
                            FROM    [dbo].[payment_request]
                            WHERE   isUserd = 0
                            ORDER BY  payment_date DESC;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public void updateHeadCheckStatus(List<T_PaymentReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[payment_request]
						   SET [isUserd] = @isUserd
						 WHERE [payment_id] = @payment_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_PaymentReceiptList item in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@isUserd", DbType.Boolean, true);
                        db.AddInParameter(cmd, "@payment_id", DbType.String, item.RequestID);
                        db.ExecuteNonQuery(cmd);
                    }

                }
                catch
                {
                    throw new Exception("更新付款通知书isUserd数据失败");
                }
            }
        }
        public void updateHeadCheckStatus(DataSet ds)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[payment_request]
						   SET [isUserd] = @isUserd
						 WHERE [payment_id] = @payment_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@isUserd", DbType.Boolean, false);
                        db.AddInParameter(cmd, "@payment_id", DbType.String, dr["RequestID"].ToString());
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("初始化付款通知书isUserd数据失败");
                }
            }
        }

        public void delete(string key)
        {
            Database db = Dao.GetDatabase();
            string sql = @"delete FROM [dbo].[payment_request] WHERE payment_id=@payment_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@payment_id", DbType.String, key);
                    db.ExecuteNonQuery(cmd);

                }
                catch
                {
                    throw new Exception("根据付款通知书主键删除付款通知书信息出错");
                }
            }
        }
    }
}
