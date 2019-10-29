using Ptc.Seteng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 關聯技師與群組
    /// </summary>
    public class TtechnicianGroupClaims
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VendorCd { get; set; }
        /// <summary>
        /// 技師帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 所屬的技師群組
        /// </summary>
        public  TtechnicianGroup TTechnicianGroup { get; set; }
        /// <summary>
        /// 所屬的技師
        /// </summary>
        public  TvenderTechnician TVenderTechnician { get; set; }
    }
}
