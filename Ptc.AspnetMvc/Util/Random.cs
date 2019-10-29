using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Util
{
    public static class Random
    {

        /// <summary>
        /// 使用GUID取得亂數
        /// </summary>
        /// <returns></returns>
        public static string GetRandomByGUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 使用GUID取得亂數-長度
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GetRandomByGUID(int Length)
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, Length);
        }
    }
}
