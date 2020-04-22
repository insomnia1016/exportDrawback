using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace ExportDrawbackManagement.Biz.Library
{
    class PaymentManager:BaseManager<T_PaymentRequest>,IPaymentManager
    {
        public void insert(T_PaymentRequest item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[payment_request]
                               ([payment_id]
                               ,[payment_date]
                               ,[payee_unit]
                               ,[amount]
                               ,[payee_opening_bank]
                               ,[payee_account_id]
                               ,[customer_bill_no]
                               ,[factory_bill_no]
                               ,[goods_model]
                               ,[payment_explain]
                               ,[note]
                               ,[dept_name]
                               ,[emp_name]
                               ,[isUserd])
                         VALUES
                               (@payment_id
                               ,@payment_date
                               ,@payee_unit
                               ,@amount
                               ,@payee_opening_bank
                               ,@payee_account_id
                               ,@customer_bill_no
                               ,@factory_bill_no
                               ,@goods_model
                               ,@payment_explain
                               ,@note
                               ,@dept_name
                               ,@emp_name
                               ,@isUserd)";
            using (DbConnection cn = db.CreateConnection())
            {

                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@payment_id", DbType.String, item.PaymentId);
                    db.AddInParameter(cmd, "@payment_date", DbType.DateTime, item.PaymentDate);
                    db.AddInParameter(cmd, "@payee_unit", DbType.String, item.PayeeUnit);
                    db.AddInParameter(cmd, "@amount", DbType.Decimal, item.Amount);
                    db.AddInParameter(cmd, "@payee_opening_bank", DbType.String, item.PayeeOpeningBank);
                    db.AddInParameter(cmd, "@payee_account_id", DbType.String, item.PayeeAccountId);
                    db.AddInParameter(cmd, "@customer_bill_no", DbType.String, item.CustomerBillNo);
                    db.AddInParameter(cmd, "@factory_bill_no", DbType.String, item.FactoryBillNo);
                    db.AddInParameter(cmd, "@goods_model", DbType.String, item.GoodsModel);
                    db.AddInParameter(cmd, "@payment_explain", DbType.String, item.PaymentExplain);
                    db.AddInParameter(cmd, "@note", DbType.String, item.Note);
                    db.AddInParameter(cmd, "@dept_name", DbType.String, item.DeptName);
                    db.AddInParameter(cmd, "@emp_name", DbType.String, item.EmpName);
                    db.AddInParameter(cmd, "@isUserd", DbType.Boolean, item.IsUserd);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("新增付款申请数据失败,请检查人品");
                }
            }
        }
        public void update(T_PaymentRequest item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE  [dbo].[payment_request]
                            SET     isUserd = @isUserd
                            WHERE   payment_id = @payment_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@isUserd", DbType.Boolean, item.IsUserd);
                    db.AddInParameter(cmd, "@payment_id", DbType.String, item.PaymentId);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("更新付款申请数据失败,请检查人品");
                }
            }
        }
        public DataSet getPaymentByID(string  payment_id)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[payment_request] WHERE payment_id = @payment_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@payment_id", DbType.String, payment_id);
                    return db.ExecuteDataSet(cmd);
                }
                catch
                {
                    throw new Exception("根据id获取付款申请数据失败，扑街仔。。。");
                }
            }
        }

        public DataSet getPayments(T_PaymentRequest item ,string start_time ,string end_time)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT * FROM [dbo].[payment_request] WHERE 1=1 ";
            if (!string.IsNullOrEmpty(item.PaymentId))
            {
                sql += " and payment_id = @payment_id ";
            }
            if (!string.IsNullOrEmpty(item.PayeeUnit))
            {
                sql += " and payee_unit = @payee_unit ";
            }
            if (!string.IsNullOrEmpty(item.PayeeOpeningBank))
            {
                sql += " and payee_opening_bank = @payee_opening_bank ";
            }
            if (!string.IsNullOrEmpty(item.PayeeAccountId))
            {
                sql += " and payee_account_id = @[payee_account_id ";
            }
            if (!string.IsNullOrEmpty(item.CustomerBillNo))
            {
                sql += " and customer_bill_no = @customer_bill_no ";
            }
            if (!string.IsNullOrEmpty(item.FactoryBillNo))
            {
                sql += " and factory_bill_no = @factory_bill_no ";
            }
            if (!string.IsNullOrEmpty(item.GoodsModel))
            {
                sql += " and goods_model = @goods_model ";
            }
            if (!string.IsNullOrEmpty(item.EmpName))
            {
                sql += " and emp_name = @emp_name ";
            }
            if (!string.IsNullOrEmpty(start_time))
            {
                sql += " and payment_date > @start_time ";
            }
            if (!string.IsNullOrEmpty(end_time))
            {
                sql += " and payment_date < @end_time ";
            }
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    if (!string.IsNullOrEmpty(item.PaymentId))
                    {
                        db.AddInParameter(cmd, "@payment_id", DbType.String, item.PaymentId);
                    }
                    if (!string.IsNullOrEmpty(item.PayeeUnit))
                    {
                        db.AddInParameter(cmd, "@payee_unit", DbType.String, item.PayeeUnit);

                    }
                    if (!string.IsNullOrEmpty(item.PayeeOpeningBank))
                    {
                        db.AddInParameter(cmd, "@payee_opening_bank", DbType.String, item.PayeeOpeningBank);

                    }
                    if (!string.IsNullOrEmpty(item.PayeeAccountId))
                    {
                        db.AddInParameter(cmd, "@payee_account_id", DbType.String, item.PayeeAccountId);

                    }
                    if (!string.IsNullOrEmpty(item.CustomerBillNo))
                    {
                        db.AddInParameter(cmd, "@customer_bill_no", DbType.String, item.CustomerBillNo);

                    }
                    if (!string.IsNullOrEmpty(item.FactoryBillNo))
                    {
                        db.AddInParameter(cmd, "@factory_bill_no", DbType.String, item.FactoryBillNo);

                    }
                    if (!string.IsNullOrEmpty(item.GoodsModel))
                    {
                        db.AddInParameter(cmd, "@goods_model", DbType.String, item.GoodsModel);

                    }
                    if (!string.IsNullOrEmpty(item.EmpName))
                    {
                        db.AddInParameter(cmd, "@emp_name", DbType.String, item.EmpName);

                    }
                    if (!string.IsNullOrEmpty(start_time))
                    {
                        db.AddInParameter(cmd, "@start_time", DbType.DateTime, DateTime.Parse(start_time));

                    }
                    if (!string.IsNullOrEmpty(end_time))
                    {
                        db.AddInParameter(cmd, "@end_time", DbType.DateTime, DateTime.Parse(end_time));

                    }
                    return db.ExecuteDataSet(cmd);
                }
                catch
                {
                    throw new Exception("根据id获取付款申请数据失败，扑街仔。。。");
                }
            }
        }
        //getLastReceiptID
        public string getLastReceiptID(string key)
        {
            Database db = Dao.GetDatabase();
            string lastReceiptID = string.Empty;
            string sql = @"SELECT * FROM [dbo].[payment_receipt_head] where  receipt_id like @receipt_id order by receipt_id desc";
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
        public string getLastPaymentID(string key)
        {
            Database db = Dao.GetDatabase();
            string lastBillNo = string.Empty;
            string sql = @"SELECT * FROM [dbo].[payment_request] where  payment_id like @payment_id order by payment_id desc";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@payment_id", DbType.String, key + "%");
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string number = ds.Tables[0].Rows[0]["payment_id"].ToString().Substring(11, 2);
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
                    throw new Exception("根据当前付款申请单取最大编号失败");
                }
            }
        }
        public DataSet getSupplierInfoByName(string name)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = @"SELECT * FROM T_Supplier WHERE FName=@name";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@name", DbType.String, name);
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }
        public int getIDByName(string name)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = @"SELECT FItemID FROM T_Supplier WHERE FName=@name";
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

        public DataSet getUnCheckReceiptLists(int FSupplyID, int currencyID)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = @"SELECT *
                            FROM IcPurChase
                            WHERE FCheckStatus <> 2
                                  AND FSupplyID = @FSupplyID
                                  AND FCurrencyID = @FCurrencyID
                            ORDER BY FDate DESC;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@FSupplyID", DbType.Int32, FSupplyID);
                db.AddInParameter(cmd, "@FCurrencyID", DbType.Int32, currencyID);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public DataSet getDoneBillNos(string customer_name)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[PaymentBillNoDone] where customer_name=@customer_name";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@customer_name", DbType.String, customer_name);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public void addReceiptHead(T_PaymentReceiptHead head)
        {

            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[payment_receipt_head]
                               ([receipt_id]
                               ,[account_id]
                               ,[receipt_date]
                               ,[receipt_type]
                               ,[customer_code]
                               ,[customer_name]
                               ,[amount]
                               ,[currency]
                               ,[receipt_charge]
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
                throw new Exception("添加付款单表头数据失败,系统要炸了，倒计时开始...");
            }
        }

        public void insertReceiptList(List<T_PaymentReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[payment_receipt_list]
                               ([receipt_id]
                               ,[receipt_no]
                               ,[FBillNo]
                               ,[InDecrease_no]
                               ,[Deposit_id]
                               ,[FDate]
                               ,[FPurchaseAmountFor]
                               ,[FPayAmountFor]
                               ,[FUnPayAmountFor]
                               ,[FCheckAmountFor]
                               ,[FCurrencyID]
                               ,[FCheckStatus]
                               ,[FNote]
                               ,[RequestID])
                         VALUES
                               (@receipt_id
                               ,@receipt_no
                               ,@FBillNo
                               ,@InDecrease_no
                               ,@Deposit_id
                               ,@FDate
                               ,@FPurchaseAmountFor
                               ,@FPayAmountFor
                               ,@FUnPayAmountFor
                               ,@FCheckAmountFor
                               ,@FCurrencyID
                               ,@FCheckStatus
                               ,@FNote
                               ,@RequestID)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_PaymentReceiptList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@receipt_id", DbType.String, list.ReceiptId);
                        db.AddInParameter(cmd, "@receipt_no", DbType.String, list.ReceiptNo);
                        db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
                        db.AddInParameter(cmd, "@InDecrease_no", DbType.String, list.IndecreaseNo);
                        db.AddInParameter(cmd, "@Deposit_id", DbType.String, list.DepositId);
                        db.AddInParameter(cmd, "@FDate", DbType.DateTime, list.FDate);
                        db.AddInParameter(cmd, "@FPurchaseAmountFor", DbType.Decimal, list.FPurchaseAmountFor);
                        db.AddInParameter(cmd, "@FPayAmountFor", DbType.Decimal, list.FPayAmountFor);
                        db.AddInParameter(cmd, "@FUnPayAmountFor", DbType.Decimal, list.FUnPayAmountFor);
                        db.AddInParameter(cmd, "@FCheckAmountFor", DbType.Decimal, list.FCheckAmountFor);
                        db.AddInParameter(cmd, "@FCurrencyID", DbType.Int32, list.FCurrencyID);
                        db.AddInParameter(cmd, "@FCheckStatus", DbType.Int32, list.FCheckStatus);
                        db.AddInParameter(cmd, "@FNote", DbType.String, list.FNote);
                        db.AddInParameter(cmd, "@RequestID", DbType.String, list.RequestID);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("批量增加付款单表体出错，不要找我。");
                }
            }
        }

        public void addToDone(List<T_PaymentReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[PaymentBillNoDone]
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
                    foreach (T_PaymentReceiptList list in lists)
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
                    throw new Exception("批量增加已付款的采购发票单号出错，不要找我。");
                }
            }
        }

        public DataSet getReceiptAuditHeads()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[payment_receipt_head]";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                //db.AddInParameter(cmd, "@name", DbType.String, name);
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }

        public DataSet getRequestList(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT a.receipt_no,
                               a.RequestID,
                               a.FPurchaseAmountFor,
                               b.payee_unit,
                               b.emp_name,
                               b.payment_date
                        FROM [dbo].[payment_receipt_list] a,
                              [dbo].[payment_request] b
                        WHERE a.InDecrease_no = b.payment_id
                              AND a.FBillNo IS NULL
                              AND a.FDate IS NULL
                              AND receipt_id = @receipt_id;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
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
									a.FPurchaseAmountFor ,
									b.customer ,
									b.agenter ,
									b.agent_date
							FROM    [dbo].[payment_receipt_list] a ,
									[dbo].[payment_Indecrease_head] b
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
                                   a.FPurchaseAmountFor,
                                   a.FPayAmountFor,
                                   a.FUnPayAmountFor,
                                   a.FCheckAmountFor,
                                   a.FCurrencyID,
                                   b.agenter,
                                   a.FNote
                            FROM [dbo].[payment_receipt_list] a,
                                 [dbo].[payment_deposit_head] b
                            WHERE a.Deposit_id = b.deposit_id
                                  AND FBillNo IS NULL
                                  AND a.FDate IS NOT NULL
                                  AND a.receipt_id = @receipt_id;";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }


        public void deleteToDone(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE  FROM [dbo].[PaymentBillNoDone] WHERE   [receipt_id] =@receipt_id";
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
                    throw new Exception("删除已付款的销售发票单号出错。");
                }
            }
        }
        public void deleteReceipt(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE FROM [dbo].[payment_receipt_head] WHERE [receipt_id]=@receipt_id;
						   DELETE FROM [dbo].[payment_receipt_list] WHERE [receipt_id]=@receipt_id;";
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
                    throw new Exception("删除付款单失败");
                }
            }
        }

        public DataSet getReceiptHeads(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[payment_receipt_head] WHERE receipt_id LIKE @receipt_id";
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
            string sql = @"SELECT * FROM [dbo].[payment_receipt_head] WHERE 1 = 1 ";
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
        public void updateReceiptHeadAuditStatus(T_PaymentReceiptHead head)
        {

            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[payment_receipt_head]
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
                throw new Exception("更新付款单表头audit_status数据失败");
            }
        }
        public void updateReceiptHead(T_PaymentReceiptHead head)
        {

            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[payment_receipt_head]
                           SET [receipt_date] = @receipt_date
                              ,[amount] = @amount
                              ,[receipt_charge] = @receipt_charge
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
                throw new Exception("更新付款单表头数据失败,系统要炸了，倒计时开始...");
            }
        }
        public void updateReceiptList(List<T_PaymentReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[payment_receipt_list]
                            SET [FCheckAmountFor] = @FCheckAmountFor,
                                [FCheckStatus] = @FCheckStatus,
                                [FNote] = @FNote
                            WHERE [receipt_id] = @receipt_id
                                  AND [receipt_no] = @receipt_no;";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_PaymentReceiptList list in lists)
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
                    throw new Exception("批量更新付款单表体出错，不要找我。");
                }
            }
        }
        public void updateToDone(List<T_PaymentReceiptList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[PaymentBillNoDone]
                               SET [check_status] = @check_status
                             WHERE [FBillNo] = @FBillNo";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    foreach (T_PaymentReceiptList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@FBillNo", DbType.String, list.FBillNo);
                        db.AddInParameter(cmd, "@check_status", DbType.Int32, list.FCheckStatus);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("批量更新已付款的销售发票单号check_status出错。");
                }
            }
        }
        public DataSet getReceiptList(string receipt_id)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = @"SELECT * FROM [dbo].[payment_receipt_list] WHERE FBillNo IS NOT NULL and receipt_id = @receipt_id";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@receipt_id", DbType.String, receipt_id);
                ds = db.ExecuteDataSet(cmd);
                return ds;

            }
        }

        public DataSet getICPurChaseList(string start_time, string end_time, string FBillNo, string customer_name, string check_status)
        {
            Database db = Dao.GetDatabase();
            DataSet ds1;
            string sql = @"SELECT a.receipt_id,
                                   a.FBillNo,
                                   a.FDate,
                                   b.customer_name as FName,
                                   a.FPurchaseAmountFor,
                                   (a.FPayAmountFor + a.FCheckAmountFor) AS FPayAmountFor,
                                   (a.FPurchaseAmountFor - a.FPayAmountFor - a.FCheckAmountFor) AS FUnPayAmountFor,
                                   a.FCurrencyID,
                                   a.FCheckStatus,
                                   a.FNote
                            FROM dbo.payment_receipt_list a,
                                 dbo.payment_receipt_head b
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
        public DataSet getICPurChaseListFromKindDee(string start_time, string end_time, string FBillNo, string customer_name, string check_status)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds1;
            string sql = @"SELECT '' AS receipt_id,
                                   a.FBillNo,
                                   a.FDate,
                                   c.FName,
                                   a.FPurchaseAmountFor ,
                                   a.FPayAmountFor,
                                   a.FUnPayAmountFor,
                                   a.FCurrencyID,
                                   a.FCheckStatus,
                                   a.FNote
                            FROM [dbo].[IcPurChase] a,
                                 [dbo].[T_Supplier] c
                            WHERE a.FSupplyID = c.FItemID
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

        public bool depositIsUsed(string deposit_id)
        {
            Database db = Dao.GetDatabase();
            bool result;
            string sql = @" SELECT * FROM [dbo].[payment_receipt_list] WHERE Deposit_id = @Deposit_id ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@Deposit_id", DbType.String, deposit_id);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    return result;

                }
                catch
                {
                    throw new Exception("根据定金单号查询是否存在失败");
                }
            }
        }

        public bool inDecreaseIsUsed(string inDecrease_no)
        {
            Database db = Dao.GetDatabase();
            bool result;
            string sql = @" SELECT * FROM [dbo].[payment_receipt_list] WHERE InDecrease_no = @InDecrease_no ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@InDecrease_no", DbType.String, inDecrease_no);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    return result;

                }
                catch
                {
                    throw new Exception("根据增减单号查询是否存在失败");
                }
            }
        }

        public bool requestIsUsed(string request_id)
        {
            Database db = Dao.GetDatabase();
            bool result;
            string sql = @" SELECT * FROM [dbo].[payment_receipt_list] WHERE RequestID = @RequestID ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@RequestID", DbType.String, request_id);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    return result;

                }
                catch
                {
                    throw new Exception("根据付款通知书号查询是否存在失败");
                }
            }
        }
    }
}
