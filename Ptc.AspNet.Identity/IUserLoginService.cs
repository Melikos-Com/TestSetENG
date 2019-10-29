using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspNet.Identity
{
    public interface IUserLoginService<TUser>
        where TUser : IdentityUser
    {
        Task<TUser> CCEngUserLogin(string userName, string password, string compcd);


        int test(string userName, string password, string compcd);
    }
}
