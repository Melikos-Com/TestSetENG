using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.AspnetMvc.Authentication.Repository;
using Ptc.AspnetMvc.Authentication.Service;
using Ptc.AspnetMvc.Models;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Transactions;
using System.Web;
using System.Web.Mvc.Filters;

namespace Ptc.AspnetMvc.Filter
{
    public class AuthenticationFilter : System.Web.Mvc.ActionFilterAttribute, IAuthenticationFilter
    {
        public IAspUserService _userService { get; set; }
   

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //未登入
            if (filterContext.Principal == null || !filterContext.Principal.Identity.IsAuthenticated)
            {
                //filterContext.Controller.TempData["UnauthorizedMessage"] = "尚未登入";
                filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
                return;
            }

            var currentUser = new UserBase();
            #region 取得用户信息
            try
            {
                currentUser = _userService.Integration(filterContext.Principal.Identity.Name);
                currentUser.CompCd = "711"; //目前網頁只for超商，部份廠商服務多個Bu(帳號主檔無公司別)
                if (currentUser == null)
                {
                    filterContext.Controller.TempData["UnauthorizedMessage"] = "找不到用户信息";
                    filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
                    return;
                }

                currentUser.CalcAuth();
            }
            catch (Exception ex)
            {

                filterContext.Controller.TempData["UnauthorizedMessage"] = ex.Message;
                filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
                return;
            }


            #endregion


            #region 检查controller action上的meta Data

            //取得Menu上的權限定義
            MenuNodeAttribute actionAuthDefine = null;
            foreach (object item in filterContext.ActionDescriptor.GetCustomAttributes(false))
            {
                actionAuthDefine = item as MenuNodeAttribute;
                if (actionAuthDefine != null)
                    break;
            }

            #endregion

            #region 取得controller加action的組合名稱groupName

            string groupName = "bypass";
            if (actionAuthDefine != null)
            {
                //組合GroupName
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                if (string.IsNullOrEmpty(actionAuthDefine.GroupName))
                    groupName = controllerName;
                else
                    groupName = controllerName + "_" + actionAuthDefine.GroupName;
            }

            #endregion


            PtcIdentity id = null;

            try
            {
                //没有设定meta Data
                if (groupName == "bypass")
                {
                    id = new PtcIdentity(
                                filterContext.Principal.Identity,
                                currentUser,
                                "No GroupName",
                                null);
                }
                else
                {
                    id = new PtcIdentity(
                          filterContext.Principal.Identity,
                          currentUser,
                          groupName,
                          actionAuthDefine);
                }


                //产生身份信息
                filterContext.Principal =
                  new GenericPrincipal(id, null);
            }
            catch (Exception ex)
            {
                filterContext.Controller.TempData["UnauthorizedMessage"] = ex.Message;
                filterContext.Result = new System.Web.Mvc.HttpUnauthorizedResult();
                return;
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //  Debug.WriteLine("OnAuthenticationChallenge");
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var cookiesColl = filterContext.HttpContext.Request.Cookies;

            foreach (var cookie in cookiesColl)
            {
                //取得cookie Name
                string cookieName = cookie.ToString();

                //透过action检查cookie上是否
                if (filterContext.ActionParameters.ContainsKey(cookieName))
                {
                    //cookie上的信息为NULL
                    if (string.IsNullOrEmpty(cookiesColl[cookieName].Value))
                        continue;

                    //转换成JObject
                    JObject restoredObject = JsonConvert.DeserializeObject<JObject>(
                        System.Web.HttpUtility.UrlDecode(cookiesColl[cookieName].Value));

                    //把Cookie塞入物件
                    Type type = filterContext.ActionParameters[cookieName].GetType();
                    foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                    {
                        //取得JSON上的JProperty
                        var existProperty = restoredObject.Properties().Where(x => x.Name == pi.Name).FirstOrDefault();

                        //找不到就跳一下个
                        if (existProperty == null)
                            continue;

                        //取得原始Obj上的值
                        object currentValue = type.GetProperty(pi.Name).GetValue(filterContext.ActionParameters[cookieName], null);

                        //从JProperty上转换出的值
                        object newValue = null;

                        //依型态转型
                        switch (type.GetProperty(pi.Name).PropertyType.ToString())
                        {
                            case "System.Nullable`1[System.DateTime]":
                                newValue = string.IsNullOrEmpty(existProperty.Value.ToString()) ? null : (DateTime?)Convert.ToDateTime(existProperty.Value.ToString());

                                if (((DateTime?)currentValue).HasValue == false)
                                {
                                    if (newValue != null)
                                    {
                                        type.GetProperty(pi.Name).SetValue(filterContext.ActionParameters[cookieName], newValue, null);
                                    }
                                }

                                break;

                            case "System.String":
                                var newPropertyValue = existProperty.Value.ToString();
                                newValue = string.IsNullOrEmpty(newPropertyValue) == true ? null : newPropertyValue;
                                if (string.IsNullOrEmpty((string)currentValue))
                                {
                                    if (newValue != null)
                                    {
                                        type.GetProperty(pi.Name).SetValue(filterContext.ActionParameters[cookieName], newValue, null);
                                    }
                                }

                                break;

                            case "System.Boolean":
                                var newBooleanValue = existProperty.Value.ToString();
                                newValue = string.IsNullOrEmpty(newBooleanValue) == true ? false : Convert.ToBoolean(newBooleanValue);
                                //if (currentValue != newValue && (currentValue == null || !currentValue.Equals(newValue)))
                                //{
                                //    type.GetProperty(pi.Name).SetValue(filterContext.ActionParameters[cookieName], newValue, null);
                                //}

                                if (((bool)currentValue) == false)
                                {
                                    if (newValue != null)
                                    {
                                        type.GetProperty(pi.Name).SetValue(filterContext.ActionParameters[cookieName], newValue, null);
                                    }
                                }

                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
