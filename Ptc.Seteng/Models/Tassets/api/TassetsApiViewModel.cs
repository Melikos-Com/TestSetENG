using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TassetsApiViewModel : PagingViewModel
    {
        /// <summary>
        /// 公司編號
        /// </summary>
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
               AppendPredicateWith.And)]
        public string CompCd { get; set; }
        /// <summary>
        /// 設備分類大
        /// </summary>
        [Avatar("Ast_Kind1",
        System.Linq.Expressions.ExpressionType.Equal,
              AppendPredicateWith.And)]
        public string AstKind1 { get; set; }
        /// <summary>
        /// 設備分類中
        /// </summary>
        [Avatar("Ast_Kind2",
        System.Linq.Expressions.ExpressionType.Equal,
              AppendPredicateWith.And)]
        public string AstKind2 { get; set; }
        /// <summary>
        /// 設備分類小
        /// </summary>
        [Avatar("Ast_Kind3",
        System.Linq.Expressions.ExpressionType.Equal,
             AppendPredicateWith.And)]
        public string AstKind3 { get; set; }

        /// <summary>
        /// 設備分類小
        /// </summary>
        [Avatar("Asset_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
             AppendPredicateWith.And)]
        public string AssetCd { get; set; }

    }
}