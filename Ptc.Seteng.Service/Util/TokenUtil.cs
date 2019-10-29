using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Ptc.Seteng
{
    public static class TokenUtil<T>
    {
        private static JwtSecurityTokenHandler tokenHandler;
        private static TokenValidationParameters validationParameters;
        private static byte[] securityKey;

        static TokenUtil()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            securityKey = GetBytes("anyoldrandomtext");
            validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "https://www.mywebsite.com",
                IssuerSigningKeys = new List<SecurityKey> { new SymmetricSecurityKey(securityKey) },
                ValidAudiences = new List<string> { "https://www.mywebsite.com" },
                ValidIssuer = "self"
            };

        }

        public static byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Boolean HasAny(string token)
        {

            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                SecurityToken securityToken;

                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                var userData = principal.Claims.FirstOrDefault();

                return (userData != null);

            }
            catch (Exception ex)
            {
                // log the exception details here                     
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return false;
            }
        }

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T Parse(string token)
        {

            if (string.IsNullOrEmpty(token))
                throw new NullReferenceException("no input token");

            try
            {
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                var userData = principal.Claims.FirstOrDefault();

                if (userData == null)
                    throw new NullReferenceException("parse error");

                return JsonConvert.DeserializeObject<T>(userData.Value);
            }
            catch (Exception ex)
            {

                return default(T);
            }


        }

        /// <summary>
        /// 產生token
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public static string Create(T user)
        {
            try
            {



                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                   {
                    new Claim( ClaimTypes.UserData, JsonConvert.SerializeObject(user, Formatting.Indented,new JsonSerializerSettings
{
    PreserveReferencesHandling = PreserveReferencesHandling.Objects
}), ClaimValueTypes.String, "(local)" )
                   }),
                    Issuer = "self",
                    Audience = "https://www.mywebsite.com",
                    Expires = DateTime.UtcNow.AddYears(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256), //<==憑證
                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {

                return "";
            }

        }

    }
}