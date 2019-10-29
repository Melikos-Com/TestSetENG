using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class Twrkknd
    {
        public Twrkknd()
        {

        }
        public Twrkknd(Twrkknd data)
        {
            this.CompCd = data.CompCd;
            this.WorkId = data.WorkId;
            this.WorkDesc = data.WorkDesc;
            this.Worksts = data.Worksts;
        }
        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 類別代碼
        /// </summary>
        public string WorkId { get; set; }
        /// <summary>
        /// 類別內容
        /// </summary>
        public string WorkDesc { get; set; }
        /// <summary>
        /// 狀態碼
        /// </summary>
        public int Worksts { get; set; }
    }
}
