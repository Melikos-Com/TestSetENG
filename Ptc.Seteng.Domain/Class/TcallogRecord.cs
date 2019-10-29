using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 推播記錄檔
    /// </summary>
    public class TcallogRecord
    {
        /// <summary>
        /// 公司別
        /// </summary>
        public string Comp_Cd { get; set; }
        /// <summary>
        /// 案件編號
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 技師帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 紀錄時間
        /// </summary>
        public DateTime RecordDatetime { get; set; }
        /// <summary>
        /// 紀錄內容
        /// </summary>
        public string RecordRemark { get; set; }
        /// <summary>
        /// 所屬的案件
        /// </summary>
        public Tcallog TCALLOG { get; set; }

    }
}
