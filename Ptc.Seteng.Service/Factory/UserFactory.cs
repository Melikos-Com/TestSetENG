using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Repository;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public class UserFactory : IUserFactory
    {
        private readonly ISystemLog _logger;
        private readonly IAspUserRepository _aspUserRepo;
        private readonly IEnumerable<IRoleIterator> _roleIterators;
        private RoleIterator _roleIterator;

        public UserFactory(ISystemLog Logger,
                           IEnumerable<IRoleIterator> RoleIterators,
                           IAspUserRepository AspUserRepo)

        {
            _logger = Logger;
            _aspUserRepo = AspUserRepo;
            _roleIterators = RoleIterators;
            _roleIterator = (RoleIterator)RoleIterators.SingleOrDefault(x => x.Key == RoleType.Admin.ToString());
            RoleIterator TLRole = (RoleIterator)RoleIterators.SingleOrDefault(x => x.Key == RoleType.TeamLeader.ToString());
            RoleIterator FMRole = (RoleIterator)RoleIterators.SingleOrDefault(x => x.Key == RoleType.Admin_Zo.ToString());
            RoleIterator StoreRole = (RoleIterator)RoleIterators.SingleOrDefault(x => x.Key == RoleType.Store.ToString());
            RoleIterator CMRole = (RoleIterator)RoleIterators.SingleOrDefault(x => x.Key == RoleType.CM.ToString());
            RoleIterator VendorRole = (RoleIterator)RoleIterators.SingleOrDefault(x => x.Key == RoleType.Vender.ToString());


            _roleIterator.SetIterator(TLRole);
            TLRole.SetIterator(FMRole);
            FMRole.SetIterator(StoreRole);
            StoreRole.SetIterator(CMRole);
            CMRole.SetIterator(VendorRole);
        }


        /// <summary>
        /// [server] 取得完整的User資訊
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserBase GetAspUser(string UserId)
        {
            UserBase currentUser = _aspUserRepo.GetWebUser(UserId);

            if (currentUser == null)
                return null;

            _roleIterator.GetDataAuth(ref currentUser);

            //_roleIterator.GetPageAuth(ref currentUser);

            return currentUser;

        }

        /// <summary>
        /// [client] 取的完整的 User 資訊
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public UserBase GetClientUser(string Token)
        {
            UserBase input = TokenUtil<UserBase>.Parse(Token);

            if (input == null)
                throw new FormatException($"解析token失敗");

            UserBase currentUser = _aspUserRepo.GetWebUser(input.UserId);

            if (currentUser == null)
                throw new FormatException($"取得使用者資訊時,找不到使用者資訊");

            _roleIterator.GetDataAuth(ref currentUser);

            _roleIterator.GetPageAuth(ref currentUser);

            return currentUser;
        }

    }
}
