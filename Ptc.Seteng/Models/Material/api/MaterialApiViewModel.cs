using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class MaterialApiViewModel : PagingViewModel
    {

        public MaterialApiViewModel() { }

        /// <summary>
        /// 公司代號
        /// </summary>
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("公司代號")]
        [DisplayFormat(NullDisplayText = "公司代號")]
        public string CompCd { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        [Avatar("Asset_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("設備代號")]
        [DisplayFormat(NullDisplayText = "設備代號")]
        public string AssetCd { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        [Avatar("Vender_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("廠商代號")]
        [DisplayFormat(NullDisplayText = "廠商代號")]
        public string VenderCd { get; set; }
        /// <summary>
        /// 材料代號
        /// </summary>
        [Avatar("Part_No",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("材料代號")]
        [DisplayFormat(NullDisplayText = "材料代號")]
        public string PartNo { get; set; }

    }
}