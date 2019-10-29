using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TusrmstResultApiViewModel
    {
        public TusrmstResultApiViewModel() { }

        public TusrmstResultApiViewModel(Tusrmst data)
        {
            this.CompCd = data.CompCd;
            this.StoreCd = data.StoreCd;
            if (data.RoleId == "Admin") this.RoleID = RoleType.Admin;
            else if (data.RoleId == "Admin_Zo") this.RoleID = RoleType.Admin_Zo;
            else if (data.RoleId == "TeamLeader") this.RoleID = RoleType.TeamLeader;
            else if (data.RoleId == "CM") this.RoleID = RoleType.CM;
            else if (data.RoleId == "Store") this.RoleID = RoleType.Store;
            this.Enable = data.IdSts;
            this.Account = data.UserId;
            this.Name = data.UserName;
            this.RoleIDName = data.TSYSROL.RoleName;
        }

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// 角色代號
        /// </summary>
        public RoleType RoleID { get; set; }
        /// <summary>
        /// 是否啟用
        /// </summary>
        public Boolean Enable { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
                /// <summary>
        /// 角色代號名稱
        /// </summary>
        public string RoleIDName { get; set; }
    }
}