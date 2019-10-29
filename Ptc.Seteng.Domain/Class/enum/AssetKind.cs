using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum AssetKind
    {

        /// <summary>
        /// 大分類
        /// </summary>
        [Description("大分類")]
        LargeCategory = 1,

        /// <summary>
        /// 中分類
        /// </summary>
        [Description("中分類")]
        MiddleCategory = 2,

        /// <summary>
        /// 小分類
        /// </summary>
        [Description("小分類")]
        SmallCategory = 3

    }
}
