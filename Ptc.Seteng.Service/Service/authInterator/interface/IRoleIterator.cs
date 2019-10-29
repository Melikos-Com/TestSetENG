using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    public interface IRoleIterator
    {
        /// <summary>
        /// 對應的驗證規則
        /// </summary>
        String Key { get; set; }

        /// <summary>
        /// 驗證規則
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean IsMatch(UserBase data);

        /// <summary>
        /// 取得資料範圍
        /// </summary>
        /// <param name="Data"></param>
        void GetDataAuth(ref UserBase Data);

        /// <summary>
        /// 取得頁面範圍
        /// </summary>
        /// <param name="Data"></param>
        void GetPageAuth(ref UserBase Data);

    }
}
