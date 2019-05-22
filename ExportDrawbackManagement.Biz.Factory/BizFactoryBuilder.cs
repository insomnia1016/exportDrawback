using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using ExportDrawbackManagement.Biz.Interface;

namespace ExportDrawbackManagement.Biz.Factory
{
    /// <summary>
    /// 业务对象工厂建造器
    /// </summary>
    public static class BizFactoryBuilder
    {
        static Dictionary<string, IBizFactory> dict = new Dictionary<string, IBizFactory>();
        public static IBizFactory BuilderBizFactory()
        {
            IBizFactory factory = null;
            string typeString = ConfigurationManager.AppSettings["BizFactoryType"];
            if (string.IsNullOrEmpty(typeString))
                throw new　ConfigurationErrorsException("BizFactoryType 未配置");
            lock (dict)
            {
                if (dict.ContainsKey(typeString))
                {
                    factory = dict[typeString];
                }
                else
                {
                    Type type = Type.GetType(typeString, true);
                    object obj = Activator.CreateInstance(type);
                    if (obj is IBizFactory)
                    {
                        factory = obj as IBizFactory;
                        dict.Add(typeString, factory);
                    }
                }
            }
            return factory;
        }
    }
}
