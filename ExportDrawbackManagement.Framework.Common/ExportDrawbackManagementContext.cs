using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security.Principal;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExportDrawbackManagement.Framework.Common
{
    /// <summary>
    /// ExportDrawbackManagement上下文
    /// </summary>
    [Serializable]
    public class ExportDrawbackManagementContext
    {

        /// <summary>
        /// 记数器
        /// </summary>
        public static int Visitcounter;

        private ExportDrawbackManagementContext()
        {
        }

        [ThreadStatic]
        static ExportDrawbackManagementContext _Current;

        /// <summary>
        /// 当前上下文
        /// </summary>
        public static ExportDrawbackManagementContext Current
        {
            get
            {
                if (ExportDrawbackManagementContext._Current == null)
                {
                    ExportDrawbackManagementContext._Current = new ExportDrawbackManagementContext();
                }
                return ExportDrawbackManagementContext._Current;
            }
        }

        LocalDataStoreSlot localSlot = Thread.AllocateDataSlot();
        /// <summary>
        /// 获取线程上下文中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetContent(string key)
        {
            Hashtable table = (Hashtable)(Thread.GetData(localSlot));

            if (table == null) return null;

            return table[key];
        }

        /// <summary>
        /// 设置线程上下文中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void SetContent(string key, object obj)
        {
            Hashtable table = (Hashtable)(Thread.GetData(localSlot));

            if (table == null)
            {
                table = new Hashtable();

                Thread.SetData(localSlot, table);
            }

            table[key] = obj;
        }

        /// <summary>
        /// 获取或设置当前ExportDrawbackManagement用户
        /// </summary>
        public ExportDrawbackManagementPrincipal User
        {
            get
            {
                object obj = GetContent("CurrentUser");
                if (obj != null && obj is ExportDrawbackManagementPrincipal)
                    return obj as ExportDrawbackManagementPrincipal;
                else
                    return null;
            }
            set
            {
                SetContent("CurrentUser", value);
            }
        }

        /// <summary>
        /// 判断当前上下文是否授权
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                if (User != null)
                {
                    return User.Identity.IsAuthenticated;
                }
                return false;
            }
        }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string ClientIP
        {
            get
            {
                return GetContent("ClientIP") as string;
            }
            set
            {
                SetContent("ClientIP", value);
            }
        }

        /// <summary>
        /// 系统访问人数
        /// </summary>
        public int VisitNum
        {
            get
            {
                return Visitcounter;
            }
        }
    }
}
