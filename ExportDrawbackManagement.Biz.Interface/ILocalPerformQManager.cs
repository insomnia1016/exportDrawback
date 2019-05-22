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
    public interface ILocalPerformQManager
    {
        DataTable F004(Q_LocalPerformQ para);
        DataTable F005(Q_LocalPerformQ para, FilterBy filterBy);
    }
}
