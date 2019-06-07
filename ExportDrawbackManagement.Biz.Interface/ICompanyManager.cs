using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface ICompanyManager
    {
        T_Customers getCompanyInfoById(int id);
        DataSet getCompanyInfo();
        void Save(T_Customers item);
        void insert(T_Customers item);
        void delete(int id);
    }
}
