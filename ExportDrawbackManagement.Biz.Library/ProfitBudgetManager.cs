using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
	public class ProfitBudgetManager : BaseManager<T_ProfitBudget>, IProfitBudgetManager
	{
		public DataSet getSEOrderInfo(T_ProfitBudget item)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");
			DataSet ds;
            string sql = @"  SELECT aa.FInterID,
                                   aa.FItemID,
                                   aa.Sale_FEntryID,
                                   bb.Buy_FEntryID,
                                   aa.sale_bill_no,
                                   aa.dept_id,
                                   aa.emp_id,
                                   bb.buy_bill_no,
                                   aa.sale_price,
                                   aa.sale_rate,
                                   e.FExchangeRate AS exchange_rate,
                                   e.FNumber AS currency,
                                   aa.sale_qty,
                                   bb.buy_price,
                                   bb.buy_rate,
                                   bb.buy_qty,
                                   f.FName,
                                   f.FNumber
                            FROM
                            (
                                SELECT a.[FInterID],
                                       a.FBillNo,
                                       b.FItemID,
                                       b.FEntryID AS Sale_FEntryID,
                                       b.FTaxPrice AS sale_price,
                                       (CASE
                                            WHEN b.FCESS > 0 THEN
                                                1
                                            ELSE
                                                0
                                        END
                                       ) AS sale_rate,
                                       b.FQty AS sale_qty,
                                       a.[FBillNo] AS sale_bill_no,
                                       a.[FDeptID] AS dept_id,
                                       a.[FEmpID] AS emp_id,
                                       a.FCurrencyID
                                FROM [dbo].[SEOrder] a,
                                     [dbo].[SEOrderEntry] b
                                WHERE a.FBrNo = b.FBrNO
                                      AND a.FInterID = b.FInterID
                                      AND a.FBillNo = @FBillNo
                            ) aa
                                LEFT JOIN
                                (
                                    SELECT c.FBillNo AS buy_bill_no,
                                           d.FEntryID AS Buy_FEntryID,
                                           d.FTaxPrice AS buy_price,
                                           (CASE
                                                WHEN d.FCess > 0 THEN
                                                    1
                                                ELSE
                                                    0
                                            END
                                           ) AS buy_rate,
                                           d.FQty AS buy_qty,
                                           d.FSourceBillNo,
                                           d.FItemID
                                    FROM [dbo].[POOrder] c,
                                         [dbo].[POOrderEntry] d
                                    WHERE c.FBrNo = d.FBrNo
                                          AND c.FInterID = d.FInterID
                                          AND d.FSourceBillNo = @FBillNo
                                ) bb
                                    ON aa.FBillNo = bb.FSourceBillNo
                                       AND aa.FItemID = bb.FItemID
                                LEFT JOIN [dbo].[t_Currency] e
                                    ON aa.FCurrencyID = e.FCurrencyID
                                LEFT JOIN [dbo].[t_ICItem] f
                                    ON aa.FItemID = f.FItemID;";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@FBillNo", DbType.String, item.SaleBillNo);
				ds = db.ExecuteDataSet(cmd);
				return ds;
			}

		}

		public string getDeptNameById(string dept_id)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");

			string sql = @" SELECT FName FROM [dbo].[t_Department] 
							WHERE FItemID = @FItemID";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@FItemID", DbType.String, dept_id);
				DataSet ds = db.ExecuteDataSet(cmd);
				if (ds.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				else
				{
					return ds.Tables[0].Rows[0][0].ToString();
				}
			}
		}

		public string getEmpNameById(string emp_id)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");

			string sql = @" SELECT FName FROM [dbo].[t_Emp]
							WHERE FItemID = @FItemID";
			using (DbConnection cn = db.CreateConnection())
			{
				DbCommand cmd = db.GetSqlStringCommand(sql);
				db.AddInParameter(cmd, "@FItemID", DbType.String, emp_id);
				DataSet ds = db.ExecuteDataSet(cmd);
				if (ds.Tables[0].Rows.Count == 0)
				{
					return null;
				}
				else
				{
					return ds.Tables[0].Rows[0][0].ToString();
				}
			}
		}

		public DataSet getProfitBudgetSummary()
		{
			Database db = Dao.GetDatabase();
			string sql = @" SELECT *  FROM [dbo].[ProfitBudget] order by update_time desc";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					return db.ExecuteDataSet(cmd);
				}
			}
			catch
			{
				throw new Exception("获取利润预算表头失败");
			}

		}
		public DataSet getProfitBudgetSummaryByID(string sale_bill_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"  SELECT *  FROM [dbo].[ProfitBudget] WHERE sale_bill_no like @sale_bill_no ";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, "%" + sale_bill_no + "%");

					return db.ExecuteDataSet(cmd);
				}
			}
			catch
			{
				throw new Exception("根据销售订单号获取利润预算表头失败");
			}

		}
		public DataSet getProfitBudgetByID(int FInterIID, string sale_bill_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"SELECT * FROM [dbo].[ProfitBudget] a,[dbo].[ProfitBudgetList] b
							WHERE a.[FInterID] = b.[FInterID]
							AND a.[sale_bill_no] = b.[sale_bill_no]
							AND a.[sale_bill_no] = @sale_bill_no
							AND a.[FInterID] = @FInterID";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@FInterID", DbType.Int32, FInterIID);
					db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
					return db.ExecuteDataSet(cmd);
				}
			}
			catch
			{
				throw new Exception("执行getProfitBudgetByID出错。");
			}

		}
        public DataSet getProfitBudgetList(T_ProfitBudgetList item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[ProfitBudgetList]
                            WHERE sale_bill_no = @sale_bill_no
                            AND Buy_FEntryID = @Buy_FEntryID
                            AND buy_bill_no = @buy_bill_no";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@Buy_FEntryID", DbType.Int32, item.BuyFentryid);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, item.SaleBillNo);
                    db.AddInParameter(cmd, "@buy_bill_no", DbType.String, item.BuyBillNo);
                    return db.ExecuteDataSet(cmd);
                }
            }
            catch
            {
                throw new Exception("执行getProfitBudgetList出错。");
            }
        }
        public DataSet getProfitBudgetList(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[ProfitBudgetList]
                            WHERE sale_bill_no = @sale_bill_no ";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
                    return db.ExecuteDataSet(cmd);
                }
            }
            catch
            {
                throw new Exception("执行getProfitBudgetList出错。");
            }
        }
		public void addProfitBudgetHead(T_ProfitBudget item)
		{
			Database db = Dao.GetDatabase();
			string sql = @" INSERT INTO [dbo].[ProfitBudget]
								   ([FInterID]
								   ,[sale_bill_no]
								   ,[extra_charges]
								   ,[update_time]
								   ,[audit_state]
                                   ,[check_status])
							 VALUES
								   (@FInterID
								   ,@sale_bill_no
								   ,@extra_charges
								   ,@update_time
								   ,@audit_state
                                   ,@check_status);";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@FInterID", DbType.Int32, item.FInterID);
					db.AddInParameter(cmd, "@sale_bill_no", DbType.String, item.SaleBillNo);
					db.AddInParameter(cmd, "@extra_charges", DbType.Decimal, item.ExtraCharges);
					db.AddInParameter(cmd, "@update_time", DbType.DateTime, item.UpdateTime);
                    db.AddInParameter(cmd, "@audit_state", DbType.Boolean, item.AuditState);
                    db.AddInParameter(cmd, "@check_status", DbType.Boolean, item.CheckStatus);
					db.ExecuteNonQuery(cmd);

				}

			}
			catch
			{
				throw new Exception("插入利润预算表头失败。");
			}
		}
		public void updateProfitBudgetHead(T_ProfitBudget item)
		{
			Database db = Dao.GetDatabase();
			string sql = @" UPDATE [dbo].[ProfitBudget]
							   SET [FInterID] = @FInterID     
								  ,[extra_charges] = @extra_charges
								  ,[update_time] = @update_time
								  ,[audit_state] = @audit_state
							 WHERE  [sale_bill_no] = @sale_bill_no";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@FInterID", DbType.Int32, item.FInterID);
					db.AddInParameter(cmd, "@sale_bill_no", DbType.String, item.SaleBillNo);
					db.AddInParameter(cmd, "@extra_charges", DbType.Decimal, item.ExtraCharges);
					db.AddInParameter(cmd, "@update_time", DbType.DateTime, item.UpdateTime);
                    db.AddInParameter(cmd, "@audit_state", DbType.Boolean, item.AuditState);
					db.ExecuteNonQuery(cmd);

				}

			}
			catch
			{
				throw new Exception("更新利润预算表头失败。");
			}
		}
		public void addProfitBudgetList(List<T_ProfitBudgetList> lists)
		{
			Database db = Dao.GetDatabase();

			string sql = @" INSERT INTO [dbo].[ProfitBudgetList]
								   ([FInterID]
								   ,[Sale_FEntryID]
								   ,[Buy_FEntryID]
								   ,[FItemID]
								   ,[sale_bill_no]
								   ,[buy_bill_no]
								   ,[dept_id]
								   ,[emp_id]
								   ,[sale_price]
								   ,[currency]
								   ,[exchange_rate]
								   ,[buy_price]
								   ,[sale_qty]
								   ,[buy_qty]
								   ,[accessory]
								   ,[accessory_price]
								   ,[length]
								   ,[width]
								   ,[height]
								   ,[capacity]
								   ,[estimate_freight_charge]
								   ,[tax_rate]
								   ,[sale_rate]
								   ,[buy_rate]
								   ,[return_rate]
								   ,[profit]
								   ,[volume]
                                    ,[un_accounting_qty]
                                    ,[check_status]
                                    ,[FName]
                                    ,[FNumber])
							 VALUES
								   (@FInterID
								   ,@Sale_FEntryID
								   ,@Buy_FEntryID
								   ,@FItemID
								   ,@sale_bill_no
								   ,@buy_bill_no
								   ,@dept_id
								   ,@emp_id
								   ,@sale_price
								   ,@currency
								   ,@exchange_rate
								   ,@buy_price
								   ,@sale_qty
								   ,@buy_qty
								   ,@accessory
								   ,@accessory_price
								   ,@length
								   ,@width
								   ,@height
								   ,@capacity
								   ,@estimate_freight_charge
								   ,@tax_rate
								   ,@sale_rate
								   ,@buy_rate
								   ,@return_rate
								   ,@profit
								   ,@volume
                                    ,@un_accounting_qty
                                    ,@check_status
                                    ,@FName
                                    ,@FNumber);";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					foreach (T_ProfitBudgetList list in lists)
					{
						DbCommand cmd = db.GetSqlStringCommand(sql);
						db.AddInParameter(cmd, "@FInterID", DbType.Int32, list.FInterID);
						db.AddInParameter(cmd, "@Sale_FEntryID", DbType.Int32, list.SaleFentryid);
						db.AddInParameter(cmd, "@Buy_FEntryID", DbType.Int32, list.BuyFentryid);
						db.AddInParameter(cmd, "@FItemID", DbType.Int32, list.FItemID);
						db.AddInParameter(cmd, "@sale_bill_no", DbType.String, list.SaleBillNo);
						db.AddInParameter(cmd, "@buy_bill_no", DbType.String, list.BuyBillNo);
						db.AddInParameter(cmd, "@dept_id", DbType.Int32, list.DeptId);
						db.AddInParameter(cmd, "@emp_id", DbType.Int32, list.EmpId);
						db.AddInParameter(cmd, "@sale_price", DbType.Decimal, list.SalePrice);
						db.AddInParameter(cmd, "@currency", DbType.String, list.Currency);
						db.AddInParameter(cmd, "@exchange_rate", DbType.Decimal, list.ExchangeRate);
						db.AddInParameter(cmd, "@buy_price", DbType.Decimal, list.BuyPrice);
						db.AddInParameter(cmd, "@sale_qty", DbType.Decimal, list.SaleQty);
						db.AddInParameter(cmd, "@buy_qty", DbType.Decimal, list.BuyQty);
						db.AddInParameter(cmd, "@accessory", DbType.String, list.Accessory);
						db.AddInParameter(cmd, "@accessory_price", DbType.Decimal, list.AccessoryPrice);
						db.AddInParameter(cmd, "@length", DbType.Decimal, list.Length);
						db.AddInParameter(cmd, "@width", DbType.Decimal, list.Width);
						db.AddInParameter(cmd, "@height", DbType.Decimal, list.Height);
						db.AddInParameter(cmd, "@capacity", DbType.Decimal, list.Capacity);
						db.AddInParameter(cmd, "@estimate_freight_charge", DbType.Decimal, list.EstimateFreightCharge);
						db.AddInParameter(cmd, "@tax_rate", DbType.Decimal, list.TaxRate);
						db.AddInParameter(cmd, "@sale_rate", DbType.Boolean, list.SaleRate);
						db.AddInParameter(cmd, "@buy_rate", DbType.Boolean, list.BuyRate);
						db.AddInParameter(cmd, "@return_rate", DbType.Decimal, list.ReturnRate);
						db.AddInParameter(cmd, "@profit", DbType.Decimal, list.Profit);
                        db.AddInParameter(cmd, "@volume", DbType.Decimal, list.Volume);
                        db.AddInParameter(cmd, "@un_accounting_qty", DbType.Decimal, list.UnAccountingQty);
                        db.AddInParameter(cmd, "@check_status", DbType.Decimal, list.CheckStatus);
                        db.AddInParameter(cmd, "@FName", DbType.String, list.FName);
                        db.AddInParameter(cmd, "@FNumber", DbType.String, list.FNumber);
						db.ExecuteNonQuery(cmd);
					}

				}
			}
			catch
			{
				throw new Exception("保存利润预算表体失败。");
			}
		}
		public void updateProfitBudgetList(List<T_ProfitBudgetList> lists)
		{
			Database db = Dao.GetDatabase();

			string sql = @" UPDATE  [dbo].[ProfitBudgetList]
							SET     [FItemID] = @FItemID
									  ,[sale_bill_no] = @sale_bill_no
									  ,[buy_bill_no] = @buy_bill_no
									  ,[dept_id] = @dept_id
									  ,[emp_id] = @emp_id
									  ,[sale_price] = @sale_price
									  ,[currency] = @currency
									  ,[exchange_rate] = @exchange_rate
									  ,[buy_price] = @buy_price
									  ,[sale_qty] = @sale_qty
									  ,[buy_qty] = @buy_qty
									  ,[accessory] = @accessory
									  ,[accessory_price] = @accessory_price
									  ,[length] = @length
									  ,[width] = @width
									  ,[height] = @height
									  ,[capacity] = @capacity
									  ,[estimate_freight_charge] = @estimate_freight_charge
									  ,[tax_rate] = @tax_rate
									  ,[sale_rate] = @sale_rate
									  ,[buy_rate] = @buy_rate
									  ,[return_rate] = @return_rate
									  ,[profit] = @profit
									  ,[volume]=@volume
							WHERE   [FInterID] = @FInterID
									AND [Sale_FEntryID] = @Sale_FEntryID
									AND [Buy_FEntryID] = @Buy_FEntryID;";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					foreach (T_ProfitBudgetList list in lists)
					{
						DbCommand cmd = db.GetSqlStringCommand(sql);
						db.AddInParameter(cmd, "@FInterID", DbType.Int32, list.FInterID);
						db.AddInParameter(cmd, "@Sale_FEntryID", DbType.Int32, list.SaleFentryid);
						db.AddInParameter(cmd, "@Buy_FEntryID", DbType.Int32, list.BuyFentryid);
						db.AddInParameter(cmd, "@FItemID", DbType.Int32, list.FItemID);
						db.AddInParameter(cmd, "@sale_bill_no", DbType.String, list.SaleBillNo);
						db.AddInParameter(cmd, "@buy_bill_no", DbType.String, list.BuyBillNo);
						db.AddInParameter(cmd, "@dept_id", DbType.Int32, list.DeptId);
						db.AddInParameter(cmd, "@emp_id", DbType.Int32, list.EmpId);
						db.AddInParameter(cmd, "@sale_price", DbType.Decimal, list.SalePrice);
						db.AddInParameter(cmd, "@currency", DbType.String, list.Currency);
						db.AddInParameter(cmd, "@exchange_rate", DbType.Decimal, list.ExchangeRate);
						db.AddInParameter(cmd, "@buy_price", DbType.Decimal, list.BuyPrice);
						db.AddInParameter(cmd, "@sale_qty", DbType.Decimal, list.SaleQty);
						db.AddInParameter(cmd, "@buy_qty", DbType.Decimal, list.BuyQty);
						db.AddInParameter(cmd, "@accessory", DbType.String, list.Accessory);
						db.AddInParameter(cmd, "@accessory_price", DbType.Decimal, list.AccessoryPrice);
						db.AddInParameter(cmd, "@length", DbType.Decimal, list.Length);
						db.AddInParameter(cmd, "@width", DbType.Decimal, list.Width);
						db.AddInParameter(cmd, "@height", DbType.Decimal, list.Height);
						db.AddInParameter(cmd, "@capacity", DbType.Decimal, list.Capacity);
						db.AddInParameter(cmd, "@estimate_freight_charge", DbType.Decimal, list.EstimateFreightCharge);
						db.AddInParameter(cmd, "@tax_rate", DbType.Decimal, list.TaxRate);
						db.AddInParameter(cmd, "@sale_rate", DbType.Boolean, list.SaleRate);
						db.AddInParameter(cmd, "@buy_rate", DbType.Boolean, list.BuyRate);
						db.AddInParameter(cmd, "@return_rate", DbType.Decimal, list.ReturnRate);
						db.AddInParameter(cmd, "@profit", DbType.Decimal, list.Profit);
                        db.AddInParameter(cmd, "@volume", DbType.Decimal, list.Volume);

						db.ExecuteNonQuery(cmd);
					}

				}
			}
			catch
			{
				throw new Exception("更新利润预算表体失败。");
			}
		}
        public void updateList(List<T_ProfitBudgetList> lists)
        {
            Database db = Dao.GetDatabase();

            string sql = @" UPDATE  [dbo].[ProfitBudgetList]
							SET     [un_accounting_qty]=@un_accounting_qty,
                                    [check_status]=@check_status
							WHERE   [sale_bill_no] = @sale_bill_no
									AND [buy_bill_no] = @buy_bill_no
									AND [Buy_FEntryID] = @Buy_FEntryID;";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    foreach (T_ProfitBudgetList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        
                        db.AddInParameter(cmd, "@Buy_FEntryID", DbType.Int32, list.BuyFentryid);
                        db.AddInParameter(cmd, "@sale_bill_no", DbType.String, list.SaleBillNo);
                        db.AddInParameter(cmd, "@buy_bill_no", DbType.String, list.BuyBillNo);
                        db.AddInParameter(cmd, "@un_accounting_qty", DbType.Decimal, list.UnAccountingQty);
                        db.AddInParameter(cmd, "@check_status", DbType.Boolean, list.CheckStatus);

                        db.ExecuteNonQuery(cmd);
                    }

                }
            }
            catch
            {
                throw new Exception("更新利润预算表体失败。");
            }
        }
        public void AddUnAccountingQtyBecauseDelete(List<T_ProfitBudgetList> lists)
        {
            Database db = Dao.GetDatabase();

            string sql = @" UPDATE  [dbo].[ProfitBudgetList]
							SET     [un_accounting_qty]+= @un_accounting_qty,
                                    [check_status] = @check_status
							WHERE   [sale_bill_no] = @sale_bill_no
									AND [buy_bill_no] = @buy_bill_no
									AND [Buy_FEntryID] = @Buy_FEntryID;";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    foreach (T_ProfitBudgetList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);

                        db.AddInParameter(cmd, "@Buy_FEntryID", DbType.Int32, list.BuyFentryid);
                        db.AddInParameter(cmd, "@sale_bill_no", DbType.String, list.SaleBillNo);
                        db.AddInParameter(cmd, "@buy_bill_no", DbType.String, list.BuyBillNo);
                        db.AddInParameter(cmd, "@un_accounting_qty", DbType.Decimal, list.UnAccountingQty);
                        db.AddInParameter(cmd, "@check_status", DbType.Boolean, list.CheckStatus);

                        db.ExecuteNonQuery(cmd);
                    }

                }
            }
            catch
            {
                throw new Exception("更新利润预算表体失败。");
            }
        }
		public void deleteBySaleBillNo(string sale_bill_no)
		{
			Database db = Dao.GetDatabase();
			string sql = @"DELETE FROM [dbo].[ProfitBudget]
								  WHERE [sale_bill_no]=@sale_bill_no;
						   DELETE FROM [dbo].[ProfitBudgetList]
								  WHERE [sale_bill_no]=@sale_bill_no;";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
					db.ExecuteNonQuery(cmd);

				}

			}
			catch
			{
				throw new Exception("删除利润预算表数据失败。");
			}
		}

		public void audit(string sale_bill_no, bool flag)
		{
			Database db = Dao.GetDatabase();
			string sql = @"UPDATE  [dbo].[ProfitBudget]
							SET     [update_time] = @update_time ,
									[audit_state] = @audit_state
							WHERE   sale_bill_no = @sale_bill_no;";
			try
			{
				using (DbConnection cn = db.CreateConnection())
				{
					DbCommand cmd = db.GetSqlStringCommand(sql);
					db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
					db.AddInParameter(cmd, "@update_time", DbType.DateTime, DateTime.Now);
					db.AddInParameter(cmd, "@audit_state", DbType.Boolean, flag);
					db.ExecuteNonQuery(cmd);

				}

			}
			catch
			{
				throw new Exception("审批利润预算表数据失败。");
			}
		}
	}
}
