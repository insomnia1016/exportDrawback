using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PLM.Biz.Entity;

namespace PLM.Biz.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocalPerformMManager
    {
        DataTable F004(Q_LocalPerformM para);
        DataTable F005(Q_LocalPerformM para, FilterBy filterBy);
    }
}
