using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianApiViewModel:PagingViewModel
    {
        public TechnicianApiViewModel() { }

        /// <summary>
        /// 密碼
        /// </summary>
        [Avatar("Password",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("密碼")]
        public string Password { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [Avatar("Account",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("帳號")]
        public string Account { get; set; }

    }
}