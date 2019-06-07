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
           string sql = @"insert into tax_list (agent_name,d_date,agent_code,owner_name,entry_id,g_no,g_name,g_qty,g_unit,decl_price,decl_total,code_ts,drawback_rate,invoice_price,invoice_total,tax_return_price,tax_return_total,state_code)
                            select b.agent_name,b.d_date,b.agent_code,b.owner_name,b.entry_id,b.g_no,b.g_name,b.g_qty,b.g_unit, b.decl_price,b.decl_total,b.code_ts,b.drawback_rate,
                            a.invoice_price,a.invoice_total,(a.invoice_price*1.13/b.drawback_rate) as tax_return_price,(a.invoice_total*1.13/b.drawback_rate) as tax_return_total,'N'
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
           string sql = @"SELECT *  FROM [exportDrawback].[dbo].[tax_list] ";
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

       public void UpdateState(T_TaxList item)
       {
           Database db = Dao.GetDatabase();
           string state = item.StateCode;
           string sql = string.Empty;
           if (state == "P")
           {
               sql = @"UPDATE [dbo].[tax_list]
                           SET [state_code] = @state_code
                              ,[tax_retutn_d_date] = @tax_retutn_d_date
                              ,[tax_return_no] = @tax_return_no
                         WHERE id=@id";
           }
           else if (state == "D")
           {
               sql = @"UPDATE [dbo].[tax_list]
                           SET [state_code] = @state_code
                              ,[tax_return_date] = @tax_return_date
                         WHERE id=@id";
           }
           else
           {
               sql = @"UPDATE [dbo].[tax_list]
                           SET [state_code] = @state_code
                              ,[tax_return_date] = @tax_return_date
                              ,[tax_retutn_d_date] = @tax_retutn_d_date
                              ,[tax_return_no] = @tax_return_no
                         WHERE id=@id";
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
                           db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
                           db.ExecuteNonQuery(cmd);
                           break;
                       case "D":
                             cmd = db.GetSqlStringCommand(sql);
                           db.AddInParameter(cmd, "@state_code", DbType.String, item.StateCode);
                           db.AddInParameter(cmd, "@tax_return_date", DbType.DateTime, item.TaxReturnDate);
                           db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
                           db.ExecuteNonQuery(cmd);
                           break;
                       default:
                            cmd = db.GetSqlStringCommand(sql);
                           db.AddInParameter(cmd, "@state_code", DbType.String, item.StateCode);
                           db.AddInParameter(cmd, "@tax_return_date", DbType.DateTime, item.TaxReturnDate);
                           db.AddInParameter(cmd, "@tax_retutn_d_date", DbType.DateTime, item.TaxRetutnDDate);
                           db.AddInParameter(cmd, "@tax_return_no", DbType.String, item.TaxReturnNo);
                           db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
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
