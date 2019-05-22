using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Biz.Interface
{
    /// <summary>
    /// 表名后缀
    /// </summary>
    [Flags]
    public enum TableOption
    {
        /// <summary>
        /// 其他系统
        /// </summary>
        OtherSystem = 1,
        /// <summary>
        /// 临时表
        /// </summary>
        TempTable = 2,
        /// <summary>
        /// 队列表
        /// </summary>
        QueueTable = 4,
        /// <summary>
        /// 预录入表
        /// </summary>
        PreparationTable = 8,
        /// <summary>
        /// 正式表
        /// </summary>
        OfficialTable = 16,
        /// <summary>
        /// 历史表
        /// </summary>
        HistoryTable = 32,
    }
}
