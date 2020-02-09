using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Configuration;

namespace ExportDrawbackManagement.Biz.Library
{
	public class ProfitBudgetManager : BaseManager<T_ProfitBudget>, IProfitBudgetManager
	{
		public DataSet getSEOrderInfo(T_ProfitBudget item)
		{
			Database db = Dao.GetDatabase("KingDeeConnection");
			DataSet ds;
			string sql = @"  SELECT a.[FInterID] ,
									b.FItemID ,
									b.FEntryID AS Sale_FEntryID ,
									d.FEntryID AS Buy_FEntryID ,
									a.[FBillNo] AS sale_bill_no ,
									a.[FDeptID] AS dept_id ,
									a.[FEmpID] AS emp_id ,
									c.FBillNo AS buy_bill_no ,
									b.FTaxPrice AS sale_price ,
									( CASE WHEN b.FCESS > 0 THEN 1
										   ELSE 0
									  END ) AS sale_rate ,
									e.FExchangeRate AS exchange_rate ,
									e.FNumber AS currency ,
									b.FQty AS sale_qty ,
									d.FTaxPrice AS buy_price ,
									( CASE WHEN d.FCess > 0 THEN 1
										   ELSE 0
									  END ) AS buy_rate ,
									d.FQty AS buy_qty
							FROM    [dbo].[SEOrder] a ,
									[dbo].[SEOrderEntry] b ,
									[dbo].[POOrder] c ,
									[dbo].[POOrderEntry] d ,
									[dbo].[t_Currency] e
							WHERE   a.FBrNo = b.FBrNO
									AND a.FInterID = b.FInterID
									AND c.FBrNo = d.FBrNo
									AND c.FInterID = d.FInterID
									AND a.FBillNo = d.FSourceBillNo
									AND b.FItemID = d.FItemID
									AND a.FCurrencyID = e.FCurrencyID
									AND a.FBillNo = @FBillNo";
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
			string sql = @" SELECT *  FROM [exportDrawback].[dbo].[ProfitBudget] order by update_time desc";
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
			string sql = @"  SELECT *  FROM [exportDrawback].[dbo].[ProfitBudget] WHERE sale_bill_no = @sale_bill_no ";
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
		public void addProfitBudgetHead(T_ProfitBudget item)
		{
			Database db = Dao.GetDatabase();
			string sql = @" INSERT INTO [dbo].[ProfitBudget]
								   ([FInterID]
								   ,[sale_bill_no]
								   ,[extra_charges]
								   ,[update_time]
								   ,[audit_state])
							 VALUES
								   (@FInterID
								   ,@sale_bill_no
								   ,@extra_charges
								   ,@update_time
								   ,@audit_state);";
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
								   ,[volume])
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
								   ,@volume);";
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
