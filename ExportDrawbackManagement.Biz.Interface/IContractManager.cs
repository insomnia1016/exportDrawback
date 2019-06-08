using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IContractManager
    {
       void addContractList(List<T_ContractList> lists);
       void addContractHead(T_ContractHead head);
       DataSet getContractSummary();
       DataSet queryContractSummary(T_ContractHead head);
       T_ContractHead getContractDetail(string id);
    }
}
