using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 區域主檔
    /// </summary>
    public class Tzocode
    {

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }

        /// <summary>
        /// 區代號
        /// </summary>
        public string ZoCd { get; set; }

        /// <summary>
        /// 區名稱
        /// </summary>
        public string ZoName { get; set; }

        /// <summary>
        /// 課代號
        /// </summary>
        public string DoCd { get; set; }

        /// <summary>
        /// 課名稱
        /// </summary>
        public string DoName { get; set; }

        /// <summary>
        /// 結束日期
        /// </summary>
        public string CloseDate { get; set; }

        /// <summary>
        /// 區課統計類型
        /// </summary>
        public string ZoType { get; set; }

        /// <summary>
        /// 臨時財編碼
        /// </summary>
        public string ZoAstCd { get; set; }

        /// <summary>
        /// 是否計算保費
        /// </summary>
        public string UpkeepSts { get; set; }

        /// <summary>
        /// 是否可以叫修
        /// </summary>
        public string CallSts { get; set; }

        /// <summary>
        /// 是否可以成立專案
        /// </summary>
        public string PrjSts { get; set; }

        /// <summary>
        /// 是否轉財會
        /// </summary>
        public string ActSts { get; set; }       

    }
}
