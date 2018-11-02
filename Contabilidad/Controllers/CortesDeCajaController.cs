using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    public class CortesDeCajaController : Controller
    {
        // GET: CortesDeCaja
        public ActionResult Index()
        {
            return View();
        }
    }
}