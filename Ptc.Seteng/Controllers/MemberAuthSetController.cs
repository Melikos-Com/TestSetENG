using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.AspnetMvc.Filter;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ptc.AspnetMvc.Authentication.Service;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ptc.Logger;
using Ptc.Logger.Service;

namespace Ptc.Seteng.Controllers
{

    [AuthenticationFilter]
    public class MemberAuthSetController : BaseController
    {

        private readonly ISystemLog _logger;
        private readonly IAspUserService _aspUserService;
        private readonly IBaseRepository<DataBase.TUSRMST, UserBase> _tusrmstRepo;
        private readonly IBaseRepository<DataBase.TSYSROL, RoleAuth> _tsysrolRepo;

        public MemberAuthSetController(ISystemLog Logger,
                                        IBaseRepository<DataBase.TUSRMST, UserBase> tusrmstRepo,
                                         IBaseRepository<DataBase.TSYSROL, RoleAuth> tsysrolRepo,
                                       IAspUserService AspUserService)
        {
            _tusrmstRepo = tusrmstRepo;
            _tsysrolRepo = tsysrolRepo;
            _logger = Logger;
            _aspUserService = AspUserService;
        }


        #region Page
        [MenuNode(
          Title = "使用者設定",
          Description = "使用者設定",
          PrefixedNodeID = "auth",
          isEntry = true)]
        public ActionResult Index()
        {

            //var selectlist = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Select(e => new SelectListItem{ Value = e.ToString() , Text = StaticHelper.GetDescriptionOfEnum((RoleType)e) });

            return View(new MemberSetSearchViewModel());
        }
        [MenuNode(
        Title = "使用者瀏覽-頁面",
        Description = "使用者瀏覽-頁面")]
        public ActionResult Search(string UserID, string Compcd)
        {
            try
            {
                #region Get UserBase
                    var con = new Conditions<DataBase.TUSRMST>();
                    //con.Include(x => x.TSYSROL);
                    con.Include(x => x.TUSRDTL);
                    con.And(x => x.Comp_Cd == Compcd &&
                                 x.User_Id == UserID);
                    UserBase user = _tusrmstRepo.Get(con);
                if (user == null)
                    throw new NullReferenceException($"no find data");
                #endregion

                #region GetRoleList
                var rolecon = new Conditions<DataBase.TSYSROL>();
                rolecon.And(x => !string.IsNullOrEmpty(x.Comp_Cd)); // 排除空白
                var rolelist = _tsysrolRepo.GetList(rolecon);
                #endregion
            
        
                
                return View("Edit", new MemberSetViewModel(user.RoleAuth ?? new RoleAuth() { PageAuth = user.PageAuth })
                {
                    AuthNodeType = AuthNodeType.Read,  //模式
                    RoleId = user.RoleId,              //權限編號
                    Compcd = user.CompCd,              //公司編號
                    UserID = user.UserId,              //帳號
                    UserName = user.UserId,            //帳號
                    PasswordHash = user.Password,      //密碼
                    CorpUserName = user.UserName,      //姓名
                    PhoneNumber = user.MobileTel,      //電話
                    Email = user.Email,                //電郵
                    RoleName = $"{user.CompCd}-{user.RoleId}",          
                    RoleNameList = rolelist            //權限下拉單
                    .Select(x => new SelectListItem()
                    {
                        Text = $"{x.CompCd}-{x.RoleName}",
                        Value = $"{x.CompCd}-{x.RoleId}",
                    }),
                });

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return View("Edit", new MemberSetViewModel());
            }
        }

        [MenuNode(
        Title = "使用者編輯-頁面",
        Description = "使用者編輯-頁面")]
        public ActionResult Edit(string UserID, string Compcd)
        {
            try
            {   
                #region Get UserBase
                var con = new Conditions<DataBase.TUSRMST>();
                //con.Include(x => x.TSYSROL);
                con.Include(x => x.TUSRDTL);
                con.And(x => x.Comp_Cd == Compcd &&
                             x.User_Id == UserID);
                UserBase user = _tusrmstRepo.Get(con);
                if (user == null)
                    throw new NullReferenceException($"no find data");
                #endregion

                #region GetRoleList
                var rolecon = new Conditions<DataBase.TSYSROL>();

                rolecon.And(x => !string.IsNullOrEmpty(x.Comp_Cd)); 

                var rolelist = _tsysrolRepo.GetList(rolecon);

                #endregion

                return View("Edit", new MemberSetViewModel(user.RoleAuth ?? new RoleAuth() { PageAuth = user.PageAuth })
                {
                    AuthNodeType = AuthNodeType.Edit,  //模式
                    RoleId = user.RoleId,              //權限編號
                    Compcd = user.CompCd,              //公司編號
                    UserID = user.UserId,              //帳號
                    UserName = user.UserId,            //帳號
                    PasswordHash = user.Password,      //密碼
                    CorpUserName = user.UserName,      //姓名
                    PhoneNumber = user.MobileTel,      //電話
                    Email = user.Email,                //電郵
                    RoleName = $"{user.CompCd}-{user.RoleId}",           
                    RoleNameList = rolelist            //權限下拉單
                    .Select(x => new SelectListItem()
                    {
                        Text = $"{x.CompCd}-{x.RoleName}",
                        Value = $"{x.CompCd}-{x.RoleId}"
                    }),
                });


            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return View("Edit", new MemberSetViewModel());
            }
        }
        #endregion

