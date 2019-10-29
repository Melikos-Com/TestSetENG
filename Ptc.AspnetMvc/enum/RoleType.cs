using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc
{
    public enum RoleType
    {

        /// <summary>
        /// 系統管理者
        /// </summary>
        [Description("系統管理者")]
        Admin = 0,
        /// <summary>
        /// 審核人員
        /// </summary>
        [Description("審核人員")]
        TL = 1,
        /// <summary>
        /// 區域人員
        /// </summary>
        [Description("區域人員")]
        FM = 2,
        /// <summary>
        /// 新開店批價人員
        /// </summary>
        [Description("新開店批價人員")]
        CM = 3,
        /// <summary>
        /// 門市人員
        /// </summary>
        [Description("門市人員")]
        Store = 4,
        /// <summary>
        /// 廠商
        /// </summary>
        [Description("廠商")]
        Vender = 5

    }
}
