using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.AspnetMvc.Authentication.Service;
using Ptc.AspnetMvc.Filter;
using Ptc.AspnetMvc.Models;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Factory;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{
    [AuthenticationFilter]
    public class NotifyAssignedController : BaseController
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TCALLOG, Tcallog> _tcallogRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _TvenderTechnicianRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _TTechnicianGroupRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> _TtechnicianGroupClaimsRepo;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _TzocodeRepo;
        private readonly IBaseRepository<DataBase.VW_MobileCallogNoFinish, MobileCallogSearch> _VWMobileCallogNoFinishRepo;
        private readonly ICallogService _callogService;

        public NotifyAssignedController(ISystemLog Logger,
                                        IBaseRepository<DataBase.TCALLOG, Tcallog> tcallogRepo,
                                        IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TvenderTechnicianRepo,
                                        IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TTechnicianGroupRepo,
                                        IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> TtechnicianGroupClaimsRepo,
                                        IBaseRepository<DataBase.TZOCODE, Tzocode> TzocodeRepo,
                                        IBaseRepository<DataBase.VW_MobileCallogNoFinish, MobileCallogSearch> VWMobileCallogNoFinishRepo,
                                        ICallogService CallogService)
        {
            _logger = Logger;
            _tcallogRepo = tcallogRepo;
            _TvenderTechnicianRepo = TvenderTechnicianRepo;
            _TTechnicianGroupRepo = TTechnicianGroupRepo;
            _TtechnicianGroupClaimsRepo = TtechnicianGroupClaimsRepo;
            _TzocodeRepo = TzocodeRepo;
            _VWMobileCallogNoFinishRepo = VWMobileCallogNoFinishRepo;
            _callogService = CallogService;
        }

        [MenuNode(
        Title = "通知、指派",
        Description = "通知、指派",
        PrefixedNodeID = "unFinishCaseManagement",
        isEntry = true,
        AuthType = AuthNodeType.All)]
        public ActionResult Index()
        {
            return View();
        }

        [MenuNode(
        Title = "取得案件-列表",
        Description = "取得案件-列表",
        AuthType = AuthNodeType.Read)]
        public ActionResult Getcallog(DataTablesReqModel<List<NotifyAssignedViewModel>> data)
        {
            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            if (_user.CompCd == "")
                _user.CompCd = "711";
            DataTablesRespModel result = new DataTablesRespModel(data.draw);
            try
            {
                #region 取得所有區域
                Conditions<DataBase.TZOCODE> conZo = new Conditions<DataBase.TZOCODE>();
                conZo.And(x => x.Comp_Cd == _user.CompCd); //公司別
                var Zo = _TzocodeRepo.GetList(conZo).ToList();
                #endregion

                #region 取得未完修案件
                Conditions<DataBase.VW_MobileCallogNoFinish> conCallog = new Conditions<DataBase.VW_MobileCallogNoFinish>();
                conCallog.And(x => x.Comp_Cd == _user.CompCd);     //公司別
                conCallog.And(x => x.Vender_Cd == _user.VenderCd); //廠商
                conCallog.And(x => x.Close_Sts == (byte)CloseSts.process); //剛立案
                conCallog.And(x => x.TimePoint == (byte)TimePoint.Dispatch); //timepoint=1 
                data.order?.ForEach(x =>
                {
                    if (data.columns[x.column].name == "Zo_Name")
                        conCallog.Order(x.dir, "Z_O");
                    else if (data.columns[x.column].name == "Do_Name")
                        conCallog.Order(x.dir, "D_O");
                    else
                        conCallog.Order(x.dir, data.columns[x.column].name);
                });
                var Data = _VWMobileCallogNoFinishRepo.GetList(conCallog);
                #endregion
                int PageIndex = (data.start / data.length);
                PagedList<MobileCallogSearch> meta = new PagedList<MobileCallogSearch>(Data, PageIndex, data.length);
                result.data = meta.Select(x => new NotifyAssignedViewModel(x, 
                                        Zo.Where(y => y.ZoCd == x.Zo && y.DoCd=="").Select(z => z.ZoName).FirstOrDefault(), 
                                        Zo.Where(y => y.ZoCd == x.Zo && y.DoCd == x.Do).Select(z => z.DoName).FirstOrDefault()).colData).ToArray();

                result.TotalCount(conCallog.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                result.error = ex.Message;
            }
            return Json(result);
        }

        [MenuNode(
        Title = "取得技師-列表",
        Description = "取得技師-列表",
        AuthType = AuthNodeType.Read)]
        public ActionResult GetTechnician(DataTablesReqModel<List<NotifyAssignedViewModel>> data)
        {
            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            if (_user.CompCd == "")
                _user.CompCd = "711";
            DataTablesRespModel result = new DataTablesRespModel(data.draw);
            try
            {
                Conditions<DataBase.TVenderTechnician> conTechnician = new Conditions<DataBase.TVenderTechnician>();
                conTechnician.And(x => x.Comp_Cd == _user.CompCd);      //公司別
                conTechnician.And(x => x.Vender_Cd == _user.VenderCd);  //廠商
                conTechnician.And(x => x.Enable == true);               //狀態：啟用
                var Data = _TvenderTechnicianRepo.GetList(conTechnician);
                PagedList<TvenderTechnician> meta = new PagedList<TvenderTechnician>(Data);
                result.data = meta.Select(x => new NotifyAssignedViewModel(x).colData).ToArray();
                result.TotalCount(conTechnician.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                result.error = ex.Message;
            }
            return Json(result);
        }

        [MenuNode(
        Title = "取得群組-列表",
        Description = "取得群組-列表",
        AuthType = AuthNodeType.Read)]
        public ActionResult GetGroup(DataTablesReqModel<List<NotifyAssignedViewModel>> data)
        {
            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            if (_user.CompCd == "")
                _user.CompCd = "711";
            DataTablesRespModel result = new DataTablesRespModel(data.draw);
            try
            {
                Conditions<DataBase.TTechnicianGroup> conGroup = new Conditions<DataBase.TTechnicianGroup>();
                conGroup.And(x => x.CompCd == _user.CompCd);       //公司別
                conGroup.And(x => x.VendorCd == _user.VenderCd);   //廠商
                var Data = _TTechnicianGroupRepo.GetList(conGroup);
                PagedList<TtechnicianGroup> meta = new PagedList<TtechnicianGroup>(Data);
                result.data = meta.Select(x => new NotifyAssignedViewModel(x).colData).ToArray();
                result.TotalCount(conGroup.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                result.error = ex.Message;
            }
            return Json(result);
        }
        [MenuNode(
        Title = "選擇技師發出通知",
        Description = "選擇技師發出通知",
        AuthType = AuthNodeType.Read)]
        public ActionResult TechnicianNotify(string[] Technician, string Sn)
        {
            try
            {
                if (Technician == null)
                    throw new Exception("未選擇推播技師");
                if (Sn == string.Empty)
                    throw new Exception("未選擇案件");

                string[] CallogSn = Sn.Split(',');
                var _user = ((PtcIdentity)this.User.Identity).currentUser;
                if (_user.CompCd == "")
                    _user.CompCd = "711";
                List<string> NotifySn = new List<string>(); //Sn
                Dictionary<string, string> Account = new Dictionary<string, string>(); //key：技師帳號、value：技師RegId

                #region 驗證技師資料
                Conditions<DataBase.TVenderTechnician> conTechnician = new Conditions<DataBase.TVenderTechnician>();
                foreach (string itemTechnician in Technician)
                {
                    _logger.Info($"廠商：{_user.VenderCd}，開始驗證技師資料，被驗證的技師有{itemTechnician}");
                    conTechnician.And(x => x.Comp_Cd == _user.CompCd);            //公司別
                    conTechnician.And(x => x.Vender_Cd == _user.VenderCd);        //廠商
                    conTechnician.And(x => x.Enable == true);                    //啟用
                    conTechnician.And(x => x.Account == itemTechnician);            //帳號
                    TvenderTechnician data = _TvenderTechnicianRepo.Get(conTechnician);
                    if (data == null)
                        _logger.Info($"查無技師資料:{itemTechnician}");
                    else
                    {
                        _logger.Info($"加入推播，帳號：{itemTechnician}");
                        Account.Add(itemTechnician, data.RegistrationID);
                    }
                    conTechnician = new Conditions<DataBase.TVenderTechnician>();
                }
                #endregion

                #region 檢查叫修編號狀態
                Conditions<DataBase.TCALLOG> conCallog = new Conditions<DataBase.TCALLOG>();
                foreach (string itemSn in CallogSn)
                {
                    _logger.Info($"廠商：{_user.VenderCd}，開始驗證案件資料，被驗證的案件有{itemSn}");
                    conCallog.And(x => x.Comp_Cd == _user.CompCd);
                    conCallog.And(x => x.Sn == itemSn);
                    conCallog.And(x => x.TAcceptedLog.Sn == null);
                    Tcallog data = _tcallogRepo.Get(conCallog);
                    if (data == null)
                        _logger.Info($"查無案件資料:{itemSn}(可能已經被認養)");
                    else if (data.CloseSts > (byte)CloseSts.process)
                        _logger.Info($"案件:{itemSn}，已經銷案。");
                    else
                    {
                        _logger.Info($"加入推播，案件：{itemSn}");
                        NotifySn.Add(itemSn);
                    }
                    conCallog = new Conditions<DataBase.TCALLOG>();
                }
                #endregion

                if (Account.Count == 0)
                    throw new Exception("勾選的技師驗證後無資料");
                if (NotifySn.Count == 0)
                    throw new Exception("勾選的案件驗證後無資料，請重新整理");

                #region 更新待受理案件+推播
                var isSuccess = _callogService.NotificationForWeb(_user, NotifySn, Account);
                #endregion
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"推播通知:{(isSuccess ? "成功" : "失敗")}"
                    }

                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = false,
                        Message = $"推播通知失敗,原因:{ex.Message}"
                    }

                });
            }
        }
        [MenuNode(
        Title = "選擇群組發出通知",
        Description = "選擇群組發出通知",
        AuthType = AuthNodeType.Read)]
        public ActionResult GroupNotify(string Groupseq, string Sn)
        {
            try
            {
                if (Groupseq == string.Empty)
                    throw new Exception("未選擇群組");
                if (Sn == string.Empty)
                    throw new Exception("未選擇案件");

                string[] CallogSn = Sn.Split(',');
                var _user = ((PtcIdentity)this.User.Identity).currentUser;
                if (_user.CompCd == "")
                    _user.CompCd = "711";
                List<string> NotifySn = new List<string>(); //Sn
                Dictionary<string, string> Account = new Dictionary<string, string>(); //key：技師帳號、value：技師RegId

                #region 檢查叫修編號狀態
                Conditions<DataBase.TCALLOG> conCallog = new Conditions<DataBase.TCALLOG>();
                foreach (string itemSn in CallogSn)
                {
                    _logger.Info($"廠商：{_user.VenderCd}，開始驗證案件資料，被驗證的案件有{itemSn}");
                    conCallog.And(x => x.Comp_Cd == _user.CompCd);
                    conCallog.And(x => x.Sn == itemSn);
                    conCallog.And(x => x.TAcceptedLog.Sn == null);
                    Tcallog data = _tcallogRepo.Get(conCallog);
                    if (data == null)
                        _logger.Info($"查無案件資料:{itemSn}(可能已經被認養)");
                    else if (data.CloseSts > (byte)CloseSts.process)
                        _logger.Info($"案件:{itemSn}，已經銷案。");
                    else
                    {
                        _logger.Info($"加入推播，案件：{itemSn}");
                        NotifySn.Add(itemSn);
                    }
                    conCallog = new Conditions<DataBase.TCALLOG>();
                }
                #endregion

                #region 群組取出技師資料
                _logger.Info($"廠商：{_user.VenderCd}，開始群組取出技師資料，群組ID：{Groupseq}");
                int seq = Convert.ToInt32(Groupseq);
                Conditions<DataBase.TTechnicianGroupClaims> conGroupClaims = new Conditions<DataBase.TTechnicianGroupClaims>();
                conGroupClaims.And(x => x.CompCd == _user.CompCd);
                conGroupClaims.And(x => x.VendorCd == _user.VenderCd);
                conGroupClaims.And(x => x.Seq == seq);
                conGroupClaims.Include(x => x.TVenderTechnician);
                var TechnicianList = _TtechnicianGroupClaimsRepo.GetList(conGroupClaims);
                TechnicianList.ForEach(account =>
                {
                    _logger.Info($"加入推播，帳號：{account.Account}");
                    Account.Add(account.Account, account.TVenderTechnician.RegistrationID);
                });
                #endregion

                if (Account.Count == 0)
                    throw new Exception("群組裡無技師");
                if (NotifySn.Count == 0)
                    throw new Exception("勾選的案件驗證後無資料，請重新整理");

                #region 更新待受理案件+推播
                var isSuccess = _callogService.NotificationForWeb(_user, NotifySn, Account);
                #endregion
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"推播通知:{(isSuccess ? "成功" : "失敗")}"
                    }

                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = false,
                        Message = $"推播通知失敗,原因:{ex.Message}"
                    }

                });
            }
        }
        
        [MenuNode(
        Title = "選擇技師進行指派",
        Description = "選擇技師進行指派",
        AuthType = AuthNodeType.Read)]
        public ActionResult TechnicianNotifyForAppoint(string[] Technician, string Sn)
        {
            try
            {
                if (Technician == null)
                    throw new Exception("未選擇推播技師");
                if (Sn == string.Empty)
                    throw new Exception("未選擇案件");

                string[] CallogSn = Sn.Split(',');
                var _user = ((PtcIdentity)this.User.Identity).currentUser;
                if (_user.CompCd == "")
                    _user.CompCd = "711";
                List<string> NotifySn = new List<string>(); //Sn
                //Dictionary<string, string> Account = new Dictionary<string, string>(); //key：技師帳號、value：技師RegId
                List<string> Account = new List<string>();
                #region 驗證技師資料
                Conditions<DataBase.TVenderTechnician> conTechnician = new Conditions<DataBase.TVenderTechnician>();
                foreach (string itemTechnician in Technician)
                {
                    _logger.Info($"廠商：{_user.VenderCd}，開始驗證技師資料，被驗證的技師有{itemTechnician}");
                    conTechnician.And(x => x.Comp_Cd == _user.CompCd);            //公司別
                    conTechnician.And(x => x.Vender_Cd == _user.VenderCd);        //廠商
                    conTechnician.And(x => x.Enable == true);                    //啟用
                    conTechnician.And(x => x.Account == itemTechnician);            //廠商帳號
                    TvenderTechnician data = _TvenderTechnicianRepo.Get(conTechnician);
                    if (data == null)
                    {
                        _logger.Info($"查無技師資料:{itemTechnician}");
                    }
                    else
                    {
                        _logger.Info($"加入推播，帳號：{itemTechnician}");
                        Account.Add(itemTechnician);
                        Account.Add(data.RegistrationID);
                        Account.Add(data.Name);
                    }
                    conTechnician = new Conditions<DataBase.TVenderTechnician>();
                }
                #endregion

                #region 檢查叫修編號狀態
                Conditions<DataBase.TCALLOG> conCallog = new Conditions<DataBase.TCALLOG>();
                foreach (string itemSn in CallogSn)
                {
                    _logger.Info($"廠商：{_user.VenderCd}，開始驗證案件資料，被驗證的案件有{itemSn}");
                    conCallog.And(x => x.Comp_Cd == _user.CompCd);
                    conCallog.And(x => x.Sn == itemSn);
                    conCallog.And(x => x.TAcceptedLog.Sn == null);
                    Tcallog data = _tcallogRepo.Get(conCallog);
                    if (data == null)
                        _logger.Info($"查無案件資料:{itemSn}(可能已經被認養)");
                    else if (data.CloseSts > (byte)CloseSts.process)
                        _logger.Info($"案件:{itemSn}，已經銷案。");
                    else
                    {
                        _logger.Info($"加入推播，案件：{itemSn}");
                        NotifySn.Add(itemSn);
                    }
                    conCallog = new Conditions<DataBase.TCALLOG>();
                }
                #endregion

                if (Account.Count == 0)
                    throw new Exception("勾選的技師驗證後無資料");
                if (NotifySn.Count == 0)
                    throw new Exception("勾選的案件驗證後無資料，請重新整理");

                #region 更新待受理案件+推播
                var isSuccess = _callogService.NotificationForAppoint(_user, NotifySn, Account);
                #endregion

                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"指派案件:{(isSuccess ? "成功" : "失敗")}"
                    }

                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = false,
                        Message = $"指派案件失敗,原因:{ex.Message}"
                    }
                });
            }
        }
    }
}