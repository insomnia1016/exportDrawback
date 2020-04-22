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
	class ReceiptAuditManager:BaseManager<T_ReceiptHead>,IReceiptAuditManager
	{
		public DataSet getUnCheckReceiptLists(int CustID,int currencyID)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");
			DataSet ds;
			string sql = @"SELECT * FROM ICSale
							WHERE FCheckStatus<>2 
							AND FCustID=@FCustID
							AND FCurrencyID = @FCurrencyID
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


		public DataSet getDoneBillNos(string customer_name)
		{
			Database db = Dao.GetDatabase();
			DataSet ds;
			string sql = @"SELECT * FROM [dbo].[BillNoDone] where customer_name=@customer_name";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@customer_name", DbType.String, customer_name);
				ds = db.ExecuteDataSet(cmd);
				return ds;
			}
		}

		public int getIDByName(string name)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");
			DataSet ds;
			string sql = @"SELECT FItemID FROM T_Organization WHERE FName=@name";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@name", DbType.String, name);
				ds = db.ExecuteDataSet(cmd);
				if (ds.Tables[0].Rows.Count > 0)
				{
					return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
				}
				else
				{
					return 0;
				}
				
			}
		}

		public DataSet getCustInfoByName(string name)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");
			DataSet ds;
			string sql = @"SELECT * FROM T_Organization WHERE FName=@name";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@name", DbType.String, name);
				ds = db.ExecuteDataSet(cmd);
				return ds;

			}
		}

		//getLastReceiptID
		public string getLastReceiptID(string key)
		{
			Database db = Dao.GetDatabase();
			string lastReceiptID = string.Empty;
			string sql = @"SELECT * FROM [dbo].[receipt_head] where  receipt_id like @receipt_id order by receipt_id desc";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@receipt_id", DbType.String, key + "%");
					DataSet ds = db.ExecuteDataSet(cmd);
					if (ds.Tables[0].Rows.Count > 0)
					{
						string number = ds.Tables[0].Rows[0]["receipt_id"].ToString().Substring(6, 5);
						int xuhao = Int32.Parse(number) + 1;
						lastReceiptID = key + xuhao.ToString().PadLeft(5, '0');
					}
					else
					{
						lastReceiptID = key + "00001";
					}
					return lastReceiptID;

				}
				catch
				{
					throw new Exception("根据当前收款单最大编号失败");
				}
			}
		}

		public void insertReceiptList(List<T_ReceiptList> lists)
		{
			Database db = Dao.GetDatabase();
			string sql = @"INSERT INTO [dbo].[receipt_list]
							   ([receipt_id]
							   ,[receipt_no]
							   ,[FBillNo]
							   ,[InDecrease_no]
                               ,[Deposit_id]
							   ,[FDate]
							   ,[FAmountFor]
							   ,[FReceiveAmountFor]
							   ,[FUnReceiveAmountFor]
							   ,[FCheckAmountFor]
							   ,[FCurrencyID]
							   ,[FCheckStatus]
							   ,[FNote])
						 VALUES
							   (@receipt_id
							   ,@receipt_no
							   ,@FBillNo
							   ,@InDecrease_no
                               ,@Deposit_id
							   ,@FDate
							   ,@FAmountFor
							   ,@FReceiveAmountFor
							   ,@FUnReceiveAmountFor
							   ,@FCheckAmountFor
							   ,@FCurrencyID
							   ,@FCheckStatus
							   ,@FNote)";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					foreach (T_ReceiptList list in lists)
					{
						DbCommand cmd = db.GetSqlStringCommand(sql);
						db.AddInParameter(cmd, "@receipt_id", DbType.String, list.ReceiptId);
						db.AddInParameter(cmd, "@receipt_no", DbType.String, list.ReceiptNo);
						db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
                        db.AddInParameter(cmd, "@InDecrease_no", DbType.String, list.IndecreaseNo);
                        db.AddInParameter(cmd, "@Deposit_id", DbType.String, list.DepositId);
						db.AddInParameter(cmd, "@FDate", DbType.DateTime, list.FDate);
						db.AddInParameter(cmd, "@FAmountFor", DbType.Decimal, list.FAmountFor);
						db.AddInParameter(cmd, "@FReceiveAmountFor", DbType.Decimal, list.FReceiveAmountFor);
						db.AddInParameter(cmd, "@FUnReceiveAmountFor", DbType.Decimal, list.FUnReceiveAmountFor);
						db.AddInParameter(cmd, "@FCheckAmountFor", DbType.Decimal, list.FCheckAmountFor);
						db.AddInParameter(cmd, "@FCurrencyID", DbType.Int32, list.FCurrencyID);
						db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, list.FCheckStatus);
						db.AddInParameter(cmd, "@FNote", DbType.String, list.FNote);
						db.ExecuteNonQuery(cmd);
					}
				}
				catch
				{
					throw new Exception("批量增加收款单表体出错，不要找我。");
				}
			}
		}
        public void updateReceiptList(List<T_ReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[receipt_list]
                            SET [FCheckAmountFor] = @FCheckAmountFor,
                                [FCheckStatus] = @FCheckStatus,
                                [FNote] = @FNote
                            WHERE [receipt_id] = @receipt_id
                                  AND [receipt_no] = @receipt_no;";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_ReceiptList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@receipt_id", DbType.String, list.ReceiptId);
                        db.AddInParameter(cmd, "@receipt_no", DbType.String, list.ReceiptNo);
                        db.AddInParameter(cmd, "@FCheckAmountFor", DbType.Decimal, list.FCheckAmountFor);
                        db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, list.FCheckStatus);
                        db.AddInParameter(cmd, "@FNote", DbType.String, list.FNote);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("批量更新收款单表体出错，不要找我。");
                }
            }
        }
		public void addToDone(List<T_ReceiptList> lists)
		{
			Database db = Dao.GetDatabase();
			string sql = @"INSERT INTO [dbo].[BillNoDone]
								   ([FBillNo]
								   ,[receipt_id]
								   ,[insert_time]
								   ,[customer_code]
								   ,[customer_name]
								   ,[check_status])
							 VALUES
								   (@FBillNo
								   ,@receipt_id
								   ,@insert_time
								   ,@customer_code
								   ,@customer_name
								   ,@check_status)";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					foreach (T_ReceiptList list in lists)
					{
						DbCommand cmd = db.GetSqlStringCommand(sql);
						db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
						db.AddInParameter(cmd, "@receipt_id", DbType.String, list.ReceiptId);
						db.AddInParameter(cmd, "@insert_time", DbType.DateTime, DateTime.Now);
						db.AddInParameter(cmd, "@customer_code", DbType.String, list.CustomerCode);
						db.AddInParameter(cmd, "@customer_name", DbType.String, list.CustomerName);
						db.AddInParameter(cmd, "@check_status", DbType.Int32, list.FCheckStatus);
						db.ExecuteNonQuery(cmd);
					}
				}
				catch
				{
					throw new Exception("批量增加已收款的销售发票单号出错，不要找我。");
				}
			}
		}

        public void updateToDone(List<T_ReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[BillNoDone]
                               SET [check_status] = @check_status
                             WHERE [FBillNo] = @FBillNo";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_ReceiptList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
                        db.AddInParameter(cmd, "@check_status", DbType.Int32, list.FCheckStatus);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("批量更新已收款的销售发票单号check_status出错。");
                }
            }
        }
        public void deleteToDone(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE  FROM [dbo].[BillNoDone] WHERE   [receipt_id] =@receipt_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
                        db.ExecuteNonQuery(cmd);
                   
                }
                catch
                {
                    throw new Exception("删除已收款的销售发票单号出错。");
                }
            }
        }
        
		public void addReceiptHead(T_ReceiptHead head)
		{

			Database db = Dao.GetDatabase();
			string sql = @"INSERT INTO [dbo].[receipt_head]
							   ([receipt_id]
                               ,[account_id]
							   ,[receipt_date]
							   ,[receipt_type]
							   ,[customer_code]
							   ,[customer_name]
							   ,[amount]
							   ,[currency]
							   ,[receipt_charge]
							   ,[FDeptID]
							   ,[FEmpID]
							   ,[FPreparer]
							   ,[FChecker]
							   ,[FCheckDate]
							   ,[FCheckStatus]
                               ,[audit_status]
                               ,[note])
						 VALUES
							   (@receipt_id
                               ,@account_id
							   ,@receipt_date
							   ,@receipt_type
							   ,@customer_code
							   ,@customer_name
							   ,@amount
							   ,@currency
							   ,@receipt_charge
							   ,@FDeptID
							   ,@FEmpID
							   ,@FPreparer
							   ,@FChecker
							   ,@FCheckDate
							   ,@FCheckStatus
                               ,@audit_status
                               ,@note)";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{

					DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@receipt_id", DbType.String, head.ReceiptId);
                    db.AddInParameter(cmd, "@account_id", DbType.String, head.AccountId);
					db.AddInParameter(cmd, "@receipt_date", DbType.DateTime, head.ReceiptDate);
					db.AddInParameter(cmd, "@receipt_type", DbType.String, head.ReceiptType);
					db.AddInParameter(cmd, "@customer_code", DbType.String, head.CustomerCode);
					db.AddInParameter(cmd, "@customer_name", DbType.String, head.CustomerName);
					db.AddInParameter(cmd, "@amount", DbType.Decimal, head.Amount);
					db.AddInParameter(cmd, "@receipt_charge", DbType.Decimal, head.ReceiptCharge);
					db.AddInParameter(cmd, "@currency", DbType.String, head.Currency);
					db.AddInParameter(cmd, "@FDeptID", DbType.Int32, head.FDeptID);
					db.AddInParameter(cmd, "@FEmpID", DbType.Int32, head.FEmpID);
					db.AddInParameter(cmd, "@FPreparer", DbType.Int32, head.FPreparer);
					db.AddInParameter(cmd, "@FChecker", DbType.Int32, head.FChecker);
					db.AddInParameter(cmd, "@FCheckDate", DbType.DateTime, head.FCheckDate);
                    db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, head.FCheckStatus);
                    db.AddInParameter(cmd, "@audit_status", DbType.Int32, head.AuditStatus);
                    db.AddInParameter(cmd, "@note", DbType.String, head.Note);

					db.ExecuteNonQuery(cmd);


				}
			}
			catch
			{
				throw new Exception("添加收款单表头数据失败,系统要炸了，倒计时开始...");
			}
		}

        public void updateReceiptHead(T_ReceiptHead head)
        {

            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[receipt_head]
                           SET [receipt_date] = @receipt_date
                              ,[amount] = @amount
                              ,[receipt_charge] = @receipt_charge
                              ,[FDeptID] = @FDeptID
                              ,[FEmpID] = @FEmpID
                              ,[FPreparer] = @FPreparer
                              ,[FChecker] = @FChecker
                              ,[FCheckDate] = @FCheckDate
                              ,[FCheckStatus] = @FCheckStatus
                              ,[note]  = @note
                         WHERE  [receipt_id] = @receipt_id";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {

                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@receipt_id", DbType.String, head.ReceiptId);
                    db.AddInParameter(cmd, "@receipt_date", DbType.DateTime, head.ReceiptDate);
                    db.AddInParameter(cmd, "@amount", DbType.Decimal, head.Amount);
                    db.AddInParameter(cmd, "@receipt_charge", DbType.Decimal, head.ReceiptCharge);
                    db.AddInParameter(cmd, "@FDeptID", DbType.Int32, head.FDeptID);
                    db.AddInParameter(cmd, "@FEmpID", DbType.Int32, head.FEmpID);
                    db.AddInParameter(cmd, "@FPreparer", DbType.Int32, head.FPreparer);
                    db.AddInParameter(cmd, "@FChecker", DbType.Int32, head.FChecker);
                    db.AddInParameter(cmd, "@FCheckDate", DbType.DateTime, head.FCheckDate);
                    db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, head.FCheckStatus);
                    db.AddInParameter(cmd, "@note", DbType.String, head.Note);

                    db.ExecuteNonQuery(cmd);


                }
            }
            catch
            {
                throw new Exception("更新收款单表头数据失败,系统要炸了，倒计时开始...");
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
							   ,[amount_all]
							   ,[agenter]
							   ,[agent_date]
							   ,[check_status])
						 VALUES
							   (@bill_no
							   ,@itemID
							   ,@customerID
							   ,@customer
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

		public DataSet getReceiptAuditHeads()
		{
			Database db = Dao.GetDatabase();
			DataSet ds;
			string sql = @"SELECT * FROM [dbo].[receipt_head]";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				//db.AddInParameter(cmd, "@name", DbType.String, name);
				ds = db.ExecuteDataSet(cmd);
				return ds;

			}
		}

		public string getTypeNameByCode(string code)
		{
			Database db = Dao.GetDatabase();
			DataSet ds;
			string sql = @"SELECT name FROM [dbo].[receipt] WHERE code = @code";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@code", DbType.String, code);
				ds = db.ExecuteDataSet(cmd);
				return ds.Tables[0].Rows[0][0].ToString();

			}
		}

		public DataSet getReceiptHeads(string receipt_id)
		{
			Database db = Dao.GetDatabase();
			DataSet ds;
			string sql = @"SELECT * FROM [dbo].[receipt_head] WHERE receipt_id LIKE @receipt_id";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@receipt_id", DbType.String, "%" + receipt_id + "%");
				ds = db.ExecuteDataSet(cmd);
				return ds;

			}
		}

        public DataSet queryReceiptHeads(string receipt_id, string customer_name = "", string start_time = "", string end_time = "")
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[receipt_head] WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(receipt_id))
            {
                sql += " and receipt_id LIKE @receipt_id ";
            }
            if (!string.IsNullOrEmpty(customer_name))
            {
                sql += " and customer_name LIKE @customer_name ";
            }
            if (!string.IsNullOrEmpty(start_time))
            {
                sql += " and receipt_date >= @start_time ";
            }
            if (!string.IsNullOrEmpty(end_time))
            {
                sql += " and receipt_date <= @end_time ";
            }
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                if (!string.IsNullOrEmpty(receipt_id))
                {
                    db.AddInParameter(cmd, "@receipt_id", DbType.String, "%" + receipt_id + "%");
                }
                if (!string.IsNullOrEmpty(customer_name))
                {
                    db.AddInParameter(cmd, "@customer_name", DbType.String, "%" + customer_name + "%");
                }
                if (!string.IsNullOrEmpty(start_time))
                {
                    db.AddInParameter(cmd, "@start_time", DbType.DateTime, DateTime.Parse(start_time));
                }
                if (!string.IsNullOrEmpty(end_time))
                {
                    db.AddInParameter(cmd, "@end_time", DbType.DateTime, DateTime.Parse(end_time));
                }
               
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }

		public DataSet getReceiptList(string receipt_id)
		{
			Database db = Dao.GetDatabase();
			DataSet ds;
			string sql = @"SELECT * FROM [dbo].[receipt_list] WHERE FBillNo IS NOT NULL and receipt_id = @receipt_id";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@receipt_id", DbType.String,  receipt_id );
				ds = db.ExecuteDataSet(cmd);
				return ds;

			}
		}

		public DataSet getInDecreaseList(string receipt_id)
		{
			Database db = Dao.GetDatabase();
			DataSet ds;
			string sql = @"SELECT   a.receipt_no ,
									a.InDecrease_no ,
									a.FAmountFor ,
									b.customer ,
									b.agenter ,
									b.agent_date
							FROM    [dbo].[receipt_list] a ,
									[dbo].[Indecrease_head] b
							WHERE   a.InDecrease_no = b.bill_no
									AND FBillNo IS  NULL
                                    AND a.FDate IS  NULL
									AND receipt_id = @receipt_id;";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
				ds = db.ExecuteDataSet(cmd);
				return ds;

			}
		}
        public DataSet getDepositList(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT a.receipt_no,
                                   a.Deposit_id,
                                   a.FDate,
                                   a.FAmountFor,
                                   a.FReceiveAmountFor,
                                   a.FUnReceiveAmountFor,
                                   a.FCheckAmountFor,
                                   a.FCurrencyID,
                                   b.agenter,
                                   a.FNote
                            FROM [dbo].[receipt_list] a,
                                 [dbo].[deposit_head] b
                            WHERE a.Deposit_id = b.deposit_id
                                  AND FBillNo IS NULL
	                              AND a.FDate IS NOT NULL
                                  AND a.receipt_id =@receipt_id";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }
        public DataSet getICSaleList(string start_time, string end_time, string FBillNo, string customer_name, string check_status)
        {
            Database db = Dao.GetDatabase();
            DataSet ds1;
            string sql = @"SELECT  a.receipt_id,
                                   a.FBillNo,
                                   a.FDate,
                                   b.customer_name as FName,
                                   a.FAmountFor,
                                   (a.FReceiveAmountFor + a.FCheckAmountFor) AS FReceiveAmountFor,
                                   (a.FAmountFor - a.FReceiveAmountFor - a.FCheckAmountFor) AS FUnReceiveAmountFor,
                                   a.FCurrencyID,
                                   a.FCheckStatus,
                                   a.FNote
                            FROM dbo.receipt_list a,
                                 dbo.receipt_head b
                            WHERE a.receipt_id = b.receipt_id
                                  AND a.FBillNo IS NOT NULL ";
            if (!string.IsNullOrEmpty(FBillNo))
            {
                sql += " AND a.FBillNo = @FBillNo ";
            }
            if (!string.IsNullOrEmpty(customer_name))
            {
                sql += " AND b.customer_name =  @customer_name ";
            }
            if (!string.IsNullOrEmpty(start_time))
            {
                sql += " AND a.FDate > @start_time ";
            }
            if (!string.IsNullOrEmpty(end_time))
            {
                sql += " AND a.FDate < @end_time ";
            }
            if (!string.IsNullOrEmpty(check_status))
            {
                sql += " AND a.FCheckStatus = @FCheckStatus ";
            }
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                if (!string.IsNullOrEmpty(FBillNo))
                {
                    db.AddInParameter(cmd, "@FBillNo", DbType.String, FBillNo);
                }
                if (!string.IsNullOrEmpty(customer_name))
                {
                    db.AddInParameter(cmd, "@customer_name", DbType.String, customer_name);
                }
                if (!string.IsNullOrEmpty(start_time))
                {
                    db.AddInParameter(cmd, "@start_time", DbType.DateTime ,DateTime.Parse(start_time));
                }
                if (!string.IsNullOrEmpty(end_time))
                {
                    db.AddInParameter(cmd, "@end_time", DbType.DateTime, DateTime.Parse(end_time));
                }
                if (!string.IsNullOrEmpty(check_status))
                {
                    db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, Int32.Parse(check_status));
                }

                ds1 = db.ExecuteDataSet(cmd);
                return ds1;
            }

        }
        public DataSet getICSaleListFromKindDee(string start_time, string end_time, string FBillNo, string customer_name, string check_status)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds1;
            string sql = @"SELECT '' AS receipt_id,
	                               a.FBillNo,
                                   a.FDate,
                                   c.FName,
                                   a.FInvoiceAmountFor AS FAmountFor,
                                   a.FReceiveAmountFor,
                                   a.FUnReceiveAmountFor,
                                   a.FCurrencyID,
                                   a.FCheckStatus,
                                   a.FNote
                            FROM [dbo].[ICSale] a,
                                 [dbo].[t_Organization] c
                            WHERE  a.FCustID = c.FItemID
                                  --AND a.FCheckStatus < 2 
                                   ";
            if (!string.IsNullOrEmpty(FBillNo))
            {
                sql += " AND a.FBillNo = @FBillNo ";
            }
            if (!string.IsNullOrEmpty(customer_name))
            {
                sql += "  AND c.FName = @FName ";
            }
            if (!string.IsNullOrEmpty(start_time))
            {
                sql += " AND a.FDate > @start_time ";
            }
            if (!string.IsNullOrEmpty(end_time))
            {
                sql += " AND a.FDate < @end_time ";
            }
            if (!string.IsNullOrEmpty(check_status))
            {
                sql += " AND a.FCheckStatus = @FCheckStatus ";
            } 
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                if (!string.IsNullOrEmpty(FBillNo))
                {
                    db.AddInParameter(cmd, "@FBillNo", DbType.String, FBillNo);
                }
                if (!string.IsNullOrEmpty(customer_name))
                {
                    db.AddInParameter(cmd, "@FName", DbType.String, customer_name);
                }
                if (!string.IsNullOrEmpty(start_time))
                {
                    db.AddInParameter(cmd, "@start_time", DbType.DateTime, DateTime.Parse(start_time));
                }
                if (!string.IsNullOrEmpty(end_time))
                {
                    db.AddInParameter(cmd, "@end_time", DbType.DateTime, DateTime.Parse(end_time));
                }
                if (!string.IsNullOrEmpty(check_status))
                {
                    db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, Int32.Parse(check_status));
                }
                ds1 = db.ExecuteDataSet(cmd);
                return ds1;
            }

        }
		public void deleteReceipt(string receipt_id)
		{
			Database db = Dao.GetDatabase();
			string sql = @"DELETE FROM [dbo].[receipt_head] WHERE [receipt_id]=@receipt_id;
						   DELETE FROM [dbo].[receipt_list] WHERE [receipt_id]=@receipt_id;";
			using (DbConnection cn = db.CreateConnection())
			{
				try
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
					db.ExecuteNonQuery(cmd);
				}
				catch
				{
					throw new Exception("删除收款单失败");
				}
			}
		}

        public void updateReceiptHeadAuditStatus(T_ReceiptHead head)
        {

            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[receipt_head]
                           SET [audit_status] = @audit_status
                         WHERE  [receipt_id] = @receipt_id";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {

                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@receipt_id", DbType.String, head.ReceiptId);
                    db.AddInParameter(cmd, "@audit_status", DbType.Int32, head.AuditStatus);
                    db.ExecuteNonQuery(cmd);


                }
            }
            catch
            {
                throw new Exception("更新收款单表头audit_status数据失败");
            }
        }

	}


}
