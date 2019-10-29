using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public class VendorFactory  : IVendorFactory
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> _technicianGroupClaimsRepo;
        private readonly IBaseRepository<DataBase.TUSRMST , Tusrmst > _userRepo;
        private readonly IBaseRepository<DataBase.TVNDZO, Tvndzo> _tvndzo;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _TzocodeRepo;

        public VendorFactory(ISystemLog Logger,
                             IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TechnicianGroupRepo,
                             IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                             IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> TechnicianGroupClaimsRepo,
                             IBaseRepository<DataBase.TUSRMST , Tusrmst > UserRepo,
                             IBaseRepository<DataBase.TVNDZO, Tvndzo> tvndzo,
                             IBaseRepository<DataBase.TZOCODE, Tzocode> TzocodeRepo)
        {
            this._logger = Logger;
            this._technicianRepo = TechnicianRepo;
            this._technicianGroupRepo = TechnicianGroupRepo;
            this._technicianGroupClaimsRepo = TechnicianGroupClaimsRepo;
            this._userRepo = UserRepo;
            this._tvndzo = tvndzo;
            this._TzocodeRepo = TzocodeRepo;
        }

        /// <summary>
        /// 查詢技師清單
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public List<TvenderTechnician> GetTechnicians(string CompCd ,string VenderCd)
        {
            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Comp_Cd == CompCd &&           //公司別
                         x.Vender_Cd == VenderCd);        //廠商別                       

            var technicians = _technicianRepo.GetList(con)
                                             .ToList();


            return technicians;
        }


        /// <summary>
        /// 查詢廠商負責區域
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetVenderZo(string CompCd, string VenderCd)
        {
            Dictionary<string, string> resault = new Dictionary<string, string>();
            var con = new Conditions<DataBase.TVNDZO>();
            con.And(x => x.Comp_Cd == CompCd &&                     //公司別
                         x.Vender_Cd == VenderCd);                  //廠商別

            var VenderZo = _tvndzo.GetList(con).ToList();

            var conZo = new Conditions<DataBase.TZOCODE>();
            conZo.And(x => x.Comp_Cd == CompCd);
            PagedList<Tzocode> Zo = _TzocodeRepo.GetList(conZo);


            var inputZo = VenderZo;
            inputZo.ForEach(x =>
            {
                //x.Zo =  Zo.Where(y => y.ZoCd == x.Zo && y.DoCd == "").Select(z => z.ZoName).FirstOrDefault();
                resault.Add(x.Zo, Zo.Where(y => y.ZoCd == x.Zo && y.DoCd == "").Select(z => z.ZoName).FirstOrDefault());
            });

            return resault;
        }


        /// <summary>
        /// 查詢群組資訊
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public TtechnicianGroup GetGroup(string CompCd, string VendorCd, int Seq)
        {
            var con = new Conditions<DataBase.TTechnicianGroup>();

            con.Include(x => x.TTechnicianGroupClaims
                              .Select(y => y.TVenderTechnician));

            con.And(x => x.CompCd == CompCd &&         //公司別
                         x.VendorCd == VendorCd &&     //廠商別
                         x.Seq == Seq);                //序號

            var group = _technicianGroupRepo.Get(con);


            return group;
        }

        /// <summary>
        /// 檢驗廠商是否啟用
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Boolean CheckVender(string CompCd, string UserId)
        {
            var conUser = new Conditions<DataBase.TUSRMST>();
            conUser.And(x => x.Comp_Cd == CompCd || x.Comp_Cd=="");
            conUser.And(x => x.User_Id  == UserId);
            conUser.And(x => x.Role_Id == "VENDER" || x.Role_Id == "CafeVender" || x.Role_Id == "APPVENDER");
            conUser.And(x => x.Id_Sts == "Y");  
            return _userRepo.Count(conUser) > 0;
        }

        /// <summary>
        /// 取得課別
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        public List<Tzocode > SelectDo(string CompCd, String[] ZO)
        {
            List<Tzocode> resault = new List<Tzocode>();
            var con = new Conditions<DataBase.TZOCODE>();
            List<Tzocode> Do = new List<Tzocode>();
            
            ZO.ForEach(x => {
                con.And(y => y.Comp_Cd == CompCd);
                con.And(y => y.D_O != "");
                con.And(y => y.Z_O == x);
                con.And(y => y.Close_Date == "9999/12/31");
                con.And(y => y.Zo_Type == "0");
                var Data = _TzocodeRepo.GetList(con).ToList();
                Data.ForEach(g =>
                {
                    Do.Add(g);
                });
                con = new Conditions<DataBase.TZOCODE>();
            });

            return Do;
        }
    }
}
