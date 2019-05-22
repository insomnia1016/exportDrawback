using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Framework.Data
{
    /// <summary>
    /// 实体对比结果对象
    /// </summary>
    public class EntityCompareResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityCompareResult()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="objectClassName"></param>
        /// <param name="tableName"></param>
        /// <param name="displayName"></param>
        public EntityCompareResult(string objectClassName, string tableName, string displayName)
        {
            ObjectClassName = objectClassName;
            TableName = tableName;
            DisplayName = displayName;
        }

        string _ObjectClassName;
        /// <summary>
        /// 对象类名
        /// </summary>
        public string ObjectClassName
        {
            get { return _ObjectClassName; }
            set { _ObjectClassName = value; }
        }

        string _TableName;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
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

        /// <summary>
        /// 差异对象集合
        /// </summary>
        public List<DifferenceItem> DifferenceItems = new List<DifferenceItem>();
    }
}
