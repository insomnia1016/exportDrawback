using System;
using System.Collections.Generic;
using System.Text;

namespace PreferentialTrade.Service.Common
{
    /// <summary>
    /// 服务调用方式
    /// </summary>
    public enum ServiceInvokeType
    {
        /// <summary>
        /// 空调用
        /// </summary>
        Empty,
        /// <summary>
        /// 本地调用
        /// </summary>
        Local,
        /// <summary>
        /// 通过WebService调用
        /// </summary>
        WebService,
        /// <summary>
        /// 通过Remoting调用
        /// </summary>
        Remoting,
    }
}
