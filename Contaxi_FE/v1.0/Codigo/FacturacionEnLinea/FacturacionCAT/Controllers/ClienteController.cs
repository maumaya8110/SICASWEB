using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace FacturacionCAT.Controllers
{
	public class ClienteController : Controller
	{
		//
		// GET: /Cliente/
		public ActionResult Index()
		{
			if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
				return RedirectToAction("Index", "Home");

			List<Models.DatosFiscales> lr = fncObtieneRazonesSocialesPorUsuario();
            if (lr.Count <= 0)
            {
                return RedirectToAction("Perfil", "Cliente");
            }

			ViewBag.ServiciosPendientes = fncObtieneServiciosPendientesPorUsuario();

			ViewBag.Message = "Facturación de Servicios";
			return View(lr);
		}

		public ActionResult Historial()
		{
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");
			List<Models.ServicioHistoria> ls = fncObtieneServiciosHistorialPorUsuario(DateTime.Now.Year, DateTime.Now.Month);
			ViewBag.Message = "Historial de Servicios Facturados.";

			return View(ls);
		}

		public ActionResult Perfil()
		{
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");

			DataTable dt = CoreFacturacion.Negocio.DatosUsuario(Models.Seguridad.UsuarioActual.UsuarioID);
			if (dt != null && dt.Rows.Count > 0)
			{
				ViewBag.Nombre = dt.Rows[0]["Nombre"].ToString();
				ViewBag.Mail = dt.Rows[0]["CorreoElectronico"].ToString();
			}
			List<Models.DatosFiscales> lr = fncObtieneRazonesSocialesPorUsuario();
			ViewBag.Message = "Información de la Cuenta";

			return View(lr);
		}

		public ActionResult Facturar(string token)
		{
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");

			DataTable dtFacturacion = CoreFacturacion.Negocio.DatosFacturacion(Models.Seguridad.UsuarioActual.UsuarioID, token);
			Models.DatosFiscales df = new Models.DatosFiscales();
			if (dtFacturacion != null && dtFacturacion.Rows.Count > 0)
			{
				DataRow dr = dtFacturacion.Rows[0];
				df.RazonSocial = dr["Nombre"].ToString();
				df.RFC = dr["RFC"].ToString();
				df.Calle = dr["Calle"].ToString();
				df.NumeroExterior = dr["NumeroExterior"].ToString();
				df.NumeroInterior = dr["NumeroInterior"].ToString();
				df.Colonia = dr["Colonia"].ToString();
				df.Ciudad = dr["Ciudad"].ToString();
				df.Localidad = dr["Localidad"].ToString();
				df.Estado = dr["Estado"].ToString();
				df.Cp = dr["CodigoPostal"].ToString();
				df.Pais = "MEXICO";
				df.Token = token;
			}

			ViewBag.ServiciosPendientes = fncObtieneServiciosPendientesPorUsuario();

			return View(df);
		}

		[HttpPost]
		public JsonResult FacturaConfirmada(string token)
		{
			string lbl = "";
            try
            {
                if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                    lbl = Url.Action("Index", "Home");
                else {
                    CoreFacturacion.ResultadoConsulta resultado = CoreFacturacion.Negocio.Facturar(Models.Seguridad.UsuarioActual.UsuarioID, token, (System.Web.HttpContextBase)HttpContext);
                    if (resultado != null)
                    {
                        if (resultado.Resultado)
                        {
                            lbl = Url.Action("Historial", "Cliente");
                        }
                        else
                            throw new Exception(resultado.Mensaje);
                    }
                    else
                        throw new Exception("No fue posible facturar el ticket. Inténtelo de nuevo.");
                }
			}
			catch (Exception ex)
			{
				lbl = "Error: " + ex.Message;
			}
			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public JsonResult AgregaServicio(string folio, string importe)
		{
			string lbl = "";
			try
			{
				CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.AgregarTicket(Models.Seguridad.UsuarioActual.UsuarioID, folio, Convert.ToDouble(importe));
				if (!r.Resultado)
					throw new Exception(r.Mensaje);
				lbl = r.Mensaje;
			}
			catch (Exception ex)
			{
				lbl = "Error: " + ex.Message;
			}
			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public JsonResult EliminarTicket(string folio)
		{
			string lbl = "";
			try
			{
				CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.EliminarTicket(Models.Seguridad.UsuarioActual.UsuarioID, folio);
				if (!r.Resultado)
					throw new Exception(r.Mensaje);
				lbl = r.Mensaje;
			}
			catch (Exception ex)
			{
				lbl = "Error: " + ex.Message;
			}
			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public JsonResult EliminaRazonSocial(Models.DatosFiscales Registro)
		{
			string lbl = "";
			try
			{
				CoreFacturacion.Negocio.EliminarRazonSocial(Models.Seguridad.UsuarioActual.UsuarioID, Registro.Token);
				lbl = "Registro elminado con éxito.";
			}
			catch (Exception ex)
			{
				lbl = ex.Message;
			}
			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public PartialViewResult CambiarContrasena()
		{
			return PartialView();
		}

		[HttpPost]
		public PartialViewResult EditarDatosFiscales()
		{
            List<Models.Estado> lEstados = new List<Models.Estado>();
            lEstados = Models.Estado.GetEstados();
            return PartialView(lEstados);
		}

		[HttpPost]
		public JsonResult GuardaRegistro(Models.DatosFiscales Registro)
		{
			string lbl = "";
			try
			{
				CoreFacturacion.Negocio.GuardarDatosFiscales(
						    Models.Seguridad.UsuarioActual.UsuarioID
						    , Registro.Token
						    , Registro.Alias.ToUpper()
						    , Registro.RazonSocial.ToUpper()
						    , Registro.RFC.ToUpper()
						    , Registro.Calle.ToUpper()
						    , Registro.NumeroExterior
						    , (Registro.NumeroInterior == null || String.IsNullOrEmpty(Registro.NumeroInterior) ? "" : Registro.NumeroInterior)
						    , Registro.Colonia.ToUpper()
						    , Registro.Cp
						    , Registro.Ciudad.ToUpper()
						    , (Registro.Localidad == null || String.IsNullOrEmpty(Registro.Localidad) ? "" : Registro.Localidad.ToUpper())
						    , Registro.Estado.ToUpper()
						    , 1);
				lbl = "Registro almacenado con éxito";
			}
			catch (Exception ex)
			{
				lbl = ex.Message + Environment.NewLine + ex.StackTrace;
			}
			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public ActionResult GuardaNombre(string nombre)
		{
			string lbl = "";

            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                lbl = Url.Action("Index", "Home");

			try
			{
				CoreFacturacion.Negocio.GuardarNombreUsuario(Models.Seguridad.UsuarioActual.UsuarioID, nombre.ToUpper());
				lbl = "Información actualizada correctamente";
			}
			catch(Exception ex)
			{
				lbl = ex.Message;
			}
			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public JsonResult ActPwd(string actualPwd, string newPwd)
		{
			string lbl = "";
			CoreFacturacion.ResultadoConsulta resultado = CoreFacturacion.Negocio.CambiarContrasena(Models.Seguridad.UsuarioActual.UsuarioID, actualPwd, newPwd);
			lbl = resultado.Mensaje;

			JsonResult j = new JsonResult();
			j.Data = lbl;
			return j;
		}

		[HttpPost]
		public JsonResult ObtieneRazonesSocialesPorUsuario()
		{
			List<Models.DatosFiscales> lr = fncObtieneRazonesSocialesPorUsuario();
			JsonResult j = new JsonResult();
			j.Data = lr;
			return j;
		}

		[HttpPost]
		public JsonResult GetServHistByUser(int year, int month)
		{
			List<Models.ServicioHistoria> ls = fncObtieneServiciosHistorialPorUsuario(year, month);
			JsonResult j = new JsonResult();
			j.Data = ls;
			return j;
		}

		[HttpPost]
		public JsonResult GetTickets()
		{
			List<Models.ServicioFactura> ls = fncObtieneServiciosPendientesPorUsuario();
			JsonResult j = new JsonResult();
			j.Data = ls;
			return j;
		}

		private List<Models.ServicioHistoria> fncObtieneServiciosHistorialPorUsuario(int year, int month)
		{
			List<Models.ServicioHistoria> ls = new List<Models.ServicioHistoria>();
			DataTable dt = CoreFacturacion.Negocio.Historial(Models.Seguridad.UsuarioActual.UsuarioID, year, month);

			foreach (DataRow dr in dt.Rows)
			{
				Models.ServicioHistoria sh = new Models.ServicioHistoria();
				sh.TokenFactura = dr["TokenFactura"].ToString();
				sh.UsuarioID = dr["UsuarioID"].ToString();
				sh.Emisor_RFC = dr["Emisor_RFC"].ToString();
				sh.Receptor_RFC = dr["Receptor_RFC"].ToString();
				sh.Serie = dr["Serie"].ToString();
				sh.Folio = dr["Folio"].ToString();
				sh.FechaFactura = Convert.ToDateTime(dr["FechaFactura"]);
				sh.Total = Convert.ToDouble(dr["Total"]);
				sh.UUID = dr["UUID"].ToString();
				sh.FechaTimbrado = Convert.ToDateTime(dr["FechaTimbrado"]);
				sh.ArchivoXML = dr["ArchivoXML"].ToString();
				sh.ArchivoPDF = dr["ArchivoPDF"].ToString();
				sh.ConError = dr["ConError"].ToString();
				sh.DetalleError = dr["DetalleError"].ToString();
				sh.RazonSocialID = dr["RazonSocialID"].ToString();
				sh.Cliente = dr["Cliente"].ToString();
				sh.Tickets = dr["TokenFactura"].ToString();
				ls.Add(sh);
			}

			return ls;
		}

		private List<Models.DatosFiscales> fncObtieneRazonesSocialesPorUsuario()
		{
			DataTable dt = CoreFacturacion.Negocio.ListadoRazonSocial(Models.Seguridad.UsuarioActual.UsuarioID);
			List<Models.DatosFiscales> lRazones = new List<Models.DatosFiscales>();

			foreach (DataRow dr in dt.Rows)
			{
				Models.DatosFiscales df = new Models.DatosFiscales();
				df.Alias = dr["Alias"].ToString();
				df.Calle = dr["Calle"].ToString();
				df.RFC = dr["RFC"].ToString();
				df.RazonSocial = dr["Nombre"].ToString();
				df.Token = dr["RazonSocialID"].ToString();
				df.Ciudad = dr["Ciudad"].ToString();
				df.Colonia = dr["Colonia"].ToString();
				df.Cp = dr["CodigoPostal"].ToString();
				df.Estado = dr["Estado"].ToString();
				df.Localidad = dr["Localidad"].ToString();
				df.NumeroExterior = dr["NumeroExterior"].ToString();
				df.NumeroInterior = dr["NumeroInterior"].ToString();

				lRazones.Add(df);
			}
			return lRazones;
		}

		private List<Models.ServicioFactura> fncObtieneServiciosPendientesPorUsuario()
		{
			DataTable dt = CoreFacturacion.Negocio.UltimaCaptura(Models.Seguridad.UsuarioActual.UsuarioID);
			List<Models.ServicioFactura> ls = new List<Models.ServicioFactura>();

			foreach (DataRow dr in dt.Rows)
			{
				Models.ServicioFactura s = new Models.ServicioFactura();
				s.Folio = dr["Folio"].ToString();
				s.Producto = dr["Producto"].ToString();
				s.Unidad = dr["UnidadMedida"].ToString();
				s.Precio = Convert.ToDouble(dr["Precio"].ToString());
				s.Fecha = Convert.ToDateTime(dr["Fechahora"]).ToString("dd/MM/yyyy hh:mm");
				s.Importe = Convert.ToDouble(dr["Importe"]);
                s.MetodoPago = dr["MetodoPago"].ToString();
                s.CuentaBanco = dr["CtaBanco"].ToString();
                ls.Add(s);
			}

			return ls;
		}

		public ActionResult DescargarDocumento(string token, string tipo)
		{
			if (Models.Seguridad.UsuarioActual == null)
				return RedirectToAction("Index", "Home");

			try
			{
				if (token.Trim().Length > 0)
				{
					DataTable dtDatos = CoreFacturacion.Negocio.DatosFacturaToken(token);
					if (dtDatos == null || dtDatos.Rows.Count <= 0)
					{
						throw new Exception("El archivo no se encuentra disponible para el usuario actual.");
					}

					string directorioFacturas = Server.MapPath("~/App_Data/Facturas");
					string archivoXML = Models.Funciones.CStr2(dtDatos.Rows[0]["ArchivoXML"]);
					if (archivoXML.StartsWith("\\") || archivoXML.StartsWith("/")) { archivoXML = archivoXML.Substring(1, archivoXML.Length - 1); }
					string archivoPDF = Models.Funciones.CStr2(dtDatos.Rows[0]["ArchivoPDF"]);
					if (archivoPDF.StartsWith("\\") || archivoPDF.StartsWith("/")) { archivoPDF = archivoPDF.Substring(1, archivoPDF.Length - 1); }

					string sfilenameXML = archivoXML;
					string sfilenamePDF = archivoPDF;
					string sFileName = "";
					string sruta="";

					if (archivoXML != "")
					{
						string directorioArchivoXML = System.IO.Path.GetDirectoryName(archivoXML);
						directorioArchivoXML = System.IO.Path.Combine(directorioFacturas, directorioArchivoXML);
						if (directorioArchivoXML.StartsWith("\\") || directorioArchivoXML.StartsWith("/"))
						{
						     directorioArchivoXML = directorioArchivoXML.Substring(1, directorioArchivoXML.Length - 1);
						}
						archivoXML = System.IO.Path.Combine(directorioArchivoXML, String.Format("{0}.xml", System.IO.Path.GetFileNameWithoutExtension(dtDatos.Rows[0]["ArchivoXML"].ToString())));
						archivoPDF = System.IO.Path.Combine(directorioArchivoXML, String.Format("{0}.pdf", System.IO.Path.GetFileNameWithoutExtension(dtDatos.Rows[0]["ArchivoPDF"].ToString())));

						switch (tipo.ToLower().Trim()){
							case "pdf":
								sFileName = sfilenamePDF;
								sruta += archivoPDF;
								break;
							case "xml": 
								sFileName = sfilenameXML;
								sruta += archivoXML;
								break;
						}
                        if (sFileName.IndexOf(@"\") > 0)
                        {
                            sFileName = new string(sFileName.ToCharArray().Reverse().ToArray());
                            sFileName = sFileName.Substring(0, sFileName.IndexOf(@"\"));
                            sFileName = new string(sFileName.ToCharArray().Reverse().ToArray());
                        }
                        if (System.IO.File.Exists(sruta))
						{
							HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
							return File(sruta, "multipart/form-data", sFileName);
						}
						else
						{
							throw new Exception("Error: No se encontró el archivo.");
						}
					}
				}
				else
				{
					throw new Exception("No se encontró la factura.");
				}
			}
			catch
			{
				throw new Exception("No se encontró la factura.");
			}
			return null;
		}
	}
}
