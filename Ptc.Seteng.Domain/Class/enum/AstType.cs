using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum AstType
    {

        /// <summary>
        /// 保固內
        /// </summary>
        [Description("保固內")]
        InWarranty = 0,

        /// <summary>
        /// 過保固
        /// </summary>
        [Description("过保固")]
        OutWarranty = 1,

    }
}
