using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TsysrolApiViewModel : PagingViewModel
    {

        public TsysrolApiViewModel() { }
        /// <summary>
        /// 公司代號
        /// </summary>   
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string CompCd { get; set; }

        /// <summary>
        /// 公司代號
        /// </summary>   
        [Avatar("Role_Id",
        System.Linq.Expressions.ExpressionType.Equal,
                AppendPredicateWith.And)]
        public string RoleId { get; set; }

       

    }
}