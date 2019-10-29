using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Workflow
{
    public enum FlowType
    {
        /// <summary>
        /// 叫修流程
        /// </summary>
        Caller = 0 ,

        /// <summary>
        /// 新開店流程
        /// </summary>
        NewStore = 1,

        /// <summary>
        /// 其他流程
        /// </summary>
        Others = 2,

    }
}
