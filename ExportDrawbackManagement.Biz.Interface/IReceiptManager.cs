using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface IReceiptManager
    {
        DataSet getReceipts();
        void addReceipt(T_Receipt item);
        void updateReceipt(T_Receipt item);
        void DeleteReceiptById(int id);
    }
}
