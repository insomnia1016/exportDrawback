// Auto generated by MappingTools at 2010-03-29 11:59:17

using System;
using System.Collections.Generic;
using System.Text;

namespace PLM.Biz.Interface
{
        /// <summary>
        /// 表
        /// ADJUSTMENT_PERFORM
        /// 的查询参数类	
        /// 调整绩效记录
        /// 参数如果是数组，那么一定是array[3]，其中array[0],array[2]分别可为空，表示最小最大值，array[1]表示等于查询。
        /// 范围查询参数必须是数值型或者日期型，枚举请手动修改参数类型或代码生成中设定。
        /// 例如，需要查询一个值=2的条件，para可以设定为array[0]=array[2]=null,array[1]=2。
        /// 需要查询大于0小于50，设定为array[0]=0,array[2]=50,array[1]=null
        /// 如果同时指定，那么表示or关系，等于array[1]，或介于array[0]-array[2]之间。
        /// 三个参数可以都不填写，或只填写1个，或2个。
        /// </summary>
	[Serializable]
	public partial class Q_AdjustmentPerform : BaseQueryPara
	{
		private Decimal?[] _tradeTotalOld = new Decimal?[3];
        /// <summary>
        /// 系统提供原成交总价
        /// </summary>
		public Decimal?[] TradeTotalOld
		{ get { return _tradeTotalOld; } set { _tradeTotalOld = value; } }

		private Decimal?[] _newCPriceLevelPlus = new Decimal?[3];
        /// <summary>
        /// 拟改成税号加入被调整商品项后的口岸价格水平
        /// </summary>
		public Decimal?[] NewCPriceLevelPlus
		{ get { return _newCPriceLevelPlus; } set { _newCPriceLevelPlus = value; } }

		private Decimal?[] _oldCDef2expectedMinus = new Decimal?[3];
        /// <summary>
        /// 原税号剔除被调整商品项后的口岸理论税差
        /// </summary>
		public Decimal?[] OldCDef2expectedMinus
		{ get { return _oldCDef2expectedMinus; } set { _oldCDef2expectedMinus = value; } }

		private DateTime?[] _executeDate = new DateTime?[3];
        /// <summary>
        /// 实施日期
        /// </summary>
		public DateTime?[] ExecuteDate
		{ get { return _executeDate; } set { _executeDate = value; } }

		private Decimal?[] _qty1Old = new Decimal?[3];
        /// <summary>
        /// 系统提供原法定数量
        /// </summary>
		public Decimal?[] Qty1Old
		{ get { return _qty1Old; } set { _qty1Old = value; } }

		private Decimal?[] _def2expectedOldR = new Decimal?[3];
        /// <summary>
        /// 原直属海关理论税差
        /// </summary>
		public Decimal?[] Def2expectedOldR
		{ get { return _def2expectedOldR; } set { _def2expectedOldR = value; } }

		private Decimal?[] _qty1OldF = new Decimal?[3];
        /// <summary>
        /// 强制输入原法定数量
        /// </summary>
		public Decimal?[] Qty1OldF
		{ get { return _qty1OldF; } set { _qty1OldF = value; } }

		private Decimal?[] _newCPriceLevelPlusR = new Decimal?[3];
        /// <summary>
        /// 拟改成税号加入被调整商品项后的直属海关价格水平
        /// </summary>
		public Decimal?[] NewCPriceLevelPlusR
		{ get { return _newCPriceLevelPlusR; } set { _newCPriceLevelPlusR = value; } }

		private Decimal?[] _tradeTotalOldF = new Decimal?[3];
        /// <summary>
        /// 强制输入原成交总价
        /// </summary>
		public Decimal?[] TradeTotalOldF
		{ get { return _tradeTotalOldF; } set { _tradeTotalOldF = value; } }

		private Int32?[] _versionN = new Int32?[3];
        /// <summary>
        /// 提交时的全国水平数据版本
        /// </summary>
		public Int32?[] VersionN
		{ get { return _versionN; } set { _versionN = value; } }

		private Decimal?[] _antiRateOld = new Decimal?[3];
        /// <summary>
        /// 系统提供原实征从价反倾销税税率
        /// </summary>
		public Decimal?[] AntiRateOld
		{ get { return _antiRateOld; } set { _antiRateOld = value; } }

		private String _executorId;
        /// <summary>
        /// 实施修改人标识
        /// </summary>
		public String ExecutorId
		{ get { return _executorId; } set { _executorId = value; } }

		private Decimal?[] _taxRateOldF = new Decimal?[3];
        /// <summary>
        /// 强制输入原实征从价增值税率
        /// </summary>
		public Decimal?[] TaxRateOldF
		{ get { return _taxRateOldF; } set { _taxRateOldF = value; } }

