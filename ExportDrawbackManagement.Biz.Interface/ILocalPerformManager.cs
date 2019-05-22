using System;
using System.Collections.Generic;
using System.Text;
using PLM.Biz.Entity;
using System.Data;

namespace PLM.Biz.Interface
{
    public interface ILocalPerformManager
    {
        DataTable F004(BaseQueryPara para);
        DataTable F007(BaseQueryPara para);
        DataTable F005(BaseQueryPara para, FilterBy filterBy);
        DataTable F008(BaseQueryPara para, string filterBy);
    }
}
