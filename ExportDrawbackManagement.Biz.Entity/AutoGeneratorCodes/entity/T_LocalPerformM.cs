// Auto generated by MappingTools at 2009-11-06 09:48:35

using System;
using System.Collections.Generic;
using System.Text;

namespace PLM.Biz.Entity
{
        /// <summary>
        /// 地方水平月度数据
        /// </summary>
	public partial class T_LocalPerformM
	{
		private String _port;
        /// <summary>
        /// 口岸代码
        /// </summary>
		public String Port
		{ get { return _port; } set { _port = value; } }

		private Decimal? _qty1;
        /// <summary>
        /// 第一（法定）数量
        /// </summary>
		public Decimal? Qty1
		{ get { return _qty1; } set { _qty1 = value; } }

		private Int64? _dutyValue;
        /// <summary>
        /// 关税完税价
        /// </summary>
		public Int64? DutyValue
		{ get { return _dutyValue; } set { _dutyValue = value; } }

		private Decimal? _realTotal;
        /// <summary>
        /// 实征税款
        /// </summary>
		public Decimal? RealTotal
		{ get { return _realTotal; } set { _realTotal = value; } }

		private String _periodId;
        /// <summary>
        /// 周期标识（PIC YYYYMM）
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

#region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// 默认表名
        /// </summary>
public const string DefaultTableName = "LOCAL_PERFORM_M";
        /// <summary>
        /// 属性名称定义 口岸代码
        /// </summary>
public const string PropertyNamePort = "Port";
        /// <summary>
        /// 字段名称定义 口岸代码
        /// </summary>
public const string FieldNamePort = "PORT";
        /// <summary>
        /// 属性名称定义 第一（法定）数量
        /// </summary>
public const string PropertyNameQty1 = "Qty1";
        /// <summary>
        /// 字段名称定义 第一（法定）数量
        /// </summary>
public const string FieldNameQty1 = "QTY_1";
        /// <summary>
        /// 属性名称定义 关税完税价
        /// </summary>
public const string PropertyNameDutyValue = "DutyValue";
        /// <summary>
        /// 字段名称定义 关税完税价
        /// </summary>
public const string FieldNameDutyValue = "DUTY_VALUE";
        /// <summary>
        /// 属性名称定义 实征税款
        /// </summary>
public const string PropertyNameRealTotal = "RealTotal";
        /// <summary>
        /// 字段名称定义 实征税款
        /// </summary>
public const string FieldNameRealTotal = "REAL_TOTAL";
        /// <summary>
        /// 属性名称定义 周期标识（PIC YYYYMM）
        /// </summary>
public const string PropertyNamePeriodId = "PeriodId";
        /// <summary>
        /// 字段名称定义 周期标识（PIC YYYYMM）
        /// </summary>
public const string FieldNamePeriodId = "PERIOD_ID";
        /// <summary>
        /// 属性名称定义 监管方式
        /// </summary>
public const string PropertyNameTradeMode = "TradeMode";
        /// <summary>
        /// 字段名称定义 监管方式
        /// </summary>
public const string FieldNameTradeMode = "TRADE_MODE";
        /// <summary>
        /// 属性名称定义 商品编号
        /// </summary>
public const string PropertyNameCodeTs = "CodeTs";
        /// <summary>
        /// 字段名称定义 商品编号
        /// </summary>
public const string FieldNameCodeTs = "CODE_TS";

        }
#endregion
	}
}
