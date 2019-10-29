using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication
{
    public interface IIdentityAuth
    {
        /// <summary>
        /// //使用者完整權限
        /// </summary>
        Dictionary<string, AuthItem> AuthItemColl { get; set; }
       

    }
}
