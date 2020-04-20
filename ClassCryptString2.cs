using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace OFA.Test.Progs
{
    public static class CryptString2
    {
        public static string Encrypt(string originalString, string strSecret)
        {
            byte[] bytesForKeyAndIV = ASCIIEncoding.ASCII.GetBytes(strSecret); //64 bit = 8 byte

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream,cryptoProvider.CreateEncryptor(bytesForKeyAndIV, bytesForKeyAndIV), CryptoStreamMode.Write);

            StreamWriter writer = new StreamWriter(cryptoStream);

            writer.Write(originalString);

            writer.Flush();

            cryptoStream.FlushFinalBlock();

            writer.Flush();

            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string cryptedString, string strSecret)
        {
            byte[] bytesForKeyAndIV = ASCIIEncoding.ASCII.GetBytes(strSecret); //64 bit = 8 byte

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));

            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytesForKeyAndIV, bytesForKeyAndIV), CryptoStreamMode.Read);

            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }
    }
}
