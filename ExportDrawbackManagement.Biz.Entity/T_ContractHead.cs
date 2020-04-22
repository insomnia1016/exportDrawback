using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExportDrawbackManagement.Biz.Entity
{
    public partial class T_ContractHead
    {
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string baoguandanhao { get; set; }
        public string xiaoshoufapiaohao { get; set; }
        public DataSet lists { get; set; }
    }
}
