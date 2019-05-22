using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace ExportDrawbackManagement.Framework.Common
{
    /// <summary>
    /// ExportDrawbackManagement身份
    /// </summary>
    [Serializable]
    public class ExportDrawbackManagementIdentity : IIdentity
    {
        private object _currUserAuth;
        /// <summary>
        /// 用户信息
        /// T_UserAuth对象
        /// </summary>
        public object CurrUserAuth { get { return _currUserAuth; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userAuth">T_UserAuth对象</param>
        public ExportDrawbackManagementIdentity(object userAuth)
        {
            _currUserAuth = userAuth;
        }
       

        #region IIdentity 成员
        /// <summary>
        /// 身份验证类型
        /// </summary>
        public string AuthenticationType
        {
            get { return "ExportDrawbackManagement"; }
        }
        /// <summary>
        /// 是否认证通过
        /// </summary>
        public bool IsAuthenticated
        {
            get { return null != this._currUserAuth; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name
        {
            get
            {
                string _username = (string)_currUserAuth.GetType().GetProperty("displayName").GetValue(this._currUserAuth, null);
                return _username;
            }
        }

        #endregion
    }
}
