using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PLM.Biz.Interface
{
    /// <summary>
    /// 全国平均单价查询
    /// </summary>
    public interface INationPerformQManager
    {
        DataTable GetData(Q_NationPerformQ para);
    }
}
