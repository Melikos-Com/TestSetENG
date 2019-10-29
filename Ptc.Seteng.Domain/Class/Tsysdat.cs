using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 系統參數檔
    /// </summary>
    public class Tsysdat
    {
        /// <summary>
        /// pk
        /// </summary>
        public string KeyCd { get; set; }
        /// <summary>
        /// smtp server
        /// </summary>
        public string SmptName { get; set; }
        /// <summary>
        /// send from
        /// </summary>
        public string SendMail { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string MailPassword { get; set; }
        /// <summary>
        /// 更換密碼週期
        /// </summary>
        public byte PwdCycle { get; set; }
        /// <summary>
        /// 登入鎖定次數
        /// </summary>
        public byte PwdTimes { get; set; }
        /// <summary>
        /// 寄信失敗告知對象
        /// </summary>
        public string ErrRecvMail { get; set; }
        /// <summary>
        /// 寄信失敗CC對象
        /// </summary>
        public string CCMail { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}
