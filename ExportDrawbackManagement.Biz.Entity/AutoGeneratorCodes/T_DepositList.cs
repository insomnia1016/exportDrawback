/******************************************************
* author :  西門飄雪
* email  :  西門飄雪
* function: 
* Auto generated by MappingTools at 2020-03-05 10:55:39
******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
        /// <summary>
        /// 
        /// </summary>
	public partial class T_DepositList
	{
		private Decimal? _amount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? Amount
		{ get { return _amount; } set { _amount = value; } }

		private String _depositId;
        /// <summary>
        /// 
        /// </summary>
		public String DepositId
		{ get { return _depositId; } set { _depositId = value; } }

		private String _note;
        /// <summary>
        /// 
        /// </summary>
		public String Note
		{ get { return _note; } set { _note = value; } }

		private String _fBillNo;
        /// <summary>
        /// 
        /// </summary>
		public String FBillNo
		{ get { return _fBillNo; } set { _fBillNo = value; } }

		private Int32 _gNo;
        /// <summary>
        /// 
        /// </summary>
		public Int32 GNo
		{ get { return _gNo; } set { _gNo = value; } }

		private DateTime? _fdate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime? Fdate
		{ get { return _fdate; } set { _fdate = value; } }

#region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// 默认表名
        /// </summary>
public const string DefaultTableName = "deposit_list";
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
public const string PropertyNameDepositId = "DepositId";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameDepositId = "deposit_id";
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
public const string PropertyNameFBillNo = "FBillNo";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFBillNo = "FBillNo";
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
public const string PropertyNameFdate = "Fdate";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameFdate = "Fdate";

        }
#endregion
	}
}
