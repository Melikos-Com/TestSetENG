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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{
    [AuthenticationFilter]
    public class TechnicianController : BaseController
    {

        private readonly ISystemLog _logger;
        private readonly ITechnicianService _technicianService;
        private readonly IVendorService _vendorService;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;


        public TechnicianController(ISystemLog Logger,
                                    IVendorService VendorService, 
                                    ITechnicianService TechnicianService,
                                    IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo)
        {
            _logger = Logger;
            _technicianRepo = TechnicianRepo;
            _technicianService = TechnicianService;
            _vendorService = VendorService;

        }

        [MenuNode(
        Title = "技師管理",
        Description = "技師管理",
        PrefixedNodeID = "home",
        isEntry = true,
        AuthType = AuthNodeType.All)]
        public ActionResult Index()
        {
            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            var AutheticationManager = HttpContext.GetOwinContext().Authentication;
            try
            {
                return View(new TechnicianViewModel()
                {

                    CompCd = _user.CompCd,
                    VenderCd = _user.VenderCd,
                    CompName = _user.CompShort,
                    VendorName = _user.VenderName,

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
                AutheticationManager.SignOut();
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Login", "Account");   
      
            }


        }
        #region Screen

        [MenuNode(
        Title = "明细",
        Description = "明细",
        AuthType = AuthNodeType.Read)]
        public ActionResult Read(TechnicianEditViewModel data)
        {

            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            try
            {

                var con = new Conditions<DataBase.TVenderTechnician>();

                con.And(x => x.Comp_Cd == data.CompCd &&
                             x.Vender_Cd == data.VenderCd &&
                             x.Account == data.Account);

                var meta = _technicianRepo.Get(con);

                if (meta == null)
                    throw new NullReferenceException($"no find data");

                return View("Read", new TechnicianEditViewModel(meta)
                {

                    CompName = _user.CompShort,
                    VendorName = _user.VenderName,
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
        Title = "新增技師",
        Description = "新增技師",
        AuthType = AuthNodeType.Create)]
        public ActionResult Add(TechnicianEditViewModel data)
        {

            var _user = ((PtcIdentity)this.User.Identity).currentUser;

            try
            {


                return View("Add", new TechnicianEditViewModel()
                {

                    CompCd = _user.CompCd,
                    VenderCd = _user.VenderCd,
                    CompName = _user.CompShort,
                    VendorName = _user.VenderName,

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
        Title = "修改技師",
        Description = "修改技師",
        AuthType = AuthNodeType.Edit)]
        public ActionResult Modify(TechnicianEditViewModel data)
        {

            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            try
            {


                var con = new Conditions<DataBase.TVenderTechnician>();

                con.And(x => x.Comp_Cd == data.CompCd &&
                             x.Vender_Cd == data.VenderCd &&
                             x.Account == data.Account);

                var meta = _technicianRepo.Get(con);

                if (meta == null)
                    throw new NullReferenceException($"no find data");

                return View("Modify", new TechnicianEditViewModel(meta)
                {
                    CompName = _user.CompShort,
                    VendorName = _user.VenderName,
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
        #endregion

        #region Features

        [MenuNode(
        Title = "取得列表-功能",
        Description = "取得列表-功能",
        AuthType = AuthNodeType.Read)]
        public ActionResult GetList(DataTablesReqModel<List<TechnicianViewModel>> data)
        {

            List<TechnicianViewModel> models = data.criteria;

            DataTablesRespModel result = new DataTablesRespModel(data.draw);
            try
            {
                if (data.criteria == null)
                    throw new Exception("沒有條件，無法查詢");

                Conditions<DataBase.TVenderTechnician> con = new Conditions<DataBase.TVenderTechnician>(data.length, (data.start / data.length));

                models?.ForEach(model =>
                {

                    var component = new List<Expression<Func<DataBase.TVenderTechnician, Boolean>>>();

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

                data.order?.ForEach(x =>
                {
                    con.Order(x.dir, data.columns[x.column].name);
                });

                #region DataRange

                var _user = (this.User.Identity as PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);

                if (_user.DataRange?.VendorCd != null)
                    con.And(x => _user.DataRange.VendorCd.Contains(x.Vender_Cd));

                #endregion

                var list = _technicianRepo.GetList(con);
                int PageIndex = (data.start / data.length);
                PagedList<TvenderTechnician> meta = new PagedList<TvenderTechnician>(list, PageIndex, data.length);


                result.data = meta.Select(x => new TechnicianResultViewModel(x).colData)
                                  .ToArray();

                result.TotalCount(con.TotalCount);


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
        Title = "新增技師-功能",
        Description = "新增技師-功能")]
        public ActionResult Added(TechnicianEditViewModel data)
        {

            try
            {
                if (!this.LoginUserInfo.CurrentGroupAction.AuthType.Value.HasFlag(AuthNodeType.Create))
                    throw new Exception("沒有新增權限");

                if (!ModelState.IsValid)
                    throw new ArgumentNullException($"新增技師資料時有欄位未輸入");

                //組合對象
                TvenderTechnician technician = new TvenderTechnician()
                {
                    CompCd = data.CompCd,                                                    //公司代號
                    VenderCd = data.VenderCd,                                                //廠商代號
                    Account = data.Account,                                                  //帳號             
                    Name = data.Name,                                                        //技師姓名
                    Password = Identity.ClearPassword.GetMd5Hash(data.Password).ToUpper(),   //密碼
                    Enable = data.Enable,                                                    //帳號是否啟用                 
                    IsVendor = data.IsVendor                                                  //是否為技師主管
                };

                Boolean isSuccess = _technicianService.CreateTechnician(technician);

                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"新增技師:{(isSuccess ? "成功" : "失敗")}"
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
                        Message = $"新增技師失敗,原因:{ex.Message}"
                    }

                });
            }
        }

        [MenuNode(
        Title = "修改技師-功能",
        Description = "修改技師-功能")]
        public ActionResult Edit(TechnicianEditViewModel data)
        {


            try
            {
                if (!this.LoginUserInfo.CurrentGroupAction.AuthType.Value.HasFlag(AuthNodeType.Edit))
                    throw new Exception("沒有修改權限");

                if (!ModelState.IsValid)
                    throw new ArgumentNullException($"修改技師資料時有欄位未輸入");

                //組合對象
                TvenderTechnician technician = new TvenderTechnician()
                {

                    Account = data.Account,
                    CompCd = data.CompCd,
                    VenderCd = data.VenderCd,
                    Name = data.Name,
                    Enable = data.Enable,
                    Password = !string.IsNullOrEmpty(data.NewPassword) ?             //if(新密码不为空) 新密码加密
                    Identity.ClearPassword.GetMd5Hash(data.NewPassword).ToUpper() :  //  else
                    data.Password,                                                   //  旧密码 
                    IsVendor = data.IsVendor                                                  //是否為技師主管

                };

                var con = new Conditions<DataBase.TVenderTechnician>();

                con.And(x => x.Comp_Cd == data.CompCd &&        //查詢條件
                             x.Vender_Cd == data.VenderCd &&    //
                             x.Account == data.Account);        //

                //執行修改
                Boolean isSuccess = _technicianService.UpdateTechnician(technician);

                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"修改技師:{(isSuccess ? "成功" : "失敗")}"
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
                        Message = $"修改技師失敗,原因:{ex.Message}"
                    }

                });
            }
        }

        #endregion

        
    }
}
