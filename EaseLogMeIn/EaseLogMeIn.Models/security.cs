using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public class Security
    {
        private static string GenerateSalt()
        {
            int a = new Random().Next(0, 19);
            return Guid.NewGuid().ToString().Substring(a, 16);
        }
        private static string EncriptString(string input, string Salt)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            using (AesCryptoServiceProvider tripleDES = new AesCryptoServiceProvider())
            {
                tripleDES.Key = Encoding.UTF8.GetBytes(Salt);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }
        public static string Encrypt(string input, out string Salt)
        {
            Salt = GenerateSalt();
            return EncriptString(input, Salt);
        }
        public static string Encrypt(string input, string Salt)
        {
            if (Salt == null || Salt.Length != 16) { throw new Exception("Invaid Encryption Salt Key"); }
            return EncriptString(input, Salt);
        }
        public static string Decrypt(string input, string Salt)
        {
            if (Salt == null || Salt.Length != 16) { throw new Exception("Invaid Encryption Salt Key"); }
            byte[] inputArray = Convert.FromBase64String(input);
            using (AesCryptoServiceProvider tripleDES = new AesCryptoServiceProvider())
            {
                tripleDES.Key = Encoding.UTF8.GetBytes(Salt);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
        }
    }
}
