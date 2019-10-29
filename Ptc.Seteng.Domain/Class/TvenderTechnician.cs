using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 技師主檔
    /// </summary>
    public class TvenderTechnician
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 廠商別
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 技師姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 裝置ID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 推播ID
        /// </summary>
        public string RegistrationID { get; set; }
        /// <summary>
        /// 最後登入時間
        /// </summary>
        public Nullable<System.DateTime> LastLoginTime { get; set; }   
    
        /// <summary>
        /// 是否啟用
        /// </summary>
        public Boolean Enable { get; set; }
        /// <summary>
        /// 技師密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 所屬的廠商
        /// </summary>    
        public Tvender TVENDER { get; set; }
        /// <summary>
        /// 是否為廠商
        /// </summary>
        public bool IsVendor { get; set; }
        /// <summary>
        /// 關聯技師與群組
        /// </summary>
        public List<TtechnicianGroupClaims> TTechnicianGroupClaims { get; set; }

        /// <summary>
        /// 可認養的案件
        /// </summary>
        public List<Tcallog> TCALLOG { get; set; }
    }
}
