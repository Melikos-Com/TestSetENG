using Newtonsoft.Json;
using Ptc.AspnetMvc.Authentication;
using Ptc.Seteng.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianEditViewModel
    {

        string _localPath;



        public TechnicianEditViewModel()
        {

            //this._localPath = ServerProfile.GetInstance().LOCAL_SITE;

        }

        public TechnicianEditViewModel(TvenderTechnician data) : this()
        {
            this.CompCd = data.CompCd;
            this.VenderCd = data.VenderCd;
            this.Account = data.Account;
            this.Name = data.Name;       
            this.Enable = data.Enable;
            this.Password = data.Password;          
            this.IsVendor = data.IsVendor;
        }
        /// <summary>
        /// 公司代號
        /// </summary>
        [Required]
        [DisplayFormat(NullDisplayText = "公司代號")]
        public string CompCd { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        [DisplayName("帳號")]
        [DisplayFormat(NullDisplayText = "帳號")]
        public string Account { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        [Required]
        [DisplayFormat(NullDisplayText = "廠商代號")]
        public string VenderCd { get; set; }
        /// <summary>
        /// 技師姓名
        /// </summary>
        [DisplayName("技師姓名")]
        [DisplayFormat(NullDisplayText = "技師姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 帳號啟用
        /// </summary>
        [DisplayName("帳號啟用")]
        [DisplayFormat(NullDisplayText = "帳號啟用")]
        public Boolean Enable { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        [DisplayFormat(NullDisplayText = "密碼")]
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 证件照
        /// </summary>
        public HttpPostedFileBase[] TechniciansfileInput { get; set; }
        /// <summary>
        /// 技能证书
        /// </summary>
        public HttpPostedFileBase[] LicensefileInput { get; set; }
        /// <summary>
        /// 大头照
        /// </summary>
        public HttpPostedFileBase[] StickersfileInput { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [DisplayName("角色")]
        [DisplayFormat(NullDisplayText = "角色")]
        public bool IsVendor { get; set; }
        /// <summary>
        /// 公司名稱(中文)
        /// </summary>
        [DisplayName("公司名稱")]
        public string CompName { get; set; }
        /// <summary>
        /// 廠商名稱(中文)
        /// </summary>
        [DisplayName("廠商名稱")]
        public string VendorName { get; set; }
    }
}

