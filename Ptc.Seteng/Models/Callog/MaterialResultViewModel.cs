using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class MaterialResultViewModel
    {
        public MaterialResultViewModel()
        {

        }

        public MaterialResultViewModel(Tvupart data)
        {

            this.Seq = data.Seq.ToString();
            this.PartNo = data.PartNo;
            this.Price = data.Price;
            this.Qty = data.Qty;
            this.Spec = data.PartSpec;
            this.Unit = data.Unit;
            this.UseDate = data.UseDate;
            this.PreCost = (data.Qty * data.Price);

            this.colData = new string[] {
                this.Seq,
                this.UseDate.ToString("yyyy/MM/dd"),
                this.PartNo,
                this.Spec,
                this.Unit,
                this.Qty.ToString(),
                this.Price.ToString(),
                this.PreCost.ToString()
            };
        }

        /// <summary>
        /// 材料序號
        /// </summary>
        [DisplayName("材料序號")]
        [DisplayFormat(NullDisplayText = "材料序號")]
        public string Seq { get; set; }
        /// <summary>
        /// 材料編號
        /// </summary>
        [DisplayName("材料編號")]
        [DisplayFormat(NullDisplayText = "材料編號")]
        public string PartNo { get; set; }
        /// <summary>
        /// 使用日期
        /// </summary>
        [DisplayName("使用日期")]
        [DisplayFormat(NullDisplayText = "使用日期")]
        public DateTime UseDate { get; set; }
        /// <summary>
        /// 規格
        /// </summary>
        [DisplayName("規格")]
        [DisplayFormat(NullDisplayText = "規格")]
        public string Spec { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        [DisplayName("單位")]
        [DisplayFormat(NullDisplayText = "單位")]
        public string Unit { get; set; }
        /// <summary>
        /// 數量
        /// </summary>
        [DisplayName("數量")]
        [DisplayFormat(NullDisplayText = "數量")]
        public decimal Qty { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        [DisplayName("金額")]
        [DisplayFormat(NullDisplayText = "金額")]
        public decimal Price { get; set; }
        /// <summary>
        /// 小計
        /// </summary>
        [DisplayName("小計")]
        [DisplayFormat(NullDisplayText = "小計")]
        public decimal PreCost { get; set; }

        /// <summary>
        /// 材料清單
        /// </summary>
        public String[] colData { get; set; }
    }
}