// Auto generated by MappingTools at 2009-11-06 09:48:37

using System;
using System.Collections.Generic;
using System.Text;

namespace PLM.Biz.Interface
{
        /// <summary>
        /// 表
        /// LOCAL_PERFORM_Y
        /// 的查询参数类	
        /// 地方水平年度数据
        /// 参数如果是数组，那么一定是array[3]，其中array[0],array[2]分别可为空，表示最小最大值，array[1]表示等于查询。
        /// 范围查询参数必须是数值型或者日期型，枚举请手动修改参数类型或代码生成中设定。
        /// 例如，需要查询一个值=2的条件，para可以设定为array[0]=array[2]=null,array[1]=2。
        /// 需要查询大于0小于50，设定为array[0]=0,array[2]=50,array[1]=null
        /// 如果同时指定，那么表示or关系，等于array[1]，或介于array[0]-array[2]之间。
        /// 三个参数可以都不填写，或只填写1个，或2个。
        /// </summary>
	[Serializable]
	public partial class Q_LocalPerformY : BaseQueryPara
	{
		private String _port;
        /// <summary>
        /// 口岸代码
        /// </summary>
		public String Port
		{ get { return _port; } set { _port = value; } }

		private Decimal?[] _qty1 = new Decimal?[3];
        /// <summary>
        /// 第一（法定）数量
        /// </summary>
		public Decimal?[] Qty1
		{ get { return _qty1; } set { _qty1 = value; } }

		private Int64?[] _dutyValue = new Int64?[3];
        /// <summary>
        /// 关税完税价
        /// </summary>
		public Int64?[] DutyValue
		{ get { return _dutyValue; } set { _dutyValue = value; } }

		private Decimal?[] _realTotal = new Decimal?[3];
        /// <summary>
        /// 实征税款
        /// </summary>
		public Decimal?[] RealTotal
		{ get { return _realTotal; } set { _realTotal = value; } }

		private String _periodId;
        /// <summary>
        /// 周期标识（PIC YYYY）
        /// </summary>
		public String PeriodId
		{ get { return _periodId; } set { _periodId = value; } }

		private String _tradeMode;
        /// <summary>
        /// 监管方式
        /// </summary>
		public String TradeMode
		{ get { return _tradeMode; } set { _tradeMode = value; } }

		private String _codeTs;
        /// <summary>
        /// 商品编号
        /// </summary>
		public String CodeTs
		{ get { return _codeTs; } set { _codeTs = value; } }

	}
}
