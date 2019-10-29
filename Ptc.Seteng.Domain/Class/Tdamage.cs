using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 故障分類主檔
    /// </summary>
    public class Tdamage
    {

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 故障代碼
        /// </summary>
        public string DamageNo { get; set; }
        /// <summary>
        /// 故障內容
        /// </summary>
        public string DamageDesc { get; set; }
        /// <summary>
        /// 叫修次數
        /// </summary>
        public int CalCnt { get; set; }
        /// <summary>
        /// 所屬的設備
        /// </summary>
        public Tassets TASSETS { get; set; }

    }
}
