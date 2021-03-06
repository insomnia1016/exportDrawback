/******************************************************
* author :  西門飄雪
* email  :  西門飄雪
* function: 
* Auto generated by MappingTools at 2020-03-21 17:16:23
******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
        /// <summary>
        /// 
        /// </summary>
	public partial class T_TaxList
	{
		private DateTime? _taxRetutnDDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? TaxRetutnDDate
		{ get { return _taxRetutnDDate; } set { _taxRetutnDDate = value; } }

		private Int64? _gNo;
        /// <summary>
        /// 
        /// </summary>
		public Int64? GNo
		{ get { return _gNo; } set { _gNo = value; } }

		private Int32 _id;
        /// <summary>
        /// 
        /// </summary>
		public Int32 Id
		{ get { return _id; } set { _id = value; } }

		private String _tradeCurr;
        /// <summary>
        /// 
        /// </summary>
		public String TradeCurr
		{ get { return _tradeCurr; } set { _tradeCurr = value; } }

		private String _stateCode;
        /// <summary>
        /// 
        /// </summary>
		public String StateCode
		{ get { return _stateCode; } set { _stateCode = value; } }

		private String _agentCode;
        /// <summary>
        /// 
        /// </summary>
		public String AgentCode
		{ get { return _agentCode; } set { _agentCode = value; } }

		private DateTime? _dDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? DDate
		{ get { return _dDate; } set { _dDate = value; } }

		private Decimal? _drawbackRate;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? DrawbackRate
		{ get { return _drawbackRate; } set { _drawbackRate = value; } }

		private String _codeTs;
        /// <summary>
        /// 
        /// </summary>
		public String CodeTs
		{ get { return _codeTs; } set { _codeTs = value; } }

		private Decimal? _taxReturnTotal;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? TaxReturnTotal
		{ get { return _taxReturnTotal; } set { _taxReturnTotal = value; } }

		private Decimal? _invoiceTotal;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? InvoiceTotal
		{ get { return _invoiceTotal; } set { _invoiceTotal = value; } }

		private String _ownerName;
        /// <summary>
        /// 
        /// </summary>
		public String OwnerName
		{ get { return _ownerName; } set { _ownerName = value; } }

		private String _entryId;
        /// <summary>
        /// 
        /// </summary>
		public String EntryId
		{ get { return _entryId; } set { _entryId = value; } }

		private Decimal? _invoicePrice;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? InvoicePrice
		{ get { return _invoicePrice; } set { _invoicePrice = value; } }

		private String _saleBillNo;
        /// <summary>
        /// 
        /// </summary>
		public String SaleBillNo
		{ get { return _saleBillNo; } set { _saleBillNo = value; } }

		private String _taxReturnNo;
        /// <summary>
        /// 
        /// </summary>
		public String TaxReturnNo
		{ get { return _taxReturnNo; } set { _taxReturnNo = value; } }

		private Decimal? _declTotal;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? DeclTotal
		{ get { return _declTotal; } set { _declTotal = value; } }

		private Decimal? _taxReturnPrice;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? TaxReturnPrice
		{ get { return _taxReturnPrice; } set { _taxReturnPrice = value; } }

		private String _gUnit;
        /// <summary>
        /// 
        /// </summary>
		public String GUnit
		{ get { return _gUnit; } set { _gUnit = value; } }

		private String _gName;
        /// <summary>
        /// 
        /// </summary>
		public String GName
		{ get { return _gName; } set { _gName = value; } }

		private String _agentName;
        /// <summary>
        /// 
        /// </summary>
		public String AgentName
		{ get { return _agentName; } set { _agentName = value; } }

		private DateTime? _taxReturnDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? TaxReturnDate
		{ get { return _taxReturnDate; } set { _taxReturnDate = value; } }

		private Decimal? _declPrice;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? DeclPrice
		{ get { return _declPrice; } set { _declPrice = value; } }

		private Decimal? _gQty;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? GQty
		{ get { return _gQty; } set { _gQty = value; } }

#region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// 默认表名
        /// </summary>
public const string DefaultTableName = "tax_list";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameTaxRetutnDDate = "TaxRetutnDDate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameTaxRetutnDDate = "tax_retutn_d_date";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameGNo = "GNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameGNo = "g_no";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameId = "Id";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameId = "id";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameTradeCurr = "TradeCurr";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameTradeCurr = "trade_curr";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameStateCode = "StateCode";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameStateCode = "state_code";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameAgentCode = "AgentCode";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameAgentCode = "agent_code";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameDDate = "DDate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameDDate = "d_date";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameDrawbackRate = "DrawbackRate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameDrawbackRate = "drawback_rate";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameCodeTs = "CodeTs";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameCodeTs = "code_ts";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameTaxReturnTotal = "TaxReturnTotal";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameTaxReturnTotal = "tax_return_total";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameInvoiceTotal = "InvoiceTotal";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameInvoiceTotal = "invoice_total";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameOwnerName = "OwnerName";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameOwnerName = "owner_name";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameEntryId = "EntryId";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameEntryId = "entry_id";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameInvoicePrice = "InvoicePrice";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameInvoicePrice = "invoice_price";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameSaleBillNo = "SaleBillNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameSaleBillNo = "sale_bill_no";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameTaxReturnNo = "TaxReturnNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameTaxReturnNo = "tax_return_no";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameDeclTotal = "DeclTotal";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameDeclTotal = "decl_total";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameTaxReturnPrice = "TaxReturnPrice";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameTaxReturnPrice = "tax_return_price";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameGUnit = "GUnit";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameGUnit = "g_unit";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameGName = "GName";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameGName = "g_name";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameAgentName = "AgentName";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameAgentName = "agent_name";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameTaxReturnDate = "TaxReturnDate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameTaxReturnDate = "tax_return_date";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameDeclPrice = "DeclPrice";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameDeclPrice = "decl_price";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameGQty = "GQty";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameGQty = "g_qty";

        }
#endregion
	}
}
