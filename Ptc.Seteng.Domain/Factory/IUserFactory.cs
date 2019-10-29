using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public interface IUserFactory
    {
        /// <summary>
        /// [server] 取得完整的User資訊
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserBase GetAspUser(string UserId);

        /// <summary>
        /// [clent] 取得完整的User資訊
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        UserBase GetClientUser(string Token);
    }
}
