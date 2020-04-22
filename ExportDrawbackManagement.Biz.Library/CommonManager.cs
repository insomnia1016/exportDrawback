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
    public class CommonManager : BaseManager<T_Users>, ICommonManager
    {
        public DataSet getUserInfoById(int person_id)
        {

            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  *  from users where person_id = @person_id";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@person_id", DbType.Int32, person_id);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public DataSet getCustomers()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  *  from customers";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

      

        public DataSet getXuFang()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  *  from xufang";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public DataSet getDeliveryMode()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  *  from delivery_mode";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public string getDeliveryModeNameByCode(string code)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  name  from delivery_mode where code=@code";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@code", DbType.String, code);
                ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }

        public DataSet getTaxReturnState()
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  *  from tax_return_state";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public string getTaxReturnStateCodeByName(string name)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  code  from tax_return_state where name=@name";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@name", DbType.String, name);
                ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }

        public string getTaxReturnStateNameByState(string code)
        {
            Database db = Dao.GetDatabase();
            DataSet ds;
            string sql = "select  name  from tax_return_state where code=@code";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@code", DbType.String, code);
                ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }

        public DataSet getCurrency()
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            
            string sql = "SELECT *  FROM [dbo].[t_Currency] WHERE FCurrencyID<>0";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                return db.ExecuteDataSet(cmd);
               
            }
        }
        public string getCurrencyByID(int id)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");

            string sql = "SELECT  FName FROM [dbo].[t_Currency] WHERE FCurrencyID=@CurrencyID";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@CurrencyID", DbType.Int32, id);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds.Tables[0].Rows[0][0].ToString();

            }
        }
        public int getIDByCurrency(string name)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");

            string sql = "SELECT FCurrencyID  FROM [dbo].[t_Currency] WHERE FName=@FName";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@FName", DbType.String, name);
                DataSet ds = db.ExecuteDataSet(cmd);
                return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

            }
        }
        public DataSet getCustomersBySearchKeyName(string name)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT FNumber,FName FROM T_Organization WHERE FName LIKE @name";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@name", DbType.String, "%" + name + "%");
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public DataSet getSuppliersBySearchKeyName(string name)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT FNumber,FName FROM T_Supplier WHERE FName LIKE @name";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@name", DbType.String, "%" + name + "%");
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        public DataSet getDepartment()
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT * FROM T_Department";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public DataSet getEmp()
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT * FROM T_Emp where FItemID > 0 ";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        public int getIDByName(string name)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT FItemID FROM T_Emp WHERE FName=@FName";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@FName", DbType.String,  name );

                ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        public string getEmpNameByID(int id)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT FName FROM T_Emp WHERE FItemID=@FItemID";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@FItemID", DbType.Int32, id);

                ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        public string getDepartmentNameByID(int id)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = "SELECT FName FROM T_Department WHERE FItemID=@FItemID";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@FItemID", DbType.Int32, id);

                ds = db.ExecuteDataSet(cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
