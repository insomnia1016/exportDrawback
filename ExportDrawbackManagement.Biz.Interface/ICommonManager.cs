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
       DataSet getDeliveryMode();
       DataSet getTaxReturnState();
       string getTaxReturnStateNameByState(string code);
       string getTaxReturnStateCodeByName(string name);
       string getDeliveryModeNameByCode(string code);
    }
}
