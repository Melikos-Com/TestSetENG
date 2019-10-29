using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TvenderApiViewModel : PagingViewModel
    {
        public TvenderApiViewModel() { }

        /// <summary>
        /// 公司代號
        /// </summary>   
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string CompCd { get; set; }

        /// <summary>
        /// 廠商代號
        /// </summary>   
        [Avatar("Vender_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string VenderCd { get; set; }

    }
}