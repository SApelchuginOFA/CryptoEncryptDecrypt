using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace OFA.Test.Progs
{
    public static class CryptFile
    {
        /*
        //  Call this function to remove the key from memory after use for security
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);
        */

        public static void EncryptFile(string inFile, string outFile, string strSecret)
        {
            FileStream fsInput = new FileStream(inFile, FileMode.Open, FileAccess.Read);

            FileStream fsEncrypted = new FileStream(outFile, FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(strSecret); //64 bit = 8 byte
            DES.IV = ASCIIEncoding.ASCII.GetBytes(strSecret); //64 bit = 8 byte

            ICryptoTransform desencrypt = DES.CreateEncryptor();

            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }


        public static void DecryptFile(string inFile, string outFile, string strSecret)
        {
            byte[] bytearrayinput = new byte[1024];
            int length;

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(strSecret); //64 bit = 8 byte
            DES.IV = ASCIIEncoding.ASCII.GetBytes(strSecret);  //64 bit = 8 byte

            FileStream fsread = new FileStream(inFile, FileMode.Open,  FileAccess.Read);

            ICryptoTransform desdecrypt = DES.CreateDecryptor();

            CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);

            FileStream fsDecrypted = new FileStream(outFile, FileMode.Create);

            do
            {
                length = cryptostreamDecr.Read(bytearrayinput, 0, 1024);
                fsDecrypted.Write(bytearrayinput, 0, length);
            } while (length == 1024);

            fsDecrypted.Flush();
            fsDecrypted.Close();
        }
    }
}
