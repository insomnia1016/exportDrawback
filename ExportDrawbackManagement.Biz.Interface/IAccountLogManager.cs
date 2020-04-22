using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IAccountLogManager
    {
       void insert(T_AccountLog item);
       void Add(T_AccountLog item);
       DataSet getAccountLogs(string account_id, string start_time, string end_time);
       DataSet getAccount(string account_id);
    }
}
