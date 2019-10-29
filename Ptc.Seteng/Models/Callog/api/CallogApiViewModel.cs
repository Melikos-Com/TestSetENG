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

    public class CallogApiViewModel : PagingViewModel
    {

        public CallogApiViewModel() { }

        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }

        /// <summary>
        /// 叫修編號
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VenderCd { get; set; }

        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }

        /// <summary>
        /// 立案時間(開始)
        /// </summary>
        public DateTime? FiDateStart { get; set; }
        /// <summary>
        /// 立案時間(結束)
        /// </summary>
        public DateTime? FiDateEnd { get; set; }
        /// <summary>
        /// 門市名稱
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 案件狀態
        /// </summary>
        public int CloseSts { get; set; }
        /// <summary>
        /// 緊急程度
        /// </summary>
        public string CallLevel { get; set; }
         
        /// <summary>
        /// 案件狀態
        /// </summary>
        public TimePoint? Timepoint { get; set; }

        /// <summary>
        /// 催修時間
        /// </summary>
        public string RcvDate { get; set; }
    }



}