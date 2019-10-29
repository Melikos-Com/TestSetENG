using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum CallKind
    {
        /// <summary>
        /// 叫修
        /// </summary>
        [Description("叫修")]
        repair = 0,
        /// <summary>
        /// 保養
        /// </summary>
        [Description("保養")]
        maintain = 1,
        /// <summary>
        /// 購料
        /// </summary>
        [Description("購料")]
        materials = 2,
        /// <summary>
        /// 巡檢
        /// </summary>
        [Description("巡檢")]
        Inspection = 3


    }
}
