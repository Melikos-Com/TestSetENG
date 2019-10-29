using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TassetsViewModel
    {
        public TassetsViewModel() { }

        /// <summary>
        /// 公司代號
        /// </summary>
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
               AppendPredicateWith.And)]
        public string Company { get; set; }

        /// <summary>
        /// 大分類代號
        /// </summary>
        [Avatar("Ast_Kind1",
        System.Linq.Expressions.ExpressionType.Equal,
               AppendPredicateWith.And)]
        public string AstKind1 { get; set; }

        /// <summary>
        /// 中分類代號
        /// </summary>
        [Avatar("Ast_Kind2",
        System.Linq.Expressions.ExpressionType.Equal,
               AppendPredicateWith.And)]
        public string AstKind2 { get; set; }

        /// <summary>
        /// 小分類代號
        /// </summary>
        [Avatar("Ast_Kind3",
        System.Linq.Expressions.ExpressionType.Equal,
               AppendPredicateWith.And)]
        public string AstKind3 { get; set; }

        [Avatar("Asset_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
             AppendPredicateWith.And)]
        public string AssetCd { get; set; }
    }
}