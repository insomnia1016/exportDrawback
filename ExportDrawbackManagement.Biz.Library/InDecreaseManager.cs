using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
	class InDecreaseManager : BaseManager<T_IndecreaseHead>, IInDecreaseManager
	{
		public void insertInDecreaseList(DataTable dt)
		{
			Database db = Dao.GetDatabase();
			string sql = @"INSERT INTO [dbo].[Indecrease_list]
							(
								[bill_no],
								[g_no],
								[amount],
								[name],
								[type],
								[apply_date],
								[note]
							)
							VALUES
							(@bill_no, @g_no, @amount, @NAME, @TYPE, @apply_date, @note);";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{

					foreach (DataRow dr in dt.Rows)
					{
						DbCommand cmd = db.GetSqlStringCommand(sql);
						db.AddInParameter(cmd, "@bill_no", DbType.String, dr["bill_no"].ToString());
						db.AddInParameter(cmd, "@g_no", DbType.Int32, Int32.Parse(dr["g_no"].ToString()));
						db.AddInParameter(cmd, "@amount", DbType.Decimal, Decimal.Parse(dr["amount"].ToString()));
						db.AddInParameter(cmd, "@NAME", DbType.String, dr["name"].ToString());
						db.AddInParameter(cmd, "@TYPE", DbType.String, dr["type"].ToString());
						db.AddInParameter(cmd, "@apply_date", DbType.DateTime, DateTime.Parse(dr["apply_date"].ToString()));
						db.AddInParameter(cmd, "@note", DbType.String, dr["note"].ToString());

						db.ExecuteNonQuery(cmd);
					}


				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		public DataSet getListsAll()
		{
			Database db = Dao.GetDatabase();
            string sql = @"SELECT  a.*,b.check_status
                            FROM    [dbo].[Indecrease_list] a ,
                                    [dbo].[Indecrease_head] b
                            WHERE   a.bill_no = b.bill_no
                            ORDER BY bill_no DESC ,
                                    g_no ASC,
		                            apply_date DESC";
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
					throw new Exception("获取全部增减单明细表失败");
				}
			}
		}
		public void delete(string bill_no, int g_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"DELETE FROM [dbo].[Indecrease_list] WHERE bill_no = @bill_no and g_no = @g_no ";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@bill_no", DbType.String, bill_no);
					db.AddInParameter(cmd, "@g_no", DbType.Int32, g_no);
					db.ExecuteNonQuery(cmd);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		public void deleteHead(string bill_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"DELETE FROM [dbo].[Indecrease_head] WHERE bill_no = @bill_no";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@bill_no", DbType.String, bill_no);
					db.ExecuteNonQuery(cmd);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
		public DataSet getInDecreaseHeadByBillNo(string bill_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"SELECT * FROM [dbo].[Indecrease_head] where  bill_no = @bill_no";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@bill_no", DbType.String, bill_no);
					DataSet ds = db.ExecuteDataSet(cmd);
					return ds;

				}
				catch
				{
					throw new Exception("根据增减单编号获取增减单表头信息失败");
				}
			}
		}
		public void addInDecreaseHead(T_IndecreaseHead head)
		{

			Database db = Dao.GetDatabase();
			string sql = @"INSERT INTO [dbo].[Indecrease_head]
							   ([bill_no]
							   ,[itemID]
							   ,[customerID]
							   ,[customer]
                               ,[currencyID]
							   ,[amount_all]
							   ,[agenter]
							   ,[agent_date]
							   ,[check_status])
						 VALUES
							   (@bill_no
							   ,@itemID
							   ,@customerID
							   ,@customer
                               ,@currencyID
							   ,@amount_all
							   ,@agenter
							   ,@agent_date
							   ,@check_status)";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{

					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@bill_no", DbType.String, head.BillNo);
					db.AddInParameter(cmd, "@itemID", DbType.Int32, head.ItemID);
					db.AddInParameter(cmd, "@customerID", DbType.Int32, head.CustomerID);
                    db.AddInParameter(cmd, "@customer", DbType.String, head.Customer);
                    db.AddInParameter(cmd, "@currencyID", DbType.String, head.CurrencyID);
					db.AddInParameter(cmd, "@amount_all", DbType.Decimal, head.AmountAll);
					db.AddInParameter(cmd, "@agenter", DbType.String, head.Agenter);
					db.AddInParameter(cmd, "@agent_date", DbType.DateTime, head.AgentDate);
					db.AddInParameter(cmd, "@check_status", DbType.Int32, head.CheckStatus);

					db.ExecuteNonQuery(cmd);


				}
			}
			catch
			{
				throw new Exception("添加增减单表头数据失败,请检查人品");
			}
		}

		public void updateHead(T_IndecreaseHead item)
		{
			Database db = Dao.GetDatabase();
			string sql = @"UPDATE [dbo].[Indecrease_head]
						   SET [itemID] = @itemID
							  ,[customerID] = @customerID
							  ,[customer] = @customer
							  ,[amount_all] = @amount_all
							  ,[agenter] = @agenter
							  ,[agent_date] = @agent_date
						 WHERE [bill_no] = @bill_no
						";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@itemID", DbType.Int32, item.ItemID);
					db.AddInParameter(cmd, "@customerID", DbType.Int32, item.CustomerID);
					db.AddInParameter(cmd, "@customer", DbType.String, item.Customer);
					db.AddInParameter(cmd, "@amount_all", DbType.Decimal, item.AmountAll);
					db.AddInParameter(cmd, "@agenter", DbType.String, item.Agenter);
					db.AddInParameter(cmd, "@agent_date", DbType.DateTime, item.AgentDate);
					db.AddInParameter(cmd, "@bill_no", DbType.String, item.BillNo);
					db.ExecuteNonQuery(cmd);
				}
				catch 
				{
					throw new Exception("更新增减单表头数据失败,请检查人品");
				}
			}
		}

        public void updateHeadCheckStatus(List<T_ReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[Indecrease_head]
						   SET [check_status] = @check_status
						 WHERE [bill_no] = @bill_no
						";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_ReceiptList item in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@check_status", DbType.Int32, 2);
                        db.AddInParameter(cmd, "@bill_no", DbType.String, item.IndecreaseNo);
                        db.ExecuteNonQuery(cmd);
                    }
                   
                }
                catch
                {
                    throw new Exception("更新增减单表头check_status数据失败");
                }
            }
        }
        public void updateHeadCheckStatus(DataSet ds)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[Indecrease_head]
						   SET [check_status] = @check_status
						 WHERE [bill_no] = @bill_no";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@check_status", DbType.Int32, 0);
                        db.AddInParameter(cmd, "@bill_no", DbType.String, dr["InDecrease_no"].ToString());
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("初始化增减单表头check_status数据失败");
                }
            }
        }

		public void updateHeadAmountAll(string bill_no,decimal amount_all)
		{
			Database db = Dao.GetDatabase();
			string sql = @"UPDATE [dbo].[Indecrease_head]
						   SET [amount_all] = @amount_all
						 WHERE [bill_no] = @bill_no
						";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@amount_all", DbType.Decimal, amount_all);
					db.AddInParameter(cmd, "@bill_no", DbType.String, bill_no);
					db.ExecuteNonQuery(cmd);
				}
				catch
				{
					throw new Exception("删除表体后重新计算增减单表头金额失败");
				}
			}
		}

		public string getLastBillNo(string key)
		{
			Database db = Dao.GetDatabase();
			string lastBillNo = string.Empty;
			string sql = @"SELECT * FROM [dbo].[Indecrease_head] where  bill_no like @bill_no order by bill_no desc";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@bill_no", DbType.String, key+"%");
					DataSet ds = db.ExecuteDataSet(cmd);
					if (ds.Tables[0].Rows.Count > 0)
					{
						string number = ds.Tables[0].Rows[0]["bill_no"].ToString().Substring(8, 2);
						int xuhao = Int32.Parse(number)+1;
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
					throw new Exception("根据当前增减单最大编号失败");
				}
			}
		}
        public DataSet getUnCheckInDecreaseLists(int CustID,int currencyID)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT  *
                            FROM    [dbo].[Indecrease_head]
                            WHERE   check_status <> 2
                                    AND currencyID = @currencyID
                                    AND itemID = @itemID
                            ORDER BY check_status DESC ,
                                    agent_date DESC;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@itemID", DbType.Int32, CustID);
                db.AddInParameter(cmd, "@currencyID", DbType.Int32, currencyID);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
		public DataSet getInDecreaseInfoByBillNo(string bill_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"SELECT * FROM [dbo].[Indecrease_list] where  bill_no = @bill_no";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@bill_no", DbType.String, bill_no);
					DataSet ds = db.ExecuteDataSet(cmd);
					return ds;

				}
				catch
				{
					throw new Exception("根据增减单编号获取增减单表体信息失败");
				}
			}
		}

		public void updateList(T_IndecreaseList item)
		{
			Database db = Dao.GetDatabase();
			string sql = @"UPDATE [dbo].[Indecrease_list]
						   SET [amount] = @amount
							  ,[name] = @NAME
							  ,[type] = @TYPE
							  ,[apply_date] = @apply_date
							  ,[note] = @note
						 WHERE [bill_no] = @bill_no
								AND [g_no] = @g_no ";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@amount", DbType.Decimal, item.Amount);
					db.AddInParameter(cmd, "@NAME", DbType.String, item.Name);
					db.AddInParameter(cmd, "@TYPE", DbType.String, item.Type);
					db.AddInParameter(cmd, "@apply_date", DbType.DateTime, item.ApplyDate);
					db.AddInParameter(cmd, "@note", DbType.String, item.Note);
					db.AddInParameter(cmd, "@bill_no", DbType.String, item.BillNo);
					db.AddInParameter(cmd, "@g_no", DbType.Int32, item.GNo);
					db.ExecuteNonQuery(cmd);
				}
				catch
				{
					throw new Exception("更新增减单表体数据失败,请检查人品");
				}
			}
		}
	}
}
