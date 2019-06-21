using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
   public class TaxListManager:BaseManager<T_TaxList>,ITaxListManager
    {
       public void generateTaxList(decimal bl)
       {
           Database db = Dao.GetDatabase();
           string sql = @"insert into tax_list (agent_name,d_date,agent_code,owner_name,entry_id,g_no,g_name,g_qty,g_unit,trade_curr,decl_price,decl_total,code_ts,drawback_rate,invoice_price,invoice_total,tax_return_price,tax_return_total,state_code)
                            select b.agent_name,b.d_date,b.agent_code,b.owner_name,b.entry_id,b.g_no,b.g_name,b.g_qty,b.g_unit,b.trade_curr, b.decl_price,b.decl_total,b.code_ts,b.drawback_rate,
                            a.invoice_price,a.invoice_total,(a.invoice_price/@bl*b.drawback_rate) as tax_return_price,(a.invoice_total/@bl*b.drawback_rate) as tax_return_total,'N'
                            from [dbo].[contract_list] a
                            inner join [dbo].[entry_list] b
                            on  a.entry_id = b.entry_id and a.g_no = b.g_no
                            where b.invoice_flag = 0 or b.invoice_flag is null";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@bl", DbType.Decimal, bl);
                    db.ExecuteNonQuery(cmd);
                }
                catch
                {
                    throw new Exception("生成退税明细表失败");
                }
            }

       }

       public DataSet getTaxList()
       {
           Database db = Dao.GetDatabase();
           string sql = @"SELECT *  FROM [dbo].[tax_list] ";
           using (DbConnection cn = db.CreateConnection())
           {
               try
               {
                   DbCommand cmd = db.GetSqlStringCommand(sql);
                   return db.ExecuteDataSet(cmd);
               }
               catch
               {
                   throw new Exception("获取退税明细表失败");
               }
           }
       }

       public DataSet queryTaxList(T_TaxList item)
       {
           Database db = Dao.GetDatabase();
           string sql = @"SELECT *  FROM [dbo].[tax_list] where 1 = 1 ";
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
           if (!string.IsNullOrEmpty(item.StateCode))
           {
               sql += " and state_code = @state_code ";
           }
           if (!string.IsNullOrEmpty(item.startTime.ToString()))
           {
               sql += " and d_date >= @startTime ";
           }
           if (!string.IsNullOrEmpty(item.endTime.ToString()))
           {
               sql += " and d_date <= @endTime ";
           }
           sql += " order by d_date desc ";
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
                   if (!string.IsNullOrEmpty(item.StateCode))
                   {
                       db.AddInParameter(cmd, "@state_code", DbType.String, item.StateCode);
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
                   throw new Exception("查询退税明细表失败");
               }
           }
       }

       public void UpdateState(T_TaxList item,string[] ids)
       {
           Database db = Dao.GetDatabase();
           string state = item.StateCode;
           string str_ids = String.Join(",", ids.ToArray());
           string sql = string.Empty;
           if (state == "P")
           {
               sql = @"UPDATE [dbo].[tax_list]
                           SET [state_code] = @state_code
                              ,[tax_retutn_d_date] = @tax_retutn_d_date
                              ,[tax_return_no] = @tax_return_no
                         WHERE id in ("+str_ids+")";
           }
           else if (state == "D")
           {
               sql = @"UPDATE [dbo].[tax_list]
                           SET [state_code] = @state_code
                              ,[tax_return_date] = @tax_return_date
                         WHERE id in (" + str_ids + ")";
           }
           else
           {
               sql = @"UPDATE [dbo].[tax_list]
                           SET [state_code] = @state_code
                              ,[tax_return_date] = @tax_return_date
                              ,[tax_retutn_d_date] = @tax_retutn_d_date
                              ,[tax_return_no] = @tax_return_no
                         WHERE id in (" + str_ids + ")";
           }
           
           using (DbConnection cn = db.CreateConnection())
           {
               try
               {
                   DbCommand cmd;
                   switch (state)
                   {
                       case "P":
                             cmd = db.GetSqlStringCommand(sql);
                           db.AddInParameter(cmd, "@state_code", DbType.String, item.StateCode);
                           db.AddInParameter(cmd, "@tax_retutn_d_date", DbType.DateTime, item.TaxRetutnDDate);
                           db.AddInParameter(cmd, "@tax_return_no", DbType.String, item.TaxReturnNo);
                           db.ExecuteNonQuery(cmd);
                           break;
                       case "D":
                             cmd = db.GetSqlStringCommand(sql);
                           db.AddInParameter(cmd, "@state_code", DbType.String, item.StateCode);
                           db.AddInParameter(cmd, "@tax_return_date", DbType.DateTime, item.TaxReturnDate);
                           db.ExecuteNonQuery(cmd);
                           break;
                       default:
                            cmd = db.GetSqlStringCommand(sql);
                           db.AddInParameter(cmd, "@state_code", DbType.String, item.StateCode);
                           db.AddInParameter(cmd, "@tax_return_date", DbType.DateTime, item.TaxReturnDate);
                           db.AddInParameter(cmd, "@tax_retutn_d_date", DbType.DateTime, item.TaxRetutnDDate);
                           db.AddInParameter(cmd, "@tax_return_no", DbType.String, item.TaxReturnNo);
                           db.ExecuteNonQuery(cmd);
                           break;
                   }
                  
               }
               catch
               {
                   throw new Exception("更新退税明细表状态位出错");
               }
           }
       }

       
    }
}
