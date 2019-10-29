using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Models;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Ptc.Seteng.Controllers.api
{

    /// <summary>
    /// 使用者登入相關
    /// </summary>
    [AllowAnonymous]
    public class UserController : ApiController
    {

        private readonly ISystemLog _logger;
        private readonly IClientUserService _userService;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IImgRepository _ImgRepo;


        public UserController(ISystemLog Logger,
                              IClientUserService UserService,
                              IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                              IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo,
                              IImgRepository ImgRepo)
        {
            _logger = Logger;
            _userRepo = UserRepo;
            _userService = UserService;
            _technicianRepo = TechnicianRepo;
            _ImgRepo = ImgRepo;

        }


        /// <summary>
        /// [廠商] 登入
        /// </summary>
        [HttpGet]
        public HttpResponseMessage VendorLogin(string Account, string Password, string UUID)
        {
            try
            {
                if (MethodHelper.IsNullOrEmpty(Account, Password, UUID))
                    throw new ArgumentNullException($"未輸入帳號密碼");

                //取得MD5資訊
                var md5Password = Identity.ClearPassword.GetMd5Hash(Password).ToUpper();

                TvenderTechnician result = _userService.VendorLogin(Account, md5Password, UUID);


                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<TechnicianResultApiViewModel>(new TechnicianResultApiViewModel(result)
                   {
                       Password = Password,
                       Token = TokenUtil<TvenderTechnician>.Create(new TvenderTechnician()
                       {
                           Account = result.Account,
                           Password = Password,
                           DeviceID = UUID,
                       })

                   }, "登入成功", 1, true));

            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Request.CreateResponse(
                          HttpStatusCode.OK,
                          new JsonResult<object>(new { clearUUID = true }, ex.ParamName, 1, true));

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new JsonResult<TechnicianResultApiViewModel>(null, ex.Message, 1, false));
            }

        }
        /// <summary>
        /// [廠商] 登出
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage VendorLogout()
        {
            try
            {
                if (this.User == null)
                    throw new ArgumentNullException($"[ERROR]=> 廠商登出時,沒有輸入帳號密碼");

                var user = ((PtcIdentity)this.User.Identity).currentUser;

                //取得MD5資訊
                var md5Password = Identity.ClearPassword.GetMd5Hash(user.Password).ToUpper();

                Boolean isSuccess = _userService.VendorLogout(user.UserId, md5Password);

                if (!isSuccess)
                    throw new Exception("[ERROR]=> 廠商登出時,登出失敗");

                return Request.CreateResponse(
                HttpStatusCode.OK,
                new JsonResult<Boolean>(isSuccess, "[廠商]登出成功", 1, true));

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.OK,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }

        }
        /// <summary>
        /// 更新RegID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpGet]
        public HttpResponseMessage UpdateRegID(string RegId)
        {
            try
            {

                if (this.User == null)
                    throw new ArgumentNullException($"[ERROR]=> 廠商刷新推播碼時,沒有輸入對應資訊");

                var user = ((PtcIdentity)this.User.Identity).currentUser;


                var con = new Conditions<DataBase.TVenderTechnician>();

                //取得MD5資訊
                var md5Password = Identity.ClearPassword.GetMd5Hash(user.Password).ToUpper();

                con.And(x => x.Account == user.UserId &&
                             x.Password == md5Password);

                con.Allow(x => x.RegistrationID);

                Boolean isSuccess = _technicianRepo.Update(con, new TvenderTechnician()
                {
                    Account = user.UserId,
                    Password = md5Password,
                    RegistrationID = RegId,
                });

                return Request.CreateResponse(
                              HttpStatusCode.OK,
                              new JsonResult<Boolean>(true, "刷新推播碼成功", 1, true));


            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }


        }
        /// <summary>
        /// 清除設備識別值
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ClearUUID(string Account, string Password)
        {
            try
            {

                if (MethodHelper.IsNullOrEmpty(Account, Password))
                    throw new ArgumentNullException($"[ERROR]=> 清除時,沒有輸入帳號密碼");

                var con = new Conditions<DataBase.TVenderTechnician>();

                //取得MD5資訊
                var md5Password = Identity.ClearPassword.GetMd5Hash(Password).ToUpper();

                con.And(x => x.Account == Account &&
                             x.Password == md5Password);

                con.Allow(x => x.DeviceID);

                Boolean isSuccess = _technicianRepo.Update(con, new TvenderTechnician()
                {

                    DeviceID = "",
                    Account = Account,
                    Password = md5Password

                });

                if (!isSuccess)
                    throw new Exception("清空設備識別值發生錯誤");

                return Request.CreateResponse(
                              HttpStatusCode.OK,
                              new JsonResult<Boolean>(true, "清空設備識別值成功", 1, true));

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateResponse(
                    HttpStatusCode.OK,
                    new JsonResult<TechnicianResultApiViewModel>(null, $"{ ex.GetType().Name}:Message:{ex.Message}", 1, false));
            }

        }
        /// <summary>
        /// 驗證Token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage IsTokenValid(string Token)
        {
            try
            {

                if (MethodHelper.IsNullOrEmpty(Token))
                    throw new ArgumentNullException("登入失敗");

                var tokenTechnician = TokenUtil<TvenderTechnician>.Parse(Token);

                if (tokenTechnician == default(TvenderTechnician))
                    throw new Exception("登入失敗");

                var con = new Conditions<DataBase.TVenderTechnician>();

                //取得MD5資訊
                var md5Password = Identity.ClearPassword.GetMd5Hash(tokenTechnician.Password).ToUpper();

                con.And(x => x.Account == tokenTechnician.Account &&
                             x.Password == md5Password);

                var currentTechnician = _technicianRepo.Get(con);


                if (currentTechnician == null)
                    throw new Exception("登入失敗");


                return Request.CreateResponse(
                              HttpStatusCode.OK,
                              new JsonResult<Boolean>(true, "使用者驗證成功", 1, true));

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    $"{ ex.GetType().Name}:Message:{ex.Message}");
            }

        }
        /// <summary>
        /// 驗證APP版本號(廠商)
        /// </summary>
        /// <param name="Version"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckVersion(string Version)
        {
            try
            {
                string NowVersion = ServerProfile.GetInstance().VendorVesion;
                string NowVersionNew = ServerProfile.GetInstance().VendorVesionNew;

                if (Version != NowVersion && Version != NowVersionNew)
                {
                    _logger.Error("版本不正確，登入者的廠商APP版本：" + Version);
                    return Request.CreateResponse(
                             HttpStatusCode.OK,
                             new JsonResult<Boolean>(false, "版本不正確", 0, false));
                }
                else
                    return Request.CreateResponse(
                                 HttpStatusCode.OK,
                                 new JsonResult<Boolean>(true, "版本正確", 1, true));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + "，登入者的廠商APP版本：" + Version);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    $"{ ex.GetType().Name}:Message:{ex.Message}");
            }
        }

        /// <summary>
        /// 取得下載連結
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage AppLink(string index)
        {
            try
            {
                string link = _ImgRepo.GetLink(index);
                return Request.CreateResponse(
                                 HttpStatusCode.OK,
                                 new JsonResult<Boolean>(true, link, 1, true));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                    $"{ ex.GetType().Name}:Message:{ex.Message}");
            }
        }

    }
}
