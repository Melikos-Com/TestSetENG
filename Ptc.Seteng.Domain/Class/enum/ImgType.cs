using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ptc.Seteng
{
    public enum ImgType
    {
        /// <summary>
        /// 修理前
        /// </summary>
        [Description("修理前")]
        BeforeFix = 1,

        /// <summary>
        /// 修理後
        /// </summary>
        [Description("修理後")]
        AfterFix = 2,

        /// <summary>
        /// 工單或店章
        /// </summary>
        [Description("工單或店章")]
        Workorder = 3,
        /// <summary>
        /// 門市簽名
        /// </summary>
        [Description("門市簽名")]
        Signature = 4,
    }
}
