﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
   public interface IEntryManager
    {
       void addEntryList(T_EntryList entity);

       DataSet getEntryList();

       void invoice(List<T_ContractList> lists);
    }
}
