using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianResultApiViewModel
    {
        public TechnicianResultApiViewModel() { }

        public TechnicianResultApiViewModel(TvenderTechnician data)
        {

            this.VenderCd = data.VenderCd;
            this.VenderName = data.TVENDER?.VenderName;
            this.CompCd = data.CompCd;
            //this.CompName = data.TVENDER?.TCMPDAT?.CompName;
            this.Enable = data.Enable;
            this.IsVendor = data.IsVendor;
            this.Name = data.Name;
            this.Account = data.Account;         
        }

        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 廠商名稱
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 廠商姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否啟用
        /// </summary>
        public Boolean Enable { get; set; }
        /// <summary>
        /// 是否為廠商
        /// </summary>
        public Boolean IsVendor { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}