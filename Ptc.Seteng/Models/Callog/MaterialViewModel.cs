using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class MaterialViewModel
    {

        public MaterialViewModel() { }

        /// <summary>
        /// 公司編號
        /// </summary>
        [DisplayName("公司編號")]
        [DisplayFormat(NullDisplayText = "公司編號")]
        public string CompCd { get; set; }

        /// <summary>
        /// 叫修編號
        /// </summary>
        [DisplayName("叫修編號")]
        [DisplayFormat(NullDisplayText = "叫修編號")]
        public string Sn { get; set; }

        /// <summary>
        /// 材料序號
        /// </summary>
        public string Seq { get; set; }

        /// <summary>
        /// 門市
        /// </summary>
        [DisplayName("門市")]
        [DisplayFormat(NullDisplayText = "門市")]
        public string StoreCd { get; set; }

        /// <summary>
        /// 設備
        /// </summary>
        [DisplayName("設備")]
        [DisplayFormat(NullDisplayText = "設備")]
        public string AssetCd { get; set; }

        /// <summary>
        /// 零件
        /// </summary>
        [DisplayName("零件")]
        [DisplayFormat(NullDisplayText = "零件")]
        public string PartNo { get; set; }

        /// <summary>
        /// 用料日期
        /// </summary>
        [DisplayName("用料日期")]
        [DisplayFormat(NullDisplayText = "用料日期")]
        public DateTime UseDate { get; set; } = DateTime.Now;

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
        /// 單價
        /// </summary>
        [DisplayName("單價")]
        [DisplayFormat(NullDisplayText = "單價")]
        public decimal Price { get; set; }

        /// <summary>
        /// 操作模式
        /// </summary>
        public AuthNodeType AuthNodeType { get; set; }
    }
}