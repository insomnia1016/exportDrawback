using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
   public class UserInfo
    {
       /// <summary>
       /// 用户名称
       /// </summary>
        public string displayName { get; set; }

       /// <summary>
       /// 用户7位工号
       /// </summary>
        public string person_id { get; set; }

       

       /// <summary>
       /// 用户权限 
       /// </summary>
        public List<string> UserRight { get; set; }


    }
}
