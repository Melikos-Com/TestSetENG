using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Service
{
    public interface IAspUserService
    {

        /// <summary>
        ///  [server]更新使用者資訊
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Boolean Update(UserBase user , RoleAuth role);

        /// <summary>
        /// [server]整合使用者資訊
        /// </summary>
        /// <returns></returns>
        UserBase Integration(string UserId);
    }
}
