using System;
using System.Collections.Generic;
using System.Text;
using ExportDrawbackManagement.Framework.Data;
using ExportDrawbackManagement.Framework.Common;
using System.Data;
using Microsoft.Practices.McsLibrary.Data.Filtering;
using ExportDrawbackManagement.Biz.Entity;
using System.Data.Common;


namespace ExportDrawbackManagement.Biz.Library
{
    public class BaseManager<T>
    {
        public BaseManager()
        {
            //Dao = new DataAccessObject<T>(GlobalDefine.DatabaseName);
        }

        public ExportDrawbackManagementIdentity Identity
        {
            get
            {
                return ExportDrawbackManagementContext.Current.User.ExportDrawbackManagementIdentity;
            }
        }

        [ThreadStatic]
        static DataAccessObject<T> _dao;

        protected DataAccessObject<T> Dao
        {
            get
            {
                if (_dao == null)
                {
                    _dao = new DataAccessObject<T>(GlobalDefine.DatabaseName);
                }
                return _dao;
            }
            private set { _dao = value; }
        }

    }
}
