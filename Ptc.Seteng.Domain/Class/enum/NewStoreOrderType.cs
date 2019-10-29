using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 新開店-單號狀態
    /// </summary>
    public enum NewStoreOrderType
    {
        /// <summary>
        /// 主檔建立完畢
        /// </summary>
        [Description("主档建立完毕")]
        Created = 0,
        /// <summary>
        /// 預算審核完畢
        /// </summary>
        [Description("预算审核完毕")]
        Budget = 1,
        /// <summary>
        /// 決算審核完畢
        /// </summary>
        [Description("决算审核完毕")]
        Decide = 2,

    }
}
