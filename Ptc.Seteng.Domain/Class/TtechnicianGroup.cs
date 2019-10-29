using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 技師群組主檔
    /// </summary>
    public class TtechnicianGroup
    {
        /// <summary>
        /// 群組序號
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 廠商別
        /// </summary>
        public string VendorCd { get; set; }
        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群組區域
        /// </summary>
        public string Responsible_Zo { get; set; }
        /// <summary>
        /// 群組課別
        /// </summary>
        public string Responsible_Do { get; set; }

        /// <summary>
        /// 關聯群組與技師
        /// </summary>
        public List<TtechnicianGroupClaims> TTechnicianGroupClaims { get; set; }

    }
}
