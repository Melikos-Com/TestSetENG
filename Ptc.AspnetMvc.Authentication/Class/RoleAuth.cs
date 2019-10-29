
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ptc.AspnetMvc.Authentication
{
    /// <summary>
    /// 權限
    /// </summary>
    [Serializable]
    public class RoleAuth
    {
        public RoleAuth()
        {
            this.PageAuth = new List<AuthItem>();
          
        }


        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 角色代碼
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 頁面權限 
        /// </summary>
        public List<AuthItem> PageAuth { get; set; }
  
        /// <summary>
        /// 修改人員
        /// </summary>
        public string UpdateUser { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }

   

    }
}