using MvcPaging;
using Ptc.Seteng;
using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class AuthSetViewModel 
    {
        public AuthSetViewModel()
        {
          
        }

        #region 查詢條件

        /// <summary>
        /// 角色名稱
        /// </summary>
        [Avatar("Role_Name",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("角色名稱")]
        public string RoleName { get; set; }

        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("公司代號")]
        public string CompCd { get; set; }

        #endregion



        /// <summary>
        ///顯示明細
        /// </summary>
        public IPagedList<RoleAuthViewModel> RoleAuthList { get; set; }
    }
}