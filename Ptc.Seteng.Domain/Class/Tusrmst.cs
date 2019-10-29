using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 使用者帳號主檔
    /// </summary>
    public class Tusrmst
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 使用者id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 使用者綽號
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// role id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 待確認的
        /// </summary>
        public string AstTeamCd { get; set; }
        /// <summary>
        /// 區域代號
        /// </summary>
        public string ZoCd { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string EmailAccount { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string TelNo { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        public string FaxNo { get; set; }
        /// <summary>
        /// 手機
        /// </summary>
        public string MobileTel { get; set; }
        /// <summary>
        /// 待確認的
        /// </summary>
        public string SeedPrefix { get; set; }
        /// <summary>
        /// 帳號狀態
        /// </summary>
        public Boolean IdSts { get; set; }
        /// <summary>
        /// 頁面權限
        /// </summary>
        public string PageAuth { get; set; }
        /// <summary>
        /// 資料範圍
        /// </summary>
        public string DataRange { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        public Nullable<System.DateTime> UpdateDate { get; set; }
        /// <summary>
        /// 裝置ID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// RegistrationID
        /// </summary>
        public string RegistrationID { get; set; }
        /// <summary>
        /// 使用者密碼
        /// </summary>
        public virtual Tusrdtl TUSRDTL { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public virtual Tsysrol TSYSROL { get; set; }
    }
}
