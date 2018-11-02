using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.DAO
{
    public static class Utils
    {
        private static string GetValueConnectionStringFromWebConfig(string key)
        {
            string cad = "Server=sicas.casco.com.mx,54903;Database=SICASSync;user id=SICASusr;password=oiuddvbh;MultipleActiveResultSets=true;";
            if (System.Configuration.ConfigurationManager.ConnectionStrings[key] != null &&
                System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString != null &&
                System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString.Length > 0)
                cad = System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
            return cad;
        }

        public static string CadenaConexionSICAS
        {
            get
            {
                return GetValueConnectionStringFromWebConfig("SICAS");
            }
        }

        public static string CadenaConexionXDS
        {
            get
            {
                return GetValueConnectionStringFromWebConfig("XDS");
            }
        }

        public static string CadenaConexionMSSQL
        {
            get
            {
                return GetValueConnectionStringFromWebConfig("sql");
            }
        }

        public static string CadenaConexionOLAP
        {
            get
            {
                return GetValueConnectionStringFromWebConfig("OLAP");
            }
        }

        public static string CadenaConexionMySQL
        {
            get
            {
                return GetValueConnectionStringFromWebConfig("mysql");
            }
        }

        public static string CadConURGI { get { return System.Configuration.ConfigurationManager.ConnectionStrings["SGC"].ConnectionString; } }
        public static string CadConMembership { get { return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; } }
        public static string CadConMenu { get { return System.Configuration.ConfigurationManager.ConnectionStrings["Menu"].ConnectionString; } }

        public static string CadConSMPP { get { return System.Configuration.ConfigurationManager.ConnectionStrings["CadConSMPP"].ConnectionString; } }

        public static int IntervalPeriod
        {
            get
            {
                string sintervalo = "15";
                if (System.Configuration.ConfigurationManager.AppSettings["interval_period"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["interval_period"].Length > 0)
                    sintervalo = System.Configuration.ConfigurationManager.AppSettings["interval_period"];
                return Convert.ToInt32(sintervalo);
            }
        }

        public static string MailFrom
        {
            get
            {
                string s = "noresponder@urgi.com.mx";
                if (System.Configuration.ConfigurationManager.AppSettings["MailFrom"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["MailFrom"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["MailFrom"];
                return s;
            }
        }

        public static string ServidorSMTP
        {
            get
            {
                string s = "mail.urgi.com.mx";
                if (System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"];
                return s;
            }
        }

        public static string SMTPuser
        {
            get
            {
                string s = "noresponder@urgi.com.mx";
                if (System.Configuration.ConfigurationManager.AppSettings["SMTPuser"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMTPuser"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["SMTPuser"];
                return s;
            }
        }

        public static int SMTPport
        {
            get
            {
                string sintervalo = "587";
                if (System.Configuration.ConfigurationManager.AppSettings["SMTPport"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMTPport"].Length > 0)
                    sintervalo = System.Configuration.ConfigurationManager.AppSettings["SMTPport"];
                return Convert.ToInt32(sintervalo);
            }
        }

        public static string SMTPpass
        {
            get
            {
                string s = "L4qu3se4";
                if (System.Configuration.ConfigurationManager.AppSettings["SMTPpass"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMTPpass"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["SMTPpass"];
                return s;
            }
        }

        public static string SMTPMailPrueba
        {
            get
            {
                string s = "galdino.melchor@casco.com.mx";
                if (System.Configuration.ConfigurationManager.AppSettings["SMTPMailPrueba"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMTPMailPrueba"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["SMTPMailPrueba"];
                return s;
            }
        }

        public static string CopiaMail
        {
            get
            {
                string s = "samuel.maldonado@urgi.com.mx";
                if (System.Configuration.ConfigurationManager.AppSettings["CopiaMail"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["CopiaMail"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["CopiaMail"];
                return s;
            }
        }

        public static int ConexionTimeOut
        {
            get
            {
                int to = 60;
                if (System.Configuration.ConfigurationManager.AppSettings["TimeOut"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["TimeOut"].Length > 0)
                    to = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"]);

                return to;
            }
        }

        public static string ServidorSMPP
        {
            get
            {
                string s = "192.168.0.54";
                if (System.Configuration.ConfigurationManager.AppSettings["ServidorSMPP"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["ServidorSMPP"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["ServidorSMPP"];
                return s;
            }
        }

        public static string SMPPuser
        {
            get
            {
                string s = "admin";
                if (System.Configuration.ConfigurationManager.AppSettings["SMPPuser"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMPPuser"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["SMPPuser"];
                return s;
            }
        }

        public static int SMPPport
        {
            get
            {
                string sintervalo = "587";
                if (System.Configuration.ConfigurationManager.AppSettings["SMPPport"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMPPport"].Length > 0)
                    sintervalo = System.Configuration.ConfigurationManager.AppSettings["SMPPport"];
                return Convert.ToInt32(sintervalo);
            }
        }

        public static string SMPPpass
        {
            get
            {
                string s = "L4qu3se4";
                if (System.Configuration.ConfigurationManager.AppSettings["SMPPpass"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMPPpass"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["SMPPpass"];
                return s;
            }
        }

        public static string SMPPSMSPrueba
        {
            get
            {
                string s = "8112044667";
                if (System.Configuration.ConfigurationManager.AppSettings["SMPPSMSPrueba"] != null &&
                    System.Configuration.ConfigurationManager.AppSettings["SMPPSMSPrueba"].Length > 0)
                    s = System.Configuration.ConfigurationManager.AppSettings["SMPPSMSPrueba"];
                return s;
            }
        }
    }

}
