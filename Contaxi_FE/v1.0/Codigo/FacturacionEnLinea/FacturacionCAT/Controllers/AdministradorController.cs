using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionCAT.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/
        public ActionResult Index()
        {
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");

            List<SelectListItem> li = new List<SelectListItem>();
            DateTime fechainicial = DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1);
            for (int i = 0; i < 12; i++)
            {
                DateTime d = fechainicial.AddMonths(i * -1);
                SelectListItem item = new SelectListItem();
                item.Value = d.ToString("yyyy-MM-dd");
                item.Text = d.ToString("yyyy - MMMM").ToUpper();
                li.Add(item);
            }
            ViewBag.slMeses = li;
            return View();
        }

        [HttpPost]
        public JsonResult GetFacturasPorMes(string fecha, string serie, string folio, string servicio, string rfc, string mail)
        {

            System.Data.DataTable resultado = CoreFacturacion.Negocio.ConsultaFacturas(Convert.ToDateTime(fecha),serie, folio, servicio, rfc, mail);
            List<CascoFacturacionLib.ItemCFDI> lcfdi = new List<CascoFacturacionLib.ItemCFDI>();
            if (resultado.Rows.Count > 0)
            {
                foreach (System.Data.DataRow dr in resultado.Rows)
                {
                    CascoFacturacionLib.ItemCFDI item = new CascoFacturacionLib.ItemCFDI();
                    item.TokenFactura = dr["TokenFactura"].ToString();
                    if (dr["ArchivoPDF"] != DBNull.Value)
                        item.ArchivoPDF = dr["ArchivoPDF"].ToString();
                    if (dr["ArchivoXML"] != DBNull.Value)
                        item.ArchivoXML = dr["ArchivoXML"].ToString();
                    if (dr["ConError"] != DBNull.Value)
                        item.ConError = Convert.ToBoolean(dr["ConError"]);

                    item.CorreoElectronico = dr["CorreoElectronico"].ToString();
                    if (dr["DetalleError"] != DBNull.Value)
                        item.DetalleError = dr["DetalleError"].ToString();
                    item.Emisor_RFC = dr["Emisor_RFC"].ToString();
                    item.FacturacionLibre = Convert.ToBoolean(dr["FacturacionLibre"]);
                    item.FechaFactura = Convert.ToDateTime(dr["FechaFactura"]).ToString("dd/MM/yyyy HH:mm");
                    item.Folio = dr["Folio"].ToString();
                    item.Receptor_Nombre = dr["Receptor_Nombre"].ToString();
                    item.Receptor_RFC = dr["Receptor_RFC"].ToString();
                    if (dr["Tickets"] != DBNull.Value)
                        item.Tickets = dr["Tickets"].ToString();
                    if (dr["Total"] != DBNull.Value)
                        item.Total = Convert.ToDouble(dr["Total"]);
                    if (dr["UUID"] != DBNull.Value)
                        item.UUID = dr["UUID"].ToString();
                    if (dr["Serie"] != DBNull.Value)
                        item.Serie = dr["Serie"].ToString();
                    lcfdi.Add(item);
                }
            }
            JsonResult j = new JsonResult();
            j.Data = lcfdi;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult CancelarFactura(string folio, string serie)
        {
            CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.CancelarFactura(Models.Seguridad.UsuarioActual.UsuarioID, serie, folio);
            JsonResult j = new JsonResult();
            j.Data = r;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        public JsonResult CancelarFacturaSerieFolio(string serie, string folio)
        {
            CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.CancelarFactura(Models.Seguridad.UsuarioActual.UsuarioID, serie, folio);
            JsonResult j = new JsonResult();
            j.Data = r;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        public ActionResult FacturasCanceladas()
        {
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");

            List<SelectListItem> li = new List<SelectListItem>();
            DateTime fechainicial = DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1);
            for (int i = 0; i < 12; i++)
            {
                DateTime d = fechainicial.AddMonths(i * -1);
                SelectListItem item = new SelectListItem();
                item.Value = d.ToString("yyyy-MM-dd");
                item.Text = d.ToString("yyyy - MMMM").ToUpper();
                li.Add(item);
            }
            ViewBag.slMeses = li;
            return View();
        }

        public JsonResult GetFacturasCanceladasPorMes(string fecha)
        {
            System.Data.DataTable resultado = CoreFacturacion.Negocio.FacturasCanceladasPorMes(Convert.ToDateTime(fecha));
            List<CascoFacturacionLib.ItemCFDI> lcfdi = new List<CascoFacturacionLib.ItemCFDI>();
            if (resultado.Rows.Count > 0)
            {
                foreach (System.Data.DataRow dr in resultado.Rows)
                {
                    CascoFacturacionLib.ItemCFDI item = new CascoFacturacionLib.ItemCFDI();
                    item.TokenFactura = dr["TokenFactura"].ToString();
                    item.CorreoElectronico = dr["CorreoElectronico"].ToString();
                    item.Emisor_RFC = dr["Emisor_RFC"].ToString();
                    item.FacturacionLibre = Convert.ToBoolean(dr["FacturacionLibre"]);
                    item.Folio = dr["Folio"].ToString();
                    item.Receptor_Nombre = dr["Receptor_Nombre"].ToString();
                    item.Receptor_RFC = dr["Receptor_RFC"].ToString();
                    item.FechaCancelacion = Convert.ToDateTime(dr["fechaCancelacion"]).ToString("dd/MM/yyyy HH:mm");
                    item.FechaFactura = Convert.ToDateTime(dr["FechaFactura"]).ToString("dd/MM/yyyy HH:mm");
                    if (dr["Tickets"] != DBNull.Value)
                        item.Tickets = dr["Tickets"].ToString();
                    if (dr["Total"] != DBNull.Value)
                        item.Total = Convert.ToDouble(dr["Total"]);
                    if (dr["UUID"] != DBNull.Value)
                        item.UUID = dr["UUID"].ToString();
                    lcfdi.Add(item);
                }
            }
            JsonResult j = new JsonResult();
            j.Data = lcfdi;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
        
        public ActionResult CancelacionDeFactura()
        {
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult ActualizaServicio()
        {
            if (Models.Seguridad.UsuarioActual == null || Models.Seguridad.UsuarioActual.UsuarioID.Length == 0)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public JsonResult ObtieneEmisores()
        {
            DataTable dt = CoreFacturacion.Negocio.ObtieneEmisores();
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem i = new SelectListItem();
                i.Value = dr["Emisor_RFC"].ToString();
                i.Text = dr["Emisor_Nombre"].ToString();
                li.Add(i);
            }
            JsonResult j = new JsonResult();
            j.Data = li;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaInfoServicio(string servicio)
        {
            DataTable r = CoreFacturacion.Negocio.ObtieneInformacionDelServicio(servicio);
            CascoFacturacionLib.ServicioURGI s = new CascoFacturacionLib.ServicioURGI();

            if (r.Rows.Count > 0)
            {
                foreach (DataRow dr in r.Rows)
                {
                    s.Folio = dr["Folio"].ToString();
                    s.Monto = Convert.ToDouble(dr["Monto"]);
                    s.Fecha = Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy hh:mm");
                    s.Contacto = dr["Contacto"].ToString();
                    s.Unidad = dr["Unidad"].ToString();
                    s.RFC_Emisor = dr["RFC_Emisor"].ToString();
                    s.MetodoPago_ID = Convert.ToInt32(dr["MetodoPago_ID"]);
                    s.CtaBanco = dr["CtaBanco"].ToString();
                }
            }

            JsonResult j = new JsonResult();
            j.Data = s;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ActualizaInfoServicio(CascoFacturacionLib.ServicioURGI servicio)
        {
            CoreFacturacion.ResultadoConsulta r = new CoreFacturacion.ResultadoConsulta();
            try
            {
                r = CoreFacturacion.Negocio.ActualizaServicio(Models.Seguridad.UsuarioActual.UsuarioID, servicio);
            }
            catch (Exception ex)
            {
                r.Resultado = false;
                r.Mensaje = ex.Message;
            }
            JsonResult j = new JsonResult();
            j.Data = r;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
        public ActionResult DescargarDocumento(string token, string tipo)
        {
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
                    string sruta = "";

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

                        switch (tipo.ToLower().Trim())
                        {
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
            catch(Exception ex)
            {
                throw new Exception("Ocurrió un error al descargar la factura.",ex);
            }
            return null;
        }
    }
}
