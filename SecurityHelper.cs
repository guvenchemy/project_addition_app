using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace project_addition_app
{
    internal class SecurityHelper
    {
        public static string HashPassword(string sifre)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] sifreBytes = Encoding.UTF8.GetBytes(sifre);
                byte[] hashBytes = sha256.ComputeHash(sifreBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public static bool CheckPasswordHash(string sifre, string hashliSifre)
        {
            string yeniHashliSifre = HashPassword(sifre);
            return yeniHashliSifre == hashliSifre;
        }
    }
}
