using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum HQCheckKind
    {

        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        WaitCheck = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Passed = 1,

        /// <summary>
        /// 退回
        /// </summary>
        [Description("退回")]
        Retreat = 2

    }
}

