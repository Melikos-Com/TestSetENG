using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Ptc.Seteng.Helpers
{
    public static class TokenUtility
    {
        static TokenUtility()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            securityKey = GetBytes("spccNewWeb");
            validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "https://www.mywebsite.com",
                IssuerSigningKeys = new List<SecurityKey> { new SymmetricSecurityKey(securityKey) },
                ValidAudiences = new List<string> { "https://www.mywebsite.com" },
                ValidIssuer = "self"
            };



        }

        private static JwtSecurityTokenHandler tokenHandler;
        private static TokenValidationParameters validationParameters;
        private static byte[] securityKey;


        /// <summary>
        /// 產生bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;

        }
        /// <summary>
        /// 驗證token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Boolean Validate(string token)
        {

            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                var userData = principal.Claims.FirstOrDefault();
                if (userData != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // log the exception details here                     
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return false;
            }


        }
        /// <summary>
        /// 根據token取得內涵資訊
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetTokenValue(string token)
        {

            if (string.IsNullOrEmpty(token))
                return string.Empty;

            try
            {
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                var userData = principal.Claims.FirstOrDefault();
                if (userData != null)
                {
                    return userData.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }


        }
        /// <summary>
        /// 產生token
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public static string CreateToken(string accountName, string BuID, string hashPassword)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                   {
                    new Claim( ClaimTypes.UserData, accountName+ "@" + hashPassword + "@" + BuID, ClaimValueTypes.String, "(local)" )
                     }),
                    Issuer = "self", //<==發行者
                    Audience = "https://www.mywebsite.com", //<==apply address
                    Expires = DateTime.UtcNow.AddYears(1), //<== timeout
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256), //<==憑證
                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception)
            {

                return "";
            }



        }
    }
}