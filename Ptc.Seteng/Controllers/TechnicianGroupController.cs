using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
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
using System.Transactions;
using System.Web;
using System.Web.Mvc;


namespace Ptc.Seteng.Controllers
{
    [AuthenticationFilter]
    public class TechnicianGroupController : BaseController
    {
        private readonly ISystemLog _logger;
        private readonly IVendorService _vendorService;
        private readonly IVendorFactory _vendorFactory;
        private readonly IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _compRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _vendorRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _TzocodeRepo;
        public TechnicianGroupController(ISystemLog Logger,
                                         IVendorService VendorService,
                                         IVendorFactory VendorFactory,
                                         IBaseRepository<DataBase.TCMPDAT, Tcmpdat> CompRepo,
                                         IBaseRepository<DataBase.TVENDER, Tvender> VendorRepo,
                                         IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                                         IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TechnicianGroupRepo,
                                         IBaseRepository<DataBase.TZOCODE, Tzocode> TzocodeRepo)
        {
            _logger = Logger;
            _compRepo = CompRepo;
            _vendorRepo = VendorRepo;
            _vendorService = VendorService;
            _vendorFactory = VendorFactory;
            _technicianRepo = TechnicianRepo;
            _technicianGroupRepo = TechnicianGroupRepo;
            _TzocodeRepo = TzocodeRepo;
        }

