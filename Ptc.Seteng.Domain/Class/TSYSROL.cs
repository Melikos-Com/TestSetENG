using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 角色權限
    /// </summary>
    public class Tsysrol
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 權限id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 權限名稱
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 敘述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// TODO:
        /// </summary>
        public byte RoleKind { get; set; }
        /// <summary>
        /// TODO:
        /// </summary>
        public string RoleSts { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }
        /// <summary>
        /// 頁面權限
        /// </summary>
        public string PageAuth { get; set; }

    }
}
