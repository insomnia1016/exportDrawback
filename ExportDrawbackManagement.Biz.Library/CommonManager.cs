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
    }
}
