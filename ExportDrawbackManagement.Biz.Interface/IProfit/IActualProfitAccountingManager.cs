using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IActualProfitAccountingManager
    {
       DataSet getData(string sale_bill_no);
       void addData(T_ActualProfitAccounting item);
       void updateData(T_ActualProfitAccounting item);
       void audit(string sale_bill_no, bool flag);
       void delete(string sale_bill_no);
    }
}
