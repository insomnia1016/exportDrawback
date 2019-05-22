using System;
using System.Collections.Generic;
using System.Text;

namespace PreferentialTrade.Service.Common
{
    /// <summary>
    /// ������÷�ʽ
    /// </summary>
    public enum ServiceInvokeType
    {
        /// <summary>
        /// �յ���
        /// </summary>
        Empty,
        /// <summary>
        /// ���ص���
        /// </summary>
        Local,
        /// <summary>
        /// ͨ��WebService����
        /// </summary>
        WebService,
        /// <summary>
        /// ͨ��Remoting����
        /// </summary>
        Remoting,
    }
}
