using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface IProfitAccountingManager
    {
        DataSet getSEOrderInfo(T_ProfitAccounting item);
        DataSet getProfitBudgetList(List<Int32> lists);
        void addProfitBudgetHead(T_ProfitAccounting item);
        void addProfitBudgetList(List<T_ProfitAccountingList> lists);
        void updateProfitBudgetList(List<T_ProfitAccountingList> lists);
        void updateProfitBudgetHead(T_ProfitAccounting item);
        DataSet getProfitBudgetByID(int FInterIID, string sale_bill_no);
        void deleteBySaleBillNo(string sale_bill_no);
        void audit(string sale_bill_no, bool flag);
        void IsActualAudit(string sale_bill_no, bool flag);
        DataSet getProfitBudgetSummary();
        DataSet getProfitBudgetSummaryByID(string sale_bill_no);
        DataSet getActualProfit(string sale_bill_no);
        string getExtraCharges(string sale_bill_no);
    }
}
