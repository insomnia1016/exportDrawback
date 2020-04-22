using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IPaymentTypeManager
    {
       DataSet getTypes();
       void updateType(T_PaymentType item);
       void addType(T_PaymentType item);
       void DeleteTypeById(int id);
       string getTypeNameByType(string code);
    }
}
