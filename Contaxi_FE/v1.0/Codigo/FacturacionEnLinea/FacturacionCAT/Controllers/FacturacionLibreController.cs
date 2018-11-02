using FacturacionCAT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionCAT.Controllers
{
    public class FacturacionLibreController : Controller
    {
        public ActionResult Index()
        {
            List<Models.Estado> lEstados = new List<Models.Estado>();
            lEstados = Models.Estado.GetEstados();
            ViewBag.session = Seguridad.ASPSessionID;
            return View(lEstados);
        }

        public ActionResult ConfirmaFactura(string token)
        {
            DataTable dtFacturacion = CoreFacturacion.Negocio.DatosFacturacionTmp(Seguridad.ASPSessionID, token);
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
            ViewBag.Title = "Confirmación";
            ViewBag.ServiciosPendientes = fncObtieneServiciosPendientes();
            return View(df);
        }

        [HttpPost]
        public JsonResult FacturaConfirmada(string token)
        {
            CoreFacturacion.ResultadoConsulta resultado;
            try
            {
                resultado = CoreFacturacion.Negocio.Facturar(Seguridad.ASPSessionID, token, (System.Web.HttpContextBase)HttpContext);
                if (resultado == null)
                {
                    resultado = new CoreFacturacion.ResultadoConsulta();
                    resultado.Resultado = false;
                    resultado.Mensaje = "Atención: No fue posible generar la facturar.";
                }
            }
            catch (Exception ex)
            {
                resultado = new CoreFacturacion.ResultadoConsulta();
                resultado.Resultado = false;
                resultado.Mensaje = ex.Message;
                string smsg = "Exception: " + ex.Message + Environment.NewLine + ex.StackTrace;
                CoreFacturacion.Negocio.EnviaMailAlAdministrador(smsg, Seguridad.ASPSessionID, token);

                if (ex.Message.Contains("CFDI - 502"))
                {
                    resultado.Mensaje = "Ocurrió un error de comunicación con el servidor de timbrado, esta situación es temporal, favor de intentar más tarde generar el CFDI. El administrador del sitio ha sido notificado.";
                }
                else if (ex.Message.Contains("CFDI - 500"))
                {
                    resultado.Mensaje = "Ocurrió un error de comunicación con el servidor de timbrado, esta situación es temporal, favor de intentar más tarde generar el CFDI. El administrador del sitio ha sido notificado.";
                }
                else if (ex.Message.Contains("CFDI - 442"))
                {
                    resultado.Mensaje = "No fue posible realizar el Timbrado Fiscal. El administrador ha sido informado, favor de ponerse en contacto con el departamento de atención a clientes (atencionaclientes@urgi.com.mx).";
                }
                else if (ex.Message.Contains("CFDI - 443"))
                {
                    resultado.Mensaje = "No fue posible realizar el Timbrado Fiscal. Favor de revisar la información capturada, pueden existir errores en algunos de los campos obligatorios para el SAT, por ejemplo: Localidad, Ciudad, Estado, RFC. Será redireccionado a la página de Facturación para que valide la información proporcionada.";
                }
            }
            JsonResult j = new JsonResult();
            j.Data = resultado;
            return j;
        }

        [HttpPost]
        public JsonResult GetDatosFiscalesUltimaFactura()
        {
            Models.DatosFiscales df = new DatosFiscales();
            df = Models.DatosFiscales.GetDatosFiscalesBySession(Seguridad.ASPSessionID);
            JsonResult j = new JsonResult();
            if (df != null)
                j.Data = df;
            else
                j.Data = false;
            return j;
        }

        [HttpPost]
        public JsonResult GuardarDatosFiscales(Models.DatosFiscales datos)
        {
            if (datos.NumeroInterior == null || datos.NumeroInterior.Trim().Length == 0)
                datos.NumeroInterior = "";

            if (datos.Token == null)
                datos.Token = String.Empty;
            if (datos.Token.Trim().Length == 0)
                datos.Token = "00000000-0000-0000-0000-000000000000";

            DataTable r = CoreFacturacion.Negocio.GuardarDatosFiscalesTemporales(Seguridad.ASPSessionID, datos.Token, 
                                                        datos.RazonSocial.ToUpper(), datos.RFC.ToUpper(), datos.Calle.ToUpper(), datos.NumeroExterior.ToUpper(), datos.NumeroInterior.ToUpper(),
                                                        datos.Colonia.ToUpper(), datos.Cp.ToUpper(), datos.Ciudad.ToUpper(), datos.Localidad.ToUpper(), datos.Estado.ToUpper()
                                                        , datos.Email);
            Seguridad.FacturarA = r.Rows[0]["RazonSocialID"].ToString();
            JsonResult j = new JsonResult();
            j.Data = Seguridad.FacturarA;
            return j;
        }
        [HttpPost]
        public JsonResult GetTickets()
        {
            List<Models.ServicioFactura> ls = fncObtieneServiciosPendientes();
            JsonResult j = new JsonResult();
            j.Data = ls;
            return j;
        }

        [HttpPost]
        public JsonResult AgregaServicio(string folio, string importe)
        {
            string lbl = "";
            try
            {
                CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.AgregarTicket(Seguridad.ASPSessionID, folio, Convert.ToDouble(importe));
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
                CoreFacturacion.ResultadoConsulta r = CoreFacturacion.Negocio.EliminarTicket(Seguridad.ASPSessionID, folio);
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

        private List<Models.ServicioFactura> fncObtieneServiciosPendientes()
        {
            DataTable dt = CoreFacturacion.Negocio.UltimaCaptura(Seguridad.ASPSessionID);
            List<Models.ServicioFactura> ls = new List<Models.ServicioFactura>();
            if (dt != null)
            {
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
            }
            return ls;
        }

        public ActionResult DescargaFactura(string token)
        {
            ViewBag.Message = "Documentos del CFDI " + token;
            ViewBag.Token = token;
            return View();
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
                        throw new Exception("El archivo no se encuentra disponible.");
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
            catch
            {
                throw new Exception("No se encontró la factura.");
            }
            return null;
        }
    }
}
