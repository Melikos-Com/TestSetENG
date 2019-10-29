using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Service;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ptc.Seteng.Service
{
    public class VendorService : IVendorService
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _tvenderRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> _technicianGroupClaimsRepo;

        public VendorService(ISystemLog Logger,
                             IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TechnicianGroupRepo,
                             IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                             IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> TechnicianGroupClaimsRepo)
        {
            this._logger = Logger;
            this._tvenderRepo = TechnicianRepo;
            this._technicianGroupRepo = TechnicianGroupRepo;
            this._technicianGroupClaimsRepo = TechnicianGroupClaimsRepo;
        }

        /// <summary>
        /// 新增群組信息
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        public Boolean CreateTechnicianGroup(TtechnicianGroup Data, String[] Accounts)
        {
            //_logger.Info($"新增群組-群組名稱:{Data.GroupName}");

            #region 檢核群組信息是否重复

            var con = new Conditions<DataBase.TTechnicianGroup>();

            con.And(x => x.CompCd == Data.CompCd &&
                        x.VendorCd == Data.VendorCd &&
                        x.GroupName == Data.GroupName);

            //if (_technicianGroupRepo.IsExist(con))
            //    throw new IndexOutOfRangeException("[ERROR]=>新增群組,檢核已有該群組存在");

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                _logger.Info($"新增群組-準備更新資料");

                #region 新增群組
                if (_technicianGroupRepo.IsExist(con))
                    throw new Exception("群組名稱相同");

                _technicianGroupRepo.Add(con, Data);

                var seq = _technicianGroupRepo.Get(con).Seq;

                #endregion

                #region 新增技師至群組

                if (Accounts != null)
                {
                    foreach (String account in Accounts)
                    {
                        TtechnicianGroupClaims technicianGroupClaims = new TtechnicianGroupClaims()
                        {
                            Seq = seq,
                            CompCd = Data.CompCd,
                            VendorCd = Data.VendorCd,
                            Account = account
                        };

                        var cond = new Conditions<DataBase.TTechnicianGroupClaims>();
                        cond.And(x => x.Seq == seq &&
                                x.CompCd == Data.CompCd &&
                                x.VendorCd == Data.VendorCd &&
                                x.Account == account);
                        if (!_technicianGroupClaimsRepo.Add(cond, technicianGroupClaims))
                            throw new Exception("[ERROR]=>新增技師至群組時,新增失敗");
                    }
                }

                #endregion

                scope.Complete();
            }
            return true;
        }
        /// <summary>
        /// 更新群組信息
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        public Boolean UpdateTechnicianGroup(TtechnicianGroup Data, String[] Accounts)
        {
            //_logger.Info($"更新群組-群組名稱:{Data.GroupName}");

            #region 檢核群組信息是否存在

            var con = new Conditions<DataBase.TTechnicianGroup>();
            con.And(x => x.Seq == Data.Seq &&
                        x.CompCd == Data.CompCd &&
                        x.VendorCd == Data.VendorCd);
            con.Allow(x => x.GroupName);
            con.Allow(x => x.Responsible_Do);
            con.Allow(x => x.Responsible_Zo);
            var group = _technicianGroupRepo.Get(con);

            if (group == null)
                throw new IndexOutOfRangeException("[ERROR]=>修改群組數據時,檢核没有該群組數據存在");

            var groupname = group.GroupName;

            #endregion

            #region 檢核群組信息是否重复

            var query = new Conditions<DataBase.TTechnicianGroup>();

            query.And(x => x.CompCd == Data.CompCd &&
                        x.VendorCd == Data.VendorCd &&
                        x.GroupName == Data.GroupName);

            var count = _technicianGroupRepo.GetList(query).Count();
            if (count > 1)
            {
                throw new IndexOutOfRangeException("[ERROR]=>修改群組數據時,檢核已經有該群組存在");
            }
            else
            {
                var gp = _technicianGroupRepo.Get(query);
                var groupseq = 0;
                if (gp != null)
                    groupseq = gp.Seq;
                if (count == 1 && groupseq != Data.Seq)
                    throw new IndexOutOfRangeException("[ERROR]=>修改群組數據時,檢核已經有該群組存在");
            }





            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                _logger.Info($"更新群組-準備更新資料");

                #region 修改群組
                    _technicianGroupRepo.Update(con, Data);
                #endregion

                #region 修改技師至群組
                var cond = new Conditions<DataBase.TTechnicianGroupClaims>();
                cond.And(x => x.Seq == Data.Seq &&
                        x.CompCd == Data.CompCd &&
                        x.VendorCd == Data.VendorCd);
                if (_technicianGroupClaimsRepo.GetList(cond).Any())
                    _technicianGroupClaimsRepo.Remove(cond);

                if (Accounts != null)
                {
                    foreach (String account in Accounts)
                    {
                        TtechnicianGroupClaims technicianGroupClaims = new TtechnicianGroupClaims()
                        {
                            Seq = Data.Seq,
                            CompCd = Data.CompCd,
                            VendorCd = Data.VendorCd,
                            Account = account
                        };

                        var condition = new Conditions<DataBase.TTechnicianGroupClaims>();
                        condition.And(x => x.Seq == Data.Seq &&
                                x.CompCd == Data.CompCd &&
                                x.VendorCd == Data.VendorCd &&
                                x.Account == account);

                        if (!_technicianGroupClaimsRepo.Add(condition, technicianGroupClaims))
                            throw new Exception("[ERROR]=>修改技師至群組時,新增失敗");
                    }
                }
                #endregion

                scope.Complete();
            }
            return true;
        }
        public Boolean AddTechnicianGroup(TtechnicianGroup Data, string account)
        {
            var con = new Conditions<DataBase.TTechnicianGroupClaims>();

            con.And(x => x.Seq == Data.Seq &&
                    x.CompCd == Data.CompCd &&
                    x.VendorCd == Data.VendorCd &&
                    x.Account == account);
            if (_technicianGroupClaimsRepo.IsExist(con))
                throw new Exception("[ERROR]=>預設群組已有該技師!!");

            TtechnicianGroupClaims technicianGroupClaims = new TtechnicianGroupClaims()
            {
                Seq = Data.Seq,
                CompCd = Data.CompCd,
                VendorCd = Data.VendorCd,
                Account = account
            };

            if (!_technicianGroupClaimsRepo.Add(con, technicianGroupClaims))
                throw new Exception("[ERROR]=>修改技師至群組時,新增失敗");

            return true;
        }

        public Boolean CreateVendorGroup(UserBase data)
        {


            #region 該廠商底下所有技師

            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Comp_Cd == data.CompCd && x.Vender_Cd == data.VenderCd);

            var query = _tvenderRepo.GetList(con);

            var accounts = new List<string>();

            query?.ForEach(x =>
            {
                accounts.Add(x.Account);
            });

            #endregion

            var model = new TtechnicianGroup()
            {

                CompCd = data.CompCd,
                VendorCd = data.VenderCd,
                GroupName = data.VenderName,
                //IsAutoPush = false,
                //IsDefault = true
            };

            return this.CreateTechnicianGroup(model, accounts.ToArray());
        }

    }
}

