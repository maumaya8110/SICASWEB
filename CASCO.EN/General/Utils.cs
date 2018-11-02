using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CASCO.EN.General
{
    public static class Utils
    {
        public static bool RegistrarEventos
        {
            get
            {
                bool b = false;
                if (ConfigurationManager.AppSettings["RegistrarEventos"] != null &&
                    ConfigurationManager.AppSettings["RegistrarEventos"].Length > 0)
                    b = Convert.ToBoolean(ConfigurationManager.AppSettings["RegistrarEventos"].ToString());
                return b;
            }
        }

        public static bool IsDegub
        {
            get
            {
                bool b = false;
                if (ConfigurationManager.AppSettings["IsDebug"] != null &&
                    ConfigurationManager.AppSettings["IsDebug"].Length > 0)
                    b = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDebug"].ToString());
                return b;
            }
        }
        

        public static string FormatoFecha
        {
            get
            {
                string s = "dddd, dd MMMM yyyy HH:mm:ss";
                if (System.Configuration.ConfigurationManager.AppSettings["FormatoFecha"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["FormatoFecha"].ToString().Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["FormatoFecha"];
                return s;
            }
        }

        public static string EventSource
        {
            get
            {
                string s = "AppLogEventSource";
                if (System.Configuration.ConfigurationManager.AppSettings["EventSource"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["EventSource"].ToString().Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["EventSource"];
                return s;
            }
        }

        public static string EventLog
        {
            get
            {
                string s = "AppLog";
                if (System.Configuration.ConfigurationManager.AppSettings["EventLog"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["EventLog"].ToString().Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["EventLog"];
                return s;
            }
        }

        public static bool LogEnable
        {
            get
            {
                bool b = false;
                if (ConfigurationManager.AppSettings["LogEnable"] != null &&
                    ConfigurationManager.AppSettings["LogEnable"].ToString().Length > 0)
                    b = Convert.ToBoolean(ConfigurationManager.AppSettings["LogEnable"]);
                return b;
            }
        }

        public static string GetConfigValue(string key)
        {
            string valret = "";
            if (ConfigurationManager.AppSettings[key] != null &&
                    ConfigurationManager.AppSettings[key].ToString().Length > 0)
                valret = ConfigurationManager.AppSettings[key].ToString();
            return valret;
        }
    }
}
