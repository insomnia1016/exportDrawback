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
    class ReceiptAuditManager:BaseManager<T_ReceiptHead>,IReceiptAuditManager
    {
        public DataSet getUnCheckReceiptLists(int empid)
        {
            Database db = Dao.GetDatabase("KingDeeConnection");
            DataSet ds;
            string sql = @"SELECT * FROM ICSale
                            WHERE FCheckStatus<>2 
                            --AND FEmpID=@EmpID
                            ORDER BY FDate DESC";
            using (DbConnection cn = db.CreateConnection())
            {
                DbCommand cmd = db.GetSqlStringCommand(sql);
                //db.AddInParameter(cmd, "@EmpID", DbType.Int32, empid);
                ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
    }
}
