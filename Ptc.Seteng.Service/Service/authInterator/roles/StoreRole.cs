using Ptc.AspnetMvc.Authentication;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    public class StoreRole : RoleIterator
    {

        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IBaseRepository<DataBase.TSTRMST, Tstrmst> _storeRepo;

        public override string Key { get; set; } = RoleType.Store.ToString();


        public StoreRole() { }

        public StoreRole(ISystemLog Logger,
                      IBaseRepository<DataBase.TSTRMST, Tstrmst> StoreRepo,
                      IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo)
        {
            this._logger = Logger;
            this._userRepo = UserRepo;
            this._storeRepo = StoreRepo;
        }

        /// <summary>
        /// 取得資料範圍
        /// </summary>
        /// <param name="Data"></param>
        public override void GetDataAuth(ref UserBase Data)
        {
            //if (!IsMatch(Data))
            //{
            //    _iterator.GetDataAuth(ref Data);
            //    return;
            //}
                

            //UserBase temp = Data;

            //var con = new Conditions<DataBase.TSTRMST>();

            ////關聯門市所屬的區域、公司
            //con.Include(x => x.TZOCODE.TCMPDAT);

            ////找到所屬的門市
            //con.And(x => x.Store_Cd == temp.StoreCd);

            ////公司別
            //con.And(x => x.Comp_Cd == temp.CompCd);

            ////取得門市(所屬權限)
            //Tstrmst range = _storeRepo.Get(con);

            //Data.CompShort = range?.TZOCODE?.TCMPDAT?.CompShort;
            //Data.StoreName = range?.StoreName;

            //var datarange = new DataRange();
            //var storecd = new List<string>() { range?.StoreCd };

            //datarange.StoreCd = storecd;
            //Data.DataRange = datarange;
        }

        /// <summary>
        /// 取得頁面權限
        /// </summary>
        /// <param name="Data"></param>
        public override void GetPageAuth(ref UserBase Data)
        {
           
        }

        /// <summary>
        /// 驗證規則
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Boolean IsMatch(UserBase data) => data.RoleId.Equals(Key);
     

    }
}
