using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ptc.Seteng.Helpers;
using Ptc.AspnetMvc.Filter;
using Ptc.AspnetMvc.Authentication.Repository;
using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.AspnetMvc.Authentication;

using Ptc.Seteng.Repository;
using Ptc.Seteng.Models;
using Ptc.AspnetMvc.Authentication.Service;
using Ptc.Seteng.Filter;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ptc.Logger;
using Ptc.Logger.Service;

namespace Ptc.Seteng.Controllers
{

    [AuthenticationFilter]
    public class MasterAuthSetController : BaseController
    {

        private readonly ISystemLog _logger;
        private readonly IAspUserRepository _authRepo;
        private readonly IAspUserService _aspUserService;
        private readonly IBaseRepository<DataBase.TSYSROL, RoleAuth> _tsysrolRepo;
        private readonly IBaseRepository<DataBase.TSYSROL, Tsysrol> _uptsysrolRepo;
        public MasterAuthSetController(ISystemLog logger,
                                       IAspUserRepository authRepo,
                                       IAspUserService aspUserService,
                                       IBaseRepository<DataBase.TSYSROL, RoleAuth> tsysrolRepo,
                                        IBaseRepository<DataBase.TSYSROL, Tsysrol> uptsysrolRepo)
        {
            _logger = logger;
            _authRepo = authRepo;
            _aspUserService = aspUserService;
            _tsysrolRepo = tsysrolRepo;
            _uptsysrolRepo = uptsysrolRepo;
        }


        #region Page
        [MenuNode(
         Title = "權限設定",
         Description = "權限設定",
         PrefixedNodeID = "auth",
         isEntry = true)]
        public ActionResult Index() => View(new AuthSetViewModel());
  
        [MenuNode(
         Title = "權限修改-頁面",
         Description = "權限修改-頁面")]
        [HttpGet]
        public ActionResult Edit(string RoleId, string Compcd)
        {
            try
            {

                var con = new Conditions<DataBase.TSYSROL>();

                con.And(x => x.Comp_Cd == Compcd &&
                             x.Role_Id == RoleId);

                RoleAuth roleAuth = _tsysrolRepo.Get(con);

                if (roleAuth == null)
                    throw new NullReferenceException($"no find data");

                return View("Edit", new RoleAuthViewModel(roleAuth, new List<AuthData>()){ WorkType = AuthNodeType.Edit });

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
            return View();


        }
        [MenuNode(
         Title = "權限瀏覽-頁面",
         Description = "權限瀏覽-頁面")]
        [HttpGet]
        public ActionResult Search(string RoleId, string Compcd)
        {
            try
            {


                var con = new Conditions<DataBase.TSYSROL>();

                con.And(x => x.Comp_Cd == Compcd &&
                             x.Role_Id == RoleId);

                RoleAuth roleAuth = _tsysrolRepo.Get(con);

                if (roleAuth == null)
                    throw new NullReferenceException($"no find data");

                return View("Edit", new RoleAuthViewModel(roleAuth, new List<AuthData>()) { WorkType = AuthNodeType.Read });

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
            return View();
        }
        #endregion

        #region ajax
        [MenuNode(
        Title = "權限查詢-功能",
        Description = "權限查詢-功能")]
        public ActionResult SearchMasterAuth(DataTablesReqModel<List<AuthSetViewModel>> data)
        {
            List<AuthSetViewModel> models = data.criteria;

            DataTablesRespModel result = new DataTablesRespModel(data.draw);

            try
            {
                Conditions<DataBase.TSYSROL> con = new Conditions<DataBase.TSYSROL>(data.length, (data.start / data.length));

                models?.ForEach(model =>
                {

                    var component = new List<Expression<Func<DataBase.TSYSROL, Boolean>>>();

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

                //con.Include(x => x.TUSRMST);
                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);


                #endregion
                var list = _tsysrolRepo.GetList(con);

                PagedList<RoleAuth> meta = new PagedList<RoleAuth>(list);



                result.data = list.Select(x => new RoleAuthViewModel(x, new List<AuthData>()))
                                  .Select(g => new string[]{
                                      g.Compcd,
                                      g.RoleName,
                                      g.RoleId,
                                  }).ToArray();

                result.TotalCount(con.TotalCount);

            }
            catch (Exception ex)
            {

                _logger.Error(ex);
                result.error = ex.Message;
            }
            return Json(result);
        }
        [MenuNode(
         Title = "權限編輯-功能",
         Description = "權限編輯-功能")]
        [HttpPost]
        public ActionResult ModifyMasterAuth(RoleAuthViewModel model)
        {
            Boolean isSuccess = false;
            try
            {


                var con = new Conditions<DataBase.TSYSROL>();

                var compcd = model?.Compcd ?? string.Empty;

                con.And(x => x.Comp_Cd == compcd &&
                             x.Role_Id == model.RoleId);

                RoleAuth roleAuth = _tsysrolRepo.Get(con);

                if (roleAuth == null)
                    throw new NullReferenceException($"no find data");
                 
                List<AuthItem> pageAuth = model.PageAuth == null ? new List<AuthItem>() :
                    model.PageAuth.Select(x => new AuthItem() { GroupName = x.id, AuthType = x.AuthType }).ToList();
                Tsysrol updaterole = new Tsysrol()
                {
                    RoleId = model.RoleId,
                    RoleName = model.RoleName,
                    CompCd = model.Compcd,
                    PageAuth = pageAuth != null ? JsonConvert.SerializeObject(pageAuth) : string.Empty,
                    UpdateDate = DateTime.Now,
                    UpdateUser = User.Identity.Name,

                };

                con.Allow(y => y.Role_Name,
                          y => y.PageAuth,
                          y => y.Update_Date,
                          y => y.Update_User);

                isSuccess = _uptsysrolRepo.Update(con, updaterole);

                MvcSiteMapProvider.SiteMaps.ReleaseSiteMap();

                return Json(new JsonResult()
                {

                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"修改權限:{(isSuccess ? "成功" : "失敗")}"
                    }

                });
            }
            catch (Exception ex)
            {
                return Json(new JsonResult()
                {

                    Data = new
                    {
                        IsSuccess = isSuccess,
                        Message = $"修改權限:{(isSuccess ? "成功" : "失敗")}"
                    }

                });
            }
        }
        #endregion


    }
}