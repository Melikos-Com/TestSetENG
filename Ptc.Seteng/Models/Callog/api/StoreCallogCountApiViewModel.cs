using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class StoreCallogCountApiViewModel
    {

        #region Constructor

        public StoreCallogCountApiViewModel() { }


        public StoreCallogCountApiViewModel(int FMCount,
                                       int TLCount,
                                       int NewCount)
        {

            this.FMCount = FMCount;
            this.TLCount = TLCount;
            this.NewCount = NewCount;
            this.StoreSearch = 0;
            this.CompanySearch = 0;

        }

        public StoreCallogCountApiViewModel(Tuple<int, int, int, int, int> member)
        {
            this.FMCount = member.Item1;
            this.TLCount = member.Item2;
            this.NewCount = member.Item3;
            this.StoreSearch = member.Item4;
            this.CompanySearch = member.Item5;
        }

        public StoreCallogCountApiViewModel(Tuple<int, int, int, int, int, int> member)
        {
            this.CallogUrgentTimeoutCount = member.Item1;
            this.CallogUrgentNotTimeoutCount = member.Item2;
            this.CallogNormalTimeoutCount = member.Item3;
            this.CallogNormalNotTimeoutCount = member.Item4;
            this.NewStoreBudgetTimeoutCount = member.Item5;
            this.NewStoreDecideTimeoutCount = member.Item6;
        }

        public StoreCallogCountApiViewModel(Tuple<int, int, int, int> member)
        {
            this.CallogUrgentTimeoutCount = member.Item1;
            this.CallogUrgentNotTimeoutCount = member.Item2;
            this.CallogNormalTimeoutCount = member.Item3;
            this.CallogNormalNotTimeoutCount = member.Item4;

        }

        public StoreCallogCountApiViewModel(Tuple<int, int> member)
        {
            this.NewStoreBudgetTimeoutCount = member.Item1;
            this.NewStoreDecideTimeoutCount = member.Item2;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 待銷案數
        /// </summary>
        public int FMCount { get; set; }

        /// <summary>
        /// 待認養數
        /// </summary>
        public int TLCount { get; set; }

        /// <summary>
        /// 待派工數
        /// </summary>
        public int NewCount { get; set; }

        /// <summary>
        /// 待派工數
        /// </summary>
        public int StoreSearch { get; set; }

        /// <summary>
        /// 待派工數
        /// </summary>
        public int CompanySearch { get; set; }

        /// <summary>
        /// 緊急逾時未完修案件數
        /// </summary>
        public int CallogUrgentTimeoutCount { get; set; }

        /// <summary>
        /// 緊急未完修案件數
        /// </summary>
        public int CallogUrgentNotTimeoutCount { get; set; }

        /// <summary>
        /// 一般逾時未完修案件數
        /// </summary>
        public int CallogNormalTimeoutCount { get; set; }

        /// <summary>
        /// 一般未完修案件數
        /// </summary>
        public int CallogNormalNotTimeoutCount { get; set; }

        /// <summary>
        /// 預算逾時新開店數
        /// </summary>
        public int NewStoreBudgetTimeoutCount { get; set; }

        /// <summary>
        /// 決算逾時新開店數
        /// </summary>
        public int NewStoreDecideTimeoutCount { get; set; }

        #endregion
    }
}