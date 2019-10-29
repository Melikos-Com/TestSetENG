using Ptc.Seteng.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianListResultViewModel
    {
        public TechnicianListResultViewModel() { }

        public TechnicianListResultViewModel(TvenderTechnician data)
        {
            this.VenderCd = data.VenderCd;
            this.CompCd = data.CompCd;
            this.VenderName = data.TVENDER?.VenderName;
            //this.CompName = data.TVENDER?.TCMPDAT?.CompName;
            this.Name = data.Name;
            this.Account = data.Account;

            colData = new String[] {
               string.Empty,
               this.VenderName ,
               this.Account ,
               this.Name ,
               $"{this.Account},{this.CompCd},{this.VenderCd}",
               this.CompCd,
               this.VenderCd,
            };

        }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 公司代号
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 厂商代号
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 厂商名稱
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 技師名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 准备回传的
        /// </summary>
        public String[] colData { get; set; }
    }
}
