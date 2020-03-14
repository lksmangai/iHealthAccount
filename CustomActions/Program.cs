using System;
using System.Collections.Generic;
using System.Xml;
using System.Configuration;
using System.Diagnostics;

namespace CustomActions
{
    public static class Program
    {
        private static string installPath;

        public static void Main(string[] args)
        {
            Console.WriteLine("************************************************************" + Environment.NewLine + "Installing iHealthAccount..." + Environment.NewLine);
            string[] argCollection = args[0].Split(Convert.ToChar(","));
            installPath = argCollection[0].Trim();
            Console.WriteLine("************************************************************" + Environment.NewLine + "Recieved arguments" + Environment.NewLine);
            string exeName = installPath + @"\HealthAccount.exe";
            string configFileName = installPath + @"\HealthAccount.exe.config";
            Console.WriteLine("************************************************************" + Environment.NewLine + "Modified configuration" + Environment.NewLine);
            Console.WriteLine("************************************************************" + Environment.NewLine);
            ModifyAppConfig(configFileName, "connectionStrings", "connectionString", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + installPath + @"Database\HealthAccount.mdb;Jet OLEDB:Database Password=admin;");
            ModifyAppConfig(configFileName, "appSettings", 4, "value", argCollection[1]);
        }

        private static void ModifyAppConfig(string fileName, string sectionName, string attributeName, string value)
        {
            ModifyAppConfig(fileName, sectionName, 0, attributeName, value);
        }

        private static void ModifyAppConfig(string fileName, string sectionName, int nodeIndex, string attributeName, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            XmlNodeList nodes = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                if (string.Compare(node.Name, sectionName, true) == 0)
                {
                    node.ChildNodes[nodeIndex].Attributes[attributeName].Value = value.Trim();
                    break;
                }
            }
            xmlDoc.Save(fileName);
        }
    }
}
