using Ptc.AspnetMvc.Authentication;
using Ptc.Seteng.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CheckTechnicianDetailViewModel
    {
        string _localPath;
        public CheckTechnicianDetailViewModel()
        {
            this._localPath = ServerProfile.GetInstance().LOCAL_SITE;
        }


        public CheckTechnicianDetailViewModel(TvenderTechnician data) : this()
        {
            this.CompCd = data.CompCd;
            this.VenderCd = data.VenderCd;
            //this.CompName = data.TVENDER?.TCMPDAT?.CompName;
            this.VenderName = data.TVENDER?.VenderName;
            this.Name = data.Name;
            this.Password = data.Password;
            this.Account = data.Account;
        }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 厂商名稱
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 技師名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
    }
}

