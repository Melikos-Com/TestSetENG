using AutoMapper;
using LinqKit;
using Newtonsoft.Json;
using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Spcc.CCEng.Repository
{
    public class AspUserRepository : IAspUserRepository
    {

        #region CRUD

        /// <summary>
        /// 單一取得
        /// </summary>
        /// <param name="mObject"></param>
        /// <returns></returns>
        public UserBase Get(object mObject)
        {

            var manager = (Conditions<DataBase.TUSRMST>)mObject;

            using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
            {

                var query = db.TUSRMST.AsQueryable();

                manager.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(manager.GetPredicate());

                if (query.SingleOrDefault() == null)
                    throw new NullReferenceException($"no find data");

                return Mapper.Map<UserBase>(query.SingleOrDefault());

            }

        }
        /// <summary>
        /// 取得清單
        /// </summary>
        /// <param name="mObject"></param>
        /// <returns></returns>
        public PagedList<UserBase> GetList(object mObject)
        {
            var manager = (Conditions<DataBase.TUSRMST>)mObject;

            using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.TUSRMST.AsQueryable();

                manager.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(manager.GetPredicate());

                int index = 0;

                //manager.ORDER.ForEach(x =>
                //{
                //    if (index == 0)
                //    {
                //        query = query.OrderBy(x.Value.Key, x.Value.Value);
                //    }
                //    else
                //    {
                //        query = query.ThenBy(x.Value.Key, x.Value.Value);
                //    }
                //    index++;
                //});

                return new PagedList<UserBase>(
                           Mapper.Map<IEnumerable<UserBase>>(query),
                           manager.PageIndex,
                           manager.PageSize);



            }

        }
        /// <summary>
        /// 單一新增
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Boolean Create(UserBase obj)
        {

            using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
            {

                //db.AspNetUsers.Add(new DataBase.AspNetUsers()
                //{

                //    Email = obj.Email_Account,
                //    UserName = obj.User_Id,
                //    Id = Guid.NewGuid().ToString(),
                //    PasswordHash = obj.PasswordHash,
                //    SecurityStamp = string.Format("{0}-{1}", obj.Id, DateTime.Now.ToBinary()),
                //    DataRange = (obj.DataRangeAuth != null) ? JsonConvert.SerializeObject(obj.DataRangeAuth) : null,
                //    PageAuth = (obj.PageAuth != null) ? JsonConvert.SerializeObject(obj.PageAuth) : null,
                //    AspNetRoles = db.AspNetRoles.Where(x => obj.RoleAuth
                //                                               .Any(g => g.Id == x.Id))?
                //                                               .ToList()
                //});

                return db.SaveChanges() > 0;
            }

        }
        /// <summary>
        /// 單一修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Boolean Edit(UserBase obj)
        {

            //定義連結DB資訊
            using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.TUSRMST.Where(x => x.User_Id == obj.UserId)
                                          .SingleOrDefault();

                if (query == null)
                    throw new NullReferenceException($"no find data,{nameof(obj)}");

                query.Email_Account = obj.Email;
                
                //query.Email = obj.Email_Account;
                //query.PasswordHash = obj.PasswordHash;


                //List<string> roleList = obj.RoleAuth.Select(x => x.Id).ToList();

             


                if (obj.RoleAuth.PageAuth.Count == 0)
                    throw new ArgumentNullException("沒有設定該user權限");

                List<DataBase.TSYSROL> currentRoles = db.TSYSROL.Where(x => x.Role_Id == obj.RoleAuth.RoleId).ToList();
              
                if (currentRoles.Count == 0)
                    throw new ArgumentNullException("找不到權限資訊");

                //query.TSYSROL = 
                //query.AspNetRoles = currentRoles;


                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 單一刪除
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Boolean Delete(string UserID)
        {
            using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;


                var query = db.AspNetUsers.Include("AspNetRoles")
                                          .Where(x => x.Id == UserID)
                                          .SingleOrDefault();


                if (query == null)
                    throw new NullReferenceException($"no find data");


                db.AspNetUsers.Remove(query);

                return db.SaveChanges() > 0;

            }
        }

        /// <summary>
        /// 批次刪除
        /// </summary>
        /// <param name="IDList"></param>
        /// <returns></returns>
        public Boolean DeleteRange(List<string> IDList)
        {
            using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;


                var query = db.AspNetUsers.Where(x => IDList.Contains(x.Id));


                if (query == null || query.Count() == 0)
                    throw new NullReferenceException($"no find data");

                db.AspNetUsers.RemoveRange(query);

                return db.SaveChanges() > 0;


            }
        }

        #endregion

    }
}
