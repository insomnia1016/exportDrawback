using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
	class DepositManager : BaseManager<T_DepositHead>, IDepositManager
	{
		public string getLastDepositID(string key)
		{
			Database db = Dao.GetDatabase();
			string lastBillNo = string.Empty;
			string sql = @"SELECT * FROM [dbo].[deposit_head] where  deposit_id like @deposit_id order by deposit_id desc";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@deposit_id", DbType.String, key + "%");
					DataSet ds = db.ExecuteDataSet(cmd);
					if (ds.Tables[0].Rows.Count > 0)
					{
						string number = ds.Tables[0].Rows[0]["deposit_id"].ToString().Substring(11, 2);
						int xuhao = Int32.Parse(number) + 1;
						lastBillNo = key + xuhao.ToString().PadLeft(2, '0');

					}
					else
					{
						lastBillNo = key + "01";
					}
					return lastBillNo;

				}
				catch
				{
					throw new Exception("根据当前定金单取最大编号失败");
				}
			}
		}

		public DataSet getUnCheckSEOrderList(int CustID, int currencyID)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");
			DataSet ds;
            string sql = @"SELECT MIN(a.FBillNo) AS FBillNo ,MIN(a.FDate) AS FDate,SUM(b.FAllAmount) AS FAmount,MIN(a.FCurrencyID) AS FCurrencyID,MIN(a.FPayDate) AS FPayDate,MIN(a.FNote) AS FNote
                            FROM dbo.SEOrder a,dbo.SEOrderEntry b
                            WHERE a.FBrNo=b.FBrNO 
                            AND a.FInterID=b.FInterID
                            --AND a.FCustID=@FCustID 
                            AND a.FCurrencyID=@FCurrencyID
                            GROUP BY a.FInterID
                            ORDER BY FDate DESC";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@FCustID", DbType.Int32, CustID);
				db.AddInParameter(cmd, "@FCurrencyID", DbType.Int32, currencyID);
				ds = db.ExecuteDataSet(cmd);
				return ds;
			}
		}
        public void addDepositHead(T_DepositHead head)
        {

            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[deposit_head]
                                   ([deposit_id]
                                   ,[itemID]
                                   ,[customerID]
                                   ,[customer]
                                   ,[currencyID]
                                   ,[amount_all]
                                   ,[unreceive_amount_for]
                                   ,[agenter]
                                   ,[agent_date]
                                   ,[check_status])
                             VALUES
                                   (@deposit_id
                                   ,@itemID
                                   ,@customerID
                                   ,@customer
                                   ,@currencyID
                                   ,@amount_all
                                   ,@unreceive_amount_for
                                   ,@agenter
                                   ,@agent_date
                                   ,@check_status)";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {

                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@deposit_id", DbType.String, head.DepositId);
                    db.AddInParameter(cmd, "@itemID", DbType.Int32, head.ItemID);
                    db.AddInParameter(cmd, "@customerID", DbType.Int32, head.CustomerID);
                    db.AddInParameter(cmd, "@customer", DbType.String, head.Customer);
                    db.AddInParameter(cmd, "@currencyID", DbType.String, head.CurrencyID);
                    db.AddInParameter(cmd, "@amount_all", DbType.Decimal, head.AmountAll);
                    db.AddInParameter(cmd, "@unreceive_amount_for", DbType.Decimal, head.UnreceiveAmountFor);
                    db.AddInParameter(cmd, "@agenter", DbType.String, head.Agenter);
                    db.AddInParameter(cmd, "@agent_date", DbType.DateTime, head.AgentDate);
                    db.AddInParameter(cmd, "@check_status", DbType.Int32, head.CheckStatus);

                    db.ExecuteNonQuery(cmd);


                }
            }
            catch
            {
                throw new Exception("添加定金表头数据失败,系统要炸了，倒计时开始...");
            }
        }
        public void insertDepositList(List<T_DepositList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[deposit_list]
                                   ([deposit_id]
                                   ,[g_no]
                                   ,[FBillNo]
                                   ,[amount]
                                   ,[Fdate]
                                   ,[note])
                             VALUES
                                   (@deposit_id
                                   ,@g_no
                                   ,@FBillNo
                                   ,@amount
                                   ,@Fdate
                                   ,@note)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_DepositList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@deposit_id", DbType.String, list.DepositId);
                        db.AddInParameter(cmd, "@g_no", DbType.Int32, list.GNo);
                        db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
                        db.AddInParameter(cmd, "@amount", DbType.Decimal, list.Amount);
                        db.AddInParameter(cmd, "@Fdate", DbType.DateTime, list.Fdate);
                        db.AddInParameter(cmd, "@note", DbType.String, list.Note);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("批量增加定金表体出错，不要找我。");
                }
            }
        }
        public void addToDone(List<T_DepositList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[SEOrderNODone]
                               ([FBillNo]
                               ,[deposit_id]
                               ,[insert_time]
                               ,[customer_code]
                               ,[customer_name])
                         VALUES
                               (@FBillNo
                               ,@deposit_id
                               ,@insert_time
                               ,@customer_code
                               ,@customer_name)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_DepositList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
                        db.AddInParameter(cmd, "@deposit_id", DbType.String, list.DepositId);
                        db.AddInParameter(cmd, "@insert_time", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "@customer_code", DbType.String, list.CustomerCode);
                        db.AddInParameter(cmd, "@customer_name", DbType.String, list.CustomerName);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("批量增加已收定金的销售订单号出错，不要找我。");
                }
            }
        }

        public DataSet getDoneDepositID(string customer_name)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[SEOrderNODone] where customer_name=@customer_name";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@customer_name", DbType.String, customer_name);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public DataSet getUnCheckDepositLists(int CustID, int currencyID, string type)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT  *
                            FROM    [dbo].[deposit_head]
                            WHERE   check_status <> 2
                                    AND currencyID = @currencyID
                                    AND itemID = @itemID ";
            if (type == "B")
            {
                sql += "  and isPayed = 0 ";
            }
            else
            {
                sql += "  and isPayed = 1 ";
            }
            sql += "   ORDER BY check_status DESC , agent_date DESC;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@itemID", DbType.Int32, CustID);
                db.AddInParameter(cmd, "@currencyID", DbType.Int32, currencyID);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public DataSet getListsAll()
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[deposit_head]";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;

                }
                catch
                {
                    throw new Exception("获取全部定金单明细表失败");
                }
            }
        }

        public DataSet getDepositHeadByKey(string key)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[deposit_head] WHERE deposit_id=@deposit_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@deposit_id", DbType.String, key);

                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;

                }
                catch
                {
                    throw new Exception("根据定金号获取定金表头信息出错");
                }
            }
        }

        public DataSet getDepositListByKey(string key)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[deposit_list] WHERE deposit_id=@deposit_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@deposit_id", DbType.String, key);

                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;

                }
                catch
                {
                    throw new Exception("根据定金号获取定金表体信息出错");
                }
            }
        }
        public DataSet getDepositHeads(string deposit_id)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[deposit_head] WHERE deposit_id LIKE @deposit_id;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@deposit_id", DbType.String, "%" + deposit_id + "%");
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }
        //deleteToDone
        public void deleteToDone(string key)
        {
            Database db = Dao.GetDatabase();
            string sql = @"delete FROM [dbo].[SEOrderNODone] WHERE deposit_id=@deposit_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@deposit_id", DbType.String, key);
                    db.ExecuteNonQuery(cmd);

                }
                catch
                {
                    throw new Exception("根据定金号删除已做定金单的销售订单号出错。");
                }
            }
        }
        public void delete(string key)
        {
            Database db = Dao.GetDatabase();
            string sql = @"delete FROM [dbo].[deposit_head] WHERE deposit_id=@deposit_id;
                           delete FROM [dbo].[deposit_list] WHERE deposit_id=@deposit_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@deposit_id", DbType.String, key);
                    db.ExecuteNonQuery(cmd);

                }
                catch
                {
                    throw new Exception("根据定金号删除定金表头表体信息出错");
                }
            }
        }
        public void updateHeadCheckStatus(List<T_ReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[deposit_head]
						   SET [check_status] = @check_status
						 WHERE [deposit_id] = @deposit_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_ReceiptList item in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@check_status", DbType.Int32, item.FCheckStatus);
                        db.AddInParameter(cmd, "@deposit_id", DbType.String, item.DepositId);
                        db.ExecuteNonQuery(cmd);
                    }

                }
                catch
                {
                    throw new Exception("更新定金单表头check_status数据失败");
                }
            }
        }
        public void updateHeadCheckStatus(DataSet ds)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[deposit_head]
						   SET [check_status] = @check_status
						 WHERE [deposit_id] = @deposit_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@check_status", DbType.Int32, 0);
                        db.AddInParameter(cmd, "@deposit_id", DbType.String, dr["deposit_id"].ToString());
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("初始化定金单表头check_status数据失败");
                }
            }
        }
        public void updateHeadIsPayed(List<T_ReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[deposit_head]
                            SET [isPayed] = @isPayed,
                            [check_amount_for] = @check_amount_for
                            WHERE [deposit_id] = @deposit_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_ReceiptList item in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@isPayed", DbType.Boolean, true);
                        db.AddInParameter(cmd, "@check_amount_for", DbType.Decimal, item.FCheckAmountFor);
                        db.AddInParameter(cmd, "@deposit_id", DbType.String, item.DepositId);
                        db.ExecuteNonQuery(cmd);
                    }

                }
                catch
                {
                    throw new Exception("更新定金单表头isPayed数据失败");
                }
            }
        }
        public void updateHeadIsPayed(DataSet ds)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[deposit_head]
                            SET [isPayed] = @isPayed,
                            [check_amount_for] = @check_amount_for
                            WHERE [deposit_id] = @deposit_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@isPayed", DbType.Boolean, false);
                        db.AddInParameter(cmd, "@check_amount_for", DbType.Decimal, 0);
                        db.AddInParameter(cmd, "@deposit_id", DbType.String, dr["deposit_id"].ToString());
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("初始化定金单表头isPayed数据失败");
                }
            }
        }
	}
}
