using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 檔案型態
    /// </summary>
    public enum FileType
    {

        /// <summary>
        /// 大頭照
        /// </summary>
        [Description("大頭照")]
        Sticker = 0,

        /// <summary>
        /// 證件照
        /// </summary>
        [Description("證件照")]

        Technician = 1,

        /// <summary>
        /// 技能證書
        /// </summary>
        [Description("技能證書")]

        License = 2,
    }
}
