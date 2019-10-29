using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ptc.Seteng
{
    public enum TimePoint
    {
        /// <summary>
        /// 立案
        /// </summary>
        [Description("立案")]
        Create = 0,
        /// <summary>
        /// 派工
        /// </summary>
        [Description("派工")]
        Dispatch = 1,
        /// <summary>
        /// 受理
        /// </summary>
        [Description("受理")]
        Accepted = 2,
        /// <summary>
        /// 到店
        /// </summary>
        [Description("到店")]
        ArriveStore = 3,
        /// <summary>
        /// 銷案
        /// </summary>
        [Description("銷案")]
        Finish = 4
    }
}
