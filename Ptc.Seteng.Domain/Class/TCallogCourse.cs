using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class TCallogCourse
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }

        /// <summary>
        /// 叫修編號
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 指派人
        /// </summary>
        public string Assignor { get; set; }
        /// <summary>
        /// 受理人
        /// </summary>
        public string Admissibility { get; set; }
        /// <summary>
        /// 時間
        /// </summary>
        public DateTime Datetime { get; set; }

        
    }
}
