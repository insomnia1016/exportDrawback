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
    public class EntryManager : BaseManager<T_EntryList>, IEntryManager
    {
        public void addEntryList(T_EntryList entity)
        {
            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[entry_list]
                               ([owner_name]
                               ,[d_date]
                               ,[agent_name]
                               ,[entry_id]
                               ,[g_no]
                               ,[g_name]
                               ,[g_qty]
                               ,[g_unit]
                               ,[decl_price]
                               ,[decl_total]
                               ,[code_ts]
                               ,[drawback_rate]
                               ,[operator]
                               ,[operate_time]
                               ,[id])
                         VALUES
                               (@owner_name
                               ,@d_date
                               ,@agent_name
                               ,@entry_id
                               ,@g_no
                               ,@g_name
                               ,@g_qty
                               ,@g_unit
                               ,@decl_price
                               ,@decl_total
                               ,@code_ts
                               ,@drawback_rate
                               ,@operator
                               ,@operate_time
                               ,@id)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@owner_name", DbType.String, entity.OwnerName);
                    db.AddInParameter(cmd, "@d_date", DbType.DateTime, entity.DDate);
                    db.AddInParameter(cmd, "@agent_name", DbType.String, entity.AgentName);
                    db.AddInParameter(cmd, "@entry_id", DbType.String, entity.EntryId);
                    db.AddInParameter(cmd, "@g_no", DbType.Decimal, entity.GNo);
                    db.AddInParameter(cmd, "@g_name", DbType.String, entity.GName);
                    db.AddInParameter(cmd, "@g_qty", DbType.Decimal, entity.GQty);
                    db.AddInParameter(cmd, "@g_unit", DbType.String, entity.GUnit);
                    db.AddInParameter(cmd, "@decl_price", DbType.Decimal, entity.DeclPrice);
                    db.AddInParameter(cmd, "@decl_total", DbType.Decimal, entity.DeclTotal);
                    db.AddInParameter(cmd, "@code_ts", DbType.String, entity.CodeTs);
                    db.AddInParameter(cmd, "@drawback_rate", DbType.Decimal, entity.DrawbackRate);
                    db.AddInParameter(cmd, "@operator", DbType.String, entity.Operator);
                    db.AddInParameter(cmd, "@operate_time", DbType.DateTime, entity.OperateTime);
                    db.AddInParameter(cmd, "@id", DbType.Int32, entity.Id);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("录入出口报关明细失败");
                }
            }
        }

        /// <summary>
        /// 获取报关单明细表
        /// </summary>
        /// <param name="number"> 默认返回50条记录</param>
        /// <returns></returns>
        public DataSet getEntryList()
        {
            Database db = Dao.GetDatabase();
            string sql = @"select  * from entry_list where invoice_flag is NUll or invoice_flag = 0  order by operate_time desc";
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
                    throw new Exception("获取报关单明细表失败");
                }
            }
        }


        public void invoice(List<T_ContractList> lists)
        {

            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[entry_list]
                           SET invoice_flag = @invoice_flag
                         WHERE entry_id = @entry_id and g_no = @g_no";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    foreach (T_ContractList list in lists)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@invoice_flag", DbType.Boolean, true);
                        db.AddInParameter(cmd, "@entry_id", DbType.String, list.EntryId);
                        db.AddInParameter(cmd, "@g_no", DbType.Decimal, list.GNo);
                       
                        db.ExecuteNonQuery(cmd);
                    }

                }
            }
            catch
            {
                throw new Exception("更新报关单明细标志位出错,请检查人品");
            }
        }

    }
}
