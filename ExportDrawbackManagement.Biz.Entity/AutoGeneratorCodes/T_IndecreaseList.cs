/******************************************************
* author :  西門飄雪
* email  :  西門飄雪
* function: 
* Auto generated by MappingTools at 2020-02-27 08:56:28
******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
        /// <summary>
        /// 
        /// </summary>
	public partial class T_IndecreaseList
	{
		private String _name;
        /// <summary>
        /// 
        /// </summary>
		public String Name
		{ get { return _name; } set { _name = value; } }

		private Decimal? _amount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? Amount
		{ get { return _amount; } set { _amount = value; } }

		private String _note;
        /// <summary>
        /// 
        /// </summary>
		public String Note
		{ get { return _note; } set { _note = value; } }

		private DateTime? _applyDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? ApplyDate
		{ get { return _applyDate; } set { _applyDate = value; } }

		private String _type;
        /// <summary>
        /// 
        /// </summary>
		public String Type
		{ get { return _type; } set { _type = value; } }

		private Int32 _gNo;
        /// <summary>
        /// 
        /// </summary>
		public Int32 GNo
		{ get { return _gNo; } set { _gNo = value; } }

		private String _billNo;
        /// <summary>
        /// 
        /// </summary>
		public String BillNo
		{ get { return _billNo; } set { _billNo = value; } }

#region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// 默认表名
        /// </summary>
public const string DefaultTableName = "Indecrease_list";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameName = "Name";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameName = "name";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameAmount = "Amount";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameAmount = "amount";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameNote = "Note";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameNote = "note";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameApplyDate = "ApplyDate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameApplyDate = "apply_date";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameType = "Type";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameType = "type";
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
public const string PropertyNameBillNo = "BillNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameBillNo = "bill_no";

        }
#endregion
	}
}
