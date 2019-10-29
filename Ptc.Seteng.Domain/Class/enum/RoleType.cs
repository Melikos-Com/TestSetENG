using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public enum RoleType
    {

        /// <summary>
        /// 系統管理者
        /// </summary>
        [Description("系統管理者")]
        Admin  =  0,
        /// <summary>
        /// 審核人員
        /// </summary>
        [Description("審核人員")]
        TeamLeader = 1,
        /// <summary>
        /// 區域人員
        /// </summary>
        [Description("區域人員")]
        Admin_Zo = 2,
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
        Vender = 5,
        /// <summary>
        /// 咖啡保修商
        /// </summary>
        [Description("咖啡保修商")]
        CafeVender = 6,
        /// <summary>
        /// APP廠商(臨時建，日後不會用)
        /// </summary>
        [Description("APP廠商")]
        APPVENDER = 7
    }
}
