using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ptc.AspnetMvc.Authentication;

using Ptc.Seteng.Repository;
using Ptc.Logger;
using Ptc.Logger.Service;

namespace Ptc.Seteng.Service
{
    public class CMRole : RoleIterator
    {

        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _zoRepo;

        public CMRole(ISystemLog Logger,
                      IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo,
                      IBaseRepository<DataBase.TZOCODE, Tzocode> ZoRepo)
        {
            _logger = Logger;
            _userRepo = UserRepo;
            _zoRepo = ZoRepo;
        }

        public CMRole() { }

        public override string Key { get; set; } = RoleType.CM.ToString();

        /// <summary>
        /// 取得資料範圍
        /// </summary>
        /// <param name="Data"></param>
        public override void GetDataAuth(ref UserBase Data)
        {
            if (!IsMatch(Data))
            {
                _iterator.GetDataAuth(ref Data);
                return;
            }


            var con = new Conditions<DataBase.TZOCODE>();

        }

        /// <summary>
        /// 取得頁面權限
        /// </summary>
        /// <param name="Data"></param>
        public override void GetPageAuth(ref UserBase Data)
        {
            if (!IsMatch(Data))
                _iterator.GetDataAuth(ref Data);

            //TODO...
        }

        public override bool IsMatch(UserBase data) =>  data.RoleId.Equals(Key);
       
    }
}
