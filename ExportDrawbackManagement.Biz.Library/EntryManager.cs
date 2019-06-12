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
                    db.AddInParameter(cmd, "@operate_time", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@id", DbType.Int32, entity.Id);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("录入出口报关明细失败");
                }
            }
        }


        public void insertEntryList(DataTable dt)
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
                               ,[id]
                               ,[trade_curr])
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
                               ,@id
                               ,@trade_curr)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@owner_name", DbType.String, dr["owner_name"].ToString());
                        db.AddInParameter(cmd, "@d_date", DbType.DateTime, DateTime.Parse(dr["d_date"].ToString()));
                        db.AddInParameter(cmd, "@agent_name", DbType.String, dr["agent_name"].ToString());
                        db.AddInParameter(cmd, "@entry_id", DbType.String, dr["entry_id"].ToString());
                        db.AddInParameter(cmd, "@g_no", DbType.Decimal, Decimal.Parse(dr["g_no"].ToString()));
                        db.AddInParameter(cmd, "@g_name", DbType.String, dr["g_name"].ToString());
                        db.AddInParameter(cmd, "@g_qty", DbType.Decimal, Decimal.Parse(dr["g_qty"].ToString()));
                        db.AddInParameter(cmd, "@g_unit", DbType.String, dr["g_unit"].ToString());
                        db.AddInParameter(cmd, "@trade_curr", DbType.String, dr["trade_curr"].ToString());
                        db.AddInParameter(cmd, "@decl_price", DbType.Decimal, Decimal.Parse(dr["decl_price"].ToString()));
                        db.AddInParameter(cmd, "@decl_total", DbType.Decimal, Decimal.Parse(dr["decl_total"].ToString()));
                        db.AddInParameter(cmd, "@code_ts", DbType.String, dr["code_ts"].ToString());
                        db.AddInParameter(cmd, "@drawback_rate", DbType.Decimal, Decimal.Parse(dr["drawback_rate"].ToString()));
                        db.AddInParameter(cmd, "@operator", DbType.String, dr["operator"].ToString());
                        db.AddInParameter(cmd, "@operate_time", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(cmd, "@id", DbType.Int32, Int32.Parse(dr["id"].ToString()));

                        db.ExecuteNonQuery(cmd); 
                    }

                    
                }
                catch (Exception ex)
                {
                    throw ex;
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

        public DataSet getListsAll()
        {
            Database db = Dao.GetDatabase();
            string sql = @"select  * from entry_list   order by invoice_flag asc, operate_time desc";
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
                    throw new Exception("获取全部报关单明细表失败");
                }
            }
        }

        public DataSet queryEntryList(T_EntryList item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"SELECT *  FROM [dbo].[entry_list] where 1 = 1 ";
            if (!string.IsNullOrEmpty(item.OwnerName))
            {
                sql += " and owner_name like @owner_name ";
            }
            if (!string.IsNullOrEmpty(item.EntryId))
            {
                sql += " and entry_id = @entry_id ";
            }
            if (!string.IsNullOrEmpty(item.GName))
            {
                sql += " and g_name like @g_name ";
            }
            if (!string.IsNullOrEmpty(item.CodeTs))
            {
                sql += " and code_ts = @code_ts ";
            }
            if (!string.IsNullOrEmpty(item.AgentName))
            {
                sql += " and agent_name like @agent_name ";
            }
            if (!string.IsNullOrEmpty(item.Operator))
            {
                sql += " and operator like @operator ";
            }
            if (!string.IsNullOrEmpty(item.startTime.ToString()))
            {
                sql += " and d_date >= @startTime ";
            }
            if (!string.IsNullOrEmpty(item.endTime.ToString()))
            {
                sql += " and d_date <= @endTime ";
            }
            sql += " order by invoice_flag asc, operate_time desc ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    if (!string.IsNullOrEmpty(item.OwnerName))
                    {
                        db.AddInParameter(cmd, "@owner_name", DbType.String, "%" + item.OwnerName + "%");
                    }
                    if (!string.IsNullOrEmpty(item.EntryId))
                    {
                        db.AddInParameter(cmd, "@entry_id", DbType.String, item.EntryId);
                    }
                    if (!string.IsNullOrEmpty(item.GName))
                    {
                        db.AddInParameter(cmd, "@g_name", DbType.String, "%" + item.GName + "%");
                    }
                    if (!string.IsNullOrEmpty(item.CodeTs))
                    {
                        db.AddInParameter(cmd, "@code_ts", DbType.String, item.CodeTs);
                    }
                    if (!string.IsNullOrEmpty(item.AgentName))
                    {
                        db.AddInParameter(cmd, "@agent_name", DbType.String, "%" + item.AgentName + "%");
                    }
                    if (!string.IsNullOrEmpty(item.Operator))
                    {
                        db.AddInParameter(cmd, "@operator", DbType.String, "%" + item.Operator + "%");
                    }
                    if (!string.IsNullOrEmpty(item.startTime.ToString()))
                    {
                        db.AddInParameter(cmd, "@startTime", DbType.DateTime, item.startTime);
                    }
                    if (!string.IsNullOrEmpty(item.endTime.ToString()))
                    {
                        db.AddInParameter(cmd, "@endTime", DbType.DateTime, DateTime.Parse(item.endTime.ToString().Split(' ').First() + " 23:59:59"));
                    }
                    return db.ExecuteDataSet(cmd);
                }
                catch
                {
                    throw new Exception("查询报关明细表失败");
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

        public void delete(string entry_id, int g_no)
        {
            Database db = Dao.GetDatabase();
            string sql = @"DELETE FROM [dbo].[entry_list] WHERE entry_id = @entry_id and g_no = @g_no ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@entry_id", DbType.String, entry_id);
                    db.AddInParameter(cmd, "@g_no", DbType.Int32, g_no);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void update(T_EntryList item)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[entry_list]
                           SET [agent_name] = @agent_name
                              ,[d_date] = @d_date
                              ,[owner_name] = @owner_name
                              ,[g_name] = @g_name
                              ,[g_qty] = @g_qty
                              ,[g_unit] = @g_unit
                              ,[decl_price] = @decl_price
                              ,[decl_total] = @decl_total
                              ,[code_ts] = @code_ts
                              ,[drawback_rate] = @drawback_rate
                              ,[operator] = @operator
                              ,[operate_time] = @operate_time
                              ,[id] = @id
                              ,[trade_curr] = @trade_curr
                         WHERE entry_id = @entry_id and g_no = @g_no ";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@agent_name", DbType.String, item.AgentName);
                    db.AddInParameter(cmd, "@d_date", DbType.DateTime, item.DDate);
                    db.AddInParameter(cmd, "@owner_name", DbType.String, item.OwnerName);
                    db.AddInParameter(cmd, "@g_name", DbType.String, item.GName);
                    db.AddInParameter(cmd, "@g_qty", DbType.Decimal, item.GQty);
                    db.AddInParameter(cmd, "@g_unit", DbType.String, item.GUnit);
                    db.AddInParameter(cmd, "@decl_price", DbType.Decimal, item.DeclPrice);
                    db.AddInParameter(cmd, "@decl_total", DbType.Decimal, item.DeclTotal);
                    db.AddInParameter(cmd, "@code_ts", DbType.String, item.CodeTs);
                    db.AddInParameter(cmd, "@drawback_rate", DbType.Decimal, item.DrawbackRate);
                    db.AddInParameter(cmd, "@operator", DbType.String, item.Operator);
                    db.AddInParameter(cmd, "@operate_time", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
                    db.AddInParameter(cmd, "@trade_curr", DbType.String, item.TradeCurr);
                    db.AddInParameter(cmd, "@entry_id", DbType.String, item.EntryId);
                    db.AddInParameter(cmd, "@g_no", DbType.Int32, item.GNo);
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
