using System;
using System.Collections.Generic;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;
using System.Reflection;

namespace ExportDrawbackManagement.Biz.Interface
{
    /// <summary>
    /// 基本查询参数
    /// </summary>
    [Serializable]
    public partial class BaseQueryPara
    {
        TableOption option = TableOption.OfficialTable;
        /// <summary>
        /// 查询表
        /// </summary>
        public TableOption Option
        {
            get { return option; }
            set { option = value; }
        }

        /// <summary>
        /// 获取一个public属性的值
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public object this[string PropertyName]
        {
            get
            {
                PropertyInfo info = this.GetType().GetProperty(PropertyName);
                if (info == null) return null;
                return info.GetValue(this, null);
            }
            set
            {
                PropertyInfo info = this.GetType().GetProperty(PropertyName);
                if (info == null) return;
                info.SetValue(this, value, null);
            }
        }
    }
}
