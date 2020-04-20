using System;
using System.IO;
//using System.Xml;
//using System.Collections.Generic;

namespace OFA.Test.Progs
{
    public enum LogMsgType
    {
        MESSAGE = 0,
        WARNING = -1,
        ERROR = -2
    }

    public static class Log
    {
        private static string _logDir = @".\logs\";

        /*
        public static KeyValuePair<string, string> getKeyValueFromXML(string xmlStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);

            string k = xmlDoc.DocumentElement.Attributes["key"].Value;
            string v = xmlDoc.DocumentElement.Attributes["value"].Value;

            KeyValuePair<string, string> keyItem = new KeyValuePair<string, string>(k, v);

            return keyItem;
        }
        */

        /*
        public static string ReadTextFile(string fileFullName)
        {
            int cntLine = 0;
            string strFile = String.Empty;

            if (File.Exists(fileFullName))
            {
                try
                {
                    using (StreamReader sReader = File.OpenText(fileFullName))
                    {
                        while (!sReader.EndOfStream)
                        {
                            cntLine++;
                            strFile += sReader.ReadLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteFileLog($"File: [{fileFullName}]  String No[{cntLine}", -2);
                    Log.WriteFileLog($"{ex.Message}", -2);
                }
            }
            else 
            {
                Log.WriteFileLog($"File: [{fileFullName}] not found", -2);
            }

            return strFile;
        }
        */

        public static string SetLogDirectory(string strLogDir)
        {
            return SetLogDirectory(false, strLogDir);
        }

        public static string SetLogDirectory(bool isUserProfile, string strLogDir)
        {
            if (isUserProfile)
            {
                strLogDir = strLogDir.Trim(new char[] {' ','\\'});

                string userProfileDir = Environment.GetEnvironmentVariable("USERPROFILE");

                _logDir = $@"{userProfileDir}\{strLogDir}\";
            }
            else
            {
                _logDir = $@"{strLogDir}";
            }

            return _logDir;
        }

        public static string GetLogDirectory()
        {
            return _logDir;
        }


        public static void WriteFileLog(string strLog, LogMsgType logType = LogMsgType.MESSAGE)
        {
            string logFileName = _logDir + DateTime.Now.ToString("yyyyMMdd") + ".log";

            if (!Directory.Exists(_logDir)) { Directory.CreateDirectory(_logDir); }

            if (logType == LogMsgType.ERROR)
            {
                strLog = String.Format("[{0}] ERROR: {1}", DateTime.Now.ToString("HH:mm:ss"), strLog);
            }
            else if (logType == LogMsgType.WARNING)
            {
                strLog = String.Format("[{0}] WARNING: {1}", DateTime.Now.ToString("HH:mm:ss"), strLog);
            }
            else
            {
                strLog = String.Format("[{0}] MESSAGE: {1}", DateTime.Now.ToString("HH:mm:ss"), strLog);
            }

            Common.WriteTextFile(logFileName, strLog);
        }
    }
}
