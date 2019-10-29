using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Identity
{
    public class ClearPassword : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            //相容舊系統直接登入，MD5從登入時轉換
            if (hashedPassword.Equals(providedPassword))
                return PasswordVerificationResult.Success;

            else return PasswordVerificationResult.Failed;
        }
        public static string GetMd5Hash(string input)
        {

            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input.ToUpper()));


            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
