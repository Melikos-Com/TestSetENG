using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 受理紀錄
    /// </summary>
    public class TacceptedLog
    {
        /// <summary>
        /// 案件編號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 技師帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 技師身分證字號
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 技師名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 受理時間
        /// </summary>
        public DateTime RcvDatetime { get; set; }
        /// <summary>
        /// 受理註記
        /// </summary>
        public string RcvRemark { get; set; }
        /// <summary>
        /// 所屬的案件
        /// </summary>
        public Tcallog TCALLOG { get; set; }
    }
}
