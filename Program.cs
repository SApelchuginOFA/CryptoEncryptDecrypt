using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
//using System.Xml;
//using System.Collections.Generic;
//using Microsoft.Extensions.Configuration;


/*

== OK CMD STRIND

--X=POST --header="Content-Type: application/json" --configFileName="Example2.xml" --XML="
  <appSettings>
    <add key='appId' value='bf69c395-54f9-4193-a1ac-7699fb646b5a' />
    <add key='tenantId' value='9b6ba481-89aa-4c3f-aeda-bf85b14ca3f6' />
    <add key='clientSecret' value='dBklC0V=ric[DhrJ.3JxgRgly3@74kTm' />
    <add key='outputFilesFolderPath' value='C:\\temp\\' />
    <add key='emailBoxUser' value='sapelchugin@Aplana.onmicrosoft.com' />
  </appSettings>
  "
*/

namespace OFA.Test.Progs
{
    class Program
    {
        //static List<KeyValuePair<string, string>> _KVList_CommandLine = new List<KeyValuePair<string, string>>();
        //static List<KeyValuePair<string, string>> _KVList_ConfigFile = new List<KeyValuePair<string, string>>();

        static void Main(string[] args)
        {

            string strSecret = "password"; // !!! must be 8 symbols !!!

            string strOriginal = "bf69c395-54f9-4193-a1ac-7699fb646b5a";

            try
            {

                // SET LOG PATH
                // ============
                // default (no need set): Path =  .\logs\
                // isUserProfile = false: Path =  you can set any absolute path, for example: C:\Tmp\POC_Logs\
                // isUserProfile = true:  Path =  %USERPROFILE%\[your name]\

                Log.SetLogDirectory(true, $@"\Tmp\POC_Logs\");

                //

                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== START POC Cripto String Encrypt/Decript");
                Log.WriteFileLog($"=== Log: {Log.GetLogDirectory()}"); 
                Log.WriteFileLog("============================");
                Log.WriteFileLog("############################");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== Start crypto Examle 1");
                Log.WriteFileLog("============================");

                //string strSecret = "qwerty";
                string strEncrypted_1 = CryptString1.Encrypt(strOriginal, strSecret);
                string strDecrypted_1 = CryptString1.Decrypt(strEncrypted_1, strSecret);

                Log.WriteFileLog($"Secret: {strSecret}");
                Log.WriteFileLog($"Key  original: {strOriginal}");
                Log.WriteFileLog($"Key encrypted: {strEncrypted_1}");
                Log.WriteFileLog($"Key decrypted: {strDecrypted_1}");

                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== END crypto Examle 1");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("############################");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== START crypto Examle 2");
                Log.WriteFileLog("============================");

                string strEncrypted_2 = CryptString2.Encrypt(strOriginal, strSecret);
                string strDecrypted_2 = CryptString2.Decrypt(strEncrypted_2, strSecret);

                Log.WriteFileLog($"Secret: {strSecret}");
                Log.WriteFileLog($"Key  original: {strOriginal}");
                Log.WriteFileLog($"Key encrypted: {strEncrypted_2}");
                Log.WriteFileLog($"Key decrypted: {strDecrypted_2}");

                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== END crypto Examle 2");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("############################");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== START crypto Examle 3");
                Log.WriteFileLog("============================");

                string strEncrypted_3 = CryptString3.Encrypt(strOriginal, strSecret);
                string strDecrypted_3 = CryptString3.Decrypt(strEncrypted_3, strSecret);

                Log.WriteFileLog($"Secret: {strSecret}");
                Log.WriteFileLog($"Key  original: {strOriginal}");
                Log.WriteFileLog($"Key encrypted: {strEncrypted_3}");
                Log.WriteFileLog($"Key decrypted: {strDecrypted_3}");

                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== END crypto Examle 3");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("############################");
                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== START crypto Examle 4");
                Log.WriteFileLog("============================");

                // For additional security Pin the key.
                //GCHandle gch = GCHandle.Alloc(sSecretKey.Key, GCHandleType.Pinned);

                string dirPath = @"C:\tmp\crypto\";
                string origFile = "originalFile.txt";
                string encryptedFile = "encryptedFile.txt";
                string decryptedFile = "decryptedFile.txt";

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    Log.WriteFileLog("=== WARNING  ===");
                    Log.WriteFileLog($"Put into {dirPath} file {origFile}", LogMsgType.WARNING);
                }
                else
                {
                    if (!File.Exists($"{dirPath}{origFile}"))
                    {
                        Log.WriteFileLog("=== WARNING  ===");
                        Log.WriteFileLog($"Put into {dirPath} file {origFile}", LogMsgType.WARNING);
                    }
                    else
                    {
                        CryptFile.EncryptFile($@"{dirPath}{origFile}", $@"{dirPath}{encryptedFile}", strSecret);
                        CryptFile.DecryptFile($@"{dirPath}{encryptedFile}", $@"{dirPath}{decryptedFile}", strSecret);

                        Log.WriteFileLog($"Secret: {strSecret}");
                        Log.WriteFileLog($"Original File Path:{dirPath}{origFile}");
                        Log.WriteFileLog($"Encrypted File Path: {dirPath}{encryptedFile}");
                        Log.WriteFileLog($"Decrypted File Path: {dirPath}{decryptedFile}");
                    }
                }

                Log.WriteFileLog("============================");
                Log.WriteFileLog("=== END crypto Examle 4");
                Log.WriteFileLog("============================");

            }
            catch (Exception ex)
            {
                Log.WriteFileLog("=== ERROR  ===");
                Log.WriteFileLog($"{ex.Message}",LogMsgType.ERROR);
            }
            finally
            {
                Log.WriteFileLog("==============================================");
                Log.WriteFileLog("===  END POC Cripto String Encrypt/Decript ===");
                Log.WriteFileLog("==============================================");

                Console.WriteLine("Press ENTER to EXIT");
                Console.ReadLine();
            }
        }
    }
}
