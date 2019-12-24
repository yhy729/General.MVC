using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace General.Core.Librs
{
    public class EncryptorHelper
    {
        /// <summary>
        /// 获取字符的MD5
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Md5(string val)
        {
            if (string.IsNullOrEmpty(val))
                return string.Empty;
            var md5 = MD5.Create();
            var s = md5.ComputeHash(Encoding.UTF8.GetBytes(val));
            var result = BitConverter.ToString(s).Replace("-", string.Empty);
            return result;
        }
    }
}
