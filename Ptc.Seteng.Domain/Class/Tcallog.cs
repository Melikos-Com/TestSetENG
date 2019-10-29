using Ptc.Seteng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 案件主檔
    /// </summary>
    public class Tcallog
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
        public string FiDate { get; set; }

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
        /// 維修廠商
        /// </summary>
        public string VenderCd { get; set; }
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
        /// 工作類型
        /// </summary>
        public string WorkId { get; set; }  
        /// <summary>
        /// 銷案類別
        /// </summary>
        public string FinishId { get; set; }
        /// <summary>
        /// 處理類型
        /// </summary>
        public string DamageProcNo { get; set; }
        /// <summary>
        /// 工程人員代號
        /// </summary>
        public string EngNo { get; set; }
        /// <summary>
        /// 銷案狀態碼
        /// </summary>
        public int CloseSts { get; set; }

        /// <summary>
        /// 受理案件檔
        /// </summary>
        public TacceptedLog TacceptedLog { get; set; }

        /// <summary>
        /// 可認養的技師
        /// </summary>
        public List<TvenderTechnician> TvenderTechnician { get; set; }

        /// <summary>
        /// 案件狀態
        /// </summary>
        public int TimePoint { get; set; }

        /// <summary>
        /// 案件圖片集合-修理前
        /// </summary>
        public IEnumerable<String> ImgBeforeFix { get; set; }
        /// <summary>
        /// 案件圖片集合-修理後
        /// </summary>
        public IEnumerable<String> ImgAfterFix { get; set; }
        /// <summary>
        /// 案件圖片-工單或店章
        /// </summary>
        public IEnumerable<String> Img { get; set; }
        /// <summary>
        /// 案件圖片-門市簽名
        /// </summary>
        public IEnumerable<String> ImgSignature { get; set; }

        /// <summary>
        /// 受理技師姓名
        /// </summary>
        public string AcceptedName { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public string Updatedate { get; set; }
        /// <summary>
        /// 維修說明
        /// </summary>
        public string WorkDesc { get; set; }

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
        /// 案件時間記錄履歷
        /// </summary>
        public List<TCallLogDateRecord> TcallLogDateRecords { get; set; }
        /// <summary>
        /// 叫修發票檔
        /// </summary>
        public TCALINV TCALINV { get; set; }
    }
}
