using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 使用零件檔
    /// </summary>
    public class Tvupart
    {
        /// <summary>
        /// 案件編號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 零件使用序號
        /// </summary>
        public byte Seq { get; set; }
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
        /// 用料日期
        /// </summary>
        public DateTime UseDate { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 零件編號
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// 零件規格
        /// </summary>
        public string PartSpec { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 零件價格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 零件名稱
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 資料來源
        /// </summary>
        public SourceFromType Source { get; set; }
        /// <summary>
        /// 所屬的案件
        /// </summary>
        public Tcallog TCALLOG { get; set; }

    }
}