		private Decimal?[] _priceLevel2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的口岸价格水平
        /// </summary>
		public Decimal?[] PriceLevel2be
		{ get { return _priceLevel2be; } set { _priceLevel2be = value; } }

		private DateTime?[] _releaseDate = new DateTime?[3];
        /// <summary>
        /// 放行日期时间
        /// </summary>
		public DateTime?[] ReleaseDate
		{ get { return _releaseDate; } set { _releaseDate = value; } }

		private Decimal?[] _taxRate2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的实征从价增值税率
        /// </summary>
		public Decimal?[] TaxRate2be
		{ get { return _taxRate2be; } set { _taxRate2be = value; } }

		private Decimal?[] _newCDef2expectedPlusR = new Decimal?[3];
        /// <summary>
        /// 拟改成税号加入被调整商品项后的直属海关理论税差
        /// </summary>
		public Decimal?[] NewCDef2expectedPlusR
		{ get { return _newCDef2expectedPlusR; } set { _newCDef2expectedPlusR = value; } }

		private String _entryId;
        /// <summary>
        /// 报关单号
        /// </summary>
		public String EntryId
		{ get { return _entryId; } set { _entryId = value; } }

		private Decimal?[] _oldCPriceLevelMinus = new Decimal?[3];
        /// <summary>
        /// 原税号剔除被调整商品项后的口岸价格水平
        /// </summary>
		public Decimal?[] OldCPriceLevelMinus
		{ get { return _oldCPriceLevelMinus; } set { _oldCPriceLevelMinus = value; } }

		private Decimal?[] _def2expected2beR = new Decimal?[3];
        /// <summary>
        /// 拟改成的直属海关理论税差
        /// </summary>
		public Decimal?[] Def2expected2beR
		{ get { return _def2expected2beR; } set { _def2expected2beR = value; } }

		private String _proposalSn;
        /// <summary>
        /// 测算流水号
        /// </summary>
		public String ProposalSn
		{ get { return _proposalSn; } set { _proposalSn = value; } }

		private Decimal?[] _priceLevel2beR = new Decimal?[3];
        /// <summary>
        /// 拟改成的直属海关价格水平
        /// </summary>
		public Decimal?[] PriceLevel2beR
		{ get { return _priceLevel2beR; } set { _priceLevel2beR = value; } }

		private String _gNo;
        /// <summary>
        /// 商品序号
        /// </summary>
		public String GNo
		{ get { return _gNo; } set { _gNo = value; } }

		private Decimal?[] _oldCPriceLevelMinusR = new Decimal?[3];
        /// <summary>
        /// 原税号剔除被调整商品项后的直属海关价格水平
        /// </summary>
		public Decimal?[] OldCPriceLevelMinusR
		{ get { return _oldCPriceLevelMinusR; } set { _oldCPriceLevelMinusR = value; } }

		private Decimal?[] _dutyRateOldF = new Decimal?[3];
        /// <summary>
        /// 强制输入原实征从价关税率
        /// </summary>
		public Decimal?[] DutyRateOldF
		{ get { return _dutyRateOldF; } set { _dutyRateOldF = value; } }

		private Decimal?[] _def2expectedOld = new Decimal?[3];
        /// <summary>
        /// 原口岸理论税差
        /// </summary>
		public Decimal?[] Def2expectedOld
		{ get { return _def2expectedOld; } set { _def2expectedOld = value; } }

		private String _codeTs2be;
        /// <summary>
        /// 拟改成的商品编号
        /// </summary>
		public String CodeTs2be
		{ get { return _codeTs2be; } set { _codeTs2be = value; } }

		private String _reason4fail2modi;
        /// <summary>
        /// 未修改原因
        /// </summary>
		public String Reason4fail2modi
		{ get { return _reason4fail2modi; } set { _reason4fail2modi = value; } }

		private Decimal?[] _regRate2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的实征从价消费税率
        /// </summary>
		public Decimal?[] RegRate2be
		{ get { return _regRate2be; } set { _regRate2be = value; } }

		private Decimal?[] _qty12be = new Decimal?[3];
        /// <summary>
        /// 拟改成的法定数量
        /// </summary>
		public Decimal?[] Qty12be
		{ get { return _qty12be; } set { _qty12be = value; } }

		private String _proposerId;
        /// <summary>
        /// 提交修改人标识
        /// </summary>
		public String ProposerId
		{ get { return _proposerId; } set { _proposerId = value; } }

		private Decimal?[] _oldCDef2expectedMinusR = new Decimal?[3];
        /// <summary>
        /// 原税号剔除被调整商品项后的直属海关理论税差
        /// </summary>
		public Decimal?[] OldCDef2expectedMinusR
		{ get { return _oldCDef2expectedMinusR; } set { _oldCDef2expectedMinusR = value; } }

