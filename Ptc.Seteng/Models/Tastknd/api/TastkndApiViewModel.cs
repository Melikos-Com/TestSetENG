using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TastkndApiViewModel : PagingViewModel
    {
        public TastkndApiViewModel()
        {

        }

        /// <summary>
        /// 公司代號
        /// </summary>   
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string CompCd{ get; set; }

        /// <summary>
        /// 設備分類代號
        /// </summary>
        [Avatar("Kind_Cd",
                System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string KindCd { get; set; }

        /// <summary>
        /// 上層分類代號
        /// </summary>
        [Avatar("Parent_Id",
                System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string ParentId { get; set; }

        /// <summary>
        /// 分類階層等級
        /// </summary>
        [Avatar("Type_Id",
                System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public AssetKind TypeId { get; set; }


    }
}