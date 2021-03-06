/******************************************************
* author :  西門飄雪
* email  :  西門飄雪
* function: 
* Auto generated by MappingTools at 2020-03-06 09:42:52
******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
        /// <summary>
        /// 
        /// </summary>
	public partial class T_ReceiptList
	{
		private Decimal? _fReceiveAmount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FReceiveAmount
		{ get { return _fReceiveAmount; } set { _fReceiveAmount = value; } }

		private Decimal? _fAmount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FAmount
		{ get { return _fAmount; } set { _fAmount = value; } }

		private String _fNote;
        /// <summary>
        /// 
        /// </summary>
		public String FNote
		{ get { return _fNote; } set { _fNote = value; } }

		private Int16 _fCheckStatus;
        /// <summary>
        /// 
        /// </summary>
		public Int16 FCheckStatus
		{ get { return _fCheckStatus; } set { _fCheckStatus = value; } }

		private String _receiptId;
        /// <summary>
        /// 
        /// </summary>
		public String ReceiptId
		{ get { return _receiptId; } set { _receiptId = value; } }

		private Decimal? _fUnReceiveAmount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FUnReceiveAmount
		{ get { return _fUnReceiveAmount; } set { _fUnReceiveAmount = value; } }

		private Decimal? _fAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FAmountFor
		{ get { return _fAmountFor; } set { _fAmountFor = value; } }

		private Decimal? _fReceiveAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FReceiveAmountFor
		{ get { return _fReceiveAmountFor; } set { _fReceiveAmountFor = value; } }

		private Decimal? _fCheckAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FCheckAmountFor
		{ get { return _fCheckAmountFor; } set { _fCheckAmountFor = value; } }

		private String _fBillNo;
        /// <summary>
        /// 
        /// </summary>
		public String FBillNo
		{ get { return _fBillNo; } set { _fBillNo = value; } }

		private String _depositId;
        /// <summary>
        /// 
        /// </summary>
		public String DepositId
		{ get { return _depositId; } set { _depositId = value; } }

		private Decimal? _fUnReceiveAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FUnReceiveAmountFor
		{ get { return _fUnReceiveAmountFor; } set { _fUnReceiveAmountFor = value; } }

		private Int32 _receiptNo;
        /// <summary>
        /// 
        /// </summary>
		public Int32 ReceiptNo
		{ get { return _receiptNo; } set { _receiptNo = value; } }

		private DateTime? _fDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? FDate
		{ get { return _fDate; } set { _fDate = value; } }

		private Int32? _fCurrencyID;
        /// <summary>
        /// 
        /// </summary>
		public Int32? FCurrencyID
		{ get { return _fCurrencyID; } set { _fCurrencyID = value; } }

		private String _indecreaseNo;
        /// <summary>
        /// 
        /// </summary>
		public String IndecreaseNo
		{ get { return _indecreaseNo; } set { _indecreaseNo = value; } }

		private Decimal? _fCheckAmount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FCheckAmount
		{ get { return _fCheckAmount; } set { _fCheckAmount = value; } }

		private Double? _fExchangeRate;
        /// <summary>
        /// 
        /// </summary>
		public Double? FExchangeRate
		{ get { return _fExchangeRate; } set { _fExchangeRate = value; } }

#region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// 默认表名
        /// </summary>
public const string DefaultTableName = "receipt_list";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFReceiveAmount = "FReceiveAmount";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFReceiveAmount = "FReceiveAmount";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFAmount = "FAmount";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFAmount = "FAmount";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFNote = "FNote";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFNote = "FNote";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFCheckStatus = "FCheckStatus";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFCheckStatus = "FCheckStatus";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameReceiptId = "ReceiptId";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameReceiptId = "receipt_id";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFUnReceiveAmount = "FUnReceiveAmount";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFUnReceiveAmount = "FUnReceiveAmount";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFAmountFor = "FAmountFor";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFAmountFor = "FAmountFor";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFReceiveAmountFor = "FReceiveAmountFor";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFReceiveAmountFor = "FReceiveAmountFor";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFCheckAmountFor = "FCheckAmountFor";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFCheckAmountFor = "FCheckAmountFor";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFBillNo = "FBillNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFBillNo = "FBillNo";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameDepositId = "DepositId";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameDepositId = "Deposit_id";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFUnReceiveAmountFor = "FUnReceiveAmountFor";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFUnReceiveAmountFor = "FUnReceiveAmountFor";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameReceiptNo = "ReceiptNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameReceiptNo = "receipt_no";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFDate = "FDate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFDate = "FDate";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFCurrencyID = "FCurrencyID";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFCurrencyID = "FCurrencyID";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameIndecreaseNo = "IndecreaseNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameIndecreaseNo = "InDecrease_no";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFCheckAmount = "FCheckAmount";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFCheckAmount = "FCheckAmount";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameFExchangeRate = "FExchangeRate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFExchangeRate = "FExchangeRate";

        }
#endregion
	}
}
