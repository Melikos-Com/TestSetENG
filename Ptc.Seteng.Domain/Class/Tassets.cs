using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 設備主檔
    /// </summary>
    public class Tassets
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 設備編號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 設備大
        /// </summary>
        public string AstKind1 { get; set; }
        /// <summary>
        /// 設備中
        /// </summary>
        public string AstKind2 { get; set; }
        /// <summary>
        /// 設備小
        /// </summary>
        public string AstKind3 { get; set; }
        /// <summary>
        /// 是否叫修
        /// </summary>
        public Boolean CallSts { get; set; }
        /// <summary>
        /// 是否包月
        /// </summary>
        public Boolean UpkeepSts { get; set; }
        /// <summary>
        /// 包月費用
        /// </summary>
        public decimal MtnCharges { get; set; }
     
        /// <summary>
        /// 緊急時效相關
        /// </summary>
        public Maintain EmcMtn { get; set; }

        /// <summary>
        /// 一般時效相關
        /// </summary>
        public Maintain UsualMtn { get; set; }

        /// <summary>
        /// 保留時效相關
        /// </summary>
        public Maintain[] Mtns { get; set; }
        /// <summary>
        /// 已使用設備序號
        /// </summary>
        public int AssetUseSeq { get; set; }
        /// <summary>
        /// 結束使用日
        /// </summary>
        public DateTime CloseDate { get; set; }
        /// <summary>
        /// 新增人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 新增日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }
        /// <summary>
        /// TODO需釐清此欄位
        /// </summary>
        public Boolean IsUsePart { get; set; }
        /// <summary>
        /// TODO需釐清此欄位
        /// </summary>
        public Boolean IsWarranty { get; set; }
        /// <summary>
        /// 計算設備保固期限,啟月年月+保固到期日
        /// </summary>
        public decimal WarrantyMonth { get; set; }
        /// <summary>
        /// 底下的故障原因
        /// </summary>
        public IEnumerable<Tdamage> TDAMAGE { get; set; }
        /// <summary>
        /// 底下的單位設備
        /// </summary>
        public IEnumerable<Tstrast> TSTRAST { get; set; }
        /// <summary>
        /// 底下的零件
        /// </summary>
        public IEnumerable<Tvprice> TVPRICE { get; set; }
        /// <summary>
        /// 折扣類別
        /// </summary>
        public string Debit_Kind { get; set; }
    }
}
