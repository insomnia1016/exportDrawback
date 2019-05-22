using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace ExportDrawbackManagement.Framework.Common
{    
    /// <summary>
    /// 备注属性
    /// </summary>
    public class RemarkAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remark"></param>
        public RemarkAttribute(string remark)
        {
            _remark = remark;
        }

        private string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private static Hashtable _cache = Hashtable.Synchronized(new Hashtable());
        /// <summary>
        /// 得到枚举值的注释
        /// </summary>
        /// <param name="enumImpl"></param>
        /// <returns></returns>
        public static string GetEnumRemark(Enum enumImpl)
        {
            if (_cache.ContainsKey(enumImpl))
                return (string)_cache[enumImpl];
            else
            {
                string names = string.Empty;
                Type type = enumImpl.GetType();
                string[] fieldNames = enumImpl.ToString().Split(',');
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    FieldInfo fd = type.GetField(fieldNames[i].Trim());
                    object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
                    string name = string.Empty;
                    foreach (RemarkAttribute attr in attrs)
                    {
                        name = attr.Remark;
                    }
                    names += name;
                    if (i < fieldNames.Length - 1)
                    {
                        names += ",";
                    }
                }
                _cache.Add(enumImpl, names);
                return names;
            }
        }
    }

}
