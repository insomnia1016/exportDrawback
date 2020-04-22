using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
    class ReceiptManager:BaseManager<T_Receipt>, IReceiptManager
    {
        public DataSet getReceipts()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "SELECT * FROM [dbo].[receipt]";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        private bool checkReceipt(T_Receipt item)
        {
            Database db = Dao.GetDatabase();
            string sql = string.Empty;
            if (item.Id > 0)
            {
                sql = @"SELECT * FROM [dbo].[receipt]
                            WHERE code = @code AND id <> @id
                            UNION 
                            SELECT * FROM [dbo].[receipt]
                            WHERE name = @NAME AND id <> @id;";
            }
            else
            {
                sql = @"SELECT * FROM [dbo].[receipt]
                            WHERE code = @code
                            UNION 
                            SELECT * FROM [dbo].[receipt]
                            WHERE name = @NAME;";
            }
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    if (item.Id > 0)
                    {
                        db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
                    }
                    db.AddInParameter(cmd, "@code", DbType.String, item.Code);
                    db.AddInParameter(cmd, "@NAME", DbType.String, item.Name);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                throw new Exception("检查部门方法checkReceipt是否已有相同代码或名称时失败");
            }
        }

        public void updateReceipt(T_Receipt item)
        {
            bool isHaved = checkReceipt(item);
            if (isHaved)
            {
                throw new Exception("已有相同代码或名称");
            }
            else
            {
                Database db = Dao.GetDatabase();

                string sql = @"UPDATE  [dbo].[receipt]
                            SET     [code] = @code ,
                                    [name] = @NAME
                            WHERE   id = @id;";
                try
                {
                    using (DbConnection cn = db.CreateConnection())
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@id", DbType.Int32, item.Id);
                        db.AddInParameter(cmd, "@code", DbType.String, item.Code);
                        db.AddInParameter(cmd, "@NAME", DbType.String, item.Name);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("更新部门方法updateReceipt失败");
                }
            }


        }

        public void addReceipt(T_Receipt item)
        {
            bool isHaved = checkReceipt(item);
            if (isHaved)
            {
                throw new Exception("已有相同代码或名称");
            }
            else
            {
                Database db = Dao.GetDatabase();

                string sql = @"INSERT  INTO [dbo].[receipt]
                                    ( [code], [name] )
                            VALUES  ( @code, @NAME );";
                try
                {
                    using (DbConnection cn = db.CreateConnection())
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql);
                        db.AddInParameter(cmd, "@code", DbType.String, item.Code);
                        db.AddInParameter(cmd, "@NAME", DbType.String, item.Name);
                        db.ExecuteNonQuery(cmd);
                    }
                }
                catch
                {
                    throw new Exception("增加部门方法addReceipt失败");
                }
            }

        }
        public void DeleteReceiptById(int id)
        {
            Database db = Dao.GetDatabase();

            string sql = @"DELETE FROM [dbo].[receipt] WHERE id = @id;";
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
                throw new Exception("删除部门方法DeleteReceiptById失败");
            }
        }

        public string  getReceiptNameByType(string code)
        {
            Database db = Dao.GetDatabase();

            string sql = @"SELECT name FROM [dbo].[receipt] WHERE code=@code";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@code", DbType.String, code);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch
            {
                throw new Exception("根据收款类型代码翻译名称失败");
            }
        }


    }
}
