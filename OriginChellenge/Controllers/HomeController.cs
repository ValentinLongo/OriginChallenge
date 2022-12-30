using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginChellenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Prueba()
        //{
        //    string pru = "Hola";
        //    pru = "Chau";
        //    ViewBag.pru = pru;
        //    return Content("hola");
        //}
    }
}