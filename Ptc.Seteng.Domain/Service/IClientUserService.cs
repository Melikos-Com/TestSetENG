using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    public interface IClientUserService
    {
        /// <summary>
        /// [client] 廠商登入
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="HashPwd"></param>
        /// <returns></returns>
        TvenderTechnician VendorLogin(string Account, string Password, string UUID);

        /// <summary>
        /// [client] 廠商登出
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Boolean VendorLogout(string Account, string Password);
   }
}
