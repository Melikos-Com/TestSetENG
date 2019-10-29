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
    public class FMRole : RoleIterator
    {
        private readonly ISystemLog _logger; 
        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _zoRepo;

        public override string Key { get; set; } = RoleType.Admin_Zo.ToString();


        public FMRole() { }

        public FMRole(ISystemLog Logger,
                      IBaseRepository<DataBase.TZOCODE, Tzocode> ZoRepo,
                      IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo)
        {
            this._logger = Logger;
            this._zoRepo = ZoRepo;
            this._userRepo = UserRepo;          
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

            //var con = new Conditions<DataBase.TZOCODE>();

            ////關聯區域底下的門市
            //con.Include(x => x.TSTRMST);

            ////關聯所屬公司
            //con.Include(x => x.TCMPDAT);

            ////找到所屬的區域
            //con.And(x => x.Zo_Manager == temp.UserId);

            ////公司別
            //con.And(x => x.Comp_Cd == temp.CompCd);

            ////取得區域(所屬權限)
            //IEnumerable<Tzocode>  range = _zoRepo.GetList(con);

            //Data.CompShort = range?.FirstOrDefault()?
            //                      .TCMPDAT?
            //                      .CompShort;

            //var datarange = new DataRange();
            //var zocd = new List<string>();
            //range.Select(x => x.ZoCd).ForEach(x =>
            //{
            //    zocd.Add(x);
            //});
            //datarange.ZoCd = zocd;
            //Data.DataRange = datarange;
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

        /// <summary>
        /// 驗證規則
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Boolean IsMatch(UserBase data) => data.RoleId.Equals(Key);
        
    }
}
