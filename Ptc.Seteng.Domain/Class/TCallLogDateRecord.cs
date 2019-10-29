using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class TCallLogDateRecord
    {
        /// <summary>
        /// 案件時間紀錄流水號
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 案件編號
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 案件時間記錄類型
        /// </summary>
        public RecordType RecordType { get; set; }

        /// <summary>
        /// 案件時間記錄值
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// 資料建立人員
        /// </summary>
        public string Create_User { get; set; }
        /// <summary>
        /// 資料建立時間
        /// </summary>
        public DateTime Create_Date { get; set; }

    }
}
