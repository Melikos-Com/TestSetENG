using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Provider
{
    public interface ITcallogProvider
    {
        /// <summary>
        /// 取得案件照片
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <returns></returns>
        IEnumerable<Byte[]> GetWebImageList(string CompCd, string Sn);
        /// <summary>
        /// 紀錄推播
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        Boolean RecordPush(string Sn, IPushRequest Data);
    }
}
