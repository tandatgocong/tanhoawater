using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace TanHoaWater.Utilities
{
    class LogIn
    {
        public static void savepass(string username, string password)
        {
            try
            {
                string uid = username + "," + password;
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+@"\user.dat"))
                {
                    sw.WriteLine(uid);

                }
            }
            catch (Exception)
            {

            }
        }
       
        public static string[] readpass()
        {
            string line;
            string[] words = null;
            try
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\user.dat");
                while ((line = sr.ReadLine()) != null)
                {
                    words = Regex.Split(line, ",");
                }
            }
            catch (Exception)
            {
            }

            return words;
        }

        private const string cryptoKey = "tanhoa";
        private static readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };
      
        public static string Encrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = IV;
                result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
            }

            return result;
        }       
        public static string Decrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Convert.FromBase64String(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                des.IV = IV;
                result = Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
               
            }

            return result;
        }
    }



}