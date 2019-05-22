using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.McsLibrary.Data.Filtering;
using System.Reflection;
using System.Data.Common;
using ExportDrawbackManagement.Framework.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ExportDrawbackManagement.Biz.Library
{
    public class Misc
    {
        /// <summary>
        /// 获得恒等条件
        /// </summary>
        /// <returns></returns>
        public static IExpression GetIdenticalExpression()
        {
            IExpression exp = new FormulaExpression("1=1");
            return exp;
        }

        public static string GetEntityFormatString(object entity)
        {
            StringBuilder strb = new StringBuilder();
            Type type = entity.GetType();
            foreach (PropertyInfo pro in type.GetProperties())
            {
                object tmp = pro.GetValue(entity, null);
                strb.AppendFormat("\t{0}:{1}\r\n", pro.Name, tmp);
            }
            return strb.ToString();
        }

        #region Log


        /// <summary>
        /// 记录管理员操作日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static void Log(string title, string msg, System.Diagnostics.TraceEventType severity)
        {
            ExportDrawbackManagementIdentity identity = ExportDrawbackManagementContext.Current.User.ExportDrawbackManagementIdentity;
            LogEntry log = new LogEntry();
            log.Severity = severity;
            log.Title = title;
            log.Message = msg;
            log.ManagedThreadName = "_";
            log.Categories.Add("AdminLog");
            log.ExtendedProperties.Add("用户工号", identity.Name);
            log.ExtendedProperties.Add("用户名", identity.Name);
            log.ExtendedProperties.Add("客户端IP", ExportDrawbackManagementContext.Current.ClientIP);
            Logger.Write(log);
        }
        /// <summary>
        /// 记录管理员操作日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static void Log(string title, string msg)
        {
            Log(title, msg, System.Diagnostics.TraceEventType.Information);
        }
        #endregion

    }
}
