using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace FacturacionCAT.Models
{
	public static class Funciones
	{
		public static string ToAbsoluteURL(string relativeUrl)
		{
			string sDelete = ConfigurationManager.AppSettings["AppName"];
			string sPort = ConfigurationManager.AppSettings["AppPort"];
            if (sDelete.Length > 0)
                relativeUrl = relativeUrl.Replace(sDelete, "");

			if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
			if (HttpContext.Current == null) return relativeUrl;
			if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Insert(0, "~");
			if (!relativeUrl.StartsWith("~/")) relativeUrl = relativeUrl.Insert(0, "~/");
			System.Uri url = HttpContext.Current.Request.Url;
			return string.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, sPort, VirtualPathUtility.ToAbsolute(relativeUrl));
		}

		public static string CStr2(object o)
		{
			if (o == null || o == DBNull.Value) return String.Empty;
			return o.ToString();
		}
	}
}