using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PLM.Biz.Entity;

namespace PLM.Biz.Interface
{
    /// <summary>
    /// 全国平均单价查询
    /// </summary>
    public interface INationPerformYManager
    {
        DataTable GetData(Q_NationPerformY para);
    }
}
