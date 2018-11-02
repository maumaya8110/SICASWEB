using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abastos.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Abastos";
            string s = HttpContext.Server.MapPath(@"Bin\App_Data\Comercial\");
			return View();
		}

        public ActionResult Ejemplo()
        {
            ViewBag.Message = "UploadFile";
            return View();
        }
        
    }
}
