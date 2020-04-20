using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace OFA.Test.Progs
{
    public static class Common
    {

        public static KeyValuePair<string, string> getKeyValueFromXML(string xmlStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);

            string k = xmlDoc.DocumentElement.Attributes["key"].Value;
            string v = xmlDoc.DocumentElement.Attributes["value"].Value;

            KeyValuePair<string, string> keyItem = new KeyValuePair<string, string>(k, v);

            return keyItem;
        }

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
                    Log.WriteFileLog($"File: [{fileFullName}]  String No[{cntLine}", LogMsgType.ERROR);
                    Log.WriteFileLog($"{ex.Message}", LogMsgType.ERROR);
                }
            }
            else 
            {
                Log.WriteFileLog($"File: [{fileFullName}] not found", LogMsgType.ERROR);
            }

            return strFile;
        }

        public static void WriteTextFile(string fileFullName, string strLine)
        {
            strLine = strLine.TrimEnd(Environment.NewLine.ToCharArray());

            Console.WriteLine(strLine);

            try
            {
                if (File.Exists(fileFullName))
                {
                    using (StreamWriter sw = File.AppendText(fileFullName))
                    {
                        sw.WriteLine(strLine);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(fileFullName))
                    {
                        sw.WriteLine(strLine);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.WriteFileLog($"File: [{fileFullName}]", LogMsgType.ERROR);
                Log.WriteFileLog($"{ex.Message}", LogMsgType.ERROR);
            }
        }

    }
}
