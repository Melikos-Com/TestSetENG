using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum ToiletType
    {
        /// <summary>
        /// 無
        /// </summary>
        [Description("無")]
        none = 0,

        /// <summary>
        /// 共用
        /// </summary>
        [Description("共用")]
        shared = 1,

        /// <summary>
        /// 女廁
        /// </summary>
        [Description("女廁")]
        Female = 2,

    }
}
