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
    public class StoreTokenAuthenticationFilter : System.Web.Http.AuthorizeAttribute
    { 

           public IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _userRepo { get; set; }
        public IBaseRepository<DataBase.TUSRMST, Tusrmst> _usermstRepo { get; set; }

        public StoreTokenAuthenticationFilter( )
        { 
           
             _userRepo = new BaseRepository<DataBase.TVenderTechnician, TvenderTechnician>();
             _usermstRepo = new BaseRepository<DataBase.TUSRMST, Tusrmst >();


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

           // var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1xyXG4gIFwiJGlkXCI6IFwiMVwiLFxyXG4gIFwiQ29tcENkXCI6IG51bGwsXHJcbiAgXCJDb21wU2hvcnRcIjogbnVsbCxcclxuICBcIlVzZXJJZFwiOiBcIkFtb25cIixcclxuICBcIlVzZXJOYW1lXCI6IG51bGwsXHJcbiAgXCJOaWNrbmFtZVwiOiBudWxsLFxyXG4gIFwiUm9sZUlkXCI6IG51bGwsXHJcbiAgXCJab0NkXCI6IG51bGwsXHJcbiAgXCJWZW5kZXJDZFwiOiBudWxsLFxyXG4gIFwiVmVuZGVyTmFtZVwiOiBudWxsLFxyXG4gIFwiU3RvcmVDZFwiOiBudWxsLFxyXG4gIFwiU3RvcmVOYW1lXCI6IG51bGwsXHJcbiAgXCJFbWFpbFwiOiBudWxsLFxyXG4gIFwiUGFzc3dvcmRcIjogXCIxMjg3NjI2NlwiLFxyXG4gIFwiVGVsTm9cIjogbnVsbCxcclxuICBcIkZheE5vXCI6IG51bGwsXHJcbiAgXCJNb2JpbGVUZWxcIjogbnVsbCxcclxuICBcIklkU3RzXCI6IGZhbHNlLFxyXG4gIFwiQ3JlYXRlX1VzZXJcIjogbnVsbCxcclxuICBcIkNyZWF0ZV9EYXRlXCI6IFwiMDAwMS0wMS0wMVQwMDowMDowMFwiLFxyXG4gIFwiVXBkYXRlX1VzZXJcIjogbnVsbCxcclxuICBcIlVwZGF0ZV9EYXRlXCI6IG51bGwsXHJcbiAgXCJUb2tlblwiOiBudWxsLFxyXG4gIFwiRGV2aWNlSURcIjogXCJub25lXCIsXHJcbiAgXCJSZWdpc3RyYXRpb25JRFwiOiBudWxsLFxyXG4gIFwiUGFnZUF1dGhcIjogbnVsbCxcclxuICBcIlJvbGVBdXRoXCI6IG51bGwsXHJcbiAgXCJVc2VyUGFnZUF1dGhcIjoge30sXHJcbiAgXCJEYXRhUmFuZ2VcIjogbnVsbFxyXG59IiwibmJmIjoxNDk2MjQzNTI5LCJleHAiOjE1Mjc3Nzk1MjksImlhdCI6MTQ5NjI0MzUyOSwiaXNzIjoic2VsZiIsImF1ZCI6Imh0dHBzOi8vd3d3Lm15d2Vic2l0ZS5jb20ifQ.1ykpD48w49nVa2zcZnMh-edk-eYHckL1m33miY0Sp24";
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
                        var input = JsonConvert.DeserializeObject<UserBase>(userData.Value);

                        var con = new Conditions<DataBase.TUSRMST >();

                        var password =  Identity.ClearPassword.GetMd5Hash(input.Password  .ToUpper()).ToUpper();

                        con.And(x => x.User_Id  == input.UserId  &&
                                     x.TUSRDTL .Pass_Wd   == password);
                        con.Include(x=>x.TUSRDTL );
                        Tusrmst  user = _usermstRepo.Get(con);
                        //查无使用者
                        if (user == null)
                            throw new Exception($"no find user info");
                        //使用者未審核通過
                        if (!user.IdSts )
                            throw new Exception($"user info is not IdSts");

                        // 裝置不同
                        if (user.DeviceID != input.DeviceID)
                            throw new Exception($"different device");
                        // 密碼修改
                        if (user.TUSRDTL.PassWd   != password)
                            throw new Exception($"Password is changed");

                        var identity = new AspnetMvc.Models.PtcIdentity(
                           System.Threading.Thread.CurrentPrincipal.Identity,
                           new UserBase()
                           {
                               CompCd = user.CompCd,//公司代號
                               RoleId = user.RoleId,//角色 
                               UserName = user.UserName ,//使用者姓名
                               UserId = user.UserId ,//使用者帳號
                               Password = input.Password ,//使用者密碼
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