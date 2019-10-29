using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TstrmstApiViewModel : PagingViewModel
    {
        /// <summary>
        /// 公司代號
        /// </summary>   
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string CompCd { get; set; }

        /// <summary>
        /// 區域代號
        /// </summary>   
        [Avatar("Zo_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string ZoCd { get; set; }

        /// <summary>
        /// 門市代號
        /// </summary>   
        [Avatar("Store_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string StoreCd { get; set; }
    }
}