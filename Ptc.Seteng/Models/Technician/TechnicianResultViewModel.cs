using Ptc.Seteng.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianResultViewModel
    {

        public TechnicianResultViewModel() { }

        public TechnicianResultViewModel(TvenderTechnician data)
        {

            this.Name = data.Name;
            this.Account = data.Account;
            this.Enable = data.Enable ? "正常" : "停用";
            this.IsVendor = data.IsVendor ? "技師主管" : "技師";

            this.CompCd = data.CompCd;
            this.VenderCd = data.VenderCd;

            colData = new String[] {
                this.Account,
                this.Name,
                this.Enable,
                this.IsVendor,
                this.CompCd,
                this.VenderCd
            };
        }

        public String[] colData { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 公司代号
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 厂商别
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 技師名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否啟用
        /// </summary>
        public string Enable { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string IsVendor { get; set; }
    }
}
