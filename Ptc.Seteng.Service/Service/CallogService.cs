using Ptc.AspnetMvc.Authentication;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Factory;
using Ptc.Seteng.Provider;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ptc.Seteng.Service
{
    public class CallogService : AttachedMethods, ICallogService
    {
        private readonly ISystemLog _logger;
        private readonly ICallogFactory _callogFactory;
        private readonly IVendorFactory _vendorFactory;
        private readonly IPushFactory _notifyFactory;
        private readonly ITechnicianProvider _technicianProvider;
        private readonly IImgRepository _ImgRepo;
        private readonly IMailFactory _MailFactory;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _venderRepo;
        private readonly IBaseRepository<DataBase.TCALLOG, Tcallog> _callogRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TVNDZO, Tvndzo> _vndzoRepo;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _zocodeRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> _technicianGroupClaimsRepo;
        private readonly IBaseRepository<DataBase.TCallogCourse, TCallogCourse> _CallogCourseRepo;
        private readonly IBaseRepository<DataBase.TSTRMST, Tstrmst> _storeRepo;
        private readonly IBaseRepository<DataBase.TCallLogDateRecord, TCallLogDateRecord> _DateRecordRepo;
        private readonly IBaseRepository<DataBase.TCALINV, TCALINV> _CALINVRepo;

        private string _url;

        public CallogService(ISystemLog Logger,
                             ICallogFactory CallogFactory,
                             IVendorFactory VendorFactory,
                             IPushFactory NotifyFactory,
                             ITechnicianProvider TechnicianProvider,
                             IImgRepository ImgRepo,
                             IBaseRepository<DataBase.TCMPDAT, Tcmpdat> CompRepo,
                             IBaseRepository<DataBase.TVENDER, Tvender> VenderRepo,
                             IBaseRepository<DataBase.TCALLOG, Tcallog> CallogRepo,
                             IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                             IBaseRepository<DataBase.TVNDZO, Tvndzo> vndzoRepo,
                             IBaseRepository<DataBase.TZOCODE, Tzocode> zocodeRepo,
                             IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> technicianGroupRepo,
                             IBaseRepository<DataBase.TTechnicianGroupClaims, TtechnicianGroupClaims> technicianGroupClaimsRepo,
                             IBaseRepository<DataBase.TCallogCourse, TCallogCourse> CallogCourseRepo,
                             IBaseRepository<DataBase.TSTRMST, Tstrmst> storeRepo,
                             IBaseRepository<DataBase.TCallLogDateRecord, TCallLogDateRecord> DateRecordRepo,
                             IBaseRepository<DataBase.TCALINV, TCALINV> CALINVRepo,
                             IMailFactory MailFactory)
                             : base(Logger, CallogRepo, CompRepo, VenderRepo, TechnicianRepo)
        {
            _logger = Logger;
            _callogRepo = CallogRepo;
            _vendorFactory = VendorFactory;
            _venderRepo = VenderRepo;
            _notifyFactory = NotifyFactory;
            _callogFactory = CallogFactory;
            _technicianRepo = TechnicianRepo;
            _technicianProvider = TechnicianProvider;
            _url = ServerProfile.GetInstance().CALLOG_PATH;
            _vndzoRepo = vndzoRepo;
            _zocodeRepo = zocodeRepo;
            _technicianGroupRepo = technicianGroupRepo;
            _technicianGroupClaimsRepo = technicianGroupClaimsRepo;
            _ImgRepo = ImgRepo;
            _CallogCourseRepo = CallogCourseRepo;
            _storeRepo = storeRepo;
            _MailFactory = MailFactory;
            DateRecordRepo = _DateRecordRepo;
            _CALINVRepo = CALINVRepo;
        }

        /// <summary>
        /// 自動通知技師:
        /// 通常在立案當下呼叫的
        /// </summary>
        /// <param name="Comp_Cd"></param>
        /// <param name="Sn"></param>
        /// <returns></returns>
        public Boolean AutoNotification(string Comp_Cd, string Sn)
        {

            _logger.Info($"立案自動通知-公司別:{Comp_Cd},案件編號:{Sn}");

            #region 驗證與取得資訊

            //取得案件
            Tcallog callog = base.GetCallog(Comp_Cd, Sn);

            //取得廠商及底下的技師群組
            var venderCon = new Conditions<DataBase.TVENDER>();

            venderCon.And(x => x.Comp_Cd == callog.CompCd &&
                               x.Vender_Cd == callog.VenderCd);

            _logger.Info($"立案自動通知-公司別:{callog.CompCd}");
            _logger.Info($"立案自動通知-廠商別:{callog.VenderCd}");


            venderCon.Include(x => x.TTechnicianGroup
                                    .Select(g => g.TTechnicianGroupClaims
                                    .Select(y => y.TVenderTechnician)));

            Tvender vender = _venderRepo.Get(venderCon);

            if (vender == null)
            {
                _logger.Error($"查無廠商-廠商別:{callog.VenderCd}");
                throw new Exception($"查無廠商");

            }

            #endregion

            //找到可以被自動推播的群組
            IEnumerable<TtechnicianGroup> groups = vender.TTechnicianGroup;
            List<TtechnicianGroup> techGroup = new List<TtechnicianGroup>();
            techGroup.AddRange(groups.Where(x => x.Responsible_Do.Contains(callog.Do)).ToList());
            if (groups.Where(x => x.Responsible_Do.Contains(callog.Do)).Count() == 0)
            {
                //沒有對應的課群組才撈區群組
                techGroup.AddRange(groups.Where(x => x.Responsible_Zo.Contains(callog.Zo)).ToList());
            }
            techGroup = techGroup.Distinct().ToList();

            //取出群組內技師並過濾
            Dictionary<string, string> accounts = new Dictionary<string, string>();

            //確認廠商是否有建立技師
            var venderTech = new Conditions<DataBase.TVenderTechnician>();
            venderTech.And(x => x.Comp_Cd == callog.CompCd &&
                               x.Vender_Cd == callog.VenderCd);
            var technicians = _technicianRepo.GetList(venderTech);

            //有建立技師，沒有建立群組則自動建立進行推播


            if (technicians.Count != 0 && groups.ToList().Count == 0)
            {
                _logger.Error($"查無群組自動建立-廠商別:{callog.VenderCd}");
                //取得廠商所負責的區域
                var venderZO = new Conditions<DataBase.TVNDZO>();
                venderZO.And(x => x.Comp_Cd == callog.CompCd &&
                               x.Vender_Cd == callog.VenderCd);
                var vndzos = _vndzoRepo.GetList(venderZO);

                string ZO = "";
                string DO = "";

                vndzos?.ForEach(vndzo =>
                {
                    ZO += "," + vndzo.Zo;
                    //取得各區對應的課別
                    var ZOCODE = new Conditions<DataBase.TZOCODE>();
                    ZOCODE.And(x => x.Comp_Cd == vndzo.CompCd &&
                               x.Z_O == vndzo.Zo &&
                               x.Upkeep_Sts == "Y");
                    var zocodes = _zocodeRepo.GetList(ZOCODE);
                    zocodes?.ForEach(zocode =>
                    {
                        DO += "," + zocode.DoCd;
                    });
                });

                //新增群組
                var con = new Conditions<DataBase.TTechnicianGroup>();
                con.And(x => x.CompCd == callog.CompCd);
                con.And(x => x.VendorCd == callog.VenderCd);
                TtechnicianGroup TGroup = new TtechnicianGroup();
                TGroup.CompCd = callog.CompCd;
                TGroup.VendorCd = callog.VenderCd;
                TGroup.GroupName = "系統產生";
                TGroup.Responsible_Zo = ZO.Substring(1);
                TGroup.Responsible_Do = DO.Substring(1);

                if (!_technicianGroupRepo.Add(con, TGroup))
                {
                    throw new Exception("[ERROR]=>自動新增群組時,新增失敗");
                }
                else
                {
                    //重新取得群組
                    vender = _venderRepo.Get(venderCon);
                    TtechnicianGroup group = vender.TTechnicianGroup.SingleOrDefault();

                    _logger.Info($"自動新增技師群組對應主檔開始");
                    var Claimscon = new Conditions<DataBase.TTechnicianGroupClaims>();
                    TtechnicianGroupClaims TClaims = new TtechnicianGroupClaims();
                    TClaims.Seq = group.Seq;
                    TClaims.CompCd = callog.CompCd;
                    TClaims.VendorCd = callog.VenderCd;
                    //新增技師群組對應主檔
                    technicians?.ForEach(technician =>
                    {
                        Claimscon.And(x => x.Seq == group.Seq);
                        Claimscon.And(x => x.CompCd == group.CompCd);
                        Claimscon.And(x => x.VendorCd == group.VendorCd);
                        Claimscon.And(x => x.Account == technician.Account);
                        TClaims.Account = technician.Account;
                        try
                        {
                            _technicianGroupClaimsRepo.Add(Claimscon, TClaims);
                        }
                        catch (Exception)
                        {
                            _logger.Error($"自動新增技師群組對應主檔時新增失敗-公司別:{callog.CompCd},廠商別:{callog.VenderCd},技師帳號:{technician.Account}");
                        }
                        Claimscon = new Conditions<DataBase.TTechnicianGroupClaims>();
                    });
                    _logger.Info($"自動新增技師群組對應主檔結束");


                    technicians?.ForEach(claim =>
                    {

                        var current = claim;

                        _logger.Info($"立案自動通知-組合物件-尋覽技師名稱:{current.Account}");

                        //啟用
                        if (current.Enable)
                        {
                            try
                            {
                                if (!accounts.Keys.Contains(current.Account))
                                {
                                    accounts.Add(current.Account, current.RegistrationID);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"立案自動通知-技師帳號:{current.Account}，放入推播清單錯誤，原因：{ex.Message}");
                            }

                        }


                    });

                }


            }
            else
            {
                techGroup?.ForEach(group =>
                {
                    _logger.Info($"立案自動通知-組合物件-尋覽群組代號:{group.Seq}");

                    group.TTechnicianGroupClaims?.ForEach(claim =>
                    {

                        var current = claim.TVenderTechnician;

                        _logger.Info($"立案自動通知-組合物件-尋覽技師名稱:{current.Account}");

                        //啟用
                        if (current.Enable)
                        {
                            try
                            {
                                if (!accounts.Keys.Contains(current.Account))
                                {
                                    accounts.Add(current.Account, current.RegistrationID);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"立案自動通知-技師帳號:{current.Account}，放入推播清單錯誤，原因：{ex.Message}");
                            }

                        }
                    });

                });
            }

            //更新TCALLOG.TimePoint 
            //var callogcon = new Conditions<DataBase.TCALLOG>();
            //callogcon.And(x => x.Comp_Cd == callog.CompCd);
            //callogcon.And(x => x.Sn == callog.Sn);
            //callogcon.Allow(x => x.TimePoint);

            //if (!_callogRepo.Update(callogcon, new Tcallog()
            //{
            //    TimePoint = 1
            //}))
            //    throw new Exception("更新TCALLOG.TimePoint失敗");


            if (callog.TacceptedLog == null)
            {

                    //準備通知-寫入待認養
                    accounts.ForEach(account =>
                    {
                        try
                        {
                            _logger.Info($"準備通知-寫入待認養 帳號:{account.Key}");

                            #region 更新資料

                            _technicianProvider.AddAwaitAcceptLog(callog.CompCd, callog.Sn, account.Key);

                            #endregion

                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"準備通知-寫入待認養 帳號:{account}，通知發生錯誤，原因：{ex.Message}");
                            if (ex.InnerException != null)
                            {
                                _logger.Error(ex.InnerException.Message);
                                if (ex.InnerException.InnerException != null)
                                    _logger.Error(ex.InnerException.InnerException.Message);
                            }
                            _logger.Error(ex.StackTrace);
                           
                        }
                    });

                //準備推播
                accounts.ForEach(account =>
                {
                    try
                    {
                        _logger.Info($"準備推播 帳號:{account.Key}");

                        string storeName = getStoreName(callog.CompCd, callog.StoreCd);
                        string CallLevel = callog.CallLevel == "1" ? "普通" : "緊急";

                        #region 推播訊息                        

                        _notifyFactory.Exucte(new JPushRequest(
                                    callog.CompCd,
                                    callog.VenderCd)
                        {
                            Sn = callog.Sn,
                            Content = $"您有一筆新案件待認養,案件編號:{callog.Sn} 店名:{storeName} 叫修等級:{CallLevel}",
                            Title = "認養案件",
                            Extras = new Dictionary<string, string>() {
                            { "FeatureName","VenderAccept"}

                    }
                        }, account.Key, account.Value);
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"準備推播 帳號:{account}，通知發生錯誤，原因：{ex.Message}");
                        if (ex.InnerException != null)
                        {
                            _logger.Error(ex.InnerException.Message);
                            if (ex.InnerException.InnerException != null)
                                _logger.Error(ex.InnerException.InnerException.Message);
                        }
                        _logger.Error(ex.StackTrace);
                    }
                });

            }

            return true;

        }
        /// <summary>
        /// 廠商通知技師認養案件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Boolean VenderNotification(Tcallog log, List<string> accounts)
        {

            _logger.Info($"案件通知-公司別:{log.CompCd},案件編號:{log.Sn}");

            #region 驗證與取得資訊

            //取得案件
            Tcallog callog = base.GetCallog(log.CompCd, log.Sn);

            //如果已經銷案不允許再指通知了
            if (callog.CloseSts > (byte)CloseSts.process)
                throw new IndexOutOfRangeException($"此案件已銷案");
            if (callog.TacceptedLog != null)
                throw new IndexOutOfRangeException($"案件已由{callog.TacceptedLog.Name}認養");
            //取得公司
            Tcmpdat comp = base.GetComp(log.CompCd);

            _logger.Info($"案件通知-公司別:{callog.CompCd}");
            _logger.Info($"案件通知-廠商別:{callog.VenderCd}");


            #endregion

            using (TransactionScope scope = new TransactionScope())
            {

                accounts.ForEach(account =>
                {

                    _logger.Info($"準備通知給帳號:{account}");

                    #region 更新資料

                    _technicianProvider.AddAwaitAcceptLog(log.CompCd, callog.Sn, account);

                    #endregion

                    #region 推播訊息                 

                    string storeName = getStoreName(callog.CompCd, callog.StoreCd);
                    string CallLevel = callog.CallLevel == "1" ? "普通" : "緊急";

                    _notifyFactory.Exucte(new JPushRequest(
                   callog.CompCd,
                   callog.VenderCd,
                   account)
                    {
                        Sn = callog.Sn,
                        Content = $"您有一筆新案件待認養,案件編號:{callog.Sn} 店名:{storeName} 叫修等級:{CallLevel}",
                        Title = "認養案件",
                        Extras = new Dictionary<string, string>() {
                            { "FeatureName","VenderAccept"}

                        }
                    });


                    #endregion

                });

                scope.Complete();

            }

            return true;

        }
        /// <summary>
        /// 技師認養案件/廠商指派案件
        /// </summary>
        /// <param name="log">畫面上選擇的案件</param>
        /// <param name="account">登入的技師/廠商選取的技師</param>
        /// <returns></returns>
        public Boolean TechnicianAccept(Tcallog log, string account, Boolean isVndAssign, string username)
        {

            _logger.Info($"案件認養/指派-公司別:{log.CompCd},案件編號:{log.Sn}");

            #region 驗證與取得資訊

            //取得案件
            Tcallog callog = base.GetCallog(log.CompCd, log.Sn);

            if (callog.TacceptedLog != null)
                throw new IndexOutOfRangeException($"案件已由{callog.TacceptedLog.Name}認養");


            //如果已經銷案不允許再指通知了
            if (callog.CloseSts > (byte)CloseSts.process)
                throw new IndexOutOfRangeException($"此案件已銷案");

            //取得技師
            TvenderTechnician technician = base.GetTechnician(log.CompCd, account);

            _logger.Info($"案件認養/指派-公司別:{callog.CompCd}");
            _logger.Info($"案件認養/指派-廠商別:{callog.VenderCd}");
            _logger.Info($"案件認養/指派-技師代號:{technician.Account}");

            #endregion

            #region 組合物件
            DateTime now = DateTime.Now;

            TacceptedLog tacceptedLog = new TacceptedLog()
            {
                Account = technician.Account,
                Sn = callog.Sn,
                RcvDatetime = now,
                RcvRemark = "no defind",
                Name = technician.Name,
            };

            callog.TacceptedLog = tacceptedLog;
            callog.TimePoint = (int)TimePoint.Accepted;

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                _logger.Info($"案件認養/指派-準備更新資料");

                #region 技師認養

                if (!_callogFactory.TechnicianAccept(callog))
                    throw new Exception("[ERROR]=>技師認養案件時,認養失敗");

                #endregion

                _logger.Info($"案件認養/指派-移除案件與技師關聯");

                #region 移除案件與技師關聯


                //bool bo = _technicianProvider.RemoveAwaitAcceptLog(log.CompCd, log.Sn);

                //if (bo == false)
                //{
                //    string Mail = ServerProfile.GetInstance().Mail;
                //    string[] MailList = Mail.Split(';');
                //    _MailFactory.Excute(new MailRequest(
                //               MailList,
                //               "移除案件與技師關聯失敗",
                //               $"認養或指派時，刪除技師與案件關聯失敗，案件編號：{log.Sn}"
                //        ));
                //    throw new Exception("[ERROR]=>技師認養案件時,認養失敗");
                //}

                int DataCount;
                using (TransactionScope tss = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DataCount = _technicianProvider.GetCallLogClaimsCount(log.CompCd, log.Sn);
                    _logger.Info($"移除案件與技師關聯[前]，CallLogClaims關聯資料筆數 ：{DataCount}， 查詢條件-公司別：{log.CompCd}、案件編號：{log.Sn}");

                    _logger.Info($"案件認養/指派-準備移除案件與技師關聯，公司別：{log.CompCd}、案件編號：{log.Sn}");
                    _technicianProvider.RemoveAwaitAcceptLog(log.CompCd, log.Sn);
                    tss.Complete();
                }

                using (TransactionScope tss = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    DataCount = _technicianProvider.GetCallLogClaimsCount(log.CompCd, log.Sn);
                    _logger.Info($"移除案件與技師關聯[後]，CallLogClaims關聯資料筆數 ：{DataCount}， 查詢條件-公司別：{log.CompCd}、案件編號：{log.Sn}");
                }

                if (DataCount != 0)
                {
                    string Mail = ServerProfile.GetInstance().Mail;
                    string[] MailList = Mail.Split(';');
                    _MailFactory.Excute(new MailRequest(
                               MailList,
                               "移除案件與技師關聯失敗",
                               $"認養或指派時，刪除技師與案件關聯失敗，案件編號：{log.Sn}"
                        ));
                    throw new Exception($"[ERROR]=>案件編號：{log.Sn}，技師認養案件時,認養失敗");
                }





                #endregion

                scope.Complete();
            }

            #region 推播訊息
            string Assignor = "";
            if (isVndAssign) //由廠商指派的才需要推播
            {
                _logger.Info($"案件認養/指派-準備通知給帳號:{account}");

                string storeName = getStoreName(callog.CompCd, callog.StoreCd);
                string CallLevel = callog.CallLevel == "1" ? "普通" : "緊急";

                _notifyFactory.Exucte(new JPushRequest(
                    callog.CompCd,
                    callog.VenderCd,
                    account)
                {
                    Sn = callog.Sn,
                    Content = $"您有一筆新案件待銷案,案件編號:{callog.Sn} 店名:{storeName} 叫修等級:{CallLevel}",                    
                    Title = "認養案件",
                    Extras = new Dictionary<string, string>() {
                            { "FeatureName","VenderConfirm"}

                        }
                });
                Assignor = username;
            }
            else
            {
                Assignor = technician.Name;
            }

            var Con = new Conditions<DataBase.TCallogCourse>();
            TCallogCourse course = new TCallogCourse()
            {
                CompCd = log.CompCd,
                Sn = log.Sn,
                Assignor = Assignor,
                Admissibility = technician.Name,
                Datetime = now
            };

            //新增案件歷程
            _CallogCourseRepo.Insert(Con, course);


            #endregion

            return true;

        }
        /// <summary>
        /// 廠商改派案件
        /// </summary>
        /// <param name="log">畫面上選擇的案件</param>
        /// <param name="account">廠商選擇的技師帳號</param>
        /// <returns></returns>
        public Boolean VendorChangeLog(Tcallog log, string account, string username)
        {

            _logger.Info($"案件改派-公司別:{log.CompCd},案件編號:{log.Sn}");

            #region 驗證與取得資訊

            //取得案件
            Tcallog callog = base.GetCallog(log.CompCd, log.Sn);

            //取得原來的受理技師帳號,並記錄
            if (callog.TacceptedLog == null)
                throw new NullReferenceException($"此案件未被認養,因此無法改派");

            //如果已經銷案不允許再指通知了
            if (callog.CloseSts > (byte)CloseSts.process)
                throw new IndexOutOfRangeException($"此案件已銷案");

            string oldAccount = callog.TacceptedLog.Account;

            //取得公司
            Tcmpdat comp = base.GetComp(log.CompCd);

            //取得技師
            TvenderTechnician technician = base.GetTechnician(log.CompCd, account);

            _logger.Info($"案件改派-公司別:{callog.CompCd}");
            _logger.Info($"案件改派-廠商別:{callog.VenderCd}");
            _logger.Info($"案件改派-要被改派的技師代號:{technician.Account}");
            _logger.Info($"案件改派-既有的技師代號:{oldAccount}");

            #endregion

            #region 組合物件                
            DateTime now = DateTime.Now;
            TacceptedLog tacceptedLog = new TacceptedLog()
            {
                Account = technician.Account,
                Sn = callog.Sn,
                RcvDatetime = DateTime.Now,
                RcvRemark = "no defind",
                Name = technician.Name,
            };

            callog.TacceptedLog = tacceptedLog;

            #endregion

            #region 廠商改派

            _logger.Info($"案件改派-準備更新資料");

            if (!_callogFactory.TechnicianAccept(callog))
                throw new Exception("[ERROR]=>廠商改派案件,改派失敗");

            #endregion

            #region 推播訊息

            _logger.Info($"案件改派-準備通知給帳號:{account}");

            string storeName = getStoreName(callog.CompCd, callog.StoreCd);
            string CallLevel = callog.CallLevel == "1" ? "普通" : "緊急";

            _notifyFactory.Exucte(new JPushRequest(
                callog.CompCd,
                callog.VenderCd,
                account)
            {
                Sn = callog.Sn,
                Content = $"您有一筆新案件待銷案,案件編號:{callog.Sn} 店名:{storeName} 叫修等級:{CallLevel}",
                Title = "廠商改派",
                Extras = new Dictionary<string, string>() {
                            { "FeatureName","VenderConfirm"}

                        }
            });

            _logger.Info($"案件改派-準備通知給帳號:{oldAccount}");
         

            _notifyFactory.Exucte(new JPushRequest(
                callog.CompCd,
                callog.VenderCd,
                oldAccount)
            {
                Sn = callog.Sn,
                Content = $"您的案件已經指派給:{ technician.Name},案件編號:{callog.Sn} 店名:{storeName} 叫修等級:{CallLevel}",
                Title = "廠商改派",
                Extras = new Dictionary<string, string>() {
                            { "FeatureName",""}

                        }
            });


            var Con = new Conditions<DataBase.TCallogCourse>();
            TCallogCourse course = new TCallogCourse()
            {
                CompCd = log.CompCd,
                Sn = log.Sn,
                Assignor = username,
                Admissibility = technician.Name,
                Datetime = now
            };

            //新增案件歷程
            _CallogCourseRepo.Insert(Con, course);


            #endregion

            return true;

        }
        /// <summary>
        /// 廠商暫存案件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Boolean VendorScratch(Tcallog input)
        {

            _logger.Info($"案件暫存-公司別:{input.CompCd},案件編號:{input.Sn}");

            string SapAssetKind = _ImgRepo.GetSpcAssetKind(input.CompCd, input.AssetCd);

            var con = new Conditions<DataBase.TCALLOG>();

            con.And(g => g.Sn == input.Sn);
            con.And(g => g.Comp_Cd == input.CompCd);
            con.Allow(g => g.TimePoint);
            con.Allow(g => g.Arrive_Date);
            con.Allow(g => g.Update_User);
            con.Allow(g => g.Update_Date);
            if (SapAssetKind == "1")
                con.Allow(g => g.Coffee_Cup);
            con.Allow(g => g.Work_Desc);           

            List<TCallLogDateRecord> AddCDR = new List<TCallLogDateRecord>();
            if (input.TcallLogDateRecords != null)
                AddCDR = input.TcallLogDateRecords.Where(x => x.Seq == 0).ToList();


            //檢查是否已有既有紀錄，若有則進行更新就好
            TCALINV sqlCALINV = GetTCALINV(input.CompCd, input.Sn);
            var conINV = new Conditions<DataBase.TCALINV>();
            conINV.And(g => g.Comp_Cd == input.CompCd);
            conINV.And(g => g.Sn == input.Sn);
            if (sqlCALINV != null)
            {
                conINV.Allow(g => g.Work_Id);
                conINV.Allow(g => g.Pre_Amt);
                conINV.Allow(g => g.Update_User);
                conINV.Allow(g => g.Update_Date);
            }


            using (TransactionScope scope = new TransactionScope())
            {
                _logger.Info($"案件暫存-準備更新資料");

                #region 儲存資料

                _callogRepo.Update(con, input);

                if (input.TCALINV != null)
                {
                    if (sqlCALINV == null)
                        _CALINVRepo.Add(conINV, input.TCALINV);
                    else
                        _CALINVRepo.Update(conINV, input.TCALINV);
                }

                if (AddCDR != null)
                    _callogFactory.AddDateRecords(AddCDR);

                #endregion

                _logger.Info($"案件暫存-準備儲存照片");

                #region 儲存照片

                _ImgRepo.AddImg(input);

                #endregion

                scope.Complete();
            }

            return true;
        }
        /// <summary>
        /// 廠商銷案案件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Boolean VendorConfirm(Tcallog input)
        {

            _logger.Info($"案件銷案-公司別:{input.CompCd},案件編號:{input.Sn}");

            string SapAssetKind = _ImgRepo.GetSpcAssetKind(input.CompCd, input.AssetCd);

        
            var con = new Conditions<DataBase.TCALLOG>();

            con.And(g => g.Sn == input.Sn);
            con.And(g => g.Comp_Cd == input.CompCd);
            con.Allow(g => g.Arrive_Date);
            con.Allow(g => g.Fc_Date);
            con.Allow(g => g.TimePoint);
            con.Allow(g => g.Work_Id);
            con.Allow(g => g.Finish_Id);
            con.Allow(g => g.Damage_Proc_No);
            if (SapAssetKind == "1")
                con.Allow(g => g.Coffee_Cup);
            con.Allow(g => g.Close_Sts);
            con.Allow(g => g.Work_Desc);
            con.Allow(g => g.VndEng_Id);
            con.Allow(g => g.AppClose_Date);
            con.Allow(g => g.Update_User);
            con.Allow(g => g.Update_Date);

            List<TCallLogDateRecord> AddCDR = new List<TCallLogDateRecord>();
            if (input.TcallLogDateRecords != null)
                AddCDR = input.TcallLogDateRecords.Where(x => x.Seq == 0).ToList();

            //檢查是否已有既有紀錄，若有則進行更新就好
            TCALINV sqlCALINV= GetTCALINV(input.CompCd, input.Sn);
            var conINV = new Conditions<DataBase.TCALINV>();
            conINV.And(g => g.Comp_Cd == input.CompCd);
            conINV.And(g => g.Sn == input.Sn);
            if (sqlCALINV != null)
            {
                conINV.Allow(g => g.Work_Id);
                conINV.Allow(g => g.Pre_Amt);
                conINV.Allow(g => g.Update_User);
                conINV.Allow(g => g.Update_Date);
            }

            using (TransactionScope scope = new TransactionScope())
            {
                _logger.Info($"案件銷案-準備更新資料");

                #region 儲存資料

                _callogRepo.Update(con, input);

                if (input.TCALINV != null )
                {
                    if (sqlCALINV == null)
                        _CALINVRepo.Add(conINV, input.TCALINV);
                    else
                        _CALINVRepo.Update(conINV, input.TCALINV);
                }

                if (AddCDR != null)
                    _callogFactory.AddDateRecords(AddCDR);

                #endregion

                _logger.Info($"案件銷案-準備儲存照片");

                #region 儲存照片

                _ImgRepo.AddImg(input);

                #endregion

                scope.Complete();
            }

            return true;
        }
        /// <summary>
        /// 取得案件數量
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int> VendorNewsCount(UserBase User)
        {

            _logger.Info($"取得案件數量-使用者帳號:{User.UserId}");

            #region  取得已認養案件數

            var conTechnician = new Conditions<DataBase.TCALLOG>();

            conTechnician.And(x => x.Close_Sts == (byte)CloseSts.process &&   //案件狀態
                                   x.Comp_Cd == "711" &&              //公司別
                                   x.Vender_Cd == User.VenderCd &&          //廠商別
                                   x.TAcceptedLog.Account == User.UserId &&
                                   x.TimePoint >= (int)TimePoint.Accepted &&
                                   x.TimePoint < (int)TimePoint.Finish);  //使用者帳號


            conTechnician.Include(x => x.TAcceptedLog);                     //受理案件

            int CountTechnician = _callogRepo.Count(conTechnician);        //取得待銷案案件數

            _logger.Info($"取得案件數量-待銷案案件數:{CountTechnician}");

            #endregion

            #region 取得待認養案件數

            var conAwaitAdopt = new Conditions<DataBase.TCALLOG>();


            conAwaitAdopt.And(x => x.Close_Sts == (byte)CloseSts.process &&     //案件狀態
                                   x.Comp_Cd == "711" &&                  //公司別
                                   x.Vender_Cd == User.VenderCd &&              //廠商別
                                   x.TimePoint == (int)TimePoint.Dispatch);

            conAwaitAdopt.And(x => x.TVenderTechnician
                                    .Any(y => y.Account == User.UserId));
            conAwaitAdopt.And(x => x.TAcceptedLog == null);


            int CountAwaitAdopt = _callogRepo.Count(conAwaitAdopt); //取得待受理案件數

            _logger.Info($"取得案件數量-待受理案件數:{CountAwaitAdopt}");

            #endregion

            #region  取得未完修案件管理案件數

            var conAwaitAssign = new Conditions<DataBase.TCALLOG>();


            conAwaitAssign.And(x => x.Close_Sts == (byte)CloseSts.process &&    //案件狀態
                                    x.Comp_Cd == "711" &&                 //公司別
                                    x.Vender_Cd == User.VenderCd &&             //廠商別
                                   x.TimePoint >= (int)TimePoint.Dispatch &&
                                   x.TimePoint < (int)TimePoint.Finish);


            int CountAwaitAssign = _callogRepo.Count(conAwaitAssign);        //取得待派工案件數

            _logger.Info($"取得案件數量-待派工案件數:{CountAwaitAssign}");

            #endregion


            return new Tuple<int, int, int>(CountTechnician, CountAwaitAdopt, CountAwaitAssign);
        }

        /// <summary>
        /// 催修通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean PushUrg(Tcallog data)
        {
            var StoreName = _ImgRepo.GetStoreName(data.CompCd, data.StoreCd);
            if (data.TimePoint == 1)
            {
                Dictionary<string, string> Technician = _ImgRepo.GetAwaitAdoptTechnician(data);
                Technician.ForEach(x =>
                {
                    _notifyFactory.Exucte(new JPushRequest(
                            data.CompCd,
                            data.VenderCd)
                    {
                        Sn = data.Sn,
                        Content = $"{StoreName}門市，案件：{data.Sn} 已催修，請儘速處理。",
                        Title = "催修案件",
                        Extras = new Dictionary<string, string>() {
                            { "FeatureName","VenderAccept"}
                    }
                    }, x.Key, x.Value);
                });
            }
            else if (data.TimePoint >= 2 && data.TimePoint < 4)
            {
                Dictionary<string, string> Technician = _ImgRepo.GetAcceptTechnician(data.CompCd, data.Sn);

                #region 推播訊息

                Technician.ForEach(x =>
                {
                    _notifyFactory.Exucte(new JPushRequest(
                            data.CompCd,
                            data.VenderCd)
                    {
                        Sn = data.Sn,
                        Content = $"{StoreName}門市，案件：{data.Sn} 已催修，請儘速處理。",
                        Title = "催修案件",
                        Extras = new Dictionary<string, string>() {
                            { "FeatureName","VenderConfirm"}
                    }
                    }, x.Key, x.Value);
                });

                #endregion

            }
            else
                throw new Exception($"案件：{data.Sn}，狀態(TimePoint)：{data.TimePoint}，不符，因此不推播");

            return true;
        }
        /// <summary>
        /// 催修通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean PushFinish(Tcallog data)
        {
            var StoreName = _ImgRepo.GetStoreName(data.CompCd, data.StoreCd);
            if (data.TimePoint == 1)
            {
                Dictionary<string, string> Technician = _ImgRepo.GetAwaitAdoptTechnician(data);
                Technician.ForEach(x =>
                {
                    _notifyFactory.Exucte(new JPushRequest(
                            data.CompCd,
                            data.VenderCd)
                    {
                        Sn = data.Sn,
                        Content = $"{StoreName}門市，案件：{data.Sn} 已銷案，無需前往門市。",
                        Title = "案件銷案",
                        Extras = new Dictionary<string, string>() {
                            { "FeatureName",""}
                    }
                    }, x.Key, x.Value);
                });
            }
            else if (data.TimePoint >= 2 && data.TimePoint < 4)
            {
                Dictionary<string, string> Technician = _ImgRepo.GetAcceptTechnician(data.CompCd, data.Sn);

                #region 推播訊息

                Technician.ForEach(x =>
                {
                    _notifyFactory.Exucte(new JPushRequest(
                            data.CompCd,
                            data.VenderCd)
                    {
                        Sn = data.Sn,
                        Content = $"{StoreName}門市，案件：{data.Sn} 已銷案，無需前往門市。",
                        Title = "案件銷案",
                        Extras = new Dictionary<string, string>() {
                            { "FeatureName",""}
                    }
                    }, x.Key, x.Value);
                });

                #endregion

            }
            else
                throw new Exception($"案件：{data.Sn}，狀態(TimePoint)：{data.TimePoint}，不符，因此不推播");

            return true;
        }
        /// <summary>
        /// 多案件、多技師通知
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns>從Web進行通知</returns>
        public Boolean NotificationForWeb(UserBase user, List<string> Sn, Dictionary<string, string> Account)
        {
            #region 更新資料
            _logger.Info("更新技師已受理案件");
            Sn.ForEach(sn =>
            {
                Account.ForEach(account =>
                {
                    _technicianProvider.AddAwaitAcceptLog(user.CompCd, sn, account.Key);
                });
            });
            #endregion
            #region 推播
            var bo = _notifyFactory.Exucte(
            new JPushRequest(user.CompCd, user.VenderCd)
            {
                Content = "您有案件待認養",
                Title = "認養案件",
                Extras = new Dictionary<string, string>()
                                        {
                                            { "FeatureName","VenderAccept"}
                                        }
            }
            , Sn
            , Account);
            #endregion
            return bo;
        }
        /// <summary>
        /// 多案件、單一技師指派
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns>從Web進行指派</returns>
        public Boolean NotificationForAppoint(UserBase user, List<string> Sn, List<string> Account)
        {

            _logger.Info("更新技師待受理案件(網頁指派案件)");
            DateTime now = DateTime.Now;
            using (TransactionScope scope = new TransactionScope())
            {
                _logger.Info($"案件認養/指派-準備更新資料");
                Sn.ForEach(sn =>
                {
                    int DataCount;
                    #region 移除案件與技師關聯

                    using (TransactionScope tss = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        DataCount = _technicianProvider.GetCallLogClaimsCount(user.CompCd, sn);
                        _logger.Info($"移除案件與技師關聯[前]，CallLogClaims關聯資料筆數 ：{DataCount}， 查詢條件-公司別：{user.CompCd}、案件編號：{sn}");

                        _logger.Info($"案件認養/指派-準備移除案件與技師關聯，公司別：{user.CompCd}、案件編號：{sn}");
                        _technicianProvider.RemoveAwaitAcceptLog(user.CompCd, sn);
                        tss.Complete();
                    }
                    #endregion

                    using (TransactionScope tss = new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        DataCount = _technicianProvider.GetCallLogClaimsCount(user.CompCd, sn);
                        _logger.Info($"移除案件與技師關聯[後]，CallLogClaims關聯資料筆數 ：{DataCount}， 查詢條件-公司別：{user.CompCd}、案件編號：{sn}");
                    }

                    if (DataCount != 0)
                    {
                        string Mail = ServerProfile.GetInstance().Mail;
                        string[] MailList = Mail.Split(';');
                        _MailFactory.Excute(new MailRequest(
                                   MailList,
                                   "移除案件與技師關聯失敗",
                                   $"認養或指派時，刪除技師與案件關聯失敗，案件編號：{sn}"
                            ));
                        throw new Exception($"[ERROR]=>案件編號：{sn}，技師認養案件時,認養失敗");
                    }
                    else
                    {
                        #region 更新資料
                        //取得案件
                        Tcallog callog = base.GetCallog(user.CompCd, sn);
                        TacceptedLog tacceptedLog = new TacceptedLog()
                        {
                            Account = Account[0].ToString(),
                            Sn = sn,
                            RcvDatetime = now,
                            RcvRemark = "no defind",
                            Name = Account[2].ToString(),
                        };

                        callog.TacceptedLog = tacceptedLog;
                        callog.TimePoint = (int)TimePoint.Accepted;

                        if (!_callogFactory.TechnicianAccept(callog))
                            throw new Exception("[ERROR]=>技師認養案件時,認養失敗");

                        #endregion
                    }

                    #region 新增案件歷程
                    Conditions<DataBase.TCallogCourse> Con = new Conditions<DataBase.TCallogCourse>();
                    TCallogCourse course = new TCallogCourse()
                    {
                        CompCd = user.CompCd,
                        Sn = sn,
                        Assignor = user.UserName,
                        Admissibility = Account[2].ToString(),
                        Datetime = now
                    };

                    //新增案件歷程
                    _CallogCourseRepo.Insert(Con, course);
                    #endregion
                });
                scope.Complete();
            }
            #region 推播
            var bo = _notifyFactory.Exucte(
            new JPushRequest(user.CompCd, user.VenderCd)
            {
                Content = "您有新案件待銷案",
                Title = "認養案件",
                Extras = new Dictionary<string, string>()
                                        {
                                            { "FeatureName","VenderConfirm"}
                                        }
            }
            , Sn
            , Account);
            #endregion
            return true;
        }

        /// <summary>
        /// 多案件、單一技師改派
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns>從Web進行改派</returns>
        public Boolean ChangeNotificationForWeb(UserBase user, List<string> Sn, TvenderTechnician Techniciandata, Dictionary<string, string> OldAccount)
        {

            _logger.Info("更新技師已受理案件(網頁改派案件)");
            using (TransactionScope scope = new TransactionScope())
            {

                //更新案件已受理技師
                Sn.ForEach(sn =>
                {
                    #region 更新資料
                    //取得案件
                    Tcallog callog = base.GetCallog(user.CompCd, sn);
                    TacceptedLog tacceptedLog = new TacceptedLog()
                    {
                        Account = Techniciandata.Account,
                        Sn = callog.Sn,
                        RcvDatetime = DateTime.Now,
                        RcvRemark = "no defind",
                        Name = Techniciandata.Name,
                    };

                    callog.TacceptedLog = tacceptedLog;


                    if (!_callogFactory.TechnicianAccept(callog))
                        throw new Exception($"[ERROR]=>廠商改派案件失敗,案件編號:{sn}");
                    #endregion

                    #region 新增案件歷程
                    Conditions<DataBase.TCallogCourse> Con = new Conditions<DataBase.TCallogCourse>();
                    TCallogCourse course = new TCallogCourse()
                    {
                        CompCd = user.CompCd,
                        Sn = sn,
                        Assignor = user.UserName,
                        Admissibility = Techniciandata.Name,
                        Datetime = DateTime.Now
                    };

                    //新增案件歷程
                    _CallogCourseRepo.Insert(Con, course);
                    #endregion
                });


                scope.Complete();
            }


            #region 推播給新技師
            Dictionary<string, string> Account = new Dictionary<string, string>();
            Account.Add(Techniciandata.Account, Techniciandata.RegistrationID);
            _notifyFactory.Exucte(
            new JPushRequest(user.CompCd, user.VenderCd)
            {
                Content = "您有新案件待銷案",
                Title = "廠商改派",
                Extras = new Dictionary<string, string>()
                                        {
                                            { "FeatureName","VenderConfirm"}
                                        }
            }
            , Sn
            , Account);

            #endregion

            #region 推播給舊技師
            _notifyFactory.Exucte(
            new JPushRequest(user.CompCd, user.VenderCd)
            {
                Content = "您的案件已經被指派給其他技師",
                Title = "廠商改派",
                Extras = new Dictionary<string, string>()
                                        {
                                                 { "FeatureName",""}
                                        }
            }
            , Sn
            , OldAccount);
            #endregion

            return true;
        }


        private  string getStoreName ( string CompCd ,string StoreCd)
        {
            string storeName = StoreCd;

            var pushStoreCon = new Conditions<DataBase.TSTRMST>();
            pushStoreCon.And(x => x.Comp_Cd == CompCd &&
                               x.Store_Cd == StoreCd);

            try
            {
                var currentStore = _storeRepo.Get(pushStoreCon);
                if (currentStore != null)
                {
                    storeName = currentStore.StoreName;
                }
            }
            catch (Exception ex)
            {

                _logger.Error($"取得店名發生錯誤，原因：{ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                _logger.Error(ex.StackTrace);
            }

            return storeName;
        }

        private TCALINV GetTCALINV (string CompCd, string Sn)
        {
            var con = new Conditions<DataBase.TCALINV>();
            con.And(x => x.Comp_Cd == CompCd);
            con.And(x => x.Sn == Sn);
            TCALINV TCALINV = _CALINVRepo.Get(con);

            return TCALINV;
        }
    }
}

