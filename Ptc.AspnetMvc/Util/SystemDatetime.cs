using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Util
{
    public static class SystemDatetime
    {

        public static DateTime GetCurrentDatetime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 取得一段區間的日期列表
        /// </summary>
        /// <param name="sDT"></param>
        /// <param name="eDT"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static List<DateTime> GetDateList(DateTime? sDT = null, DateTime? eDT = null, int day = 0)
        {
            if (sDT.HasValue == false)
                sDT = DateTime.Now;

            if (eDT.HasValue == false || eDT < sDT)
                eDT = sDT.Value.AddDays(day);

            List<DateTime> result = new List<DateTime>();
            DateTime currentDT = sDT.Value;

            while (currentDT <= eDT)
            {
                result.Add(currentDT);
                currentDT = currentDT.AddDays(1);
            }

            return result;
        }
    }
}
