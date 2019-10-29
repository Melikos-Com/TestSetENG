using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ptc.AspnetMvc.Authentication;

using Ptc.Seteng.Repository;

namespace Ptc.Seteng.Service
{
    public class AdminRole : RoleIterator
    {


        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _compRepo;


        

        public AdminRole(
                         IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo,
                         IBaseRepository<DataBase.TCMPDAT, Tcmpdat> CompRepo)
        {

            _userRepo = UserRepo;
            _compRepo = CompRepo;
        }

        public AdminRole() { }

        public override string Key { get; set; } = RoleType.Admin.ToString();

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


            UserBase temp = Data;

            var con = new Conditions<DataBase.TCMPDAT>();

            //關聯區域底下的門市
            //con.Include(x => x.TSTRMST);

            ////關聯所屬公司
            //con.Include(x => x.TCMPDAT.TVENDER);
           
            //對應公司別
            con.And(x => x.Comp_Cd == temp.CompCd);
        
            //取得區域清單(所屬權限)
            Tcmpdat range = _compRepo.Get(con);

            Data.CompShort = range?.CompShort;
          

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

        public override bool IsMatch(UserBase data) => data.RoleId.Equals(Key);
       
    }
}
