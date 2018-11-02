using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
	public static class Seguridad
	{
		public static CoreFacturacion.Usuario UsuarioActual
		{
			get
			{
                return (CoreFacturacion.Usuario)HttpContext.Current.Session["Usuario"];
			}
			set
			{
                HttpContext.Current.Session["Usuario"] = value;
            }
		}

        public static string FacturarA
        {
            get
            {
                string s = "";
                try
                {
                    s = HttpContext.Current.Request.Cookies["AXFacturarA"].Value.ToString();
                }
                catch
                {
                    s = "";
                }

                return s;
            }
            set
            {
                HttpCookie c = null;
                try
                {
                    c = HttpContext.Current.Request.Cookies["AXFacturarA"];
                }
                catch
                {
                    c = null;
                }
                if (c != null)
                {
                    HttpContext.Current.Response.Cookies.Remove("AXFacturarA");
                }

                c = new HttpCookie("AXFacturarA");
                c.Value = value;
                c.Expires = DateTime.Now.AddDays(2);
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }

        public static String ASPSessionID
		{
			get
			{
				String sessionID = "";
				try
				{
					sessionID = Models.Funciones.CStr2(HttpContext.Current.Request.Cookies["AXSessionID"].Value);
				}
				catch
				{
					sessionID = "";
				}
				if (String.IsNullOrEmpty(sessionID))
				{
					sessionID = Guid.NewGuid().ToString();
					HttpCookie c = new HttpCookie("AXSessionID");
					c.Value = sessionID;
					c.Expires = DateTime.Now.AddDays(2);
					HttpContext.Current.Response.Cookies.Add(c);
				}
				return sessionID;
			}

			set
			{
				HttpCookie c = null;
				try
				{
					c = HttpContext.Current.Request.Cookies["AXSessionID"];
				}
				catch
				{
					c = null;
				}
				if (c != null)
					HttpContext.Current.Response.Cookies.Remove("AXSessionID");

				c = new HttpCookie("AXSessionID");
				c.Value = value;
				c.Expires = DateTime.Now.AddDays(2);
				HttpContext.Current.Response.Cookies.Add(c);
			}
		}
	}
}