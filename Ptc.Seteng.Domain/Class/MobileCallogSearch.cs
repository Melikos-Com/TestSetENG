using Ptc.Seteng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// View
    /// </summary>
    public class MobileCallogSearch
    {
        /// <summary>
        /// 案件編號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// 門市名稱
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 門市電話
        /// </summary>
        public string Telno { get; set; }
        /// <summary>
        /// 門市地址
        /// </summary>
        public string StoreAddr { get; set; }
        /// <summary>
        /// 區代號
        /// </summary>
        public string Zo { get; set; }

        /// <summary>
        /// 課代號
        /// </summary>
        public string Do { get; set; }

        /// <summary>
        /// 叫修時間
        /// </summary>
        public DateTime FiDate { get; set; }

        /// <summary>
        /// 叫修人職稱
        /// </summary>
        public string CallerId { get; set; }

        /// <summary>
        /// 叫修人姓名
        /// </summary>
        public string CallerName { get; set; }
        /// <summary>
        /// 設備大類
        /// </summary>
        public string AstKind1 { get; set; }
        /// <summary>
        /// 設備中類
        /// </summary>
        public string AstKind2 { get; set; }
        /// <summary>
        /// 設備小類
        /// </summary>
        public string AstKind3 { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 財產編號
        /// </summary>
        public string AssetNo { get; set; }
        /// <summary>
        /// SAP財產編號
        /// </summary>
        public string SapAssetNo { get; set; }
        /// <summary>
        /// 故障代碼
        /// </summary>
        public string DamageNo { get; set; }
        /// <summary>
        /// 故障名稱
        /// </summary>
        public string DamageDesc { get; set; }
        /// <summary>
        /// 維修廠商
        /// </summary>
        public string VenderCd { get; set; }

        /// <summary>
        /// 維修廠商名稱
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 叫修等級
        /// </summary>
        public string CallLevel { get; set; }
        /// <summary>
        /// 叫修備註
        /// </summary>
        public string CallDesc { get; set; }
        /// <summary>
        /// 補充備註
        /// </summary>
        public string RemarkAdd { get; set; }
        /// <summary>
        /// 通知時間
        /// </summary>
        public string FvDate { get; set; }
        /// <summary>
        /// 應完成時間
        /// </summary>
        public string FdDate { get; set; }
        /// <summary>
        /// 應到店時間
        /// </summary>
        public string FaDate { get; set; }
        /// <summary>
        /// 到店時間
        /// </summary>
        public string ArriveDate { get; set; }
        /// <summary>
        /// 完成時間
        /// </summary>
        public string FcDate { get; set; }
        /// <summary>
        /// 銷案類別
        /// </summary>
        public string FinishId { get; set; }
        /// <summary>
        /// 工程人員代號
        /// </summary>
        public string EngNo { get; set; }
        /// <summary>
        /// 銷案狀態碼
        /// </summary>
        public int CloseSts { get; set; }

        /// <summary>
        /// 待受理案件檔的帳號
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 已受理案件的Sn(判斷是否已受理用)
        /// </summary>
        public string AcceptSn { get; set; }
        /// <summary>
        /// 已受理案件的帳號
        /// </summary>
        public string AcceptAccount { get; set; }
        /// <summary>
        /// 已受理案件的技師
        /// </summary>
        public string Acceptname { get; set; }
        
        /// <summary>
        /// 案件狀態
        /// </summary>
        public string Timepoint { get; set; }

        /// <summary>
        /// 維修說明
        /// </summary>
        public string Workdesc { get; set; }

        /// <summary>
        /// 案件受理時間
        /// </summary>
        public DateTime? AcceptDatetime { get; set; }

        /// <summary>
        /// 工作類型
        /// </summary>
        public string WorkName { get; set; }

        /// <summary>
        /// 銷案類型
        /// </summary>
        public string Finish_Name { get; set; }

        /// <summary>
        /// 處理類型
        /// </summary>
        public string Mtn_Desc { get; set; }
        /// <summary>
        ///// 到店時間
        ///// </summary>
        //public string Arrive_Date { get; set; }
        ///// <summary>
        ///// 離店時間
        ///// </summary>
        //public string Fc_Date { get; set; }
        /// <summary>
        /// 催修時間
        /// </summary>
        public string RcvDate { get; set; }
        /// <summary>
        /// 咖啡杯數
        /// </summary>
        public decimal? CoffeeCup { get; set; }
        /// <summary>
        /// 銷案人員
        /// </summary>
        public string VndEngId { get; set; }

        /// <summary>
        /// 銷案時間
        /// </summary>
        public string AppCloseDate { get; set; }

        /// <summary>
        /// 預估金額
        /// </summary>
        public string Pre_Amt { get; set; }
    }
}
