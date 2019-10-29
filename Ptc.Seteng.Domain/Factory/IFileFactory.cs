using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface IFileFactory
    {
        /// <summary>
        /// 取得案件下的圖片
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="CompCd"></param>
        /// <returns></returns>
        IEnumerable<String> GetCallLogImg(string Sn, string CompCd);
    }
}
