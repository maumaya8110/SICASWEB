using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasNatural.Controllers
{
    public class InventariosController : Controller
    {
        // GET: Inventarios
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObtieneListaBodegas()
        {
            JsonResult j = new JsonResult();
            try
            {
                List<CASCO.EN.EquipoDeGas.Inventarios.Almacen> lalmacenes = CASCO.DAO.EquipoDeGas.Inventarios.ObtieneAlmacenesPorUsuario(User.Identity.Name);
                j.Data = lalmacenes;
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                throw ex;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
    }
}