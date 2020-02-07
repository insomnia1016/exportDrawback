﻿using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface IProfitBudgetManager
    {
       DataSet getSEOrderInfo(T_ProfitBudget item);
       string getDeptNameById(string dept_id);
       string getEmpNameById(string emp_id);
       void addProfitBudgetHead(T_ProfitBudget item);
       void addProfitBudgetList(List<T_ProfitBudgetList> lists);
       void updateProfitBudgetHead(T_ProfitBudget item);
       void updateProfitBudgetList(List<T_ProfitBudgetList> lists);
       void deleteBySaleBillNo(string sale_bill_no);
       DataSet getProfitBudgetSummary();
       DataSet getProfitBudgetByID(int FInterIID, string sale_bill_no);
    }
}
