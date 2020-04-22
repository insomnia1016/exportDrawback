/******************************************************
* author :  西門飄雪
* email  :  西門飄雪
* function: 
* Auto generated by MappingTools at 2020-03-10 13:57:35
******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
        /// <summary>
        /// 
        /// </summary>
	public partial class T_Account
	{
		private Decimal? _amount;
        /// <summary>
        /// 
        /// </summary>
		public Decimal? Amount
		{ get { return _amount; } set { _amount = value; } }

		private Int32? _currencyID;
        /// <summary>
        /// 
        /// </summary>
		public Int32? CurrencyID
		{ get { return _currencyID; } set { _currencyID = value; } }

		private String _accountName;
        /// <summary>
        /// 
        /// </summary>
		public String AccountName
		{ get { return _accountName; } set { _accountName = value; } }

		private String _accountId;
        /// <summary>
        /// 
        /// </summary>
		public String AccountId
		{ get { return _accountId; } set { _accountId = value; } }

		private String _openingBank;
        /// <summary>
        /// 
        /// </summary>
		public String OpeningBank
		{ get { return _openingBank; } set { _openingBank = value; } }

#region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
public static class NameDefine
        {
        /// <summary>
        /// 默认表名
        /// </summary>
public const string DefaultTableName = "account";
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
public const string PropertyNameCurrencyID = "CurrencyID";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameCurrencyID = "currencyID";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameAccountName = "AccountName";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameAccountName = "account_name";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameAccountId = "AccountId";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameAccountId = "account_id";
        /// <summary>
        /// 属性名称定义 
        /// </summary>
public const string PropertyNameOpeningBank = "OpeningBank";
        /// <summary>
        /// 字段名称定义 
        /// </summary>
public const string FieldNameOpeningBank = "opening_bank";

        }
#endregion
	}
}
