using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 使用者密碼主檔
    /// </summary>
   public class Tusrdtl
    {
        /// <summary>
        /// 使用者id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string PassWd { get; set; }
        /// <summary>
        /// 密碼更新時間
        /// </summary>
        public Nullable<DateTime> PwdChgTime { get; set; }
        /// <summary>
        /// 待確認的
        /// </summary>
        public short LoginErrTimes { get; set; }
        /// <summary>
        /// 待確認的
        /// </summary>
        public int LoginTimes { get; set; }
        /// <summary>
        /// 待確認的
        /// </summary>
        public Nullable<DateTime> LoginDateTime { get; set; }
        /// <summary>
        /// 待確認的
        /// </summary>
        public byte ChgPasSts { get; set; }
        /// <summary>
        /// 所屬的帳號
        /// </summary>
        public Tusrmst TUSRMST { get; set; }

    }
}
