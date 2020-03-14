﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.Diagnostics;

namespace HealthAccount.Setup.CustomActions
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            string[] argCollection = args[0].Split(Convert.ToChar(","));
            installPath = argCollection[0].Trim();
            string exeName = installPath + @"\HealthAccount.UI.exe";
            /*
            System.IO.File.WriteAllText(installPath + @"\Log\log.txt", "start" + "\n");
            System.IO.File.AppendAllText(installPath + @"\Log\log.txt", installPath + @"\HealthAccount.UI.exe.config" + "\n");
            */
            string configFileName = installPath + @"\HealthAccount.UI.exe.config";
            ModifyAppConfig(configFileName, "connectionStrings", "connectionString", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + installPath + @"Database\HealthAccount.mdb;Jet OLEDB:Database Password=admin;");
            ModifyAppConfig(configFileName, "appSettings", 4, "value", argCollection[1]);                        
        }

        private static string installPath;

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
                //System.IO.File.AppendAllText(installPath + @"\Log\log.txt", node.Name + "  " + sectionName + "\n");

                if (string.Compare(node.Name, sectionName, true) == 0)
                {
                    node.ChildNodes[nodeIndex].Attributes[attributeName].Value = value.Trim();
                    //System.IO.File.AppendAllText(installPath + @"\Log\log.txt", node.ChildNodes[nodeIndex].Attributes[attributeName].Value + "\n");
                    break;
                }
            }
            xmlDoc.Save(fileName);

            //System.IO.File.AppendAllText(installPath + @"\Log\log.txt", "finish" + "\n");
        }
    }
}
