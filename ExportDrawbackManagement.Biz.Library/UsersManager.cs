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
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class UsersManager : BaseManager<T_Users>, IUsersManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pswd">密码</param>
        /// <param name="ds">返回该条记录的结果集</param>
        /// <returns>true表示登录成功，false表示失败</returns>
        public bool login(string name, string pswd, out DataSet ds)
        {
            Database db = Dao.GetDatabase();

            string sql = "select  *  from users where username = @username and password = @password ";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@username", DbType.String, name);
                db.AddInParameter(cmd, "@password", DbType.String, pswd);
                ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows.Count >= 1;

            }

        }
        private bool checkUsername(string username)
        {
            Database db = Dao.GetDatabase();
            string sql1 = "select * from users where username = @username";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql1);
                db.AddInParameter(cmd, "@username", DbType.String, username);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows.Count >= 1;
            }
        }

        private string getPersonIdByUsername(string username)
        {
            Database db = Dao.GetDatabase();
            string sql1 = "select person_id from users where username = @username";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql1);
                db.AddInParameter(cmd, "@username", DbType.String, username);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public string register(T_Users entity)
        {
            if (this.checkUsername(entity.Username))
            {
                throw new Exception("已存在该用户名");
            }

            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[users]
                               ([name]
                               ,[username]
                               ,[password]
                               ,[derpartment]
                               )
                         VALUES
                               (@name
                               ,@username
                               ,@password
                               ,@derpartment)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@name", DbType.String, entity.Name);
                    db.AddInParameter(cmd, "@username", DbType.String, entity.Username);
                    db.AddInParameter(cmd, "@password", DbType.String, entity.Password);
                    db.AddInParameter(cmd, "@derpartment", DbType.String, entity.Derpartment);
                    db.ExecuteNonQuery(cmd);
                    return getPersonIdByUsername(entity.Username);
                    
                }
                catch
                {
                    throw new Exception("注册失败");
                }
            }

        }
        public void add(T_Users entity)
        {
            if (this.checkUsername(entity.Username))
            {
                throw new Exception("已存在该用户名");
            }

            Database db = Dao.GetDatabase();
            string sql = @"INSERT INTO [dbo].[users]
                               ([name]
                               ,[username]
                               ,[password]
                               ,[roles]
                               ,[derpartment])
                         VALUES
                               (@NAME
                               ,@username
                               ,@PASSWORD
                               ,@roles
                               ,@derpartment)";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@NAME", DbType.String, entity.Name);
                    db.AddInParameter(cmd, "@username", DbType.String, entity.Username);
                    db.AddInParameter(cmd, "@PASSWORD", DbType.String, entity.Password);
                    db.AddInParameter(cmd, "@roles", DbType.String, entity.Roles);
                    db.AddInParameter(cmd, "@derpartment", DbType.String, entity.Derpartment);
                    db.ExecuteNonQuery(cmd);

                }
                catch
                {
                    throw new Exception("用户新增失败");
                }
            }

        }
        private bool checkPassword(int person_id, string password)
        {
            Database db = Dao.GetDatabase();
            string sql = "select * from users where  [person_id] = @person_id and [password] = @password";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@person_id", DbType.Int32, person_id);
                db.AddInParameter(cmd, "@password", DbType.String, password);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows.Count > 0;
            }
        }

        public bool changePassword(int person_id, string oldPswd, string newPswd,out string msg)
        {
            if (!checkPassword(person_id, oldPswd))
            {
                msg = "旧密码错误，请重新输入";
                return false;
            }
            Database db = Dao.GetDatabase();
            string sql1 = @"update [dbo].[users]
                            set [password] = @password 
                            where [person_id] = @person_id";
            using (DbConnection cn = db.CreateConnection())
            {
                try
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql1);
                    db.AddInParameter(cmd, "@password", DbType.String, newPswd);
                    db.AddInParameter(cmd, "@person_id", DbType.Int32, person_id);
                    db.ExecuteNonQuery(cmd);
                    msg = "密码修改成功";
                    return true;
                }
                catch
                {
                    throw new Exception("密码修改失败");
                }
            }
        }

        public DataSet getUsers()
        {
            Database db = Dao.GetDatabase();
            string sql = "select * from users";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public void UpdateUserInfo(T_Users user)
        {
            Database db = Dao.GetDatabase();
            string sql = @"UPDATE [dbo].[users]
                           SET [name] = @name
                              ,[username] = @username
                              ,[password] = @password
                              ,[roles] = @roles
                              ,[derpartment] = @derpartment
                         WHERE [person_id] = @person_id";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);
                    db.AddInParameter(cmd, "@name", DbType.String, user.Name);
                    db.AddInParameter(cmd, "@username", DbType.String, user.Username);
                    db.AddInParameter(cmd, "@password", DbType.String, user.Password);
                    db.AddInParameter(cmd, "@roles", DbType.String, user.Roles);
                    db.AddInParameter(cmd, "@derpartment", DbType.String, user.Derpartment);
                    db.AddInParameter(cmd, "@person_id", DbType.Int32, Int32.Parse(user.PersonId));
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch
            {
                throw new Exception("更新用户信息失败,请检查人品");
            }
            

        }

        public void deleteUser(int person_id)
        {
            Database db = Dao.GetDatabase();
            string sql = @"delete from [dbo].[users]
                         WHERE [person_id] = @person_id";
            try
            {
                using (DbConnection cn = db.CreateConnection())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sql);

                    db.AddInParameter(cmd, "@person_id", DbType.Int32, person_id);
                    db.ExecuteNonQuery(cmd);
                }
            }
            catch
            {
                throw new Exception("删除用户信息失败,请检查人品");
            }

        }
    }
}
