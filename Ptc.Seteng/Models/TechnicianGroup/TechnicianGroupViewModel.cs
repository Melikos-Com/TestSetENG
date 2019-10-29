using Ptc.AspnetMvc.Authentication;
using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class TechnicianGroupViewModel
    {

        public TechnicianGroupViewModel() { }

        /// <summary>
        /// 公司代號
        /// </summary>
        [Avatar("CompCd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        public string CompCd { get; set; }

        /// <summary>
        /// 廠商代號
        /// </summary>
        [Avatar("VendorCd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        public string VendorCd { get; set; }

        /// <summary>
        /// 技師群組
        /// </summary>
        [Avatar("GroupName",
        System.Linq.Expressions.ExpressionType.Parameter,
               AppendPredicateWith.And)]
        [DisplayName("群組名稱")]
        public string GroupName { get; set; }

        /// <summary>
        /// 公司代號(中文)
        /// </summary>
        [DisplayName("公司名稱")]
        public string CompName { get; set; }

        /// <summary>
        /// 廠商編號(中文)
        /// </summary>
        [DisplayName("廠商名稱")]
        public string VendorName { get; set; }

        /// <summary>
        /// 負責區域(中文)
        /// </summary>
        [DisplayName("負責區域")]
        public string ResponsibleZo { get; set; }

        /// <summary>
        /// 負責課別(中文)
        /// </summary>
        [DisplayName("負責課別")]
        public string ResponsibleDZo { get; set; }

        /// <summary>
        /// 操作模式
        /// </summary>
        public AuthNodeType ActionType { get; set; }

    }
}
