using System;
using System.Collections.Generic;
using System.Text;
using ExportDrawbackManagement.Biz.Interface;

namespace ExportDrawbackManagement.Biz.Factory
{
    /// <summary>
    /// 本地业务对象工厂
    /// </summary>
    public class LocalBizFactory:IBizFactory
    {
        Dictionary<Type, Type> dict = new Dictionary<Type, Type>();
        const string NameSpacePrefix = "ExportDrawbackManagement.Biz.Library";        
        #region IBizFactory 成员
        /// <summary>
        /// 创建业务对象实例
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public object CreateInstance(Type interfaceType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentException("interfaceType不能为空。");
            }
            Type type = null;
            if (dict.ContainsKey(interfaceType))
            {
                type = dict[interfaceType];
            }
            else
            {
                type = interfaceType;
                if (interfaceType.IsInterface)
                {
                    string typeString = null;
                    string name = interfaceType.Name;
                    if (name[0] == 'I')
                        name = name.Remove(0, 1);
                    typeString = string.Format("{0}.{1},{0}", NameSpacePrefix, name);
                    type = Type.GetType(typeString);

                }

                dict.Add(interfaceType, type);
            }
                if (type == null)
                {
                    throw new ApplicationException(string.Format("接口{0}没有合法的实现类。", interfaceType.FullName));
                }            
            object obj = Activator.CreateInstance(type);
            return obj;
        }

        #endregion
    }
}
