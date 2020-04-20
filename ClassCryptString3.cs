using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace OFA.Test.Progs
{
    public static class CryptString3
    {
        public static string Encrypt(string plainText, string strSecret)
        {
            byte[] encryptedBytes;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = ASCIIEncoding.ASCII.GetBytes(strSecret + strSecret);  //128 bit = 16 byte
                rijAlg.IV  = ASCIIEncoding.ASCII.GetBytes(strSecret + strSecret);  //128 bit = 16 byte

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }


        public static string Decrypt(string encryptText, string strSecret)
        {
            string decryptText = null;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = ASCIIEncoding.ASCII.GetBytes(strSecret + strSecret); //128 bit = 16 byte
                rijAlg.IV = ASCIIEncoding.ASCII.GetBytes(strSecret + strSecret); //128 bit = 16 byte

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decryptText;
        }
    }
}
