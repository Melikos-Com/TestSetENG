using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class UseMaterialResultApiViewModel
    {
        public UseMaterialResultApiViewModel() { }


        public UseMaterialResultApiViewModel(Tvupart data)
        {
            this.Unit = data.Unit;
            this.Sn = data.Sn;
            this.AssetCd = data.AssetCd;
            this.CompCd = data.CompCd;
            this.PartNo = data.PartNo;
            this.PartSpec = data.PartSpec;
            this.Price = data.Price;
            this.Qty = data.Qty;
            this.Seq = data.Seq;
            this.StoreCd = data.StoreCd;
            this.StoreName = data.StoreName;
            this.UseDate = data.UseDate;
            this.PartName = data.PartName;
        }
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
        /// 品名
        /// </summary>
        public string PartName { get; set; }
    }
}