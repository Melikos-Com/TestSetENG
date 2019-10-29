using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Workflow
{
    public interface ILog
    {
        /// <summary>
        /// 編號
        /// </summary>
        string Sn { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        string CompCd { get; set; }
        /// <summary>
        /// 準備下一個流程
        /// </summary>
        int NextSts { get; set; }
        /// <summary>
        /// 流程註記
        /// </summary>
        FlowType FlowType { get; set; }

    }
}
