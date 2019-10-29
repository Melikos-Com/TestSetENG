using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum CancelCaseType
    {
        /// <summary>
        /// 一般案件
        /// </summary>
        [Description("一般案件")]
        General = 0,

        /// <summary>
        /// CC誤叫修
        /// </summary>
        [Description("CC誤叫修")]
        CC = 1 ,

        /// <summary>
        /// 門市誤叫修
        /// </summary>
        [Description("門市誤叫修")]
        Store = 2,
        
    }
}