        #region ajax

        [MenuNode(
        Title = "使用者查詢-功能",
        Description = "使用者查詢-功能")]
        public ActionResult SearchMemberAuth(DataTablesReqModel<List<MemberSetSearchViewModel>> data)
        {
            List<MemberSetSearchViewModel> models = data.criteria;

            DataTablesRespModel result = new DataTablesRespModel(data.draw);

            try
            {
                Conditions<DataBase.TUSRMST> con = new Conditions<DataBase.TUSRMST>(data.length, (data.start / data.length));

                models?.ForEach(model =>
                {

                    var component = new List<Expression<Func<DataBase.TUSRMST, Boolean>>>();

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

                //con.Include(x => x.TSYSROL);
                con.Include(x => x.TUSRDTL);
                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                //con.And(x => x.Comp_Cd == _user.CompCd);


                #endregion
                var list = _tusrmstRepo.GetList(con);
         
                PagedList<UserBase> meta = new PagedList<UserBase>(list);



                result.data = list.Select(x => new MemberSetResultViewModel(x))
                                  .Select(g => new string[] {
                                      g.CompCd,
                                      g.UserID,
                                      g.UserName,
                                      g.RoleName,
                                  })
                                  .ToArray();



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
        Title = "使用者編輯-功能",
        Description = "使用者編輯-功能")]
        [HttpPost]
        public ActionResult ModifyMemberAuth(MemberSetViewModel model)
        {
            Boolean isSuccess = false;
            try
            {
                var compcd = model?.Compcd ?? model.Compcd;
                var user = new UserBase()
                {
                    CompCd = compcd,
                    UserId = model.UserID,
                    RoleAuth = new RoleAuth() { RoleName = model.RoleName },
                    PageAuth = (model?.PageAuth == null ? new List<AuthItem>() : model.PageAuth.Select(x => new AuthItem()
                    {
                        GroupName = x.id,
                        AuthType = x.AuthType
                    })
                    .ToList()),
                };

                var con = new Conditions<DataBase.TSYSROL>();

                con.And(x => x.Comp_Cd == compcd &&
                             x.Role_Id == model.RoleName);
                var roleAuth = _tsysrolRepo.Get(con);

                if (roleAuth == null)
                    throw new Exception("[ERROR]=>該腳色未設定權限");

                var role = new RoleAuth()
                {
                    CompCd = compcd,
                    RoleId = model.RoleName,
                    UpdateTime = DateTime.Now,
                    PageAuth = roleAuth.PageAuth
                };
          
                isSuccess = _aspUserService.Update(user, role);

                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = true,
                        Message = $"修改帳號資料:{(isSuccess ? "成功" : "失敗")}"

                    }

                });


            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Json(new JsonResult()
                {
                    Data = new
                    {
                        IsSuccess = false,
                        Message = $"修改帳號資料失敗,原因:{ex.Message}"
                    }

                });
            }
        }
        [MenuNode(
        Title = "取得TreeView-功能",
        Description = "取得TreeView-功能")]
        public ActionResult RefreshTreeView(string RoleID, string Compcd)
        {
           
            JsonResult Result = new JsonResult();
            RoleAuth roleAuth = new RoleAuth();
            try
            {
                var con = new Conditions<DataBase.TSYSROL>();

                con.And(x => x.Comp_Cd == Compcd &&
                             x.Role_Id == RoleID);

                roleAuth = _tsysrolRepo.Get(con);

                if (roleAuth == null)
                    throw new NullReferenceException($"no find data");

                Result.Data = new MemberSetViewModel(roleAuth);
   
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Json(Result);
        }
        #endregion

    }





}