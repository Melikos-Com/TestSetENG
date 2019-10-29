using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Models
{
    public class MemberSetSearchViewModel
    {
        public MemberSetSearchViewModel()
        {

        }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [DisplayName("使用者帳號")]
        [DisplayFormat(NullDisplayText = "使用者帳號")]
        [Avatar("User_Id",
                System.Linq.Expressions.ExpressionType.Parameter,
                AppendPredicateWith.And)]
        public string UserName { get; set; }
        /// <summary>
        /// 使用者姓名
        /// </summary>
        [DisplayName("使用者姓名")]
        [DisplayFormat(NullDisplayText = "使用者姓名")]
        [Avatar("User_Name",
               System.Linq.Expressions.ExpressionType.Parameter,
               AppendPredicateWith.And)]
        public string CorpUserName { get; set; }

        /// <summary>
        /// 角色權限
        /// </summary>
        [DisplayName("角色編號")]
        [DisplayFormat(NullDisplayText = "角色編號")]
        [Avatar("Role_Id",
               System.Linq.Expressions.ExpressionType.Parameter,
               AppendPredicateWith.And)]
        public string RoleId { get; set; }

        public  List<SelectListItem> RoleIdltem { get; set; }
        public RoleType rt { get; set; }

        [Avatar("Comp_Cd",
         System.Linq.Expressions.ExpressionType.Parameter,
         AppendPredicateWith.And)]
        [DisplayName("公司代號")]
        public string CompCd { get; set; }
    }
}