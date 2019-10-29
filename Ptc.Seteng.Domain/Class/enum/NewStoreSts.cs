using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{

    /// <summary>
    /// 新開店-案件狀態
    /// </summary>
    [Flags]
    public enum NewStoreSts
    {

        /// <summary>
        /// 立案
        /// </summary>
        [Description("立案")]
        Created = 0,
        /// <summary>
        /// 廠商預算填寫完
        /// </summary>
        [Description("厂商预算填写")]
        VenderBudget = 1,
        /// <summary>
        /// CM預算填寫完
        /// </summary>
        [Description("CM预算审核")]
        CMBudget = 2,
        /// <summary>
        /// TL預算填寫完
        /// </summary>
        [Description("TL预算审核")]
        TLBudget =  3,
        /// <summary>
        /// 廠商決算填寫完
        /// </summary>
        [Description("厂商决算填写")]
        VenderDecide = 4,
        /// <summary>
        /// FM決算填寫完
        /// </summary>
        [Description("FM决算审核")]
        FMDecide = 5,
        /// <summary>
        /// TL決算填寫完
        /// </summary>
        [Description("TL决算审核")]
        TLDecide = 6,
        /// <summary>
        /// 結算
        /// </summary>
        [Description("結算")]
        Billing = 9,
    }
}
