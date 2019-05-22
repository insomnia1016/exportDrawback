using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace ExportDrawbackManagement.Framework.Common
{
    /// <summary>
    /// ExportDrawbackManagementPrincipal
    /// </summary>
    [Serializable]
    public class ExportDrawbackManagementPrincipal : IPrincipal
    {
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="roles"></param>
        public ExportDrawbackManagementPrincipal(ExportDrawbackManagementIdentity identity, string[] roles)
        {
            _Identity = identity;
            _Roles = roles;
        }

        ExportDrawbackManagementIdentity _Identity;
        string[] _Roles;
        /// <summary>
        /// ExportDrawbackManagement身份
        /// </summary>
        public ExportDrawbackManagementIdentity ExportDrawbackManagementIdentity
        {
            get { return _Identity; }
        }

        /// <summary>
        /// 是否包含所有角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool IsInRoles(params string[] roles)
        {
            foreach (string role in roles)
            {
                if (!IsInRole(role))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 是否包含任一角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool IsInAnyRoles(params string[] roles)
        {
            foreach (string role in roles)
            {
                if (IsInRole(role))
                    return true;
            }
            return false;
        }

        #region IPrincipal 成员
        /// <summary>
        /// 身份
        /// </summary>
        public IIdentity Identity
        {
            get { return _Identity; }
        }

        /// <summary>
        /// 是否包含角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (_Roles != null && !string.IsNullOrEmpty(role))
            {
                foreach (string r in _Roles)
                {
                    if (r.Equals(role, StringComparison.OrdinalIgnoreCase) || r.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        #endregion
    }
}
