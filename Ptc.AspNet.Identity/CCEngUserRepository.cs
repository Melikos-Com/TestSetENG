using Microsoft.AspNet.Identity;
using Ptc.AspNet.Identity.database;
using Ptc.Spcc.CCEng;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ptc.AspNet.Identity
{

    public class CCEngUserRepository <TUser> : IUserLoginService<TUser>
        where TUser : IdentityUser  

    {
        private UserTable<TUser> userTable;

        public SpccEngUserEntities Database { get; private set; }

        public CCEngUserRepository(DbContext context)
        {

            var database = new SpccEngUserEntities();

            Database = database;
            userTable = new UserTable<TUser>(database);
        }
        public int test(string userName, string password, string compcd)
        {
           
          

            return 0;
        }
        public Task<TUser> CCEngUserLogin(string userName, string password, string compcd)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Null or empty argument: userName");

            List<TUser> result = userTable.GetUserByName(userName) as List<TUser>;



            // Should I throw if > 1 user?
            if (result != null && result.Count == 1)
            {


                return Task.FromResult<TUser>(result[0]);
            }

            return Task.FromResult<TUser>(null);
        }
      
    }
}
