using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum PushType
    {
        /// <summary>
        /// 廠商
        /// </summary>
        [Description("廠商")]
        Vendor,
        /// <summary>
        /// 群組
        /// </summary>
        [Description("群組")]
        Group,
        /// <summary>
        /// 帳號
        /// </summary>
        [Description("帳號")]
        Accunt,

    }
}
