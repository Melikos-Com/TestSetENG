using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface IPushFactory
    {

        /// <summary>
        /// 執行推播
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean Exucte(JPushRequest data);

        /// <summary>
        /// 案件立案-自動推播
        /// </summary>
        /// <param name="data"></param>
        /// <param name="account"></param>
        /// <param name="RegId"></param>
        /// <returns></returns>
        Boolean Exucte(JPushRequest data, string account, string RegId);

        /// <summary>
        /// 多案件、多技師推撥
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <param name="RegId"></param>
        /// <returns>從Web進行通知</returns>
        Boolean Exucte(JPushRequest data, List<string> Sn, Dictionary<string, string> Account);
        /// <summary>
        /// 多案件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        Boolean Exucte(JPushRequest data, List<string> Sn, List<string> Account);
    }
}
