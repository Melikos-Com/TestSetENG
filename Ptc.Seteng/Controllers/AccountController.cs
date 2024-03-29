﻿using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ptc.Seteng.Models;
using Ptc.Seteng.Helpers;
using Newtonsoft.Json;
using Ptc.AspnetMvc.Authentication;
using System.Security.Cryptography;
using System;
using System.Text;
using Ptc.AspnetMvc.Models;
using Ptc.Seteng.Service;
using Ptc.AspnetMvc.Authentication.Service;

namespace Ptc.Seteng.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }
        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ServerProfile.GetInstance().Debugger=="0")
                return View();
            else
            {
                Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                return null;
            }           
           
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (MethodHelper.IsNullOrEmpty(model.User_Id,model.Password,model.CompCd))
            {
                ModelState.AddModelError("", "請輸入帳號密碼。");
                if (ServerProfile.GetInstance().Debugger == "0")
                    return View();
                else
                {
                    Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                    return null;
                }
            }
            

            var jsonUserInfo = JsonConvert.SerializeObject(new UserBase()
            {
                CompCd = model.CompCd,
                UserId = model.User_Id
            });
            //var test = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "md5");
            
            //轉成MD5
            var md5Password = Identity.ClearPassword.GetMd5Hash(model.Password).ToUpper();

            var user = await UserManager.FindAsync(jsonUserInfo, md5Password);


            if (user == null)
            {
                ModelState.AddModelError("", "使用者帳號或密碼無效。");
                if (ServerProfile.GetInstance().Debugger == "0")
                    return View();
                else
                {
                    Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                    return null;
                }
            }

            await SignInAsync(user, model.RememberMe);

            return  RedirectToAction("Index", "Technician");

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoginByToken(string tokenBase64, string returnUrl)
        {    
            //轉成LoginViewModel
            LoginViewModel model = new LoginViewModel();

            string token = System.Text.Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(tokenBase64));

            //驗證結果
            var isSuccess = TokenUtility.Validate(token);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "TOKEN驗證異常");
                if (ServerProfile.GetInstance().Debugger == "0")
                    return RedirectToAction("Index", "Home");
                else
                {
                    Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                    return null;
                }
            }


            //取得內容
            var tokenString = TokenUtility.GetTokenValue(token);
            if (string.IsNullOrEmpty(tokenString))
            {
                ModelState.AddModelError("", "TOKEN 取得內容異常");
                if (ServerProfile.GetInstance().Debugger == "0")
                    return RedirectToAction("Index", "Home");
                else
                {
                    Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                    return null;
                }
            }


            var tokenArray = tokenString.Split('@');
            model.User_Id = tokenArray[0];
            model.Password = tokenArray[2];
            model.CompCd = tokenArray[1];
            if (MethodHelper.IsNullOrEmpty(model.User_Id,
                                            model.CompCd))
            {
                ModelState.AddModelError("", "解析內容異常");
            }

            //轉成json
            var jsonUserInfo = JsonConvert.SerializeObject(new UserBase()
            {
                CompCd = model.CompCd,
                UserId = model.User_Id
            });
            var user = await UserManager.FindAsync(jsonUserInfo, model.Password);


            if (user == null)
            {
                ModelState.AddModelError("", "使用者帳號或密碼無效。");
                if (ServerProfile.GetInstance().Debugger == "0")
                    return RedirectToAction("Index", "Home");
                else
                {
                    Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                    return null;
                }
            }

            await SignInAsync(user, model.RememberMe);


            return RedirectToLocal(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath + returnUrl);
           


        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            //return View();
            Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
            return null;
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AspNet.Identity.IdentityUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);

                    // 如需如何启用帐户确认和密码重设的详细信息，请造访 http://go.microsoft.com/fwlink/?LinkID=320771
                    // 传送包含此链接的电子邮件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "确认您的账户", "请单击此连结确认您的账户 <a href=\"" + callbackUrl + "\">这里</a>");

                    if (ServerProfile.GetInstance().Debugger == "0")
                        return RedirectToAction("Index", "Home");
                    else
                    {
                        Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                        return null;
                    }
                }
                else
                {
                    AddErrors(result);
                }
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "使用者不存在或未確認。");
                    return View();
                }

                // 如需如何启用帐户确认和密码重设的详细信息，请造访 http://go.microsoft.com/fwlink/?LinkID=320771
                // 传送包含此链接的电子邮件
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "重设密码", "请按 <a href=\"" + callbackUrl + "\">这里</a> 重设密码");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            if (ServerProfile.GetInstance().Debugger == "0")
                return View();
            else
            {
                Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                return null;
            }
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "找不到使用者。");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            if (ServerProfile.GetInstance().Debugger == "0")
                return View();
            else
            {
                Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                return null;
            }
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                await SignInAsync(user, isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "您的密碼已變更。"
                : message == ManageMessageId.SetPasswordSuccess ? "已設您的密碼。"
                : message == ManageMessageId.RemoveLoginSuccess ? "已移除外部登入。"
                : message == ManageMessageId.Error ? "發生錯誤。"
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // 用户没有密码，因此，请移除遗漏 OldPassword 字段所导致的任何验证错误
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 要求重新导向至外部登入提供者
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // 若用户已经有登入數據，请使用此外部登入提供者登入使用者
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // 若使用者没有账户，请提示使用者建立账户
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // 要求重新导向至外部登入提供者以连结目前使用者的登入
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // 从外部登入提供者处取得用户信息
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new AspNet.Identity.IdentityUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);

                        // 如需如何启用帐户确认和密码重设的详细信息，请造访 http://go.microsoft.com/fwlink/?LinkID=320771
                        // 传送包含此链接的电子邮件
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // SendEmail(user.Email, callbackUrl, "确认您的账户", "请单击此连结确认您的账户");

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            MvcSiteMapProvider.SiteMaps.ReleaseSiteMap();
            Ptc.AspnetMvc.Util.Cache.Clear();
            AuthenticationManager.SignOut();
            if (ServerProfile.GetInstance().Debugger == "0")
                return RedirectToAction("Index", "Home");
            else
            {
                Response.Redirect(ServerProfile.GetInstance().WebFormUrl, false);
                return null;
            }
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helper
        // 新增外部登入時用来当做 XSRF 保护
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(Ptc.AspNet.Identity.IdentityUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // 如需传送邮件的详细信息，请造访 http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
