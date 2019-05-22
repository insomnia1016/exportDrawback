using System;
using System.Collections.Generic;
using System.Text;
using PLM.Biz.Entity;
using PLM.Biz.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace PLM.Biz.Library
{
    public class NationPerformQManager : BaseManager<T_NationPerformQ>, INationPerformQManager
    {
        #region INationPerformQManager 成员

        public DataTable GetData(Q_NationPerformQ para)
        {
            //获得本年度统计数据
            /*
             * 某税号商品全国平均单价=Σ各贸易方式完税价格/Σ各贸易方式法定数量=（J+M+P）/(K+N+Q)
             * 目前数据库的数据仅有这3类数据，不存在其他数据，
             * 因为不确定以后还有没有其他贸易方式需要统计，故不写死具体贸易方式(JMP,KNQ)，全部计算。
             * */
            Database db = Dao.GetDatabase();
            StringBuilder sql = new StringBuilder();
            sql.Append(
                string.Format(@"select round(sum(t.duty_value)/sum(t.qty_1),4) from {0} t where t.traf_mode='A' and t.period_id=:PeriodID and t.code_ts=:CodeTS",
                this.Dao.DefaultTableName));
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, ":PeriodID", DbType.AnsiString, para.PeriodId);
            db.AddInParameter(cmd, ":CodeTS", DbType.AnsiString, para.CodeTs);

            cmd.CommandText = sql.ToString();
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        #endregion
    }
}
