using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CheckTechnicianViewModel
    {
        public CheckTechnicianViewModel() { }

        /// <summary>
        /// 厂商代号
        /// </summary>
        [Avatar("Vender_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("厂商名稱")]
        public string VenderCd { get; set; }
        /// <summary>
        /// 公司代号
        /// </summary>
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("公司名稱")]
        public string CompCd { get; set; }
        /// <summary>
        /// 技師姓名
        /// </summary>
        [Avatar("Name",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("技師姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 是否已经总部审核过了
        /// </summary>
        [Avatar("IsHQCheck",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("总部审核")]
        public int IsHQCheck { get; set; } 
        /// <summary>
        /// 是否总部审核了
        /// </summary>
        [Avatar("Account",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("账号")]
        public string Account { get; set; }
        ///// <summary>
        ///// 是否启用
        ///// </summary>
        //[Avatar("Enable",
        //System.Linq.Expressions.ExpressionType.Equal,
        //AppendPredicateWith.And)]
        //[DisplayName("技師名稱")]
        //public Boolean Enable { get; set; } ;

    }
}
