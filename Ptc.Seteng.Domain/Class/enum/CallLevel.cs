using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum CallLevel
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        general = 1,
        /// <summary>
        /// 緊急
        /// </summary>
        [Description("緊急")]
        warning = 2

    }
}
