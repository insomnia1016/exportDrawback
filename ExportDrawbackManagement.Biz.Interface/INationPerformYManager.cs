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
    public interface INationPerformYManager
    {
        DataTable GetData(Q_NationPerformY para);
    }
}
