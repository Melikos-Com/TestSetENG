using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Service;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Provider;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;

namespace Ptc.Seteng.Service
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ISystemLog _logger;
        private readonly ITechnicianProvider _technicianProvider;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;
        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _tusrstRepo;

        public string _technicianPath;
        public string _stickerPath;
        public string _license;


        public TechnicianService(ISystemLog Logger,
                                 ITechnicianProvider TechnicianProvider,
                                 IBaseRepository<DataBase.TUSRMST, Tusrmst> tusrstRepo,
                                 IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TechnicianGroupRepo,
                                 IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo)
        {
            this._tusrstRepo = tusrstRepo;
            this._logger = Logger;
            this._technicianRepo = TechnicianRepo;
            this._technicianProvider = TechnicianProvider;
            this._technicianGroupRepo = TechnicianGroupRepo;
            this._stickerPath = ServerProfile.GetInstance().STICKER_PATH;
            this._technicianPath = ServerProfile.GetInstance().TECHNICIAN_PATH;
            this._license = ServerProfile.GetInstance().LICENSE_PATH;
        }

        /// <summary>
        /// 认可技師
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean ConfirmTechnician(TvenderTechnician data)
        {
            _logger.Info($"認可技師-技師帳號:{data.Account}");

            //using (TransactionScope scope = new TransactionScope())
            //{

            //    _logger.Info($"認可技師-準備更新資料");

            //    var con = new Conditions<DataBase.TVenderTechnician>();

            //    con.And(g => g.Account == data.Account);
            //    con.And(g => g.Comp_Cd == data.CompCd);
            //    con.And(g => g.Vender_Cd == data.VenderCd);


            //    con.Allow(g => g.IsHQCheck,
            //              g => g.IsHQCheckRemark,
            //              g => g.HQCheckTime);

            //    _technicianRepo.Update(con, data);


            //    scope.Complete();
            //}

            return true;

        }

        /// <summary>
        /// 新增技師
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean CreateTechnician(TvenderTechnician data)

        {

            _logger.Info($"新增技師-技師帳號:{data.Account}");

            #region 檢核技師是否重覆

            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Account == data.Account);

            if (_technicianRepo.GetList(con).Any())
                throw new IndexOutOfRangeException("[ERROR]=>該帳號已被使用，請重新填寫");

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {

                _logger.Info($"新增技師-準備新增資料");

                #region 新增技師信息
                con.And(x => x.Account == data.Account);
                if (!_technicianRepo.Add(con, data))
                    throw new Exception("[ERROR]=>新增失敗");

                #endregion

                scope.Complete();
            }

            return true;

        }

        /// <summary>
        /// 更新技師
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean UpdateTechnician(TvenderTechnician data)
        {

            _logger.Info($"更新技師-技師帳號:{data.Account}");

            #region 找到技師相關訊息
            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Comp_Cd == data.CompCd);
            con.And(x => x.Vender_Cd == data.VenderCd);
            con.And(x => x.Account == data.Account);

            TvenderTechnician meta = _technicianRepo.Get(con);

            if (meta == null)
                throw new NullReferenceException($"[ERROR]=> 更新技師訊息時,找不到技師相關訊息");

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {

                _logger.Info($"更新技師-準備更新資料");

                #region 更新技師訊息


                con.Allow(x => x.Enable,     //帳號啟用
                          x => x.Name,       //名字
                          x => x.IsVendor,   //角色
                          x => x.Password);  //密碼

                _technicianRepo.Update(con, data);


                #endregion

                scope.Complete();
            }

            return true;

        }
        /// <summary>
        /// 合并所选择的技師
        /// </summary>
        /// <param name="technicians"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        public List<TvenderTechnician> MergeTechnician(IEnumerable<TvenderTechnician> technicians,
                                                       IEnumerable<TtechnicianGroup> groups)
        {

            _logger.Info($"APP_合併技師/群組,產生推播對象");

            HashSet<TvenderTechnician> result = new HashSet<TvenderTechnician>();

            #region 瀏覽群組

            groups?.ForEach(group =>
            {

                _logger.Info($"APP_合併技師/群組,產生推播對象-瀏覽群組:{group.Seq}");

                var con = new Conditions<DataBase.TTechnicianGroup>();

                con.And(x => x.CompCd == group.CompCd &&
                             x.VendorCd == group.VendorCd &&
                             x.Seq == group.Seq);

                con.Include(x => x.TTechnicianGroupClaims.Select(y => y.TVenderTechnician));

                var currGroup = _technicianGroupRepo.Get(con);

                if (currGroup == null)
                    throw new NullReferenceException($"合并技師數據時,找不到群組信息");

                //找到群組下的技師,放入hashSet
                currGroup.TTechnicianGroupClaims.ForEach(claims =>
                {
                    //群組下的技師,要找到已經通過總部審核並啟用的
                    if (claims.TVenderTechnician.Enable)
                    {
                        if (!result.Any(x => x.Account == claims.Account))
                            result.Add(claims.TVenderTechnician);
                    }

                });

            });


            #endregion

            #region 瀏覽技師

            technicians?.ForEach(technician =>
            {

                _logger.Info($"APP_合併技師/群組,產生推播對象-瀏覽技師:{technician.Account}");

                var con = new Conditions<DataBase.TVenderTechnician>();

                con.And(x => x.Comp_Cd == technician.CompCd &&
                             x.Vender_Cd == technician.VenderCd &&
                             x.Account == technician.Account);

                var currTechnician = _technicianRepo.Get(con);

                if (currTechnician == null)
                    throw new NullReferenceException($"合并技師數據時,找不到技師信息");

                //技師要已經通過總部審核並啟用的
                if (currTechnician.Enable)
                {
                    if (!result.Any(x => x.Account == currTechnician.Account))
                        result.Add(currTechnician);
                }

            });

            #endregion

            return result.ToList();
        }
    }
}
