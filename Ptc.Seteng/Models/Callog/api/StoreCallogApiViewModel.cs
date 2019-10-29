using Ptc.Seteng.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TypeLite;

namespace Ptc.Seteng.Models
{

    public class StoreCallogApiViewModel : PagingViewModel
    {

        public StoreCallogApiViewModel() { }

        /// <summary>
        /// 公司別
        /// </summary>
        [Avatar("Comp_Cd",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("公司別")]
        [DisplayFormat(NullDisplayText = "公司別")]
        public string CompCd { get; set; }
         
        public string VenderCd { get; set; }

        [Avatar("Store_Cd",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("門市代號")]
        [DisplayFormat(NullDisplayText = "門市代號")]
        public string StoreCd { get; set; }

        /// <summary>
        /// 案件TL負責人員
        /// </summary>
        [Avatar("AstEng_Leader",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("案件TL負責人員")]
        [DisplayFormat(NullDisplayText = "案件TL負責人員")]
        public string AstEngLeader { get; set; }
        /// <summary>
        /// 案件FM負責人員
        /// </summary>
        [Avatar("Zo_Manager",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("案件FM負責人員")]
        [DisplayFormat(NullDisplayText = "案件FM負責人員")]
        public string ZoManager { get; set; }


        /// <summary>
        /// 立案日(開始)
        /// </summary>
        [Avatar("Rcv_Datetime",
        System.Linq.Expressions.ExpressionType.GreaterThanOrEqual,
        AppendPredicateWith.And)]
        [DisplayName("受理日(開始)")]
        [DisplayFormat(NullDisplayText = "受理日(開始)")]
        public DateTime? RcvDateStart { get; set; }
        /// <summary>
        /// 立案日(結束)
        /// </summary>
        [Avatar("Rcv_Datetime",
        System.Linq.Expressions.ExpressionType.LessThan,
        AppendPredicateWith.And)]
        [DisplayName("受理日(結束)")]
        [DisplayFormat(NullDisplayText = "受理日(結束)")]
        public DateTime? RcvDateEnd { get; set; }
        /// <summary>
        /// 立案日(開始)
        /// </summary>
        [Avatar("Create_Date",
        System.Linq.Expressions.ExpressionType.GreaterThanOrEqual,
        AppendPredicateWith.And)]
        [DisplayName("立案日(開始)")]
        [DisplayFormat(NullDisplayText = "立案日(開始)")]
        public DateTime? CreateDateStart { get; set; }
        /// <summary>
        /// 立案日(結束)
        /// </summary>
        [Avatar("Create_Date",
        System.Linq.Expressions.ExpressionType.LessThan,
        AppendPredicateWith.And)]
        [DisplayName("立案日(結束)")]
        [DisplayFormat(NullDisplayText = "立案日(結束)")]
        public DateTime? CreateDateEnd { get; set; }
        /// <summary>
        /// 廠商銷案日(開始)
        /// </summary>
        [Avatar("Vnd_End_Date",
        System.Linq.Expressions.ExpressionType.GreaterThanOrEqual,
        AppendPredicateWith.And)]
        [DisplayName("廠商銷案日(開始)")]
        [DisplayFormat(NullDisplayText = "廠商銷案日(開始)")]
        public DateTime? VndEndDateStart { get; set; }
        /// <summary>
        /// 廠商銷案日(結束)
        /// </summary>
        [Avatar("Vnd_End_Date",
        System.Linq.Expressions.ExpressionType.LessThan,
        AppendPredicateWith.And)]
        [DisplayName("廠商銷案日(結束)")]
        [DisplayFormat(NullDisplayText = "廠商銷案日(結束)")]
        public DateTime? VndEndDateEnd { get; set; }
        /// <summary>
        /// 門市名稱
        /// </summary>
        [Avatar("Store_Name",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("門市名稱")]
        [DisplayFormat(NullDisplayText = "門市名稱")]
        public string StoreName { get; set; }
        /// <summary>
        /// 廠商名稱
        /// </summary>
        [Avatar("Vender_Short",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("廠商名稱")]
        [DisplayFormat(NullDisplayText = "廠商名稱")]
        public string VenderShort { get; set; }
        /// 設備名稱
        /// </summary>
        [Avatar("Asset_Name",
        System.Linq.Expressions.ExpressionType.Parameter,
        AppendPredicateWith.And)]
        [DisplayName("設備名稱")]
        [DisplayFormat(NullDisplayText = "設備名稱")]
        public string AssetName { get; set; }
        /// <summary>
        /// 案件類型
        /// </summary>
        [Avatar("Call_Kind",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("案件類型")]
        public CallKind? CallKind { get; set; }
        /// <summary>
        /// 案件狀態
        /// </summary>
        [Avatar("Call_Sts",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("案件狀態")]
        [DisplayFormat(NullDisplayText = "案件狀態")]
        public CloseSts? CloseSts { get; set; }
        /// <summary>
        /// 緊急程度
        /// </summary>
        [Avatar("Call_Level",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("緊急程度")]
        public CallLevel? CallLevel { get; set; } 
        /// <summary>
        /// 是否逾時
        /// </summary>
        public Boolean? IsTimeout { get; set; }
        /// <summary>
        /// 是否日期逾時
        /// </summary>
        public Boolean? IsDateTimeout { get; set; }
        /// <summary>
        /// 是否誤叫修
        /// </summary>  
        [Avatar("Cancel_Case_Type",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("是否誤叫修")]
        public Boolean? IsCancelCaseType { get; set; }
        /// <summary>
        /// 是否已經廠商結案
        /// </summary>
        public Boolean? IsVndEnd { get; set; }


        /// <summary>
        /// 誤叫修
        /// </summary>  
        [Avatar("Cancel_Case_Type",
        System.Linq.Expressions.ExpressionType.Equal,
        AppendPredicateWith.And)]
        [DisplayName("誤叫修")]
        public CancelCaseType? CancelCaseType { get; set; }
        /// <summary>
        /// 區域
        /// </summary>
        [DisplayName("區域")]
        public string Zo { get; set; }
        /// <summary>
        /// 是否已經門市結案
        /// </summary>
        public Boolean? IsStoreEnd { get; set; }
        /// <summary>
        /// 是否已經審核結案(FM)
        /// </summary>
        public Boolean? IsCfmEnd { get; set; }
    }



}