using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PLM.Biz.Interface
{
    /// <summary>
    /// ȫ��ƽ�����۲�ѯ
    /// </summary>
    public interface INationPerformQManager
    {
        DataTable GetData(Q_NationPerformQ para);
    }
}
