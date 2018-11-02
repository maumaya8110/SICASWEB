using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasNatural.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObtieneMaster()
        {
            JsonResult j = new JsonResult();
            try
            {
                j.Data = CASCO.DAO.EquipoDeGas.EquiposGas.ObtieneMaster();
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                if (ex.InnerException != null)
                    j.Data = ex.InnerException.Message;
                else
                    j.Data = ex.Message;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }

        [HttpPost]
        public JsonResult InsertaRegistro(CASCO.EN.EquipoDeGas.RegistroMaster registro)
        {
            JsonResult j = new JsonResult();
            try
            {
                j.Data = CASCO.DAO.EquipoDeGas.EquiposGas.InsertaRegistroMaster(registro, User.Identity.Name);
            }
            catch (Exception ex)
            {
                CASCO.EN.General.EventLog.WriteError(ex.ToString());
                if (ex.InnerException != null)
                    j.Data = ex.InnerException.Message;
                else
                    j.Data = ex.Message;
            }
            j.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return j;
        }
    }
}