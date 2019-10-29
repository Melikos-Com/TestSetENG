using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 廠商主檔
    /// </summary>
    public class Tvender
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 廠商編號
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 廠商名稱
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string VenderAddr { get; set; }
        /// <summary>
        /// 統一編號
        /// </summary>
        public string VenderId { get; set; }
        /// <summary>
        /// 課稅別
        /// </summary>
        public string TaxType { get; set; }
        /// <summary>
        /// 聯繫人
        /// </summary>
        public string ContactWindow { get; set; }
        /// <summary>
        /// 聯繫電話
        /// </summary>
        public string TelNo { get; set; }
        /// <summary>
        /// 傳真號碼
        /// </summary>
        public string FaxNo { get; set; }
        /// <summary>
        /// 電子信箱
        /// </summary>
        public string EmailAccount { get; set; }
        /// <summary>
        /// 例假日電話
        /// </summary>
        public string HolidayTelNo { get; set; }
        /// <summary>
        /// 緊急電話
        /// </summary>
        public string EmcTelNo { get; set; }
        /// <summary>
        /// 新案件通知碼
        /// </summary>
        public string ShowNewCall { get; set; }
        /// <summary>
        /// 是否資料交換
        /// </summary>
        public string IsDataTrans { get; set; }
        /// <summary>
        /// 是否推播
        /// </summary>
        public string IsPush { get; set; }
        /// <summary>
        /// 交換參數代碼
        /// </summary>
        public string TransPara { get; set; }
        /// <summary>
        /// 關係企業代碼
        /// </summary>
        public string Affiliates { get; set; }
        /// <summary>
        /// 結束日期
        /// </summary>
        public string CloseDate { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public string UpdateDate { get; set; }

        /// <summary>
        /// 底下的群組
        /// </summary>
        public IEnumerable<TtechnicianGroup> TTechnicianGroup { get; set; }

        /// <summary>
        /// 底下的技師
        /// </summary>
        public IEnumerable<TvenderTechnician> TVenderTechnician { get; set; }
    }
}
