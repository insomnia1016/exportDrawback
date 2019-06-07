﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
   public class UserInfo
    {
        private String _name;
        /// <summary>
        /// 
        /// </summary>
        public String Name
        { get { return _name; } set { _name = value; } }

        private String _rank;
        /// <summary>
        /// 
        /// </summary>
        public String Rank
        { get { return _rank; } set { _rank = value; } }

        private String _derpartment;
        /// <summary>
        /// 
        /// </summary>
        public String Derpartment
        { get { return _derpartment; } set { _derpartment = value; } }

        private String _personId;
        /// <summary>
        /// 
        /// </summary>
        public String PersonId
        { get { return _personId; } set { _personId = value; } }

        private String _roles;
        /// <summary>
        /// 
        /// </summary>
        public String Roles
        { get { return _roles; } set { _roles = value; } }

        private String _password;
        /// <summary>
        /// 
        /// </summary>
        public String Password
        { get { return _password; } set { _password = value; } }

        private String _username;
        /// <summary>
        /// 
        /// </summary>
        public String Username
        { get { return _username; } set { _username = value; } }

        #region 名称常量定义
        /// <summary>
        /// 名称常量定义
        /// </summary>
        public static class NameDefine
        {
            /// <summary>
            /// 默认表名
            /// </summary>
            public const string DefaultTableName = "users";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNameName = "Name";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNameName = "name";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNameRank = "Rank";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNameRank = "rank";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNameDerpartment = "Derpartment";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNameDerpartment = "derpartment";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNamePersonId = "PersonId";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNamePersonId = "person_id";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNameRoles = "Roles";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNameRoles = "roles";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNamePassword = "Password";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNamePassword = "password";
            /// <summary>
            /// 属性名称定义 
            /// </summary>
            public const string PropertyNameUsername = "Username";
            /// <summary>
            /// 字段名称定义 
            /// </summary>
            public const string FieldNameUsername = "username";

        }
        #endregion

    }
}
