using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ptc.AspnetMvc.Authentication;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ptc.AspNet.Identity
{
    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IUser interface 
    /// </summary>
    public class IdentityUser : Microsoft.AspNet.Identity.EntityFramework.IdentityUser, IIdentityAuth/*IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>*/
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Ptc.AspNet.Identity.IdentityUser> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }


        public System.Collections.Generic.Dictionary<string, AuthItem> AuthItemColl
        {
            get
            {
                return new System.Collections.Generic.Dictionary<string, AuthItem>();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Constructor that takes user name as argument
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        /// <summary>
        /// User ID
        /// </summary>
        public override string Id { get; set; }
        public  string Compcd { get; set; }
        /// <summary>
        /// User's name
        /// </summary>
        public override string UserName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public override string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public override bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public override string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public override string SecurityStamp { get; set; }

        ///在特定點在 UserManager 物件存留期中自動創建的 GUID。
        ///通常情況下，它具有創建和更新時添加或移除密碼更改或社會的登錄名。
        ///安全郵票一般使用者資訊創建快照並會自動將使用者記錄，如果什麼都沒有改變。
       
        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        public override string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public override bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public override bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public override DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public override bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public override int AccessFailedCount { get; set; }


        public  string RoleId { get; set; }
    }
}
