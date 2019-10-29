using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class StoreUserApiViewModel
    {

        public StoreUserApiViewModel(Tusrmst data , Tstrmst store) {
            //this.CompName = store.TZOCODE?.TCMPDAT?.CompName;
            this.StoreName = store.StoreName;
            this.ZoName = store.ZoName;
            this.CompCd = data.CompCd;
            this.StoreCd = data.StoreCd;
            this.UserName = data.UserName;
            this.UserId = data.UserId;
        }

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }

        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 使用者代號
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompName { get; set; }

        /// <summary>
        /// 門市名稱
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 區域名稱
        /// </summary>
        public string ZoName { get; set; }
    }
}