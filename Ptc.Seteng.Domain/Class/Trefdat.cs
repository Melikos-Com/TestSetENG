using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 參數檔
    /// </summary>
    public class Trefdat
    {
        /// <summary>
        /// LABEL
        /// </summary>
        public string LabelCd { get; set; }
        /// <summary>
        /// KEY VALUE
        /// </summary>
        public string KeyCd { get; set; }
        /// <summary>
        /// 資料
        /// </summary>
        public string RefData { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string RefNote { get; set; }
    }
}
