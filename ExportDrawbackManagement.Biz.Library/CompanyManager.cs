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
   public class CompanyManager:BaseManager<T_Customers>,ICompanyManager
    {
       public T_Customers getCompanyInfoById(int id)
       {
           T_Customers customer = new T_Customers();
           Database db = Dao.GetDatabase();
           DataSet ds;
           string sql = "select  *  from customers where id = @id";
           using (DbConnection cn = db.CreateConnection())
           {
               DbCommand cmd = db.GetSqlStringCommand(sql);
               db.AddInParameter(cmd, "@id", DbType.Int32, id);
               ds = db.ExecuteDataSet(cmd);
               customer.Address = ds.Tables[0].Rows[0]["address"].ToString();
               customer.CompanyName = ds.Tables[0].Rows[0]["company_name"].ToString();
               customer.Dailiren = ds.Tables[0].Rows[0]["dailiren"].ToString();
               customer.Id = Int32.Parse(ds.Tables[0].Rows[0]["id"].ToString());
               customer.Jingban = ds.Tables[0].Rows[0]["jingban"].ToString();
               customer.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
               customer.Fadingdaibiaoren = ds.Tables[0].Rows[0]["fadingdaibiaoren"].ToString();
               return customer;
           }
       }
       public T_Customers getXuFangInfoById(int id)
       {
           T_Customers customer = new T_Customers();
           Database db = Dao.GetDatabase();
           DataSet ds;
           string sql = "select  *  from xufang where id = @id";
           using (DbConnection cn = db.CreateConnection())
           {
               DbCommand cmd = db.GetSqlStringCommand(sql);
               db.AddInParameter(cmd, "@id", DbType.Int32, id);
               ds = db.ExecuteDataSet(cmd);
               customer.Address = ds.Tables[0].Rows[0]["address"].ToString();
               customer.CompanyName = ds.Tables[0].Rows[0]["company_name"].ToString();
               customer.Dailiren = ds.Tables[0].Rows[0]["dailiren"].ToString();
               customer.Id = Int32.Parse(ds.Tables[0].Rows[0]["id"].ToString());
               customer.Jingban = ds.Tables[0].Rows[0]["jingban"].ToString();
               customer.Tel = ds.Tables[0].Rows[0]["tel"].ToString();
               customer.Fadingdaibiaoren = ds.Tables[0].Rows[0]["fadingdaibiaoren"].ToString();
               return customer;
           }
       }

       public DataSet getCompanyInfo()
       {
           Database db = Dao.GetDatabase();
          
           string sql = "select  *  from customers";
           using (DbConnection cn = db.CreateConnection())
           {
               DbCommand cmd = db.GetSqlStringCommand(sql);
               return db.ExecuteDataSet(cmd);
              
           }
       }


       public void Save(T_Customers item)
       {
           Database db = Dao.GetDatabase();
           string sql = @"UPDATE [dbo].[customers]
                               SET [company_name] = @company_name
                                  ,[address] = @address
                                  ,[tel] = @tel
                                  ,[jingban] = @jingban
                                  ,[fadingdaibiaoren] = @fadingdaibiaoren
                                  ,[dailiren] = @dailiren
                             WHERE [id]=@id";
           try
           {
               using (DbConnection cn = db.CreateConnection())
               {
                   DbCommand cmd = db.GetSqlStringCommand(sql);
                   db.AddInParameter(cmd, "@company_name", DbType.String, item.CompanyName);
                   db.AddInParameter(cmd, "@address", DbType.String, item.Address);
                   db.AddInParameter(cmd, "@tel", DbType.String, item.Tel);
                   db.AddInParameter(cmd, "@jingban", DbType.String, item.Jingban);
                   db.AddInParameter(cmd, "@fadingdaibiaoren", DbType.String, item.Fadingdaibiaoren);
                   db.AddInParameter(cmd, "@dailiren", DbType.String, item.Dailiren);
                   db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
                   db.ExecuteNonQuery(cmd);
               }
           }
           catch
           {
               throw new Exception("更新客户信息失败,请检查人品");
           }
       }

       public void delete(int id)
       {
           Database db = Dao.GetDatabase();
           string sql = @"DELETE FROM [dbo].[customers] where [id] = @id";
           try
           {
               using (DbConnection cn = db.CreateConnection())
               {
                   DbCommand cmd = db.GetSqlStringCommand(sql);
                   db.AddInParameter(cmd, "@id", DbType.Int32, id);
                   db.ExecuteNonQuery(cmd);
               }
           }
           catch
           {
               throw new Exception("删除客户信息失败,请检查人品");
           }
       }


       public void insert(T_Customers item)
       {
           Database db = Dao.GetDatabase();
           string sql = @"INSERT INTO [dbo].[customers]
                               ([company_name]
                               ,[address]
                               ,[tel]
                               ,[jingban]
                               ,[fadingdaibiaoren]
                               ,[dailiren])
                         VALUES
                               (@company_name
                               ,@address
                               ,@tel
                               ,@jingban
                               ,@fadingdaibiaoren
                               ,@dailiren)";
           try
           {
               using (DbConnection cn = db.CreateConnection())
               {
                   DbCommand cmd = db.GetSqlStringCommand(sql);
                   db.AddInParameter(cmd, "@company_name", DbType.String, item.CompanyName);
                   db.AddInParameter(cmd, "@address", DbType.String, item.Address);
                   db.AddInParameter(cmd, "@tel", DbType.String, item.Tel);
                   db.AddInParameter(cmd, "@jingban", DbType.String, item.Jingban);
                   db.AddInParameter(cmd, "@fadingdaibiaoren", DbType.String, item.Fadingdaibiaoren);
                   db.AddInParameter(cmd, "@dailiren", DbType.String, item.Dailiren);
                   db.ExecuteNonQuery(cmd);
               }
           }
           catch
           {
               throw new Exception("新增客户信息失败,请检查人品");
           }
       }
    }
}
