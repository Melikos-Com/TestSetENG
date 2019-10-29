using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class Tdammtn
    {
        public Tdammtn()
        {

        }
        public Tdammtn(Tdammtn data)
        {
            this.CompCd = data.CompCd;
            this.AssetCd = data.AssetCd;
            this.DamageProcNo = data.DamageProcNo;
            this.MtnDesc = data.DamageProcNo+" "+data.MtnDesc;
            this.CountSts = data.CountSts;
        }
        /// <summary>
        /// 公司別
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// 故障處理代碼
        /// </summary>
        public string DamageProcNo { get; set; }
        /// <summary>
        /// 處理內容
        /// </summary>
        public string MtnDesc { get; set; }
        /// <summary>
        /// 有效叫修案件碼
        /// </summary>
        public string CountSts { get; set; }
    }
}
