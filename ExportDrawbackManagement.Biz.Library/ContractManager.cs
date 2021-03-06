﻿using System;
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
    public class ContractManager : BaseManager<T_ContractHead>, IContractManager
    {
        public void addContractHead(T_ContractHead head)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[contract_head]
                           ([contract_id]
                           ,[xufang]
                           ,[xufang_address]
                           ,[xufang_jingbanren]
                           ,[xufang_tel]
                           ,[xufang_fadingdaibiaoren]
                           ,[xufang_dailiren]
                           ,[xufang_qianzi_date]
                           ,[gongfang]
                           ,[gongfang_address]
                           ,[gongfang_tel]
                           ,[gongfang_jingbanren]
                           ,[gongfang_fadingdaibiaoren]
                           ,[gongfang_dailiren]
                           ,[gongfang_qianzi_date]
                           ,[delivery_date]
                           ,[invoice_all]
                           ,[delivery_mode]
                           ,[payment_days])
                     VALUES
                           (@contract_id
                           ,@xufang
                           ,@xufang_address
                           ,@xufang_jingbanren
                           ,@xufang_tel
                           ,@xufang_fadingdaibiaoren
                           ,@xufang_dailiren
                           ,@xufang_qianzi_date
                           ,@gongfang
                           ,@gongfang_address
                           ,@gongfang_tel
                           ,@gongfang_jingbanren
                           ,@gongfang_fadingdaibiaoren
                           ,@gongfang_dailiren
                           ,@gongfang_qianzi_date
                           ,@delivery_date
                           ,@invoice_all
                           ,@delivery_mode
                           ,@payment_days)";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@contract_id", DbType.String, head.ContractId);
                    db.AddInParameter(cmd, "@xufang", DbType.String, head.Xufang);
                    db.AddInParameter(cmd, "@xufang_address", DbType.String, head.XufangAddress);
                    db.AddInParameter(cmd, "@xufang_jingbanren", DbType.String, head.XufangJingbanren);
                    db.AddInParameter(cmd, "@xufang_tel", DbType.String, head.XufangTel);
                    db.AddInParameter(cmd, "@xufang_fadingdaibiaoren", DbType.String, head.XufangFadingdaibiaoren);
                    db.AddInParameter(cmd, "@xufang_dailiren", DbType.String, head.XufangDailiren);
                    db.AddInParameter(cmd, "@xufang_qianzi_date", DbType.DateTime, head.XufangQianziDate);
                    db.AddInParameter(cmd, "@gongfang", DbType.String, head.Gongfang);
                    db.AddInParameter(cmd, "@gongfang_address", DbType.String, head.GongfangAddress);
                    db.AddInParameter(cmd, "@gongfang_tel", DbType.String, head.GongfangTel);
                    db.AddInParameter(cmd, "@gongfang_jingbanren", DbType.String, head.GongfangJingbanren);
                    db.AddInParameter(cmd, "@gongfang_fadingdaibiaoren", DbType.String, head.GongfangFadingdaibiaoren);
                    db.AddInParameter(cmd, "@gongfang_dailiren", DbType.String, head.GongfangDailiren);
                    db.AddInParameter(cmd, "@gongfang_qianzi_date", DbType.DateTime, head.GongfangQianziDate);
                    db.AddInParameter(cmd, "@delivery_date", DbType.DateTime, head.DeliveryDate);
                    db.AddInParameter(cmd, "@invoice_all", DbType.Decimal, head.InvoiceAll);
                    db.AddInParameter(cmd, "@delivery_mode", DbType.String, head.DeliveryMode);
                    db.AddInParameter(cmd, "@payment_days", DbType.Int32, head.PaymentDays);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch
            {
                throw new Exception("保存合同表头信息出错,请检查人品");
            }
        }



        public void addContractList(List<T_ContractList> lists)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[contract_list]
                           ([contract_id]
                           ,[contract_no]
                           ,sale_bill_no]
                           ,[entry_id]
                           ,[g_no]
                           ,[g_name]
                           ,[g_qty]
                           ,[invoice_price]
                           ,[invoice_total]
                           ,[g_unit])
                     VALUES
                           (@contract_id
                           ,@contract_no
                           ,@sale_bill_no
                           ,@entry_id
                           ,@g_no
                           ,@g_name
                           ,@g_qty
                           ,@invoice_price
                           ,@invoice_total
                           ,@g_unit)";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    foreach (T_ContractList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@contract_id", DbType.String, list.ContractId);
                        db.AddInParameter(cmd, "@contract_no", DbType.Decimal, list.ContractNo);
                        db.AddInParameter(cmd, "@sale_bill_no", DbType.String, list.SaleBillNo);
                        db.AddInParameter(cmd, "@entry_id", DbType.String, list.EntryId);
                        db.AddInParameter(cmd, "@g_no", DbType.Decimal, list.GNo);
                        db.AddInParameter(cmd, "@g_name", DbType.String, list.GName);
                        db.AddInParameter(cmd, "@g_qty", DbType.Decimal, list.GQty);
                        db.AddInParameter(cmd, "@invoice_price", DbType.Decimal, list.InvoicePrice);
                        db.AddInParameter(cmd, "@invoice_total", DbType.Decimal, list.InvoiceTotal);
                        db.AddInParameter(cmd, "@g_unit", DbType.String, list.GUnit);
                        db.ExecuteNonQuery(cmd);
                    }

                }
            }
            catch
            {
                throw new Exception("保存合同表头信息出错,请检查人品");
            }
        }


        public DataSet getContractSummary()
        {
            Database db = Dao.GetDatabase();
            string sql = @"select contract_id,xufang,delivery_date,invoice_all,delivery_mode from [dbo].[contract_head]
                            order by delivery_date desc";
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
                throw new Exception("获取合同概要出错,请检查人品");
            }
        }
        public DataSet queryContractSummary(T_ContractHead head)
        {
            Database db = Dao.GetDatabase();
            string sql = @"select distinct a.contract_id,xufang,delivery_date,invoice_all,delivery_mode from [dbo].[contract_head] a,[dbo].[contract_list] b
                            where a.contract_id = b.contract_id ";
            if (!string.IsNullOrEmpty(head.ContractId))
            {
                sql += "and a.contract_id = @contract_id ";
            }
            if (!string.IsNullOrEmpty(head.baoguandanhao))
            {
                sql += " and entry_id = @entry_id ";
            }
            if (!string.IsNullOrEmpty(head.xiaoshoufapiaohao))
            {
                sql += " and sale_bill_no = @sale_bill_no ";
            }
            if (!string.IsNullOrEmpty(head.Xufang))
            {
                sql += " and xufang like @xufang ";
            }
            if (!string.IsNullOrEmpty(head.XufangJingbanren))
            {
                sql += " and xufang_jingbanren like @xufang_jingbanren ";
            }
            if (!string.IsNullOrEmpty(head.startTime.ToString()))
            {
                sql += " and delivery_date > @startTime ";
            }
            if (!string.IsNullOrEmpty(head.endTime.ToString()))
            {
                sql += " and delivery_date < @endTime ";
            }
            sql += " order by delivery_date desc ";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    if (!string.IsNullOrEmpty(head.ContractId))
                    {
                        db.AddInParameter(cmd, "@contract_id", DbType.String, head.ContractId);
                    }
                    if (!string.IsNullOrEmpty(head.baoguandanhao))
                    {
                        db.AddInParameter(cmd, "@entry_id", DbType.String, head.baoguandanhao);

                    }
                    if (!string.IsNullOrEmpty(head.xiaoshoufapiaohao))
                    {
                        db.AddInParameter(cmd, "@sale_bill_no", DbType.String, head.xiaoshoufapiaohao);
                    }
                    if (!string.IsNullOrEmpty(head.Xufang))
                    {
                        db.AddInParameter(cmd, "@xufang", DbType.String, "%" + head.Xufang + "%");

                    }
                    if (!string.IsNullOrEmpty(head.XufangJingbanren))
                    {
                        db.AddInParameter(cmd, "@xufang_jingbanren", DbType.String, "%" + head.XufangJingbanren + "%");

                    }
                    if (!string.IsNullOrEmpty(head.startTime.ToString()))
                    {
                        db.AddInParameter(cmd, "@startTime", DbType.DateTime, head.startTime);

                    }
                    if (!string.IsNullOrEmpty(head.endTime.ToString()))
                    {
                        db.AddInParameter(cmd, "@endTime", DbType.DateTime, DateTime.Parse(head.endTime.ToString().Split(' ').First() + " 23:59:59"));
                    }
                    return db.ExecuteDataSet(cmd);
                }
            }
            catch
            {
                throw new Exception("获取合同概要出错,请检查人品");
            }
        }

        public T_ContractHead getContractDetail(string id)
        {
            T_ContractHead item = new T_ContractHead();
            Database db = Dao.GetDatabase();
            string sql1 = @"select * from contract_head where contract_id = @contract_id order by delivery_date desc";
            string sql2 = @"select * from contract_list where contract_id = @contract_id  order by g_no";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql1);
                    db.AddInParameter(cmd, "@contract_id", DbType.String, id);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    DataRow dr = ds.Tables[0].Rows[0];
                    item.Xufang = dr["xufang"].ToString();
                    item.XufangAddress = dr["xufang_address"].ToString();
                    item.XufangJingbanren = dr["xufang_jingbanren"].ToString();
                    item.XufangTel = dr["xufang_tel"].ToString();
                    item.XufangFadingdaibiaoren = dr["xufang_fadingdaibiaoren"].ToString();
                    item.XufangDailiren = dr["xufang_dailiren"].ToString();
                    if (!string.IsNullOrEmpty(dr["xufang_qianzi_date"].ToString()))
                    {
                        item.XufangQianziDate = DateTime.Parse(dr["xufang_qianzi_date"].ToString());

                    }
                    item.Gongfang = dr["gongfang"].ToString();
                    item.GongfangAddress = dr["gongfang_address"].ToString();
                    item.GongfangTel = dr["gongfang_tel"].ToString();
                    item.GongfangJingbanren = dr["gongfang_jingbanren"].ToString();
                    item.GongfangFadingdaibiaoren = dr["gongfang_fadingdaibiaoren"].ToString();
                    item.GongfangDailiren = dr["gongfang_dailiren"].ToString();
                    if (!string.IsNullOrEmpty(dr["gongfang_qianzi_date"].ToString()))
                    {
                        item.GongfangQianziDate = DateTime.Parse(dr["gongfang_qianzi_date"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["delivery_date"].ToString()))
                    {
                        item.DeliveryDate = DateTime.Parse(dr["delivery_date"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["invoice_all"].ToString()))
                    {
                        item.InvoiceAll = decimal.Parse(dr["invoice_all"].ToString());
                    }
                    CommonManager cm = new CommonManager();
                    item.DeliveryMode = cm.getDeliveryModeNameByCode(dr["delivery_mode"].ToString());
                    if (!string.IsNullOrEmpty(dr["payment_days"].ToString()))
                    {
                        item.PaymentDays = Int32.Parse(dr["payment_days"].ToString());
                    }

                    DbCommand cmd1 = db.GetSqlStringCommand(sql2);
                    db.AddInParameter(cmd1, "@contract_id", DbType.String, id);
                    DataSet ds1 = db.ExecuteDataSet(cmd1);
                    ds1.Tables[0].Columns.Add("delivery_date", typeof(System.DateTime));
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        row["delivery_date"] = item.DeliveryDate;
                    }

                    DataRow dr1 = ds1.Tables[0].NewRow();

                    dr1["g_name"] = "合计：";
                    dr1["g_qty"] = DBNull.Value;
                    dr1["g_unit"] = "";
                    dr1["delivery_date"] = DBNull.Value;
                    dr1["invoice_price"] = DBNull.Value;
                    dr1["invoice_total"] = item.InvoiceAll;
                    ds1.Tables[0].Rows.Add(dr1);
                    

                    item.lists = ds1;
                }
                return item;
            }
            catch
            {
                throw new Exception("获取合同详细出错,人品太差了");
            }
        }
    }
}