        [MenuNode(
        Title = "技師群組",
        Description = "技師群組",
        PrefixedNodeID = "home",
        isEntry = true,
        AuthType = AuthNodeType.All)]
        public ActionResult Index()
        {

            //取得登入人员
            var user = ((PtcIdentity)this.User.Identity).currentUser;

            return View(new TechnicianGroupViewModel()
            {

                CompCd = user.CompCd,
                VendorCd = user.VenderCd,
                CompName = user.CompShort,
                VendorName = user.VenderName,

            });
        }
        [MenuNode(
        Title = "取得列表-功能",
        Description = "取得列表-功能",
        AuthType = AuthNodeType.Read)]
        public ActionResult GetList(DataTablesReqModel<List<TechnicianGroupViewModel>> Data)
        {
            List<TechnicianGroupViewModel> models = Data.criteria;

            DataTablesRespModel result = new DataTablesRespModel(Data.draw);
            try
            {
                if (Data.criteria == null)
                    throw new Exception("沒有條件，無法查詢");

                Conditions<DataBase.TTechnicianGroup> con = new Conditions<DataBase.TTechnicianGroup>(Data.length, (Data.start / Data.length));

                models?.ForEach(model =>
                {

                    var component = new List<Expression<Func<DataBase.TTechnicianGroup, Boolean>>>();

                    model.GetProperties()?
                         .Select(x => x.Avatar<AvatarAttribute>(model))
                         .Where(x => x.Key != null)
                         .ForEach(g =>
                         {
                             component.Add(con.CombinationExpression(
                                  g.Key.SubstituteName,
                                  g.Key.ExpressionType,
                                  g.Value));
                         });

                    con.ConvertToMultiFilter(component);
                });

                Data.order?.ForEach(x =>
                {
                    con.Order(x.dir, Data.columns[x.column].name);
                });

                //con.Include(x => x.TVENDER.TCMPDAT);
                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.TVENDER.Comp_Cd == _user.CompCd);

                if (_user.DataRange?.VendorCd != null)
                    con.And(x => _user.DataRange.VendorCd.Contains(x.VendorCd));

                #endregion
                var list = _technicianGroupRepo.GetList(con);
                int PageIndex = (Data.start / Data.length);
                PagedList<TtechnicianGroup> meta = new PagedList<TtechnicianGroup>(list, PageIndex, Data.length);

                var conZo = new Conditions<DataBase.TZOCODE>();
                conZo.And(x => x.Comp_Cd == _user.CompCd);
                PagedList<Tzocode> Zo = _TzocodeRepo.GetList(conZo);

                result.data = meta.Select(x => new TechnicianGroupResultViewModel(x, Zo).colData).ToArray();

                result.TotalCount(con.TotalCount);


            }
            catch (AutoMapper.AutoMapperMappingException ex)
            {
                _logger.Error(ex);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        _logger.Error(ex.InnerException.InnerException.Message);
                }
                result.error = ex.InnerException.Message;
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
                result.error = ex.Message;
            }
            return Json(result);
        }
        [MenuNode(
        Title = "新增群組-畫面",
        Description = "新增群組-畫面",
        AuthType = AuthNodeType.Create)]
        public ActionResult CreateView(TechnicianGroupEditViewModel model)
        {

            try
            {
                if (MethodHelper.IsNullOrEmpty(model.CompCd, model.VendorCd))
                    throw new ArgumentNullException($"移至新增群組畫面,没有傳入對應信息");

                Dictionary<string, string> tvndzo = _vendorFactory.GetVenderZo(model.CompCd, model.VendorCd);

                List<TvenderTechnician> technicians = _vendorFactory.GetTechnicians(model.CompCd, model.VendorCd);


                return View("Edit", new TechnicianGroupEditViewModel()
                {

                    CompCd = model.CompCd,
                    VendorCd = model.VendorCd,
                    ActionType = AuthNodeType.Create,
                    AccountDualBoxList = technicians?.Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Account,
                            Selected = false
                        };

                    }),
                    DoDualBoxList = new List<SelectListItem>(),

                    VenderZoDualBoxList = tvndzo?.Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Text = x.Value,
                            Value = x.Key,
                            Selected = false
                        };

                    })
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
                return View();
            }

        }
        [MenuNode(
        Title = "修改群組-畫面",
        Description = "修改群組-畫面",
        AuthType = AuthNodeType.Edit)]
        public ActionResult EditView(TechnicianGroupEditViewModel model)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(model.CompCd, model.VendorCd))
                    throw new ArgumentNullException($"移至修改群組畫面,没有傳入對應信息");

                TtechnicianGroup group = _vendorFactory.GetGroup(model.CompCd, model.VendorCd, model.Seq);
                ModelState.Clear();
                List<TvenderTechnician> technicians = _vendorFactory.GetTechnicians(model.CompCd, model.VendorCd);

                var mask = group?.TTechnicianGroupClaims?
                                 .Select(x => x.TVenderTechnician.Account)
                                 .ToList();

                

                #region 處理區域與課別
                //取得所有區課
                var conZo = new Conditions<DataBase.TZOCODE>();
                conZo.And(x => x.Comp_Cd == model.CompCd);
                PagedList<Tzocode> Zo = _TzocodeRepo.GetList(conZo);
                //取得保修商負責區域
                List<string> tvndzo = _vendorFactory.GetVenderZo(model.CompCd, model.VendorCd).Select(x => x.Key).ToList();

                var ZoMask = group?.TTechnicianGroupClaims?
                 .Select(x => x.TTechnicianGroup.Responsible_Zo)
                 .ToList();

                var ZoMaskFinal = new List<string>();
                ZoMask.ForEach(x =>
                {
                    bool hasMultiple = x.Contains(',');

                    if (hasMultiple)
                    {
                        string[] ary = x.Split(',');
                        ary.ForEach(y => ZoMaskFinal.Add(y));
                    }
                    else
                    {
                        ZoMaskFinal.Add(x);
                    }
                });

                var DoMask = group?.TTechnicianGroupClaims?
                 .Select(x => x.TTechnicianGroup.Responsible_Do)
                 .ToList();

                var DoMaskFinal = new List<string>();
                DoMask.ForEach(x =>
                {
                    bool hasMultiple = x.Contains(',');

                    if (hasMultiple)
                    {
                        string[] ary = x.Split(',');
                        ary.ForEach(y => DoMaskFinal.Add(y));
                    }
                    else
                    {
                        DoMaskFinal.Add(x);
                    }
                });
                #endregion

                return View("Edit", new TechnicianGroupEditViewModel()
                {

                    Seq = model.Seq,
                    CompCd = model.CompCd,
                    VendorCd = model.VendorCd,
                    GroupName = group.GroupName,
                    ActionType = AuthNodeType.Edit,
                    AccountDualBoxList = technicians?.Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Value = x.Account,
                            Text = x.Name,
                            Selected = (mask.Contains(x.Account))
                        };

                    }),
                    VenderZoDualBoxList = Zo?.Where(y => y.DoCd == "" && y.CloseDate == "9999/12/31" && tvndzo.Contains(y.ZoCd)).Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Text = x.ZoName,
                            Value = x.ZoCd,
                            Selected = (ZoMaskFinal.Contains(x.ZoCd))
                        };

                    }),
                    DoDualBoxList = Zo?.Where(y => y.DoCd != "" && y.CloseDate == "9999/12/31" && y.UpkeepSts == "Y" && ZoMaskFinal.Contains(y.ZoCd)).Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Text = x.DoName,
                            Value = x.DoCd,
                            Selected = (DoMaskFinal.Contains(x.DoCd))
                        };

                    })
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
                return View();
            }

        }
        [MenuNode(
        Title = "瀏覽群組-畫面",
        Description = "瀏覽群組-畫面",
        AuthType = AuthNodeType.Read)]
        public ActionResult ReadView(TechnicianGroupEditViewModel model)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(model.CompCd, model.VendorCd))
                    throw new ArgumentNullException($"移至瀏覽群組畫面,没有傳入對應信息");
                
                
                TtechnicianGroup group = _vendorFactory.GetGroup(model.CompCd, model.VendorCd, model.Seq);

                List<TvenderTechnician> technicians = _vendorFactory.GetTechnicians(model.CompCd, model.VendorCd);

                var mask = group?.TTechnicianGroupClaims?
                                 .Select(x => x.TVenderTechnician.Account)
                                 .ToList();


                #region 處理區域與課別
                //取得所有區課
                var conZo = new Conditions<DataBase.TZOCODE>();
                conZo.And(x => x.Comp_Cd == model.CompCd);
                PagedList<Tzocode> Zo = _TzocodeRepo.GetList(conZo);
                //取得保修商負責區域
                List<string> tvndzo = _vendorFactory.GetVenderZo(model.CompCd, model.VendorCd).Select(x => x.Key).ToList();

                var ZoMask = group?.TTechnicianGroupClaims?
                 .Select(x => x.TTechnicianGroup.Responsible_Zo)
                 .ToList();

                var ZoMaskFinal = new List<string>();
                ZoMask.ForEach(x =>
                {
                    bool hasMultiple = x.Contains(',');

                    if (hasMultiple)
                    {
                        string[] ary = x.Split(',');
                        ary.ForEach(y => ZoMaskFinal.Add(y));
                    }
                    else
                    {
                        ZoMaskFinal.Add(x);
                    }
                });

                var DoMask = group?.TTechnicianGroupClaims?
                 .Select(x => x.TTechnicianGroup.Responsible_Do)
                 .ToList();

                var DoMaskFinal = new List<string>();
                DoMask.ForEach(x =>
                {
                    bool hasMultiple = x.Contains(',');

                    if (hasMultiple)
                    {
                        string[] ary = x.Split(',');
                        ary.ForEach(y => DoMaskFinal.Add(y));
                    }
                    else
                    {
                        DoMaskFinal.Add(x);
                    }
                });
                #endregion

                return View("Edit", new TechnicianGroupEditViewModel()
                {

                    Seq = model.Seq,
                    CompCd = model.CompCd,
                    VendorCd = model.VendorCd,
                    GroupName = group.GroupName,
                    ActionType = AuthNodeType.Read,
                    AccountDualBoxList = technicians?.Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Value = x.Account,
                            Text = x.Name,
                            Selected = (mask.Contains(x.Account))
                        };

                    }),
                    VenderZoDualBoxList = Zo?.Where(y=>y.DoCd == "" && y.CloseDate == "9999/12/31" && tvndzo.Contains(y.ZoCd)).Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Text = x.ZoName,
                            Value = x.ZoCd,
                            Selected = (ZoMaskFinal.Contains(x.ZoCd))
                        };

                    }),
                    DoDualBoxList = Zo?.Where(y=>y.DoCd != "" && y.CloseDate == "9999/12/31" && y.UpkeepSts == "Y" && ZoMaskFinal.Contains(y.ZoCd)).Select(x =>
                    {

                        return new SelectListItem()
                        {
                            Text = x.DoName,
                            Value = x.DoCd,
                            Selected = (DoMaskFinal.Contains(x.DoCd))
                        };

                    })
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
                return View();
            }

        }
        [MenuNode(
        Title = "新增群組數據-功能",
        Description = "新增群組數據-功能",
        AuthType = AuthNodeType.Create)]
        public ActionResult Create(TechnicianGroupEditViewModel model)
        {
            try
            {

                if (model == null)
                    throw new ArgumentNullException($"新增群組數據-功能時,並未傳入任何信息");
                if (model.Zo == null)
                    throw new Exception($"並未傳入區域名稱");
                if (model.Accounts == null)
                    throw new Exception($"並未傳入技師名稱");

                #region  組合區域與課別
                //組合區域
                string ZO = "";
                model.Zo.ForEach(x =>
                {
                    ZO += "," + x;
                });
                if (ZO != "")
                {
                    ZO = ZO.Substring(1);
                }
                //組合課別
                string DO = "";
                model.Do.ForEach(x =>
                {
                    DO += "," + x;
                });
                if (DO != "")
                {
                    DO = DO.Substring(1);
                }
                #endregion


                //群組信息
                TtechnicianGroup techniciangroup = new TtechnicianGroup()
                {
                    CompCd = model.CompCd,
                    VendorCd = model.VendorCd,
                    GroupName = model.GroupName,
                    Responsible_Zo = ZO,
                    Responsible_Do = DO,
                };

                Boolean isSuccess = _vendorService.CreateTechnicianGroup(techniciangroup, model.Accounts);

                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"新增群組資料:{ (isSuccess ? "成功" : "失敗")} ",
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
                        Message = $"新增群組數據失敗,原因:{ex.Message}"
                    }
                });
            }

        }
        [MenuNode(
        Title = "修改群組數據-功能",
        Description = "修改群組數據-功能",
        AuthType = AuthNodeType.Edit)]
        public ActionResult Edit(TechnicianGroupEditViewModel model)
        {
            try
            {

                if (model == null)
                    throw new ArgumentNullException($"修改群組數據時,並未傳入任何信息");
                if (model.Zo == null)
                    throw new Exception($"並未傳入區域名稱");
                if (model.Accounts == null)
                    throw new Exception($"並未傳入技師名稱");

                #region  組合區域與課別
                //組合區域
                string ZO = "";
                model.Zo.ForEach(x =>
                {
                    ZO += "," + x;
                });
                if (ZO != "")
                {
                    ZO = ZO.Substring(1);
                }
                //組合課別
                string DO = "";
                model.Do.ForEach(x =>
                {
                    DO += "," + x;
                });
                if (DO != "")
                {
                    DO = DO.Substring(1);
                }
                #endregion

                //修改群組信息
                TtechnicianGroup techniciangroup = new TtechnicianGroup()
                {
                    Seq = model.Seq,
                    CompCd = model.CompCd,
                    VendorCd = model.VendorCd,
                    GroupName = model.GroupName,
                    Responsible_Zo = ZO,
                    Responsible_Do = DO,
                };

                Boolean isSuccess = _vendorService.UpdateTechnicianGroup(techniciangroup, model.Accounts);

                return Json(new JsonResult()
                {

                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = String.Format("修改群組數據:{0}", isSuccess ? "成功" : "失敗")
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
                        Message = $"修改群組數據失敗,原因:{ex.Message}"
                    }

                });
            }
        }
        [MenuNode(
        Title = "刪除群組數據-功能",
        Description = "刪除群組數據-功能",
        AuthType = AuthNodeType.Delete)]
        public ActionResult Delete(TechnicianGroupEditViewModel model)
        {
            try
            {

                if (model == null)
                    throw new ArgumentNullException($"刪除技師數據時,並未傳入任何信息");

                var con = new Conditions<DataBase.TTechnicianGroup>();

                con.And(x => x.Seq == model.Seq);
                con.And(x => x.CompCd == model.CompCd);
                con.And(x => x.VendorCd == model.VendorCd);

                var query = _technicianGroupRepo.Get(con);

                if (query == null)
                    throw new ArgumentNullException($"刪除技師信息時,找不到技師相關信息");


                Boolean isSuccess = _technicianGroupRepo.Remove(con);

                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = String.Format("刪除群組數據:{0}", isSuccess ? "成功" : "失敗")
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
                        Message = $"刪除群組數據失敗,原因:{ex.Message}"
                    }

                });
            }

        }
        [MenuNode(
        Title = "取得課別",
        Description = "取得課別",
        AuthType = AuthNodeType.Create)]
        public ActionResult SelectDo(TechnicianGroupEditViewModel model)
        {
            try
            {

                if (model.Zo == null)
                    throw new ArgumentNullException($"取得課別-功能時,並未傳入任何信息");

                List<Tzocode> Dolist = _vendorFactory.SelectDo(model.CompCd, model.Zo);

     
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = true,
                        Message = $"取得區課成功",
                        ReturnData = Dolist
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
                        Message = $"取得區課失敗",
                    }

                });
            }
        }
    }
}
