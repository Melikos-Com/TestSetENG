using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{

    /// <summary>
    /// 維護相關
    /// </summary>
    public class Maintain
    {

        public Maintain() { }


        public Maintain(short limit, decimal amt, decimal penalty) {

            this.Limit = limit;
            this.Amt = amt;
            this.Penalty = penalty;

        }

        /// <summary>
        /// 保留時效
        /// </summary>
        public short Limit { get; set; }

        /// <summary>
        /// 人工費用
        /// </summary>
        public decimal Amt { get; set; }

        /// <summary>
        /// 違約金
        /// </summary>
        public decimal Penalty { get; set; }

    }
}
