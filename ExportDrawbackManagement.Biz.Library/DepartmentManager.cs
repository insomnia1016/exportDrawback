using ExportDrawbackManagement.Biz.Entity;
using ExportDrawbackManagement.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Library
{
    class DepartmentManager : BaseManager<T_Department>, IDepartmentManager
    {
        public DataSet getDepartments()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "SELECT * FROM [dbo].[department]";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        private bool checkDepartment(T_Department item)
        {
            Database db = Dao.GetDatabase();
            string sql = string.Empty;
            if (item.Id > 0)
            {
                sql = @"SELECT * FROM [dbo].[department]
                            WHERE code = @code AND id <> @id
                            UNION 
                            SELECT * FROM [dbo].[department]
                            WHERE name = @NAME AND id <> @id;";
            }
            else
            {
                sql = @"SELECT * FROM [dbo].[department]
                            WHERE code = @code
                            UNION 
                            SELECT * FROM [dbo].[department]
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
                throw new Exception("检查部门方法checkDepartment是否已有相同代码或名称时失败");
            }
        }

        public void updateDepartment(T_Department item){
            bool isHaved = checkDepartment(item);
            if (isHaved)
            {
                throw new Exception("已有相同代码或名称");
            }
            else
            {
                Database db = Dao.GetDatabase();

                string sql = @"UPDATE  [dbo].[department]
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
                    throw new Exception("更新部门方法updateDepartment失败");
                }
            }
            
            
        }

        public void addDepartment(T_Department item)
        {
            bool isHaved = checkDepartment(item);
            if (isHaved)
            {
                throw new Exception("已有相同代码或名称");
            }
            else
            {
                Database db = Dao.GetDatabase();

                string sql = @"INSERT  INTO [dbo].[department]
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
                    throw new Exception("增加部门方法addDepartment失败");
                }
            }
           
        }
        public void DeleteDepartmentById(int id)
        {
            Database db = Dao.GetDatabase();

            string sql = @"DELETE FROM [dbo].[department] WHERE id = @id;";
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
                throw new Exception("删除部门方法DeleteDepartmentById失败");
            }
        }
    }
}
