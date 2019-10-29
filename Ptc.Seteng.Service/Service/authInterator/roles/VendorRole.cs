using Ptc.AspnetMvc.Authentication;

using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    public class VendorRole : RoleIterator
    {


        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _vendorRepo;
        private readonly IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _cmpdatRepo;

        public override string Key { get; set; } = RoleType.Vender.ToString();


        public VendorRole() { }

        public VendorRole(
                          IBaseRepository<DataBase.TVENDER, Tvender> VendorRepo,
                          IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo,
                          IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _cmpdatRepo)
        {

            this._userRepo = UserRepo;
            this._vendorRepo = VendorRepo;
            this._cmpdatRepo = _cmpdatRepo;
        }
        /// <summary>
        /// 取得資料範圍
        /// </summary>
        /// <param name="Data"></param>
        public override void GetDataAuth(ref UserBase Data)
        {

            if (!IsMatch(Data))
                throw new Exception($"此角色無法使用該功能!");

            UserBase temp = Data;

            var conVnd = new Conditions<DataBase.TVENDER>();

            //關聯所屬公司
            //con.Include(x => x.TCMPDAT);

            //廠商別
            conVnd.And(x => x.Vender_Cd == temp.UserId);

            //公司別
            conVnd.And(x => x.Comp_Cd == temp.CompCd);

            ////廠商到期日
            //con.And(x => x.Close_Date >= DateTime.Now);

            Tvender range = _vendorRepo.Get(conVnd);
            Data.VenderName = range?.VenderName;


            if (range == null)
                throw new Exception("廠商主檔 查無資料");

            var conComp = new Conditions<DataBase.TCMPDAT>();
            conComp.And(x => x.Comp_Cd == range.CompCd);
            Tcmpdat Comp = _cmpdatRepo.Get(conComp);
            Data.CompShort = Comp?.CompShort;

            var datarange = new DataRange();
            var vendorcd = new List<string>() { range?.VenderCd };
            datarange.VendorCd = vendorcd;
            Data.DataRange = datarange;







        }

        /// <summary>
        /// 取得頁面權限
        /// </summary>
        /// <param name="Data"></param>
        public override void GetPageAuth(ref UserBase Data)
        {
            if (!IsMatch(Data))
                throw new ArgumentOutOfRangeException($"no find auth level!");
        }

        /// <summary>
        /// 驗證規則
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool IsMatch(UserBase data) => data.RoleId.Equals(Key);

    }
}
