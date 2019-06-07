/******************************************************
* author :  chenjianwu
* email  :  chenjianwu@sh.intra.customs.gov.cn
* function: 
* Auto generated by MappingTools at 2019-06-03 14:28:04
******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
        /// <summary>
        /// 
        /// </summary>
	public partial class T_ContractList
	{
		private Int64 _gNo;
        /// <summary>
        /// 
        /// </summary>
		public Int64 GNo
		{ get { return _gNo; } set { _gNo = value; } }

		private String _contractId;
        /// <summary>
        /// 
        /// </summary>
		public String ContractId
		{ get { return _contractId; } set { _contractId = value; } }

		private String _entryId;
        /// <summary>
        /// 
        /// </summary>
		public String EntryId
		{ get { return _entryId; } set { _entryId = value; } }

		private Int64 _contractNo;
        /// <summary>
        /// 
        /// </summary>
		public Int64 ContractNo
		{ get { return _contractNo; } set { _contractNo = value; } }

		private Decimal? _invoicePrice;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? InvoicePrice
		{ get { return _invoicePrice; } set { _invoicePrice = value; } }

		private String _gName;
        /// <summary>
        /// 
        /// </summary>
		public String GName
		{ get { return _gName; } set { _gName = value; } }

		private String _gUnit;
        /// <summary>
        /// 
        /// </summary>
		public String GUnit
		{ get { return _gUnit; } set { _gUnit = value; } }

		private Decimal? _invoiceTotal;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? InvoiceTotal
		{ get { return _invoiceTotal; } set { _invoiceTotal = value; } }

		private Decimal _gQty;
        /// <summary>
        /// 
        /// </summary>
		public Decimal GQty
		{ get { return _gQty; } set { _gQty = value; } }

#region ���Ƴ�������
        /// <summary>
        /// ���Ƴ�������
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// Ĭ�ϱ���
        /// </summary>
public const string DefaultTableName = "contract_list";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameGNo = "GNo";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameGNo = "g_no";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameContractId = "ContractId";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameContractId = "contract_id";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameEntryId = "EntryId";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameEntryId = "entry_id";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameContractNo = "ContractNo";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameContractNo = "contract_no";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameInvoicePrice = "InvoicePrice";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameInvoicePrice = "invoice_price";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameGName = "GName";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameGName = "g_name";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameGUnit = "GUnit";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameGUnit = "g_unit";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameInvoiceTotal = "InvoiceTotal";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameInvoiceTotal = "invoice_total";
        /// <summary>
        /// �������ƶ��� 
        /// </summary>
public const string PropertyNameGQty = "GQty";
        /// <summary>
        /// �ֶ����ƶ��� 
        /// </summary>
public const string FieldNameGQty = "g_qty";

        }
#endregion
	}
}