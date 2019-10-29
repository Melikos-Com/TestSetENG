using AutoMapper;
using LinqKit;
using Newtonsoft.Json;
using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Repository;
using Ptc.Seteng.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ptc.Seteng.Repository
{
    public class AspUserRepository : IAspUserRepository
    {

        /// <summary>
        /// [server] 取得使用者資訊
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserBase GetWebUser(string username)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var query = db.TUSRMST
                              .Include("TUSRDTL")
                              .Include("TSYSROL")
                              .FirstOrDefault(x => x.User_Id == username);

                return Mapper.Map<UserBase>(query);

            }
        }

    


    }
}