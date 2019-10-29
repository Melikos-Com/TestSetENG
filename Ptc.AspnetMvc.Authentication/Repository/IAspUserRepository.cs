
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Repository
{
    public interface IAspUserRepository
    {
       
        /// <summary>
        /// [server] 取得使用者資訊
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserBase GetWebUser(string UserId);


    }
}
