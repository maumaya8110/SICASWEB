using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult CorteCajaPuntos()
        {
            List<CASCO.EN.General.Empresa> lempresas = CASCO.DAO.General.GetEmpresasUsuario(User.Identity.Name);
            List<CASCO.EN.General.Caja> lcajas = CASCO.DAO.General.GetCajasUsuario(User.Identity.Name);

            CASCO.EN.General.Empresa eaux = new CASCO.EN.General.Empresa();
            eaux.id = 0;
            eaux.descripcion = "Todas";
            eaux.selected = "selected";
            eaux.aux = "0";
            lempresas.Insert(0, eaux);

            CASCO.EN.General.Caja caux = new CASCO.EN.General.Caja();
            caux.id = 0;
            caux.descripcion = "Todas";
            caux.selected = "selected";
            caux.aux = "0";
            lcajas.Insert(0, caux);

            ViewBag.Empresas = lempresas;
            ViewBag.Cajas = lcajas;
            return View();
        }

        [HttpPost]
        public JsonResult GetCortesPorPunto(string fechainicio, string fechafin, int empresa_id, int caja_id)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            provider = new System.Globalization.CultureInfo("es-MX");
            DateTime dteInicio = DateTime.ParseExact(fechainicio, "dd/MM/yyyy", provider);
            DateTime dteFin = DateTime.ParseExact(fechafin, "dd/MM/yyyy", provider);
            List<CASCO.EN.Contabilidad.CorteCaja> l = CASCO.DAO.Contabilidad.CorteCaja.GetCortesCaja(User.Identity.Name, dteInicio, dteFin, empresa_id, caja_id);
            JsonResult j = new JsonResult();
            j.Data = l;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
    }
}