using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CallogCountApiViewModel
    {

        #region Constructor

        public CallogCountApiViewModel() { }


        public CallogCountApiViewModel(int hasTechnicianCount,
                                       int awaitAdoptCount,
                                       int awaitAssignCount)
        {

            this.HasTechnicianCount = hasTechnicianCount;
            this.AwaitAdoptCount = awaitAdoptCount;
            this.AwaitAssignCount = awaitAssignCount;

        }

        public CallogCountApiViewModel(Tuple<int, int, int> member)
        {
            this.HasTechnicianCount = member.Item1;
            this.AwaitAdoptCount = member.Item2;
            this.AwaitAssignCount = member.Item3;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 待銷案數
        /// </summary>
        public int HasTechnicianCount { get; set; }

        /// <summary>
        /// 待認養數
        /// </summary>
        public int AwaitAdoptCount { get; set; }

        /// <summary>
        /// 待派工數
        /// </summary>
        public int AwaitAssignCount { get; set; }

        #endregion
    }
}