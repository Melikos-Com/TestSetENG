using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 單位設備主檔
    /// </summary>
    public class Tstrast
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 設施編號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 系統編號
        /// </summary>
        public int AssetSeq { get; set; }
        /// <summary>
        /// 財產編號
        /// </summary>
        public string AssetNo { get; set; }
        /// <summary>
        /// 店號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// 保固廠商
        /// </summary>
        public string MaintainVender { get; set; }
        /// <summary>
        /// 維修廠商
        /// </summary>
        public string FixVender { get; set; }
        /// <summary>
        /// 啟用日期
        /// </summary>
        public Nullable<DateTime> BeginDate { get; set; }
        /// <summary>
        /// 保固到期日
        /// </summary>
        public Nullable<DateTime> LimitDate { get; set; }
        /// <summary>
        /// 刪除碼
        /// </summary>
        public string DelSts { get; set; }
        /// <summary>
        /// 異動原因
        /// </summary>
        public string ModReason { get; set; }
        /// <summary>
        /// 撥入日期
        /// </summary>
        public Nullable<DateTime> TransDate { get; set; }
        /// <summary>
        /// 停用日期
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; }
        /// <summary>
        /// 設施成本
        /// </summary>
        public Nullable<decimal> AssetCost { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 建立人員    
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 修改人員
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
        /// 所屬的設備
        /// </summary>
        public Tassets TASSETS { get; set; }
        /// <summary>
        /// 底下的案件清單
        /// </summary>
        public IEnumerable<Tcallog> TCALLOG { get; set; }
        /// <summary>
        /// 所屬的門市
        /// </summary>
        public Tstrmst TSTRMST { get; set; }
        /// <summary>
        /// 所屬的廠商
        /// </summary>
        public Tvender TVENDER { get; set; }

    }
}
