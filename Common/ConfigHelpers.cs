using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Common
{
    public static class ConfigHelpers
    {
        #region DB status code
        public const int STATUS_NEW = 0;
        public const int STATUS_AVAILABLE = 1;
        public const int STATUS_DISABLE = 2;
        public const int STATUS_DELETED = 3;
        #endregion

        #region Hashing
        public static string EncryptStringMD5(string str)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(str));

            int length = bytes.Length;

            for(int i = 0; i < length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString().ToUpper();
        }
        #endregion

        #region JWT token
        public const string JWT_SECRET = "helloiamjwtsecret";
        public const int JWT_EXPIRE_IN_MINUTES = 360; // 6 hours
        #endregion
    }
}