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
        #region INationPerformQManager ��Ա

        public DataTable GetData(Q_NationPerformQ para)
        {
            //��ñ����ͳ������
            /*
             * ĳ˰����Ʒȫ��ƽ������=����ó�׷�ʽ��˰�۸�/����ó�׷�ʽ��������=��J+M+P��/(K+N+Q)
             * Ŀǰ���ݿ�����ݽ�����3�����ݣ��������������ݣ�
             * ��Ϊ��ȷ���Ժ���û������ó�׷�ʽ��Ҫͳ�ƣ��ʲ�д������ó�׷�ʽ(JMP,KNQ)��ȫ�����㡣
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
