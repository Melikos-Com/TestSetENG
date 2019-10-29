using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class Tfinish
    {
        public Tfinish()
        {

        }
        public Tfinish(Tfinish data)
        {
            this.CompCd = data.CompCd;
            this.FinishId = data.FinishId;
            this.FinishName = data.FinishName;
            this.CountSts = data.CountSts;
            this.VenderSts = data.VenderSts;
            this.FinishSts = data.FinishSts;
        }
        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 銷案代號
        /// </summary>
        public string FinishId { get; set; }
        /// <summary>
        /// 銷案名稱
        /// </summary>
        public string FinishName { get; set; }
        /// <summary>
        /// 有效叫修案件碼
        /// </summary>
        public string CountSts { get; set; }
        /// <summary>
        /// 廠商銷案碼
        /// </summary>
        public string VenderSts { get; set; }
        /// <summary>
        /// 狀態碼
        /// </summary>
        public int FinishSts { get; set; }
    }
}
