using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SecretHelper
{
    public static class Md5
    {
        public static string Encrypt(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(s).Replace("-", "").ToUpper();
        }

    }

}
