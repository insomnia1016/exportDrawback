/******************************************************
* author :  chenjianwu
* email  :  chenjianwu@sh.intra.customs.gov.cn
* function: 
* Auto generated by MappingTools at 2020-02-24 14:58:03
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
		private String _fBillNo;
        /// <summary>
        /// 
        /// </summary>
		public String FBillNo
		{ get { return _fBillNo; } set { _fBillNo = value; } }

		private Decimal? _fUnReceiveAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FUnReceiveAmountFor
		{ get { return _fUnReceiveAmountFor; } set { _fUnReceiveAmountFor = value; } }

		private Int16 _fCheckStatus;
        /// <summary>
        /// 
        /// </summary>
		public Int16 FCheckStatus
		{ get { return _fCheckStatus; } set { _fCheckStatus = value; } }

		private Decimal? _fCheckAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FCheckAmountFor
		{ get { return _fCheckAmountFor; } set { _fCheckAmountFor = value; } }

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

		private Double? _fExchangeRate;
        /// <summary>
        /// 
        /// </summary>
		public Double? FExchangeRate
		{ get { return _fExchangeRate; } set { _fExchangeRate = value; } }

		private Decimal? _fAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FAmountFor
		{ get { return _fAmountFor; } set { _fAmountFor = value; } }

		private Int32? _fCurrencyID;
        /// <summary>
        /// 
        /// </summary>
		public Int32? FCurrencyID
		{ get { return _fCurrencyID; } set { _fCurrencyID = value; } }

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

		private Decimal? _fCheckAmount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FCheckAmount
		{ get { return _fCheckAmount; } set { _fCheckAmount = value; } }

		private Int32 _receiptNo;
        /// <summary>
        /// 
        /// </summary>
		public Int32 ReceiptNo
		{ get { return _receiptNo; } set { _receiptNo = value; } }

		private String _fNote;
        /// <summary>
        /// 
        /// </summary>
		public String FNote
		{ get { return _fNote; } set { _fNote = value; } }

		private Decimal? _fReceiveAmountFor;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? FReceiveAmountFor
		{ get { return _fReceiveAmountFor; } set { _fReceiveAmountFor = value; } }

		private DateTime? _fDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? FDate
		{ get { return _fDate; } set { _fDate = value; } }

#region ���Ƴ�������
        /// <summary>
        /// ���Ƴ�������
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// Ĭ�ϱ���
        /// </summary>
public const string DefaultTableName = "receipt_list";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFBillNo = "FBillNo";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFBillNo = "FBillNo";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFUnReceiveAmountFor = "FUnReceiveAmountFor";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFUnReceiveAmountFor = "FUnReceiveAmountFor";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFCheckStatus = "FCheckStatus";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFCheckStatus = "FCheckStatus";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFCheckAmountFor = "FCheckAmountFor";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFCheckAmountFor = "FCheckAmountFor";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFReceiveAmount = "FReceiveAmount";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFReceiveAmount = "FReceiveAmount";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFAmount = "FAmount";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFAmount = "FAmount";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFExchangeRate = "FExchangeRate";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFExchangeRate = "FExchangeRate";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFAmountFor = "FAmountFor";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFAmountFor = "FAmountFor";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFCurrencyID = "FCurrencyID";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFCurrencyID = "FCurrencyID";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameReceiptId = "ReceiptId";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameReceiptId = "receipt_id";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFUnReceiveAmount = "FUnReceiveAmount";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFUnReceiveAmount = "FUnReceiveAmount";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFCheckAmount = "FCheckAmount";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFCheckAmount = "FCheckAmount";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameReceiptNo = "ReceiptNo";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameReceiptNo = "receipt_no";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFNote = "FNote";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFNote = "FNote";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFReceiveAmountFor = "FReceiveAmountFor";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFReceiveAmountFor = "FReceiveAmountFor";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameFDate = "FDate";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameFDate = "FDate";

        }
#endregion
	}
}