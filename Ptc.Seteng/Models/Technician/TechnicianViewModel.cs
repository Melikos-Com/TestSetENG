using Ptc.AspnetMvc.Authentication;
using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianViewModel
    {

        public TechnicianViewModel() { }

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
        /// <summary>
        /// 技師姓名
        /// </summary>
        [Avatar("Name",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("技師姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [Avatar("Account",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("帳號")]
        public string Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Avatar("Password",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("密碼")]
        public string Password { get; set; }
        /// <summary>
        /// 帳號啟用
        /// </summary>
        [Avatar("Enable",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("帳號啟用")]
        public Boolean? Enable { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [DisplayName("公司名稱")]
        public string CompName { get; set; }
        /// <summary>
        /// 廠商名稱
        /// </summary>
        [DisplayName("廠商名稱")]
        public string VendorName { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        [Avatar("IsVendor",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("角色")]
        public Boolean? IsVendor { get; set; }
    }
}
