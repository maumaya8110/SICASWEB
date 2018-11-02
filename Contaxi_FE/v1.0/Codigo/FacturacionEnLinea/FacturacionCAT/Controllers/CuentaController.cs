using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace FacturacionCAT.Controllers
{
	public class CuentaController : Controller
	{
		public ActionResult Login()
		{
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
            {
				switch (Models.Seguridad.UsuarioActual.TipoUsuario)
				{
					case CoreFacturacion.TipoUsuarioEnum.Administrador: return RedirectToAction("Index","Administrador");
					case CoreFacturacion.TipoUsuarioEnum.Cliente: return RedirectToAction("Index", "Cliente");
					default: return RedirectToAction("LogOut", "Cuenta");
				}
			}
			return View();
		}

        public ActionResult LogOut()
        {
            Session.Abandon();
            //Models.Seguridad.UsuarioActual.UsuarioID = "";
            //Models.Seguridad.UsuarioActual.TipoUsuario = CoreFacturacion.TipoUsuarioEnum.NoDefinido;
            return RedirectToAction("Index", "Home");
        }

		public ActionResult NuevoRegistro()
		{
			return View();
		}

		public ActionResult RegistroOK()
		{
			//ViewBag.Mensaje = Properties.Settings.Default.MensajeRegistroOK;
			return View();
		}

		public ActionResult Validar(string token, string cuenta)
		{
			DataTable dtResultado = null;
			string lbl = "";
			if (token.Trim().Length == 0 || cuenta.Trim().Length == 0)
			{
				throw new Exception("Faltan parametrós para ejecutar la acción. Favor de verificar la URL");
			}
			try
			{
				dtResultado = CoreFacturacion.SQLHelper.GetTable("FF_ValidaCuenta", cuenta, token);
				if (dtResultado != null && dtResultado.Rows.Count > 0)
				{
					DataRow dr = dtResultado.Rows[0];
					lbl = dr["Mensaje"].ToString();
				}
			}
			catch (Exception ex)
			{
				lbl = ex.Message;
			}
			ViewBag.Mensaje = lbl;
			return View();
		}

		[HttpPost]
		public JsonResult Recuperacion(string email)
		{
			CoreFacturacion.ResultadoConsulta resultado = CoreFacturacion.Negocio.RecuperarContrasena(email);
			if (resultado.Resultado)
			{
				String token = resultado.Valor;
				String correoElectronico = email;
				String mensaje = String.Format(FacturacionCAT.Properties.Settings.Default.MailRecuperacionBody, token);

				CascoFacturacionLib.MailHelper.SMTP mh = new CascoFacturacionLib.MailHelper.SMTP(
					System.Configuration.ConfigurationManager.AppSettings["MailServer"]
					, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MailPort"])
					, System.Configuration.ConfigurationManager.AppSettings["MailUsername"]
					, System.Configuration.ConfigurationManager.AppSettings["MailPassword"]
					, System.Configuration.ConfigurationManager.AppSettings["MailAccount"]
					, System.Configuration.ConfigurationManager.AppSettings["MailFrom"]
					, false);

				if (mh.SendMail(email.Trim().ToLower(), FacturacionCAT.Properties.Settings.Default.MailRecuperacionAsunto, mensaje, true, null, 0))
				{
                    resultado.Valor = Url.Action("Index","Home");
				}
				else
				{
                    resultado.Mensaje = "No se pudo enviar el mensaje de recuperación de contraseña.";
                    resultado.Resultado = false;
                }
			}
            JsonResult j = new JsonResult();
			j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			j.Data = resultado;
			return j;
		}

		[HttpPost]
		public ActionResult IngresaUsuario(string username, string pass)
		{
			string lbl = "";
			CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.ExisteUsuario(username, pass);
			if (r != null && r.Resultado)
			{
				CoreFacturacion.Usuario u = new CoreFacturacion.Usuario();
				u.UsuarioID = r.Valor;
				u.TipoUsuario = CoreFacturacion.Negocio.TipoUsuario(u.UsuarioID);
				Models.Seguridad.UsuarioActual = u;
				switch (Models.Seguridad.UsuarioActual.TipoUsuario)
				{
					case CoreFacturacion.TipoUsuarioEnum.Cliente:
						lbl = Url.Action("Index","Cliente"); break;
					case CoreFacturacion.TipoUsuarioEnum.Administrador:
						lbl = Url.Action("Index","Administrador");break;
					default:
						lbl = Url.Action("LogOut","Cuenta");break;
				}
                r.Valor = lbl;
			}
			else
			{
                r.Resultado = false;
                //Models.Seguridad.UsuarioActual.TipoUsuario = CoreFacturacion.TipoUsuarioEnum.NoDefinido;
                //Models.Seguridad.UsuarioActual.UsuarioID = "";
			}
			JsonResult j = new JsonResult();
			j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			j.Data = r;
			return j;
		}

        [HttpPost]
        public ActionResult RegistraUsuarioSimple(string email, string password)
        {
            DataTable dtResultado = null;
            string UsuarioID = "";
            string lbl = "";
            try
            {
                dtResultado = CoreFacturacion.SQLHelper.GetTable("[FF_RegistrarUsuario_Single]", email, password);
                if (dtResultado != null && dtResultado.Rows.Count > 0)
                {
                    DataRow dr = dtResultado.Rows[0];
                    if (Convert.ToBoolean(dr["Resultado"]))
                    {
                        UsuarioID = dr["Valor"].ToString();
                        String sitio = HttpContext.Request.Url.Host;
                        String urlBase = Models.Funciones.ToAbsoluteURL(Url.Action("Validar", "Cuenta"));
                        String token = UsuarioID;
                        String correoElectronico = email.ToLower().Trim();
                        String mensaje = String.Format(FacturacionCAT.Properties.Settings.Default.MailRegistroBody, urlBase, token, correoElectronico, sitio);

                        CascoFacturacionLib.MailHelper.SMTP mh = new CascoFacturacionLib.MailHelper.SMTP(
                            System.Configuration.ConfigurationManager.AppSettings["MailServer"]
                            , Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MailPort"])
                            , System.Configuration.ConfigurationManager.AppSettings["MailUsername"]
                            , System.Configuration.ConfigurationManager.AppSettings["MailPassword"]
                            , System.Configuration.ConfigurationManager.AppSettings["MailAccount"]
                            , System.Configuration.ConfigurationManager.AppSettings["MailFrom"]
                            , false
                        );

                        if (mh.SendMail(
                            correoElectronico
                            , FacturacionCAT.Properties.Settings.Default.MailRegistroAsunto
                            , mensaje
                            , true
                            , null))
                        {
                            HttpContext.Session["Registro_CorreoElectronico"] = correoElectronico;
                            HttpContext.Session["Registro_Mensaje"] = dr["Mensaje"];
                            HttpContext.Session["Registro_Valor"] = UsuarioID;
                            lbl = Url.Action("RegistroOK", "Cuenta");
                        }
                        else
                            throw new Exception(dr["Mensaje"].ToString());
                    }
                    else
                        throw new Exception(dr["Mensaje"].ToString());
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session["Registro_CorreoElectronico"] = "";
                HttpContext.Session["Registro_Mensaje"] = "";
                HttpContext.Session["Registro_Valor"] = "";
                CoreFacturacion.SQLHelper.ExecuteNonQuery("FF_CancelaRegistro", UsuarioID);
                lbl = ex.Message;
            }

            JsonResult j = new JsonResult();
            j.Data = lbl;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return j;
        }
    }
}
