using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class MemberSetResultViewModel
    {

        public MemberSetResultViewModel(UserBase temp)
        {
            this.CompCd = temp.CompCd;
            this.UserID = temp.UserId;
            this.UserName = temp.UserName;
            this.CorpUserName = temp.Nickname;
            this.RoleName = temp.RoleAuth.RoleName;
        }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [DisplayName("使用者帳號")]
        [DisplayFormat(NullDisplayText = "使用者帳號")]
        public string UserName { get; set; }
        /// <summary>
        /// 使用者姓名
        /// </summary>
        [DisplayName("使用者姓名")]
        [DisplayFormat(NullDisplayText = "使用者姓名")]
        public string CorpUserName { get; set; }
        /// <summary>
        /// 權限名稱
        /// </summary>
        [DisplayName("權限名稱")]
        [DisplayFormat(NullDisplayText = "權限名稱")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [DisplayName("角色ID")]
        [DisplayFormat(NullDisplayText = "角色ID")]
        public string UserID { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DisplayName("公司名稱")]
        [DisplayFormat(NullDisplayText = "公司名稱")]
        public string  CompCd { get; set; }
    }
}