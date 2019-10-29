using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ptc.Seteng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc.Filters;
using Ptc.AspnetMvc.Authentication.Repository;
using System.Web.Http.Controllers;
using Ptc.AspnetMvc.Authentication;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Factory;

namespace Ptc.Seteng.Filter
{
    public class TokenAuthenticationFilter : System.Web.Http.AuthorizeAttribute
    { 

        public IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _userRepo { get; set; }
        public IBaseRepository<DataBase.TUSRMST, Tusrmst> _usermstRepo { get; set; }

        public IBaseRepository<DataBase.TUSRVENRELATION, TUSRVENRELATION> _TUSRVENRELATIONRepo { get; set; }

        public TokenAuthenticationFilter( )
        { 
           
             _userRepo = new BaseRepository<DataBase.TVenderTechnician, TvenderTechnician>();
             _usermstRepo = new BaseRepository<DataBase.TUSRMST, Tusrmst >();
            _TUSRVENRELATIONRepo = new BaseRepository<DataBase.TUSRVENRELATION, TUSRVENRELATION>();

        }

        /// <summary>
        /// 取得bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
           

            var token = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query).Get("token");

            if (token == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = GetBytes("anyoldrandomtext");
                var validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = "https://www.mywebsite.com",
                    ValidateLifetime = true,
                    IssuerSigningKeys = new List<SecurityKey> { new SymmetricSecurityKey(securityKey) },
                    ValidAudiences = new List<string> { "https://www.mywebsite.com" },
                    ValidIssuer = "self"
                };

                try
                {
                    SecurityToken securityToken;
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                    var userData = principal.Claims.FirstOrDefault();

                    if (userData != null)
                    {

                        //解析token
                        var input = JsonConvert.DeserializeObject<TvenderTechnician>(userData.Value);

                        var con = new Conditions<DataBase.TVenderTechnician>();

                        var password =  Identity.ClearPassword.GetMd5Hash(input.Password).ToUpper();

                        con.And(x => x.Account == input.Account &&
                                     x.Password == password);

                        TvenderTechnician user = _userRepo.Get(con);
                        //查无使用者
                        if (user == null)
                            throw new Exception("no find user info");
                        //使用者已關閉
                        if (user.Enable == false )
                            throw new Exception("user info is not Enable");

                        var tusrvenlation = new Conditions<DataBase.TUSRVENRELATION>();
                        tusrvenlation.And(x => x.Comp_Cd == user.CompCd);
                        tusrvenlation.And(x => x.Vender_Cd == user.VenderCd);
                        var resault = _TUSRVENRELATIONRepo.Get(tusrvenlation);
                        if (resault == null)
                            throw new Exception("Vender is not find");

                        //廠商已關閉 
                        var conUser = new Conditions<DataBase.TUSRMST>();
                        conUser.And(x => x.Comp_Cd == user .CompCd || x.Comp_Cd=="");
                        conUser.And(x => x.User_Id == resault.User_Id);
                        conUser.And(x => x.Role_Id == "VENDER" || x.Role_Id == "CafeVender" || x.Role_Id == "APPVENDER");
                        conUser.And(x => x.Id_Sts == "Y");
                        if (_usermstRepo.Count(conUser) == 0)
                            throw new Exception("user vender is close");

                        // 裝置不同
                        if (user.DeviceID != input.DeviceID )
                            throw new Exception("different device");

                        var identity = new AspnetMvc.Models.PtcIdentity(
                           System.Threading.Thread.CurrentPrincipal.Identity,
                           new UserBase()
                           {
                               VenderCd = user.VenderCd,
                               CompCd = user.CompCd,
                               UserName = user.Name,
                               UserId = user.Account,
                               Password = input.Password
                           },
                           "phone",
                           null);

                        SetPrincipal(new GenericPrincipal(identity, null));
                    }
                    else
                    {
                        HandleUnauthorizedRequest(actionContext);
                    }
                }
                catch (Exception)
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //base.OnAuthorization(actionContext);
        }

        private void SetPrincipal(IPrincipal principal)
        {
            System.Threading.Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }


    }
}