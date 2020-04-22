using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace ExportDrawbackManagement.Biz.Library
{
    class PaymentTypeManager : BaseManager<T_PaymentType>, IPaymentTypeManager
    {
        public DataSet getTypes()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "SELECT * FROM [dbo].[payment_type]";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        private bool checkType(T_PaymentType item)
        {
            Database db = Dao.GetDatabase();
            string sql = string.Empty;
            if (item.Id > 0)
            {
                sql = @"SELECT * FROM [dbo].[payment_type]
                            WHERE code = @code AND id <> @id
                            UNION 
                            SELECT * FROM [dbo].[payment_type]
                            WHERE name = @NAME AND id <> @id;";
            }
            else
            {
                sql = @"SELECT * FROM [dbo].[payment_type]
                            WHERE code = @code
                            UNION 
                            SELECT * FROM [dbo].[payment_type]
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
                throw new Exception("检查部门方法checkType是否已有相同代码或名称时失败");
            }
        }

        public void updateType(T_PaymentType item)
        {
            bool isHaved = checkType(item);
            if (isHaved)
            {
                throw new Exception("已有相同代码或名称");
            }
            else
            {
                Database db = Dao.GetDatabase();

                string sql = @"UPDATE  [dbo].[payment_type]
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
                    throw new Exception("更新部门方法updateType失败");
                }
            }


        }

        public void addType(T_PaymentType item)
        {
            bool isHaved = checkType(item);
            if (isHaved)
            {
                throw new Exception("已有相同代码或名称");
            }
            else
            {
                Database db = Dao.GetDatabase();

                string sql = @"INSERT  INTO [dbo].[payment_type]
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
                    throw new Exception("增加部门方法addType失败");
                }
            }

        }
        public void DeleteTypeById(int id)
        {
            Database db = Dao.GetDatabase();

            string sql = @"DELETE FROM [dbo].[payment_type] WHERE id = @id;";
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
                throw new Exception("删除部门方法DeleteTypeById失败");
            }
        }

        public string getTypeNameByType(string code)
        {
            Database db = Dao.GetDatabase();

            string sql = @"SELECT name FROM [dbo].[payment_type] WHERE code=@code";
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
