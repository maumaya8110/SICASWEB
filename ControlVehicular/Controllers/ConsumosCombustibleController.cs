using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlVehicular.Controllers
{
    public class ConsumosCombustibleController : Controller
    {
        // GET: ConsumosCombustible
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UnidadesConConsumo()
        {
            List<CASCO.EN.ControlVehicular.Unidad> lunidades = CASCO.DAO.ControlVehicular.ConsumosCombustible.GetUnidadesConConsumo();
            JsonResult j = new JsonResult();
            j.Data = lunidades;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult ConsumosPorFecha(int unidad, string inicio, string fin)
        {
            DateTime dteIni = Convert.ToDateTime(inicio);
            DateTime dteFin = Convert.ToDateTime(fin);
            List<CASCO.EN.ControlVehicular.ConsumoCombustible> lunidades = CASCO.DAO.ControlVehicular.ConsumosCombustible.GetConsumosPorFecha(unidad, dteIni, dteFin);
            JsonResult j = new JsonResult();
            j.Data = lunidades;
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
    }
}