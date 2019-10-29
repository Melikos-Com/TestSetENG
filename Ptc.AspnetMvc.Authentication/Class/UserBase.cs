
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication
{
    [Serializable]
    public class UserBase
    {
        public UserBase()
        {       
            this.UserPageAuth = new Dictionary<string, AuthItem>();
        }

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; } = "711";
        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompShort { get; set; } = "統一超商";
        /// <summary>
        /// 帳號
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// user綽號
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 對應的角色
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 區域代號
        /// </summary>
        public string ZoCd { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 廠商名稱
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// 門市名稱
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// EMAIL
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 連絡電話
        /// </summary>
        public string TelNo { get; set; }
        /// <summary>
        /// 傳真號碼
        /// </summary>
        public string FaxNo { get; set; }
        /// 手機號碼
        /// </summary>
        public string MobileTel { get; set; }
      
        /// <summary>
        /// TODO:待確認
        /// </summary>
        public Boolean IdSts { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string Create_User { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime Create_Date { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string Update_User { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public Nullable<DateTime> Update_Date { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 裝置代號
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// RegistrationID
        /// </summary>
        public string RegistrationID { get; set; }
        /// <summary>
        /// <summary>
        /// 頁面操作權限
        /// </summary>
        public List<AuthItem> PageAuth { get; set; }
        /// <summary>
        /// 權限
        /// </summary>
        public RoleAuth RoleAuth { get; set; }
        /// <summary>
        /// 使用者頁面權限
        /// </summary>
        public Dictionary<string, AuthItem> UserPageAuth { get; private set; }
        /// <summary>
        /// 計算相關權限
        /// </summary>
        public void CalcAuth()
        {

            HashSet<AuthData> mergeDataRage = new HashSet<AuthData>();
            HashSet<AuthData> mergeBusiness = new HashSet<AuthData>();
            HashSet<AuthItem> mergePageAuth = new HashSet<AuthItem>();



            mergePageAuth.UnionWith(RoleAuth.PageAuth.Where(x => x.isDeny == false && x.AuthType.HasValue).ToList()); 
          

            if (this.PageAuth == null)
                this.PageAuth = new List<AuthItem>();



            #region 頁面權限調整

            //排除isDeny=true    使用者主檔

            var userDenyPageAuthList = this.PageAuth.Where(x => x.isDeny).ToList();

            mergePageAuth.ExceptWith(userDenyPageAuthList);

            //移除既有準備依使用者覆蓋 角色檔
            var userOverridePageAuthList = this.PageAuth.Where(x => x.isDeny == false).ToList();
            mergePageAuth.ExceptWith(userOverridePageAuthList);

            //加回user覆蓋的
            mergePageAuth.UnionWith(userOverridePageAuthList);

            #endregion

         
            UserPageAuth = mergePageAuth.ToDictionary(x => x.GroupName);
        }

        /// <summary>
        /// 資料範圍
        /// </summary>
        public DataRange DataRange { get; set; }

    }
}
