using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface IUsersManager
    {
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pswd">密码</param>
        /// <param name="ds">返回该记录的结果集</param>
        /// <returns>true表示登录成功，false表示登录失败</returns>
         bool login(string name, string pswd,out DataSet ds);

         string register(T_Users entity);

         bool changePassword(int person_id, string oldPswd, string newPswd, out string msg);

         DataSet getUsers();

         void UpdateUserInfo(T_Users user);
         void deleteUser(int person_id);
         void add(T_Users entity);
    }
}
