using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum CloseSts
    {

        /// <summary>
        /// 未銷
        /// </summary>
        [Description("未銷")]
        process = 0,

        /// <summary>
        /// 手機銷案
        /// </summary>
        [Description("手機銷案")]
        Phone = 1,

        /// <summary>
        /// 網頁銷案
        /// </summary>
        [Description("網頁銷案")]
        Web = 2,

        /// <summary>
        /// CC銷案
        /// </summary>
        [Description("CC銷案")]
        CC = 3,

        /// <summary>
        /// 手機+網頁銷案
        /// </summary>
        [Description("手機+網頁銷案")]
        PhoneAndWeb = 4

    }
}
