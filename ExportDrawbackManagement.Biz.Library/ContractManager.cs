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
            string sql = @"select * from contract_head where contract_id = @contract_id";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@contract_id", DbType.String, id);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    DataRow dr = ds.Tables[0].Rows[0];
                    item.Xufang = dr["xufang"].ToString();
                    //item
                }
            }
            catch
            {
                throw new Exception("获取合同概要出错,请检查人品");
            }
        }
    }
}
