using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum StoreType
    {
        /// <summary>
        /// 直營店
        /// </summary>
        [Description("直營店")]
        Guide = 0,

        /// <summary>
        /// 加盟店
        /// </summary>
        [Description("加盟店")]
        Franchise = 1

    }
}
