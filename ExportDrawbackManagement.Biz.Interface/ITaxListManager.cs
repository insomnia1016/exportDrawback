﻿using ExportDrawbackManagement.Biz.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    public interface ITaxListManager
    {
        void generateTaxList(decimal bl);
        DataSet getTaxList();
        void UpdateState(T_TaxList item, string[] ids);
        DataSet queryTaxList(T_TaxList item);
        DataSet getTaxListByKeys(string entryId, string sale_bill_no);
        decimal getTaxReturnTotal(string sale_bill_no);
    }
}
