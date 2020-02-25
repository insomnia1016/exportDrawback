using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IReceiptAuditManager
    {
       DataSet getUnCheckReceiptLists(int empid);
    }
}
