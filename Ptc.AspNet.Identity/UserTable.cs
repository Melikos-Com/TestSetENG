using Ptc.AspNet.Identity.database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ptc.AspNet.Identity
{
    /// <summary>
    /// Class that represents the Users table in the MySQL Database
    /// </summary>
    public class UserTable<TUser>
        where TUser : IdentityUser
    {
        private SetengUser _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserTable(SetengUser database)
        {
            _database = database;
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            return _database.TUSRMST.AsQueryable().Where(x => x.User_Id == userId).Select(g => g.User_Name).FirstOrDefault();
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            return _database.TUSRMST.AsQueryable().Where(x => x.User_Name == userName).Select(g => g.User_Id).FirstOrDefault();
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public TUser GetUserById(string userId)
        {
            TUser user = null;

            var rows = _database.TUSRMST.AsQueryable().Where(x => x.User_Id == userId).ToList();

            if (rows != null && rows.Count == 1)
            {
                var row = rows.FirstOrDefault();
                var Check = _database.TUSRVENRELATION.Where(x => x.User_Id == row.User_Id && x.Comp_Cd == "711").ToList().Count; //檢查有沒有服務711
                if (Check == 0)
                    return user;
                else
                {
                    user = (TUser)Activator.CreateInstance(typeof(TUser));
                    user.Id = row.User_Id;
                    user.UserName = row.User_Name;
                    user.SecurityStamp = string.Empty;
                }
            }

            return user;
        }

        /// <summary>
        /// Returns a list of TUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string compCd, string userName)
        {

            var rows = _database.TUSRMST
                                .AsQueryable()
                                .Include("TUSRDTL")
                                .Where(x => x.User_Id == userName &&
                                            (x.Comp_Cd == compCd || x.Comp_Cd == "") &&
                                            x.Id_Sts == "Y")                //判斷廠商帳號是否啟用
                                .ToList();

            var Check = _database.TUSRVENRELATION.Where(y => y.User_Id == userName && y.Comp_Cd == "711").ToList().Count; //檢查有沒有服務711
            if (Check == 0)
            {
                return null;
            }
            var users = rows?.Select(x =>
                {
                    TUser user = (TUser)Activator.CreateInstance(typeof(TUser));
                    user.Id = x.User_Id;
                    user.UserName = x.User_Id;
                    user.PasswordHash = x.TUSRDTL.Pass_Wd;
                    user.RoleId = x.Role_Id;
                    return user;
                }).ToList();

            return users;
        }

        public List<TUser> GetUserByEmail(string email)
        {
            return null;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {

            var passHash = _database.TUSRDTL.AsQueryable().Where(x => x.User_Id == userId).Select(g => g.Pass_Wd).FirstOrDefault();
            if (string.IsNullOrEmpty(passHash))
            {
                return null;
            }

            return passHash;
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public int SetPasswordHash(string userId, string passwordHash)
        {


            var User = _database.TUSRDTL.AsQueryable().Where(x => x.User_Id == userId).FirstOrDefault();

            if (User != null)
            {
                User.Pass_Wd = passwordHash;
            }

            return _database.SaveChanges();
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(string userId)
        {
            return string.Empty;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(TUser user)
        {

            try
            {
                return _database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private int Delete(string userId, string compcd)
        {

            var User = _database.TUSRMST.AsQueryable().Where(x => x.Comp_Cd == compcd && x.User_Id == userId).FirstOrDefault();
            _database.TUSRMST.Remove(User);

            return _database.SaveChanges();

        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(TUser user)
        {
            //return Delete(user.Id, user.CompCd);
            return 0;
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(TUser user)
        {

            // var User = _database.TUSRMST.AsQueryable().Where(x => x.Comp_Cd == user.CompCd && x.User_Id == user.UserId).FirstOrDefault();
            //
            //
            // return _database.SaveChanges();
            return 0;
        }

        public void UpdateByName(TUser user)
        {
        }

    }
}
