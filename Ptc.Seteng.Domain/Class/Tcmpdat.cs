using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 公司資料檔
    /// </summary>
    public class Tcmpdat
    {
        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }

        /// <summary>
        /// 公司簡稱
        /// </summary>
        public string CompShort { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompAddr { get; set; }

        /// <summary>
        /// Email帳號
        /// </summary>
        public string EmailAccount { get; set; }

        /// <summary>
        /// 排列序號
        /// </summary>
        public short SortSeq { get; set; }

        /// <summary>
        /// 店名檢查碼
        /// </summary>
        public string ChkStrNm { get; set; }

        /// <summary>
        /// 公司營運狀態
        /// </summary>
        public decimal CompSts { get; set; }

        /// <summary>
        /// 公司圖案
        /// </summary>
        public byte[] CompImage { get; set; }

        /// <summary>
        /// 修改人員
        /// </summary>
        public string UpdateUser { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
