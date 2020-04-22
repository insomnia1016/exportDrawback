using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
    class ProfitAccountingManager : BaseManager<T_ProfitBudget>, IProfitAccountingManager
    {
        public DataSet getSEOrderInfo(T_ProfitAccounting item)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = @"  SELECT a.[FInterID],
                                   b.FItemID,
                                   b.FOrderInterID,
                                   b.FOrderEntryID,
                                   b.FEntryID AS Sale_FEntryID,
                                   d.FEntryID AS Buy_FEntryID,
                                   a.[FBillNo] AS sale_bill_no,
                                   c.FBillNo AS buy_bill_no,
                                   b.FTaxPrice AS sale_price,
	                               b.FQty AS sale_qty,
                                   b.FOrderBillNo
                            FROM dbo.ICSale a,
                                 dbo.ICSaleEntry b,
                                 [dbo].[POOrder] c,
                                 [dbo].[POOrderEntry] d
                            WHERE a.FInterID = b.FInterID
                                  AND c.FBrNo = d.FBrNo
                                  AND c.FInterID = d.FInterID
                                  AND b.FOrderBillNo = d.FSourceBillNo
                                  AND b.FItemID = d.FItemID
                                  AND a.FBillNo = @FBillNo ";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@FBillNo", DbType.String, item.SaleBillNo);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }

        }

        /// <summary>
        /// 调用存储过程，根据销售发票号获取相应的额外费用
        /// </summary>
        /// <param name="sale_bill_no">销售发票号</param>
        /// <returns>额外费用</returns>
        public string getExtraCharges(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
           
            string storedProcedureName = "Cal_Extra_Charges";
            DbCommand cmd = db.GetStoredProcCommand(storedProcedureName);
            db.AddInParameter(cmd, "sale_bill_no", DbType.AnsiString, sale_bill_no);
            db.AddOutParameter(cmd, "extra_charges", DbType.Double, sizeof(Double));
            db.ExecuteNonQuery(cmd);
            object extra_charges = db.GetParameterValue(cmd, "extra_charges");
            return extra_charges.ToString();
          
        }

        public DataSet getProfitBudgetList(List<Int32> lists)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string FInterIDs = "";
            if(lists.Count==0){
                return  null;
            }else {
                FInterIDs = lists[0].ToString();
                for (int i = 1; i < lists.Count; i++)
                {
                    FInterIDs += "," + lists[i].ToString();
                }
            }

            string sql = @"  SELECT b.* FROM [dbo].[ProfitBudget] a, [dbo].[ProfitBudgetList] b WHERE a.sale_bill_no=b.sale_bill_no AND a.audit_state=1 and  b.FInterID IN (" + FInterIDs + "); ";
            using (DbConnection cn = db.CreateConnection())
            {
                
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }

        }

        public void addProfitBudgetHead(T_ProfitAccounting item)
        {
            Database db = Dao.GetDatabase();
            string sql = @" INSERT INTO [dbo].[ProfitAccounting]
								   ([FInterID]
								   ,[sale_bill_no]
								   ,[extra_charges]
								   ,[update_time]
								   ,[audit_state]
                                   ,[is_actual_audit])
							 VALUES
								   (@FInterID
								   ,@sale_bill_no
								   ,@extra_charges
								   ,@update_time
								   ,@audit_state
                                   ,@is_actual_audit);";
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
                    db.AddInParameter(cmd, "@is_actual_audit", DbType.Boolean, item.IsActualAudit);
                    db.ExecuteNonQuery(cmd);

                }

            }
            catch
            {
                throw new Exception("插入利润核算表头失败。");
            }
        }

        public void addProfitBudgetList(List<T_ProfitAccountingList> lists)
        {
            Database db = Dao.GetDatabase();

            string sql = @" INSERT INTO [dbo].[ProfitAccountingList]
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
                                    ,[commission]
                                   ,[SEOrderID]
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
                                   ,@commission
                                   ,@SEOrderID
                                    ,@FName
                                    ,@FNumber);";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    foreach (T_ProfitAccountingList list in lists)
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
                        db.AddInParameter(cmd, "@commission", DbType.Decimal, list.Commission);
                        db.AddInParameter(cmd, "@SEOrderID", DbType.String, list.SEOrderID);
                        db.AddInParameter(cmd, "@FName", DbType.String, list.FName);
                        db.AddInParameter(cmd, "@FNumber", DbType.String, list.FNumber);
                        db.ExecuteNonQuery(cmd);
                    }

                }
            }
            catch
            {
                throw new Exception("保存利润核算表体失败。");
            }
        }
        public void updateProfitBudgetHead(T_ProfitAccounting item)
        {
            Database db = Dao.GetDatabase();
            string sql = @" UPDATE [dbo].[ProfitAccounting]
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
                throw new Exception("更新利润核算表头失败。");
            }
        }
        public void updateProfitBudgetList(List<T_ProfitAccountingList> lists)
        {
            Database db = Dao.GetDatabase();

            string sql = @" UPDATE  [dbo].[ProfitAccountingList]
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
                                      ,[commission] = @commission
									  ,[volume]=@volume
							WHERE   [FInterID] = @FInterID
									AND [Sale_FEntryID] = @Sale_FEntryID
									AND [Buy_FEntryID] = @Buy_FEntryID;";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    foreach (T_ProfitAccountingList list in lists)
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
                        db.AddInParameter(cmd, "@commission", DbType.Decimal, list.Commission);
                        db.AddInParameter(cmd, "@volume", DbType.Decimal, list.Volume);

                        db.ExecuteNonQuery(cmd);
                    }

                }
            }
            catch
            {
                throw new Exception("更新利润核算表体失败。");
            }
        }

        public DataSet getProfitBudgetByID(int FInterIID, string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[ProfitAccounting] a,[dbo].[ProfitAccountingList] b
							WHERE a.[FInterID] = b.[FInterID]
							AND a.[sale_bill_no] = b.[sale_bill_no]
							AND a.[sale_bill_no] = @sale_bill_no ";
            if (FInterIID != 0)
            {
                sql += " AND a.[FInterID] = @FInterID ";
            }
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    if (FInterIID != 0)
                    {
                        db.AddInParameter(cmd, "@FInterID", DbType.Int32, FInterIID);
                    }
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
                    return db.ExecuteDataSet(cmd);
                }
            }
            catch
            {
                throw new Exception("执行getProfitBudgetByID出错。");
            }

        }
        public void deleteBySaleBillNo(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE FROM [dbo].[ProfitAccounting]
								  WHERE [sale_bill_no]=@sale_bill_no;
						   DELETE FROM [dbo].[ProfitAccountingList]
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
                throw new Exception("删除利润核算表数据失败。");
            }
        }
        public DataSet getProfitBudgetSummary()
        {
            Database db = Dao.GetDatabase();
            string sql = @" SELECT *  FROM [dbo].[ProfitAccounting] order by update_time desc";
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
                throw new Exception("获取利润核算表头失败");
            }

        }
        public DataSet getProfitBudgetSummaryByID(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"  SELECT *  FROM [dbo].[ProfitAccounting] WHERE sale_bill_no like @sale_bill_no ";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, "%"+sale_bill_no+"%");

                    return db.ExecuteDataSet(cmd);
                }
            }
            catch
            {
                throw new Exception("根据销售发票号获取利润核算表头失败");
            }

        }
        public void audit(string sale_bill_no, bool flag)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE  [dbo].[ProfitAccounting]
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
                throw new Exception("审批利润核算表数据失败。");
            }
        }
        public void IsActualAudit(string sale_bill_no, bool flag)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE  [dbo].[ProfitAccounting]
							SET     [update_time] = @update_time ,
									[is_actual_audit] = @is_actual_audit
							WHERE   sale_bill_no = @sale_bill_no;";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@sale_bill_no", DbType.String, sale_bill_no);
                    db.AddInParameter(cmd, "@update_time", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@is_actual_audit", DbType.Boolean, flag);
                    db.ExecuteNonQuery(cmd);

                }

            }
            catch
            {
                throw new Exception("审批利润核算表数据失败。");
            }
        }
        public DataSet getActualProfit(string sale_bill_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT  MIN(a.extra_charges) AS extra_charges ,
                                    SUM(commission) AS commission ,
                                    SUM(sale_price * sale_qty * exchange_rate) AS actual_amount ,
                                    SUM(b.buy_price * b.buy_qty) AS actual_pay
                            FROM    [dbo].[ProfitAccounting] a ,
                                    [dbo].[ProfitAccountingList] b
                            WHERE   a.[FInterID] = b.[FInterID]
                                    AND a.[sale_bill_no] = b.[sale_bill_no]
                                    AND a.[sale_bill_no] = @sale_bill_no";
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
                throw new Exception("获取实际利润核算数据失败。");
            }
        }
    }
}
