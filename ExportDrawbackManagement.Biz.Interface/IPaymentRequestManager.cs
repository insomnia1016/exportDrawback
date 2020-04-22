using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IPaymentRequestManager
    {
      
       void updateHeadCheckStatus(List<T_PaymentReceiptList> lists);
       void updateHeadCheckStatus(DataSet ds);
       DataSet getUnCheckPaymentRequestLists();
       void delete(string key);
    }
}
