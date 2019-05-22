using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Framework.Data
{
    /// <summary>
    /// 实体差异对象
    /// </summary>
    public class DifferenceItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DifferenceItem()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="columnName"></param>
        /// <param name="displayName"></param>
        /// <param name="originalValue"></param>
        /// <param name="currentValue"></param>
        public DifferenceItem(string propertyName, string columnName, string displayName, object originalValue, object currentValue)
        {
            PropertyName = propertyName;
            ColumnName = columnName;
            DisplayName = displayName;
            OriginalValue = originalValue;
            CurrentValue = currentValue;
        }

        string _PropertyName;
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }

        string _ColumnName;
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        string _DisplayName;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        object _OriginalValue;
        /// <summary>
        /// 原始值
        /// </summary>
        public object OriginalValue
        {
            get { return _OriginalValue; }
            set { _OriginalValue = value; }
        }

        object _CurrentValue;
        /// <summary>
        /// 当前值
        /// </summary>
        public object CurrentValue
        {
            get { return _CurrentValue; }
            set { _CurrentValue = value; }
        }
    }
}
