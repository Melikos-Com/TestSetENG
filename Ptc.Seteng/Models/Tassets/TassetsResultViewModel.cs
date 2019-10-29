using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TassetsResultViewModel
    {
        public TassetsResultViewModel()
        {

        }

        public TassetsResultViewModel(Tassets data)
        {
            this.CompCd = data.CompCd;
            this.AssetCd = data.AssetCd;
            this.AssetName = data.AssetName;
            this.AstKind1 = data.AstKind1;
            this.AstKind2 = data.AstKind2;
            this.AstKind3 = data.AstKind3;

            colData = new String[] {
                this.CompCd,
                this.AssetCd,
                this.AssetName,
                this.AstKind1,
                this.AstKind2,
                this.AstKind3
            };

        }

        /// <summary>
        /// 公司編號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 設備分類大
        /// </summary>
        public string AstKind1 { get; set; }
        /// <summary>
        /// 設備分類中
        /// </summary>
        public string AstKind2 { get; set; }
        /// <summary>
        /// 設備分類小
        /// </summary>
        public string AstKind3 { get; set; }

        /// <summary>
        /// 準備回傳的
        /// </summary>
        public String[] colData { get; set; }

    }
}