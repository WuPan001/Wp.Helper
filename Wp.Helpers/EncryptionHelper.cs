using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers
{
    /// <summary>
    /// 加密解密帮助类
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="code">待加密字符串</param>
        /// <returns></returns>
        public static string GetMD5EncryptionCode(string code)
        {
            var result = new StringBuilder();
            using MD5 mD5 = MD5.Create();
            byte[] buffer = Encoding.Default.GetBytes(code);
            byte[] newBuffer = mD5.ComputeHash(buffer);//开始加密
            foreach (var item in newBuffer)
            {
                result.Append(item.ToString("X2"));
            }
            return result.ToString();
        }
    }
}