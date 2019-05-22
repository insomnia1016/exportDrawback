using System;
using System.Collections.Generic;
using System.Text;
using PLM.Framework.Common;

namespace PLM.Biz.Interface
{
    public partial class Q_LocalPerformM
    {
        private QueryType _queryType;
        /// <summary>
        /// 价格水平查询种类
        /// </summary>
        public QueryType QueryType
        { get { return _queryType; } set { _queryType = value; } }

        private String _trafMode;
        /// <summary>
        /// 运输方式：A-不分运输方式；5-空运。
        /// </summary>
        public String TrafMode
        { get { return _trafMode; } set { _trafMode = value; } }
    }

    public partial class Q_LocalPerformQ
    {
        private QueryType _queryType;
        /// <summary>
        /// 价格水平查询种类
        /// </summary>
        public QueryType QueryType
        { get { return _queryType; } set { _queryType = value; } }

        private String _trafMode;
        /// <summary>
        /// 运输方式：A-不分运输方式；5-空运。
        /// </summary>
        public String TrafMode
        { get { return _trafMode; } set { _trafMode = value; } }
    }

    public partial class Q_LocalPerformY
    {
        private QueryType _queryType;
        /// <summary>
        /// 价格水平查询种类
        /// </summary>
        public QueryType QueryType
        { get { return _queryType; } set { _queryType = value; } }

        private String _trafMode;
        /// <summary>
        /// 运输方式：A-不分运输方式；5-空运。
        /// </summary>
        public String TrafMode
        { get { return _trafMode; } set { _trafMode = value; } }
    }

    /// <summary>
    /// 价格水平查询种类
    /// </summary>
    public enum QueryType
    {
        [Remark("一般贸易商品价格水平")]
        一般贸易商品价格水平 = 0,
        [Remark("重点税源商品价格水平")]
        重点税源商品价格水平 = 1,
        [Remark("进料料件内销价格水平")]
        进料料件内销价格水平 = 2,
        [Remark("来料料件内销价格水平")]
        来料料件内销价格水平 = 3,
        [Remark("所有料件内销价格水平")]
        所有料件内销价格水平 = 4,
    }

    public enum Modi_Flag
    {
        [Remark("成交总价")]
        P,
        [Remark("数量")]
        Q,
        [Remark("税号")]
        C,
    }
}
