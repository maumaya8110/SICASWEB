using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abastos.Controllers
{
    public class SolicitudMaterialesController : Controller
    {
        public ActionResult Index()
        {
            CASCO.EN.Abastos.Usuario u = CASCO.DAO.Abastos.SolicitudMateriales.GetInformacionUsuario(User.Identity.Name);
            return View(u);
        }

        public ActionResult Elaboracion()
        {
            CASCO.EN.Abastos.Usuario u = CASCO.DAO.Abastos.SolicitudMateriales.GetInformacionUsuario(User.Identity.Name);
            return View(u);
        }

        public ActionResult Aprobacion()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            CASCO.EN.Abastos.Usuario u = CASCO.DAO.Abastos.SolicitudMateriales.GetInformacionUsuario(User.Identity.Name);
            return View(u);
        }

        public ActionResult Autorizacion()
        {
            CASCO.EN.Abastos.Usuario u = CASCO.DAO.Abastos.SolicitudMateriales.GetInformacionUsuario(User.Identity.Name);
            return View(u);
        }

        [HttpPost]
        public JsonResult ConsultaSolicitud(CASCO.EN.Abastos.SolicitudMaterialesConsulta solicitud)
        {
            JsonResult j = new JsonResult();

            if (CASCO.EN.General.Utils.LogEnable)
            {
                string prs = "";
                prs = string.Format("Estatus: {0}, Fecha Inicio: {1}, Fecha Fin: {2}", solicitud.Estatus.id, solicitud.FechaDesde, solicitud.FechaHasta);
                CASCO.EN.General.EventLog.WriteInfo(prs);
            }

            try { 
            j.Data = Models.SolicitudMateriales.Consulta(solicitud);
            }
            catch(Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaSolicitudesPorEstatus(int estatus_id)
        {
            CASCO.EN.Abastos.SolicitudMaterialesConsulta solicitud = new CASCO.EN.Abastos.SolicitudMaterialesConsulta();
            solicitud.Estatus.id = estatus_id;
            JsonResult j = new JsonResult();
            j.Data = Models.SolicitudMateriales.Consulta(solicitud);
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult InsertaSolicitud(CASCO.EN.Abastos.SolicitudMateriales solicitud)
        {
            Models.SolicitudMateriales.Inserta(solicitud);
            JsonResult j = new JsonResult();
            j.Data = solicitud;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ActualizaEstatusSolicitud(int solicitud_id, int estatus_id)
        {
            bool b = Models.SolicitudMateriales.ActualizaEstatus(User.Identity.Name, solicitud_id, estatus_id);
            JsonResult j = new JsonResult();
            j.Data = b;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaServiciosPorDivisionEmpresaDeptoAlmacen(int division, int empresa, int departamento, int? almacen)
        {
            List<CASCO.EN.Abastos.ArticuloSolicitudMateriales> articulos = Models.SolicitudMateriales.GetServiciosPorDivisionEmpresaDeptoAlmacen(division, empresa, departamento, almacen);
            JsonResult j = new JsonResult();
            j.Data = articulos;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaAlmacenesPorDivisionEmpresaDepto(int division, int empresa, int departamento)
        {
            List<CASCO.EN.Abastos.Almacen> articulos = Models.SolicitudMateriales.GetAlmacenesPorDivisionEmpresaDepto(division, empresa, departamento);
            JsonResult j = new JsonResult();
            j.Data = articulos;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsultaSoportes(int division, int empresa, int departamento)
        {
            List<CASCO.EN.Abastos.SoporteElectronicoSolicitudMateriales> soportes = Models.SolicitudMateriales.ConsultaSoportes(division, empresa, departamento);
            JsonResult j = new JsonResult();
            j.Data = soportes;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult AgregaComentarioASolicitud(int idSolicitud, string comentario)
        {
            string comentarios = Models.SolicitudMateriales.AgregaComentarioASolicitud(idSolicitud, comentario, User.Identity.Name);
            JsonResult j = new JsonResult();
            j.Data = comentarios;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public ActionResult Upload()
        {
            string sruta_upload = "";
            bool isSaveFile = false;
            HttpPostedFileBase btfile = Request.Files[0];
            int Solicitud_ID = Convert.ToInt32(Request.Params["Solicitud_ID"].ToString());
            int SoporteSolicitud_ID = Convert.ToInt32(Request.Params["SoporteSolicitud_ID"].ToString());
            int Soporte_ID = Convert.ToInt32(Request.Params["Soporte_ID"].ToString());
            String _Path = Server.MapPath("~/Uploads/" + Solicitud_ID);
            String _PathFile = "";

            CASCO.EN.Abastos.SolicitudMaterialesConsulta sc = new CASCO.EN.Abastos.SolicitudMaterialesConsulta();
            sc.id = Solicitud_ID;
            object lsolicitudes = Models.SolicitudMateriales.Consulta(sc);
            CASCO.EN.Abastos.SolicitudMateriales solicitud = ((List<CASCO.EN.Abastos.SolicitudMateriales>)lsolicitudes)[0];
            CASCO.EN.Abastos.SoporteElectronicoSolicitudMateriales se = new CASCO.EN.Abastos.SoporteElectronicoSolicitudMateriales();
            foreach (CASCO.EN.Abastos.SoporteElectronicoSolicitudMateriales sop in solicitud.soportes)
            {
                if (SoporteSolicitud_ID > 0)
                {
                    if(sop.id == SoporteSolicitud_ID)
                    {
                        se = sop;
                        break;
                    }
                }
                else if (SoporteSolicitud_ID == 0)
                {
                    if (sop.soporte.id == Soporte_ID)
                    {
                        se = sop;
                        break;
                    }
                }
            }

            if (se.id > 0 || se.soporte.id > 0)
            {
                try
                {
                    string fname = btfile.FileName;
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = btfile.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }

                    if (!Directory.Exists(_Path))
                        Directory.CreateDirectory(_Path);

                    fname = SoporteSolicitud_ID.ToString() + fname.Substring(fname.IndexOf("."), 4);
                    _PathFile = Path.Combine(_Path, fname);
                    btfile.SaveAs(_PathFile);
                    isSaveFile = true;
                    sruta_upload = "/" + Solicitud_ID + "/" + fname;
                    se.Ruta_Documento = sruta_upload;
                    if (!Models.SolicitudMateriales.InsertaDocumentoSoporte(Solicitud_ID, se))
                        sruta_upload = "";
                }
                catch
                {
                    if (isSaveFile)
                        System.IO.File.Delete(_PathFile);
                    sruta_upload = "";
                }
            }

            JsonResult j = new JsonResult();
            j.Data = sruta_upload;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult EliminaDetalle(int idSolicitud, int idx)
        {
            bool b = Models.SolicitudMateriales.EliminaRegistroDetalle(idSolicitud, idx);
            JsonResult j = new JsonResult();
            j.Data = b;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
    }
}
