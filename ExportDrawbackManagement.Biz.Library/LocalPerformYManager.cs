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
        #region ILocalPerformYManager 成员

        /// <summary>
        /// 按照税号查询价格水平相关数据。任务书004
        /// "code_ts","localprice","nationprice","result1"理论税收,"result2"理论税差,"result3"价格水平
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public DataTable F004(Q_LocalPerformY para)
        {
            #region 变量定义
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
                case QueryType.一般贸易商品价格水平:
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
                case QueryType.重点税源商品价格水平:
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
                case QueryType.进料料件内销价格水平:
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
                case QueryType.来料料件内销价格水平:
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
                default: throw new ApplicationException("错误的统计方式");
            }
            #endregion

            #region fill datatable
            //关区平均单价
            cmd = db.GetSqlStringCommand(localSql);
            db.AddInParameter(cmd, ":PeriodID", DbType.AnsiString, para.PeriodId);
            if (!string.IsNullOrEmpty(para.CodeTs))
                db.AddInParameter(cmd, ":CodeTS", DbType.AnsiString, para.CodeTs);
            if (!para.Port.EndsWith("00"))
                db.AddInParameter(cmd, ":PORT", DbType.AnsiString, para.Port);
            cmd.CommandText = localSql;
            local = db.ExecuteDataSet(cmd).Tables[0];

            //全国平均单价
            cmd = db.GetSqlStringCommand(nationSql);
            db.AddInParameter(cmd, ":PeriodID", DbType.AnsiString, para.PeriodId);
            if (!string.IsNullOrEmpty(para.CodeTs))
                db.AddInParameter(cmd, ":CodeTS", DbType.AnsiString, para.CodeTs);
            cmd.CommandText = nationSql;
            nation = db.ExecuteDataSet(cmd).Tables[0];
            #endregion

            #region 结果返回
            DataTable result = new DataTable("result");
            //"税号,商品名称,全国平均单价,关区平均单价,理论税收,理论税差,价格水平
            result.Columns.Add("code_ts", typeof(string));
            result.Columns.Add("g_name", typeof(string));
            result.Columns.Add("nationprice", typeof(Decimal));
            result.Columns.Add("localprice", typeof(Decimal));
            result.Columns.Add("result1", typeof(Decimal));//理论税收
            result.Columns.Add("result2", typeof(Decimal));//理论税差
            result.Columns.Add("result3", typeof(Decimal));//价格水平
            result.Columns.Add("rank");//价格水平
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
        /// 关区价格水平查询
        /// <ul>columns<li>"port"</li><li>"localprice"</li><li>"nationprice"</li><li>"real_total"实证税款</li><li>"result2"理论税收</li><li>"result3"价格水平</li></ul>
        /// </summary>
        /// <param name="para"></param>
        /// <param name="filterby">口岸类型</param>
        /// <returns><ul>columns<li>"port"</li><li>"localprice"</li><li>"nationprice"</li><li>"real_total"实证税款</li><li>"result2"理论税收</li><li>"result3"价格水平</li></ul></returns>
        public DataTable F005(Q_LocalPerformY para, FilterBy filterby)
        {
            return Function005.F005(para, filterby);

        }


        #endregion       

    }
}