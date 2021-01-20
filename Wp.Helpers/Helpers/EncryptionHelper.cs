using System;
using System.Security.Cryptography;
using System.Text;

namespace Wp.Helpers.Helpers
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

        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="key">秘钥，长度必须为16、24或32位的字符串</param>
        /// <param name="code">待加密的字符串</param>
        /// <returns></returns>
        public static string GetAESEncryptionCode(string key, string code)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(code);
                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AES解密算法
        /// </summary>
        /// <param name="key">秘钥，长度必须为16、24或32位的字符串</param>
        /// <param name="code">待解密的字符串</param>
        /// <returns></returns>
        public static string GetAESDecryptionCode(string key, string code)
        {
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Convert.FromBase64String(code);
                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}