		private Decimal?[] _newCDef2expectedPlus = new Decimal?[3];
        /// <summary>
        /// 拟改成税号加入被调整商品项后的口岸理论税差
        /// </summary>
		public Decimal?[] NewCDef2expectedPlus
		{ get { return _newCDef2expectedPlus; } set { _newCDef2expectedPlus = value; } }

		private String _codeTsOldF;
        /// <summary>
        /// 强制输入原商品编号
        /// </summary>
		public String CodeTsOldF
		{ get { return _codeTsOldF; } set { _codeTsOldF = value; } }

		private Decimal?[] _def2expected2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的口岸理论税差
        /// </summary>
		public Decimal?[] Def2expected2be
		{ get { return _def2expected2be; } set { _def2expected2be = value; } }

		private Decimal?[] _tradeTotal2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的成交总价
        /// </summary>
		public Decimal?[] TradeTotal2be
		{ get { return _tradeTotal2be; } set { _tradeTotal2be = value; } }

		private Decimal?[] _taxRateOld = new Decimal?[3];
        /// <summary>
        /// 系统提供原实征从价增值税率
        /// </summary>
		public Decimal?[] TaxRateOld
		{ get { return _taxRateOld; } set { _taxRateOld = value; } }

		private String _codeTsOld;
        /// <summary>
        /// 系统提供原商品编号
        /// </summary>
		public String CodeTsOld
		{ get { return _codeTsOld; } set { _codeTsOld = value; } }

		private Decimal?[] _dutyRateOld = new Decimal?[3];
        /// <summary>
        /// 系统提供原实征从价关税率
        /// </summary>
		public Decimal?[] DutyRateOld
		{ get { return _dutyRateOld; } set { _dutyRateOld = value; } }

		private Decimal?[] _dutyRate2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的实征从价关税率
        /// </summary>
		public Decimal?[] DutyRate2be
		{ get { return _dutyRate2be; } set { _dutyRate2be = value; } }

		private DateTime?[] _proposeDate = new DateTime?[3];
        /// <summary>
        /// 提交日期
        /// </summary>
		public DateTime?[] ProposeDate
		{ get { return _proposeDate; } set { _proposeDate = value; } }

		private String _modiFlag;
        /// <summary>
        /// 修改种类标志，分别为修改报关单价p、数量q、税号c
        /// </summary>
		public String ModiFlag
		{ get { return _modiFlag; } set { _modiFlag = value; } }

		private Decimal?[] _priceLevelOld = new Decimal?[3];
        /// <summary>
        /// 原口岸价格水平
        /// </summary>
		public Decimal?[] PriceLevelOld
		{ get { return _priceLevelOld; } set { _priceLevelOld = value; } }

		private Decimal?[] _antiRate2be = new Decimal?[3];
        /// <summary>
        /// 拟改成的实征从价反倾销税税率
        /// </summary>
		public Decimal?[] AntiRate2be
		{ get { return _antiRate2be; } set { _antiRate2be = value; } }

		private Decimal?[] _regRateOldF = new Decimal?[3];
        /// <summary>
        /// 强制输入原实征从价消费税率
        /// </summary>
		public Decimal?[] RegRateOldF
		{ get { return _regRateOldF; } set { _regRateOldF = value; } }

		private Decimal?[] _regRateOld = new Decimal?[3];
        /// <summary>
        /// 系统提供原实征从价消费税率
        /// </summary>
		public Decimal?[] RegRateOld
		{ get { return _regRateOld; } set { _regRateOld = value; } }

		private Decimal?[] _antiRateOldF = new Decimal?[3];
        /// <summary>
        /// 强制输入原实征从价反倾销税税率
        /// </summary>
		public Decimal?[] AntiRateOldF
		{ get { return _antiRateOldF; } set { _antiRateOldF = value; } }

		private String _abnormalityCode;
        /// <summary>
        /// 测算异常代码
        /// </summary>
		public String AbnormalityCode
		{ get { return _abnormalityCode; } set { _abnormalityCode = value; } }

		private Int32?[] _versionL = new Int32?[3];
        /// <summary>
        /// 提交时的本地水平数据版本
        /// </summary>
		public Int32?[] VersionL
		{ get { return _versionL; } set { _versionL = value; } }

		private Decimal?[] _priceLevelOldR = new Decimal?[3];
        /// <summary>
        /// 原直属海关价格水平
        /// </summary>
		public Decimal?[] PriceLevelOldR
		{ get { return _priceLevelOldR; } set { _priceLevelOldR = value; } }

	}
}
