using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum PenaltyType
    {
        /// <summary>
        /// 計時
        /// </summary>
        [Description("計時")]
        Timing = 0,

        /// <summary>
        /// 計次
        [Description("計次")]
        Metering = 1,


    }
}
