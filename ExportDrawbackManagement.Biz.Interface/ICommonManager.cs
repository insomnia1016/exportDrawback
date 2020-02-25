using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface ICommonManager
    {
       DataSet getUserInfoById(int person_id);

       DataSet getCustomers();
       DataSet getXuFang();
       DataSet getDeliveryMode();
       DataSet getTaxReturnState();
       string getTaxReturnStateNameByState(string code);
       string getTaxReturnStateCodeByName(string name);
       string getDeliveryModeNameByCode(string code);
       DataSet getCurrency();
       String getCurrencyByID(int id);
       DataSet getCustomersBySearchKeyName(string name);
    }
}
