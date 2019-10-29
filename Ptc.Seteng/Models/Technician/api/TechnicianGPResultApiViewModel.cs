using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianGPResultApiViewModel
    {
        public TechnicianGPResultApiViewModel() { }

        public TechnicianGPResultApiViewModel(TtechnicianGroup data)
        {
            this.CompCd = data.CompCd;
            this.GroupName = data.GroupName;
            this.VendorCd = data.VendorCd;
            this.Seq = data.Seq;

            var teClaim = data.TTechnicianGroupClaims;

            //取得已經通過總部審核跟已啟用的
            this.Count = teClaim.Select(x => x.TVenderTechnician)
                                .Count(g => g.Enable);
        }

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VendorCd { get; set; }
        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 序號
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 人數
        /// </summary>
        public int Count { get; set; }

    }

}