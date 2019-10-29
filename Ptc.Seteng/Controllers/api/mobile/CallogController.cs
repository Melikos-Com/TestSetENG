using Ptc.AspnetMvc.Filter;
using Ptc.AspnetMvc.Models;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Factory;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Provider;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ptc.Seteng.Controllers.api
{
    /// <summary>
    /// 案件相關
    /// </summary>
    [AllowAnonymous]
    public class CallogController : ApiController
    {

        private readonly ISystemLog _logger;
        private readonly ICallogService _callogService;
        private readonly ITcallogProvider _callogProvider;
        private readonly ICallogFactory _callogFactory;
        private readonly IImgRepository _ImgRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _venderRepo;
        private readonly IBaseRepository<DataBase.TCALLOG, Tcallog> _callogRepo;
        private readonly IBaseRepository<DataBase.VW_MobileCallogSearch, MobileCallogSearch> _VWMobileCallogRepo;
        private readonly IBaseRepository<DataBase.VW_MobileCallogNoFinish, MobileCallogSearch> _VWMobileCallogNoFinishRepo;
        private readonly IBaseRepository<DataBase.TDAMMTN, Tdammtn> _TdammtnRepo;
        private readonly IBaseRepository<DataBase.TFINISH, Tfinish> _TfinishRepo;
        private readonly IBaseRepository<DataBase.TWRKKND, Twrkknd> _TwrkkndRepo;
        private readonly IBaseRepository<DataBase.TCallLogRecord, TcallogRecord> _RecardRepo;
        private readonly IBaseRepository<DataBase.TCallogCourse, TCallogCourse> _CallogCourseRepo;
        private readonly IBaseRepository<DataBase.TREFDAT, Trefdat> _TrefdatRepo;
        private readonly IBaseRepository<DataBase.TCallLogDateRecord, TCallLogDateRecord> _TCallLogDateRecordRep;
        private readonly IBaseRepository<DataBase.TCALINV, TCALINV> _CALINVRepo;
        private readonly IBaseRepository<DataBase.TASSETS, Tassets> _AssetRepo;




        public CallogController(ISystemLog Logger,
                                ICallogService CallogService,
                                ITcallogProvider CallogProvider,
                                ICallogFactory CallogFactory,
                                IImgRepository ImgRepo,
                                IBaseRepository<DataBase.TVENDER, Tvender> VenderRepo,
                                IBaseRepository<DataBase.TCALLOG, Tcallog> CallogRepo,
                                IBaseRepository<DataBase.VW_MobileCallogSearch, MobileCallogSearch> VWMobileCallogRepo,
                                IBaseRepository<DataBase.VW_MobileCallogNoFinish, MobileCallogSearch> VWMobileCallogNoFinishRepo,
                                IBaseRepository<DataBase.TDAMMTN, Tdammtn> TdammtnRepo,
                                IBaseRepository<DataBase.TFINISH, Tfinish> TfinishRepo,
                                IBaseRepository<DataBase.TWRKKND, Twrkknd> TwrkkndRepo,
                                IBaseRepository<DataBase.TCallLogRecord, TcallogRecord> RecardRepo,
                                IBaseRepository<DataBase.TCallogCourse, TCallogCourse> CallogCourseRepo,
                                IBaseRepository<DataBase.TREFDAT, Trefdat> TrefdatRepo,
                                IBaseRepository<DataBase.TCallLogDateRecord, TCallLogDateRecord> TCallLogDateRecordRepo,
                                IBaseRepository<DataBase.TCALINV, TCALINV> CALINVRepo,
                                 IBaseRepository<DataBase.TASSETS, Tassets> AssetRepo)
        {
            _logger = Logger;
            _callogRepo = CallogRepo;
            _CALINVRepo = CALINVRepo;
            _venderRepo = VenderRepo;
            _callogFactory = CallogFactory;
            _callogService = CallogService;
            _callogProvider = CallogProvider;
            _VWMobileCallogRepo = VWMobileCallogRepo;
            _VWMobileCallogNoFinishRepo = VWMobileCallogNoFinishRepo;
            _TdammtnRepo = TdammtnRepo;
            _TfinishRepo = TfinishRepo;
            _TwrkkndRepo = TwrkkndRepo;
            _ImgRepo = ImgRepo;
            _RecardRepo = RecardRepo;
            _CallogCourseRepo = CallogCourseRepo;
            _TrefdatRepo = TrefdatRepo;
            _TCallLogDateRecordRep = TCallLogDateRecordRepo;
            _AssetRepo = AssetRepo;
        }

        #region 廠商

        /// <summary>
        /// 自動通知
        /// 通常於立案完成時呼叫
        /// </summary>
        /// <param name="Sn"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage AutoNotification(string Comp_Cd, string Sn)
        {

            try
            {
                if (string.IsNullOrEmpty(Sn) || string.IsNullOrEmpty(Comp_Cd))
                    throw new Exception($"沒有輸入對應案件編號");

                if (!_callogService.AutoNotification(Comp_Cd, Sn))
                    throw new Exception($"通知失敗");

                return Request.CreateResponse(
                       HttpStatusCode.OK,
                       new JsonResult<Boolean>(true, "通知成功", 1, true));

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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

        }
        /// <summary>
        /// 廠商通知技師可受理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage VenderNotification(CallogDetailApiViewModel data)
        {
            try
            {
                if (data == null || data.Accounts.Count == 0)
                    throw new ArgumentNullException($"通知失敗");

                #region 組合物件

                Tcallog callog = new Tcallog()
                {
                    Sn = data.Sn,
                    CompCd = data.CompCd,
                };

                List<string> accounts = data.Accounts
                                            .Select(x => x.Account)
                                            .ToList();

                #endregion

                if (!_callogService.VenderNotification(callog, accounts))
                    throw new Exception("通知失敗");

                return Request.CreateResponse(
                       HttpStatusCode.OK,
                       new JsonResult<Boolean>(true, "通知成功", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

        }
        /// <summary>
        /// 廠商指派案件/技師認養案件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage TechnicianAccept(CallogDetailApiViewModel data)
        {
            try
            {
                //取得user資訊
                var user = ((PtcIdentity)this.User.Identity).currentUser;
                if (data == null || string.IsNullOrEmpty(data.AcceptedAccount))
                {
                    if (data.IsVndAssign) //指派案件
                        throw new ArgumentNullException($"指派失敗");
                    else //技師認養案件 
                        throw new ArgumentNullException($"認養失敗");
                }


                #region 組合物件

                Tcallog callog = new Tcallog()
                {
                    Sn = data.Sn,
                    CompCd = data.CompCd,
                };

                #endregion

                if (!_callogService.TechnicianAccept(callog, data.AcceptedAccount, data.IsVndAssign, user.UserName))
                {
                    if (data.IsVndAssign) //指派案件
                        throw new Exception("指派失敗");
                    else //技師認養案件
                        throw new Exception("認養失敗");
                }



                if (data.IsVndAssign) //指派案件
                    return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new JsonResult<Boolean>(true, "指派完成", 1, true));
                else //技師認養案件
                    return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new JsonResult<Boolean>(true, "認養完成", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new JsonResult<TechnicianResultApiViewModel>(null, ex.Message, 1, false));
            }

        }
        /// <summary>
        /// 廠商改派案件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage VendorChangeLog(CallogDetailApiViewModel data)
        {
            try
            {
                //取得user資訊
                var user = ((PtcIdentity)this.User.Identity).currentUser;
                if (data == null || string.IsNullOrEmpty(data.AcceptedAccount))
                    throw new ArgumentNullException($"改派失敗");
                #region 組合物件

                Tcallog callog = new Tcallog()
                {
                    Sn = data.Sn,
                    CompCd = data.CompCd,
                };

                #endregion



                if (!_callogService.VendorChangeLog(callog, data.AcceptedAccount, user.UserName))
                    throw new Exception("改派失敗");

                return Request.CreateResponse(
                HttpStatusCode.OK,
                new JsonResult<Boolean>(true, "改派成功", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

        }


        /// <summary>
        /// 銷案
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage VendorConfirm(CallogDetailApiViewModel data)
        {
            try
            {

                if (data == null)
                    throw new ArgumentNullException($"[廠商]確認了結時,並未給入參數");

                var user = ((PtcIdentity)this.User.Identity).currentUser;
                string dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                #region 組合物件

                //更新資料內容
                Tcallog callog = new Tcallog()
                {
                    CompCd = data.CompCd,
                    Sn = data.Sn,
                    ArriveDate = data.ArriveDate,
                    FcDate = dt,
                    TimePoint = (int)TimePoint.Finish,
                    ImgBeforeFix = data.ImgBeforeFix,
                    ImgAfterFix = data.ImgAfterFix,
                    Img = data.Img,
                    ImgSignature = data.ImgSignature,
                    WorkId = data.TypeWorkId,
                    FinishId = data.TypeFinishId,
                    DamageProcNo = data.TypePorcnoId,
                    WorkDesc = data.FixMark,
                    CoffeeCup = Convert.ToInt32(data.CoffeeCup),
                    CloseSts = (int)CloseSts.Phone,
                    AcceptedName = data.AcceptedName,
                    VndEngId = data.AcceptedAccount,
                    AppCloseDate = dt,
                    UpdateUser = data.AcceptedAccount,
                    Updatedate = dt
                };

                #endregion

                #region 更新資料

                #region 驗證與取得資訊

                //取得案件
                var con = new Conditions<DataBase.TCALLOG>();
                con.And(x => x.Comp_Cd == data.CompCd && x.Sn == data.Sn);
                con.Include(x => x.TAcceptedLog);
                con.Include(x => x.TCallLogDateRecord);
                Tcallog meta = _callogRepo.Get(con);

                _logger.Info($"案件銷案-公司別:{meta.CompCd}");
                _logger.Info($"案件銷案-廠商別:{meta.VenderCd}");

                //已被改派
                if (meta.TacceptedLog.Name != user.UserName)
                    throw new IndexOutOfRangeException($"此案件已被改派");
                //如果已經銷案不允許再指通知了
                if (meta.CloseSts > (byte)CloseSts.process)
                    throw new IndexOutOfRangeException($"此案件已銷案");

                callog.AssetCd = meta.AssetCd;
                #endregion

                /////因應SISO流程進行物件組合及驗證
                //if (IsSISOVender(user.VenderCd))
                callog = SetSISOCallog(data, meta, callog);

                if (!_callogService.VendorConfirm(callog))
                    throw new Exception("銷案失敗");

                #endregion


                return Request.CreateResponse(
                HttpStatusCode.OK,
                new JsonResult<Boolean>()
                {
                    isSuccess = true,
                    message = string.Format("銷案成功")
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

        }
        /// <summary>
        /// 暫存工單
        /// </summary> 
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage ScratchOfVndCfm(CallogDetailApiViewModel data)
        {
            try
            {
                if (data == null)
                    throw new ArgumentNullException($"暫存失敗");

                var user = ((PtcIdentity)this.User.Identity).currentUser;

                #region 組合物件
                //int CoffeeCup = 0;
                //Int32.TryParse(data.CoffeeCup, out CoffeeCup);
                //更新資料內容
                Tcallog callog = new Tcallog()
                {
                    CompCd = data.CompCd,
                    Sn = data.Sn,
                    ArriveDate = data.ArriveDate,
                    TimePoint = data.ArriveDate == null ? (int)TimePoint.Accepted : (int)TimePoint.ArriveStore,
                    ImgBeforeFix = data.ImgBeforeFix,
                    ImgAfterFix = data.ImgAfterFix,
                    Img = data.Img,
                    ImgSignature = new List<string>(),
                    WorkDesc =string.IsNullOrEmpty( data.FixMark)?"":data.FixMark,                   
                    AcceptedName = data.AcceptedName,
                    UpdateUser = data.AcceptedAccount,
                    Updatedate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),                 
                };
                if (!string.IsNullOrEmpty(data.CoffeeCup) && Int16.TryParse(data.CoffeeCup,out short CoffeeCup))                   
                    callog.CoffeeCup = CoffeeCup;


                #endregion

                #region 更新資訊

                #region 驗證與取得資訊

                //取得案件
                var con = new Conditions<DataBase.TCALLOG>();
                con.And(x => x.Comp_Cd == data.CompCd && x.Sn == data.Sn);
                con.Include(x => x.TAcceptedLog);
                con.Include(x => x.TCallLogDateRecord);
                Tcallog meta = _callogRepo.Get(con);

                _logger.Info($"案件銷案-公司別:{meta.CompCd}");
                _logger.Info($"案件銷案-廠商別:{meta.VenderCd}");

                //已被改派
                if (meta.TacceptedLog.Name != user.UserName)
                    throw new IndexOutOfRangeException($"此案件已被改派");
                //如果已經銷案不允許再指通知了
                if (meta.CloseSts > (byte)CloseSts.process)
                    throw new IndexOutOfRangeException($"此案件已銷案");

                callog.AssetCd = meta.AssetCd;
                #endregion

                /////因應SISO流程進行物件組合及驗證
                //if (IsSISOVender(user.VenderCd))
                callog = SetSISOCallog(data, meta, callog);

                if (!_callogService.VendorScratch(callog))
                    throw new Exception("[廠商]暫存資料時,暫存失敗");

                #endregion

                return Request.CreateResponse(
                       HttpStatusCode.OK,
                       new JsonResult<Boolean>()
                       {
                           isSuccess = true,
                           message = "紀錄成功",
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }
        }
        /// <summary>
        /// 取得案件數量
        /// </summary>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage GetVendorGetNewsCount()
        {
            try
            {

                var user = ((PtcIdentity)this.User.Identity).currentUser;

                Tuple<int, int, int> countMember = _callogService.VendorNewsCount(user);

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<CallogCountApiViewModel>()
                   {
                       element = new CallogCountApiViewModel(countMember),
                       isSuccess = true,
                       message = "取得數量成功",
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }


        }
        /// <summary>
        /// 案件歷程
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetCallogHistory(string CompCd, string Sn)
        {
            try
            {
                var con = new Conditions<DataBase.TCallogCourse>();
                con.And(x => x.Comp_Cd == CompCd);
                con.And(x => x.Sn == Sn);
                con.Order(OrderType.Desc, x => x.Datetime);
                PagedList<TCallogCourse> meta = _CallogCourseRepo.GetList(con);
                IEnumerable<CallogCourse> resault = meta.Select(x => new CallogCourse(x));
                return Request.CreateResponse(
                 HttpStatusCode.OK,
                 new JsonResult<IEnumerable<CallogCourse>>(resault, "取得成功", 1, true));

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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }
        }
        /// <summary>
        /// 推播紀錄查詢
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetRecard(string account)
        {
            try
            {
                var con = new Conditions<DataBase.TCallLogRecord>();
                con.And(x => x.Account == account);
                string Snow = DateTime.Now.AddDays(-1).ToShortDateString();
                string Enow = DateTime.Now.AddDays(1).ToShortDateString();
                DateTime Sdate = Convert.ToDateTime(Snow);
                DateTime Edate = Convert.ToDateTime(Enow);
                con.And(x => x.RecordDatetime >= Sdate);
                con.And(x => x.RecordDatetime < Edate);
                con.Order(OrderType.Desc, x => x.RecordDatetime);
                PagedList<TcallogRecord> meta = _RecardRepo.GetList(con);
                IEnumerable<PushRecord> resault = meta.Select(x => new PushRecord(x));
                return Request.CreateResponse(
                 HttpStatusCode.OK,
                 new JsonResult<IEnumerable<PushRecord>>(resault, "取得成功", 1, true));

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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }
        }
        /// <summary>
        /// 催修通知
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage PushUrg(string Comp, string Sn)
        {
            try
            {
                var con = new Conditions<DataBase.TCALLOG>();
                con.And(x => x.Comp_Cd == Comp && x.Sn == Sn);
                Tcallog meta = _callogRepo.Get(con);
                if (meta == null)
                    throw new Exception("查無案件");

                if (!_callogService.PushUrg(meta))
                    throw new Exception("催修通知失敗");

                return Request.CreateResponse(HttpStatusCode.OK,
                         new JsonResult<bool>(true, "催修通知成功", 1, true));

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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }
        }
        /// <summary>
        /// 銷案通知
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage PushFinish(string Comp, string Sn)
        {
            try
            {
                var con = new Conditions<DataBase.TCALLOG>();
                con.And(x => x.Comp_Cd == Comp && x.Sn == Sn);
                Tcallog meta = _callogRepo.Get(con);
                if (meta == null)
                    throw new Exception("查無案件");

                if (!_callogService.PushFinish(meta))
                    throw new Exception("銷案通知失敗");

                return Request.CreateResponse(HttpStatusCode.OK,
                         new JsonResult<bool>(true, "銷案通知成功", 1, true));

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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }
        }
        #endregion

        #region "未完修案件管理"


        /// <summary>
        /// 未完修案件管理清單
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage GetVenderManagerCallog(CallogApiViewModel data)
        {
            try
            {
                //取得user清單
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.VW_MobileCallogNoFinish>(data.pageSize, data.page);

                #region 查詢條件 

                //固定狀態(正在執行中的-[技師尚未結案])
                con.And(x => x.Close_Sts == (byte)CloseSts.process);
                con.And(x => x.TimePoint >= (byte)TimePoint.Dispatch);
                con.And(x => x.TimePoint < (byte)TimePoint.Finish);

                con.And(x => x.Comp_Cd == data.CompCd);
                con.And(x => x.Vender_Cd == data.VenderCd);

                //修改日期
                if (data.FiDateStart.HasValue)
                {
                    data.FiDateStart = data.FiDateStart.Value.Date;
                    con.And(x => x.Fi_Date >= data.FiDateStart.Value);

                }
                if (data.FiDateEnd.HasValue)
                {
                    data.FiDateEnd = data.FiDateEnd.Value.Date.AddDays(1);
                    con.And(x => x.Fi_Date < data.FiDateEnd.Value);
                }

                if (data.StoreCd != null)
                    con.And(x => x.Store_Cd == data.StoreCd);
                if (data.StoreName != null)
                    con.And(x => x.Store_Name.Contains(data.StoreName));
                if (data.Sn != null)
                    con.And(x => x.Sn.Contains(data.Sn));
                if (data.CallLevel != null)
                    con.And(x => x.Call_Level == data.CallLevel);
                if (data.Timepoint.HasValue)
                    con.And(x => x.TimePoint == (int)data.Timepoint.Value);
                #endregion

                con.Order(OrderType.Desc, x => x.Call_Level);
                con.Order(OrderType.Asc, x => x.Fd_Date);

                PagedList<MobileCallogSearch> list = _VWMobileCallogNoFinishRepo.TakeCountGetList(con);

                if (!list.Any())
                {
                    return Request.CreateResponse(
                            HttpStatusCode.OK, new JsonResult<List<CallogResultApiViewModel>>()
                            {

                                isSuccess = true,
                                element = new List<CallogResultApiViewModel>(),
                                message = "無資料",
                                totalCount = con.TotalCount,
                            });

                }

                IEnumerable<CallogResultApiViewModel> result = list.Select(x => new CallogResultApiViewModel(x, true));

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<IEnumerable<CallogResultApiViewModel>>(result, "[廠商]查詢待改派案件成功", con.TotalCount, true));


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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

        }

        /// <summary>
        /// 取得案件明細(未完修)
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Sn"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage GetNoFinish(string token, string Sn)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(Sn))
                    throw new ArgumentException($"取得案件明細時,並未給入參數");

                //取得user資訊
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.VW_MobileCallogNoFinish>();

                con.And(x => x.Sn == Sn);
                con.And(x => x.Comp_Cd == "711");

                MobileCallogSearch meta = _VWMobileCallogNoFinishRepo.Get(con);

                if (meta == null)
                    throw new NullReferenceException($"無資料");



                return Request.CreateResponse(
                  HttpStatusCode.OK,
                  new JsonResult<CallogDetailApiViewModel>(new CallogDetailApiViewModel(meta), "取得單一案件成功", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");

            }
        }
        #endregion

        #region "待認養案件"

        /// <summary>
        /// 取得待認養案件清單
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage GetAwaitAdoptCallog(CallogApiViewModel data)
        {
            try
            {
                //取得user資訊
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.VW_MobileCallogSearch>(data.pageSize, data.page);


                //固定狀態(正在執行中的-[技師尚未銷案])
                con.And(x => x.Close_Sts == (byte)CloseSts.process);

                con.And(x => x.Comp_Cd == "711");
                con.And(x => x.Vender_Cd == user.VenderCd);
                con.And(x => x.TimePoint == (int)TimePoint.Dispatch);
                con.And(x => x.Account == user.UserId);
                con.And(x => x.AcceptSn == null);

                con.Order(OrderType.Desc, x => x.Call_Level);
                con.Order(OrderType.Asc, x => x.Fd_Date);

                PagedList<MobileCallogSearch> list = _VWMobileCallogRepo.GetList(con);


                if (!list.Any())
                {
                    return Request.CreateResponse(
                            HttpStatusCode.OK, new JsonResult<List<CallogResultApiViewModel>>()
                            {

                                isSuccess = true,
                                element = new List<CallogResultApiViewModel>(),
                                message = "無資料",
                                totalCount = con.TotalCount,
                            });

                }

                IEnumerable<CallogResultApiViewModel> result = list.Select(x => new CallogResultApiViewModel(x, false));

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<IEnumerable<CallogResultApiViewModel>>(result, "[廠商]查詢待受理案件成功", con.TotalCount, true));


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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }
        }
        /// <summary>
        /// 取得案件明細(待認養)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage GetByAccept(string token, string Sn)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(Sn))
                    throw new ArgumentException($"取得案件明細時,並未給入參數");

                //取得user資訊
                var user = ((PtcIdentity)this.User.Identity).currentUser;


                var con = new Conditions<DataBase.VW_MobileCallogSearch>();

                con.And(x => x.Sn == Sn);
                con.And(x => x.Comp_Cd == "711");
                con.And(x => x.TimePoint == (int)TimePoint.Dispatch);
                con.And(x => x.Account == user.UserId);
                con.And(x => x.AcceptSn == null);
                MobileCallogSearch meta = _VWMobileCallogRepo.Get(con);

                if (meta == null)
                    throw new NullReferenceException($"無資料");



                return Request.CreateResponse(
                  HttpStatusCode.OK,
                  new JsonResult<CallogDetailApiViewModel>(new CallogDetailApiViewModel(meta), "取得單一案件成功", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");

            }
        }
        #endregion

        #region "已認養案件"

        /// <summary>
        /// 取得已認養清單
        /// </summary>
        /// <param name="EngName"></param>
        /// <param name="CompCd"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage GetHasTechnicianCallog(CallogApiViewModel data)
        {

            try
            {
                //取得user資訊
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.VW_MobileCallogSearch>(data.pageSize, data.page);

                //固定狀態(正在執行中的-[技師尚未銷案])
                con.And(x => x.Close_Sts == (byte)CloseSts.process);

                con.And(x => x.Comp_Cd == "711"); //使用者帳號
                con.And(x => x.Vender_Cd == user.VenderCd);
                con.And(x => x.AcceptAccount == user.UserId);
                con.And(x => x.TimePoint >= (int)TimePoint.Accepted);
                con.And(x => x.TimePoint < (int)TimePoint.Finish);

                con.Order(OrderType.Desc, x => x.Call_Level);
                con.Order(OrderType.Asc, x => x.Fd_Date);

                PagedList<MobileCallogSearch> list = _VWMobileCallogRepo.GetList(con);

                if (!list.Any())
                {
                    return Request.CreateResponse(
                            HttpStatusCode.OK, new JsonResult<List<CallogResultApiViewModel>>()
                            {

                                isSuccess = true,
                                element = new List<CallogResultApiViewModel>(),
                                message = "無資料",
                                totalCount = con.TotalCount,
                            });

                }

                IEnumerable<CallogResultApiViewModel> result = list.Select(x => new CallogResultApiViewModel(x, true));

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<IEnumerable<CallogResultApiViewModel>>(result, "[廠商]查詢未銷案件成功", con.TotalCount, true));


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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }


        }

        /// <summary>
        /// 取得案件明細(已認養)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage GetByConfirm(string token, string Sn)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(Sn))
                    throw new ArgumentException($"取得案件明細時,並未給入參數");


                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.VW_MobileCallogSearch>();

                con.And(x => x.Sn == Sn);
                con.And(x => x.Comp_Cd == "711");
                con.And(x => x.AcceptAccount == user.UserId);
                con.And(x => x.TimePoint >= (int)TimePoint.Accepted);
                con.And(x => x.TimePoint < (int)TimePoint.Finish);

                MobileCallogSearch meta = _VWMobileCallogRepo.Get(con);

                if (meta == null)
                    throw new NullReferenceException($"無資料");

                var conTwr = new Conditions<DataBase.TWRKKND>();
                conTwr.And(x => x.Comp_Cd == meta.CompCd);
                conTwr.And(x => x.Work_Sts == 0);
                PagedList<Twrkknd> ListTwr = _TwrkkndRepo.GetList(conTwr);

                var conTfi = new Conditions<DataBase.TFINISH>();
                conTfi.And(x => x.Comp_Cd == meta.CompCd);
                conTfi.And(x => x.Finish_Sts == 0);
                PagedList<Tfinish> ListTfi = _TfinishRepo.GetList(conTfi);

                var conTda = new Conditions<DataBase.TDAMMTN>();
                conTda.And(x => x.Comp_Cd == meta.CompCd);
                conTda.And(x => x.Asset_Cd == meta.AssetCd);
                //conTda.And(x => x.Count_Sts == "Y");
                PagedList<Tdammtn> ListTda = _TdammtnRepo.GetList(conTda);

                IEnumerable<string> BeforeFix = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.BeforeFix);

                IEnumerable<string> AfterFix = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.AfterFix);

                IEnumerable<string> WorkOrder = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.Workorder);

                IEnumerable<string> Signature = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.Signature);

                string conAsset = _ImgRepo.GetSpcAssetKind(meta.CompCd, meta.AssetCd);

                //取得案件時間歷程記錄
                var conCDR = new Conditions<DataBase.TCallLogDateRecord>();
                conCDR.And(x => x.Comp_Cd == meta.CompCd);
                conCDR.And(x => x.SN == meta.Sn);
                PagedList<TCallLogDateRecord> dateRecords = _TCallLogDateRecordRep.GetList(conCDR);
                Dictionary<RecordType, List<TCallLogDateRecord>> dateRecordDics = dateRecords != null ? dateRecords.GroupBy(x => x.RecordType).ToDictionary(x => x.Key, x => x.ToList()) : null;


                return Request.CreateResponse(
                  HttpStatusCode.OK,
                  new JsonResult<CallogDetailApiViewModel>(new CallogDetailApiViewModel(meta, ListTwr, ListTfi, ListTda, BeforeFix, AfterFix, WorkOrder, Signature, conAsset, dateRecordDics), "取得單一案件成功", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");

            }
        }
        #endregion

        #region "叫修案件查詢"

        /// <summary>
        /// 取得案件清單(叫修案件查詢)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [TokenAuthenticationFilter]
        public HttpResponseMessage GetList(CallogApiViewModel data)
        {
            try
            {
                if (data == null)
                    throw new ArgumentNullException($"案件查詢時,並未給入參數");

                var con = new Conditions<DataBase.VW_MobileCallogNoFinish>(data.pageSize, data.page);

                con.And(x => x.Comp_Cd == data.CompCd);
                con.And(x => x.Vender_Cd == data.VenderCd);
                con.And(x => x.TimePoint >= (int)TimePoint.Dispatch);
                //修改日期
                if (data.FiDateStart.HasValue)
                {
                    data.FiDateStart = data.FiDateStart.Value.Date;
                    con.And(x => x.Fi_Date >= data.FiDateStart.Value);

                }
                if (data.FiDateEnd.HasValue)
                {
                    data.FiDateEnd = data.FiDateEnd.Value.Date.AddDays(1);
                    con.And(x => x.Fi_Date < data.FiDateEnd.Value);
                }

                if (data.StoreCd != null)
                    con.And(x => x.Store_Cd == data.StoreCd);
                if (data.StoreName != null)
                    con.And(x => x.Store_Name.Contains(data.StoreName));
                if (data.Sn != null)
                    con.And(x => x.Sn.Contains(data.Sn));
                if (data.CallLevel != null)
                    con.And(x => x.Call_Level == data.CallLevel);
                if (data.Timepoint.HasValue)
                    con.And(x => x.TimePoint == (int)data.Timepoint.Value);

                con.Order(data.OrderType, x => x.Sn);

                PagedList<MobileCallogSearch> list = _VWMobileCallogNoFinishRepo.TakeCountGetList(con);


                if (!list.Any())
                {
                    return Request.CreateResponse(
                          HttpStatusCode.OK, new JsonResult<List<CallogResultApiViewModel>>()
                          {

                              isSuccess = true,
                              element = new List<CallogResultApiViewModel>(),
                              message = "無資料",
                              totalCount = con.TotalCount,
                          });
                }


                IEnumerable<CallogResultApiViewModel> result = list.Select(x => new CallogResultApiViewModel(x, true));


                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<IEnumerable<CallogResultApiViewModel>>(result, "查詢案件成功", con.TotalCount, true));



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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

        }

        /// <summary>
        /// 取得案件明細(案件查詢)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage GetBySearch(string token, string Sn)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(Sn))
                    throw new ArgumentException($"取得案件明細時,並未給入參數");



                var con = new Conditions<DataBase.VW_MobileCallogNoFinish>();

                con.And(x => x.Sn == Sn);
                con.And(x => x.Comp_Cd == "711");

                MobileCallogSearch meta = _VWMobileCallogNoFinishRepo.Get(con);

                if (meta == null)
                    throw new NullReferenceException($"無資料");
                IEnumerable<string> BeforeFix = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.BeforeFix);

                IEnumerable<string> AfterFix = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.AfterFix);

                IEnumerable<string> WorkOrder = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.Workorder);

                IEnumerable<string> Signature = _ImgRepo.SearchImg(meta.CompCd, meta.Sn, (int)ImgType.Signature);

                string AssetKind = _ImgRepo.GetSpcAssetKind(meta.CompCd, meta.AssetCd);

                return Request.CreateResponse(
                  HttpStatusCode.OK,
                  new JsonResult<CallogDetailApiViewModel>(new CallogDetailApiViewModel(meta, BeforeFix, AfterFix, WorkOrder, Signature, AssetKind), "取得單一案件成功", 1, true));
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
                _logger.Error(ex.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");

            }
        }
        #endregion


        //private Boolean IsSISOVender(string VenderCd)
        //{
        //    //確認廠商是否導入SISO流程
        //    var refcon = new Conditions<DataBase.TREFDAT>();
        //    refcon.And(x => x.Label_Cd == "SISOVender");
        //    refcon.And(x => x.Key_Cd == VenderCd);
        //    Trefdat refData = _TrefdatRepo.Get(refcon);

        //    return _TrefdatRepo.Get(refcon) != null ? true : false;
        //}

        /// <summary>
        /// 因應SISO流程調整組合物件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="callog"></param>
        /// <returns></returns>
        private Tcallog SetSISOCallog(CallogDetailApiViewModel data, Tcallog meta, Tcallog retCallog)
        {
            try
            {
                if (meta.TcallLogDateRecords != null)
                    retCallog.TcallLogDateRecords = meta.TcallLogDateRecords;
                else
                    retCallog.TcallLogDateRecords = new List<TCallLogDateRecord>();

                RecordType type = new RecordType();

                //到店時間值及類型確認
                type = data.IsStoreScanSI ? RecordType.ArriveDateTemp : RecordType.ArriveDateNotScan;
                retCallog.TcallLogDateRecords = setTCallLogDateRecord(retCallog.TcallLogDateRecords, data, type, data.ArriveDate);

                //門市簽名檔
                retCallog.ImgSignature = data.ImgSignature == null ? new List<string>() : data.ImgSignature;

                //門市簽名檔類型確認
                type = data.IsSignature ? RecordType.Signature : RecordType.SignatureNot;
                retCallog.TcallLogDateRecords = setTCallLogDateRecord(retCallog.TcallLogDateRecords, data, type, data.SignatureDate);

                //離店時間值及類型確認
                type = data.IsStoreScanSO ? RecordType.LeaveDateTemp : RecordType.LeaveDateNotScan;
                retCallog.TcallLogDateRecords = setTCallLogDateRecord(retCallog.TcallLogDateRecords, data, type, data.LeaveDate);


                //預估金額
                Int32 PreAmt = 0;
                if (!string.IsNullOrEmpty(data.Pre_Amt) && Int32.TryParse(data.Pre_Amt, out PreAmt) && PreAmt > 0)
                {
                    //確認是否已有既有的紀錄
                    var con = new Conditions<DataBase.TCALINV>();
                    con.And(x => x.Comp_Cd == meta.CompCd);
                    con.And(x => x.Sn == meta.Sn);
                    TCALINV TCALINV = _CALINVRepo.Get(con);

                    if (TCALINV == null)
                    {
                        var conAsset = new Conditions<DataBase.TASSETS>();
                        conAsset.And(x => x.Comp_Cd == meta.CompCd);
                        conAsset.And(x => x.Asset_Cd == meta.AssetCd);

                        Tassets assets = _AssetRepo.Get(conAsset);

                        if (assets != null)
                        {
                            retCallog.TCALINV = new TCALINV()
                            {
                                CompCd = data.CompCd,
                                Sn = data.Sn,
                                Invoice_No = "",
                                Invoice_Date = "",
                                Tot_Amt = 0,
                                Comp_Amt = 0,
                                Direct_Amt = 0,
                                Join_Amt = 0,
                                Trans_YM = 0,
                                Update_User = data.AcceptedName,
                                Update_Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                Work_Id =string.IsNullOrEmpty( data.TypeWorkId)?"": data.TypeWorkId,
                                Pre_Amt = int.Parse(data.Pre_Amt),
                                Create_User_Id = data.AcceptedAccount,
                                Debit_Kind = assets.Debit_Kind,
                                Acc_Type = assets.AstKind1 == "2" ? "2" : "1" //若設備不為營繕設備(Ast_kind1 != 2)，則設定為"1"
                            };
                        }
                        else
                            retCallog.TCALINV = null;
                    }
                    else
                    {
                        TCALINV.Work_Id = string.IsNullOrEmpty(data.TypeWorkId) ? "" : data.TypeWorkId;
                        TCALINV.Pre_Amt = int.Parse(data.Pre_Amt);
                        TCALINV.Update_User = data.AcceptedName;
                        TCALINV.Update_Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        retCallog.TCALINV = TCALINV;
                    }

                }

                return retCallog;
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
                _logger.Error(ex.StackTrace);
                return retCallog;
            }

        }

        private List<TCallLogDateRecord> setTCallLogDateRecord(List<TCallLogDateRecord> dateRecords, CallogDetailApiViewModel data, RecordType type, string RecordDate)
        {
            TCallLogDateRecord item = dateRecords.Where(x => x.RecordType == type).FirstOrDefault();
            if (!string.IsNullOrEmpty(RecordDate) && item == null)
            {
                item = new TCallLogDateRecord()
                {
                    CompCd = data.CompCd,
                    SN = data.Sn = data.Sn,
                    RecordType = type,
                    RecordDate = DateTime.Parse(RecordDate),
                    Create_User = data.AcceptedName,
                    Create_Date = DateTime.Now,
                };
                dateRecords.Add(item);
            }

            return dateRecords;
        }
    }
}