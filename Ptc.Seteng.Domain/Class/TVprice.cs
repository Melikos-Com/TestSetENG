using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 廠商零件價格檔
    /// </summary>
    public class Tvprice
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 零件編號
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// 中文品名
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 英文品名
        /// </summary>
        public string PartNameEn { get; set; }
        /// <summary>
        /// 訂貨單位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 零件價格
        /// </summary>
        public Nullable<decimal> Price { get; set; }
        /// <summary>
        /// 保修期
        /// </summary>
        public string ExpDate { get; set; }
        /// <summary>
        /// 規格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 建立時間
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
        /// 所屬設備
        /// </summary>
        public Tassets TASSETS { get; set; }
    }
}
