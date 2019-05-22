using System;
using System.Collections.Generic;
using System.Text;
using PLM.Biz.Entity;
using PLM.Biz.Interface;
using Microsoft.Practices.McsLibrary.Data.Filtering;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace PLM.Biz.Library
{
    public class LocalPerformYManager : BaseManager<T_LocalPerformY>, ILocalPerformYManager
    {
        const string nationTableName = T_NationPerformY.NameDefine.DefaultTableName;
        #region ILocalPerformYManager ��Ա

        /// <summary>
        /// ����˰�Ų�ѯ�۸�ˮƽ������ݡ�������004
        /// "code_ts","localprice","nationprice","result1"����˰��,"result2"����˰��,"result3"�۸�ˮƽ
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public DataTable F004(Q_LocalPerformY para)
        {
            #region ��������
            Database db = Dao.GetDatabase();
            StringBuilder sql = new StringBuilder();
            DbCommand cmd;
            DataTable local = null, nation = null;
            string localSql = string.Empty, nationSql = string.Empty;
            if (string.IsNullOrEmpty(para.TrafMode)) para.TrafMode = "A";
            #endregion

            #region prepare command
            switch (para.QueryType)
            {
                case QueryType.һ��ó����Ʒ�۸�ˮƽ:
                    #region case 0
                    localSql = string.Format(
                        @"select t.code_ts,c.g_name, round(sum(t.duty_value)/sum(t.qty_1),9) as price,round(sum(real_total),9) as real_total 
                        from {0} t 
                        left join vpo.vp_latest_g_name c on t.code_ts = c.code_ts
                        where t.period_id=:PeriodID and t.trade_mode='0110' {1} {2} group by t.code_ts,c.g_name",
                        this.Dao.DefaultTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.Port.EndsWith("00") ? string.Empty : " and t.port=:PORT ");
                    nationSql = string.Format(
                        @"select t.code_ts, round(sum(t.duty_value)/sum(t.qty_1),9) as price from {0} t 
                            where t.traf_mode = '{2}' and t.period_id=:PeriodID {1} group by t.code_ts",
                        nationTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.TrafMode);
                    #endregion
                    break;
                case QueryType.�ص�˰Դ��Ʒ�۸�ˮƽ:
                    #region case 1
                    localSql = string.Format(
                        @"
                        select n.code_ts,c.g_name, round(sum(t.duty_value)/sum(t.qty_1),9) as price,round(sum(t.real_total),9) as real_total from
                        (select distinct t.code_ts from {0} t where t.rank<=500 and t.period_id=:PeriodID and t.traf_mode='{4}') n 
                        left join {1} t on n.code_ts=t.code_ts                         
                        left join vpo.vp_latest_g_name c on t.code_ts = c.code_ts
                        where t.code_ts is not null and t.period_id=:PeriodID and t.trade_mode='0110' {2} {3} group by n.code_ts,c.g_name
                        ",
                        nationTableName,
                        this.Dao.DefaultTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.Port.EndsWith("00") ? string.Empty : " and t.port=:PORT ",
                        para.TrafMode);
                    nationSql = string.Format(
                        @"select n.code_ts, round(sum(t.duty_value)/sum(t.qty_1),9) as price from 
                        (select distinct t.code_ts from {0} t where t.rank<=500 and t.period_id=:PeriodID and t.traf_mode='{2}') n 
                        left join {0} t on n.code_ts=t.code_ts where t.traf_mode = '{2}' and t.period_id=:PeriodID {1} group by n.code_ts",
                        nationTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.TrafMode);
                    #endregion
                    break;
                case QueryType.�����ϼ������۸�ˮƽ:
                    #region case 2
                    localSql = string.Format(
                        @"select t.code_ts,c.g_name, round(sum(t.duty_value)/sum(t.qty_1),9) as price,round(sum(real_total),9) as real_total 
                        from {0} t 
                        left join vpo.vp_latest_g_name c on t.code_ts = c.code_ts
                        where t.period_id=:PeriodID and t.trade_mode='0644' {1} {2} group by t.code_ts,c.g_name",
                        this.Dao.DefaultTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.Port.EndsWith("00") ? string.Empty : " and t.port=:PORT ");
                    nationSql = string.Format(
                        @"select t.code_ts, round(sum(t.duty_value)/sum(t.qty_1),9) as price 
                        from {0} t where t.traf_mode = '{2}' and t.period_id=:PeriodID and (t.trade_mode='0644' or t.trade_mode='0245') {1} group by t.code_ts",
                        nationTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.TrafMode);
                    #endregion
                    break;
                case QueryType.�����ϼ������۸�ˮƽ:
                    #region case 3
                    localSql = string.Format(
                        @"select t.code_ts,c.g_name, round(sum(t.duty_value)/sum(t.qty_1),9) as price,round(sum(real_total),9) as real_total 
                        from {0} t 
                        left join vpo.vp_latest_g_name c on t.code_ts = c.code_ts
                        where t.period_id=:PeriodID and t.trade_mode='0245' {1} {2} group by t.code_ts,c.g_name",
                        this.Dao.DefaultTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.Port.EndsWith("00") ? string.Empty : " and t.port=:PORT ");
                    nationSql = string.Format(
                        @"select t.code_ts, round(sum(t.duty_value)/sum(t.qty_1),9) as price 
                        from {0} t where t.traf_mode = '{2}' and t.period_id=:PeriodID and (t.trade_mode='0644' or t.trade_mode='0245') {1} group by t.code_ts",
                        nationTableName,
                        string.IsNullOrEmpty(para.CodeTs) ? string.Empty : " and t.code_ts=:CodeTS ",
                        para.TrafMode);
                    #endregion
                    break;
                default: throw new ApplicationException("�����ͳ�Ʒ�ʽ");
            }
            #endregion

            #region fill datatable
            //����ƽ������
            cmd = db.GetSqlStringCommand(localSql);
            db.AddInParameter(cmd, ":PeriodID", DbType.AnsiString, para.PeriodId);
            if (!string.IsNullOrEmpty(para.CodeTs))
                db.AddInParameter(cmd, ":CodeTS", DbType.AnsiString, para.CodeTs);
            if (!para.Port.EndsWith("00"))
                db.AddInParameter(cmd, ":PORT", DbType.AnsiString, para.Port);
            cmd.CommandText = localSql;
            local = db.ExecuteDataSet(cmd).Tables[0];

            //ȫ��ƽ������
            cmd = db.GetSqlStringCommand(nationSql);
            db.AddInParameter(cmd, ":PeriodID", DbType.AnsiString, para.PeriodId);
            if (!string.IsNullOrEmpty(para.CodeTs))
                db.AddInParameter(cmd, ":CodeTS", DbType.AnsiString, para.CodeTs);
            cmd.CommandText = nationSql;
            nation = db.ExecuteDataSet(cmd).Tables[0];
            #endregion

            #region �������
            DataTable result = new DataTable("result");
            //"˰��,��Ʒ����,ȫ��ƽ������,����ƽ������,����˰��,����˰��,�۸�ˮƽ
            result.Columns.Add("code_ts", typeof(string));
            result.Columns.Add("g_name", typeof(string));
            result.Columns.Add("nationprice", typeof(Decimal));
            result.Columns.Add("localprice", typeof(Decimal));
            result.Columns.Add("result1", typeof(Decimal));//����˰��
            result.Columns.Add("result2", typeof(Decimal));//����˰��
            result.Columns.Add("result3", typeof(Decimal));//�۸�ˮƽ
            result.Columns.Add("rank");//�۸�ˮƽ
            DataRow row;
            DataRow[] rows;
            Decimal nationprice, localprice, result1, result2, result3;
            foreach (DataRow r in local.Rows)
            {
                row = result.NewRow();
                row["code_ts"] = r["code_ts"];
                row["g_name"] = r["g_name"];
                localprice = (decimal)r["price"];
                rows = nation.Select(string.Format("code_ts = '{0}'", r["code_ts"]));
                if (rows.Length == 1)
                {
                    nationprice = (Decimal)rows[0]["price"];
                    result3 = localprice / nationprice;
                    result1 = (Decimal)r["real_total"] / result3;
                    result2 = (Decimal)r["real_total"] - result1;

                    row["localprice"] = decimal.Round(localprice, 4);
                    row["nationprice"] = decimal.Round(nationprice, 4);
                    row["result1"] = decimal.Round(result1, 2);
                    row["result2"] = decimal.Round(result2, 2);
                    row["result3"] = decimal.Round(result3, 4);
                }
                else
                {
                    row["localprice"] = decimal.Round(localprice, 4);
                    row["nationprice"] = DBNull.Value;
                    row["result1"] = DBNull.Value;
                    row["result2"] = DBNull.Value;
                    row["result3"] = DBNull.Value;
                }
                result.Rows.Add(row);
            }

            return result;
            #endregion
        }

        /// <summary>
        /// �����۸�ˮƽ��ѯ
        /// <ul>columns<li>"port"</li><li>"localprice"</li><li>"nationprice"</li><li>"real_total"ʵ֤˰��</li><li>"result2"����˰��</li><li>"result3"�۸�ˮƽ</li></ul>
        /// </summary>
        /// <param name="para"></param>
        /// <param name="filterby">�ڰ�����</param>
        /// <returns><ul>columns<li>"port"</li><li>"localprice"</li><li>"nationprice"</li><li>"real_total"ʵ֤˰��</li><li>"result2"����˰��</li><li>"result3"�۸�ˮƽ</li></ul></returns>
        public DataTable F005(Q_LocalPerformY para, FilterBy filterby)
        {
            return Function005.F005(para, filterby);

        }


        #endregion       

    }
}