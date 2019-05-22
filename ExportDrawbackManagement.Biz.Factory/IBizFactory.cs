using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    /// <summary>
    /// 业务对象工厂
    /// </summary>
    public interface IBizFactory
    {
        /// <summary>
        /// 创建业对象实例
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        object CreateInstance(Type interfaceType);
    }
}
