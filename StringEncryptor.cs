using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class StringEncryptor
    {
        public static string Encrypt(string PlainText,string Key)
        {
            byte[] EncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objOfMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] SecurityKeyArray = objOfMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            objOfMD5CryptoService.Clear();

            var objOfTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            objOfTripleDESCryptoService.Key = SecurityKeyArray;
            objOfTripleDESCryptoService.Mode = CipherMode.ECB;
            objOfTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objOfCryptoTransform = objOfTripleDESCryptoService.CreateEncryptor();

            byte[] ResultArray = objOfCryptoTransform.TransformFinalBlock(EncryptedArray, 0, EncryptedArray.Length);

            objOfTripleDESCryptoService.Clear();

            return Convert.ToBase64String(ResultArray, 0, ResultArray.Length);

        }
        public static string Decrypt(string CipherText,string Key)
        {
            byte[] EncryptArray = Convert.FromBase64String(CipherText);

            MD5CryptoServiceProvider objOfMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] SecurityKeyArray = objOfMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            objOfMD5CryptoService.Clear();

            var objOfTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            objOfTripleDESCryptoService.Key = SecurityKeyArray;
            objOfTripleDESCryptoService.Mode = CipherMode.ECB;
            objOfTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objOfCryptoTransform = objOfTripleDESCryptoService.CreateDecryptor();

            byte[] ResultArray = objOfCryptoTransform.TransformFinalBlock(EncryptArray, 0, EncryptArray.Length);

            objOfTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(ResultArray);
        }
    }
}
