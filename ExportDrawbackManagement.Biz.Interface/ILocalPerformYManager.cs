using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PLM.Biz.Entity;

namespace PLM.Biz.Interface
{
    /// <summary>
    /// ȫ��ƽ�����۲�ѯ
    /// </summary>
    public interface ILocalPerformYManager
    {
        DataTable F004(Q_LocalPerformY para);
        DataTable F005(Q_LocalPerformY para, FilterBy filterBy);
    }
}
