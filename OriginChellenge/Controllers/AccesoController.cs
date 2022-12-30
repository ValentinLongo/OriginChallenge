using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OriginChellenge.Models;

namespace OriginChellenge.Controllers
{
    public class AccesoController : Controller
    {
        public static int IdTarjetaLogin;
        // GET: Acceso
        public ActionResult Tarjeta()
        {
            return View();
        }

        public ActionResult PIN()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerificarTarjeta(Tarjeta mTarjeta)
        {
            Tarjeta tarj = new Tarjeta();
            using (dbOriginEntities2 db = new dbOriginEntities2())
            {
                tarj = db.Tarjeta.Where(t => t.NumeroTarjeta == mTarjeta.NumeroTarjeta).FirstOrDefault();
            }
            if (tarj == null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                return RedirectToAction("PIN", "Acceso");
            }
        }

        [HttpPost]
        public ActionResult VerificarPIN(Tarjeta mTarjeta)
        {
            Tarjeta tarj = new Tarjeta();
            using (dbOriginEntities2 db = new dbOriginEntities2())
            {
                tarj = db.Tarjeta.Where(t => t.PIN == mTarjeta.PIN).FirstOrDefault();
                IdTarjetaLogin = Convert.ToInt32(tarj.IdTarjeta);
            }
            if (tarj == null)
            {
                return RedirectToAction("Error", "Error");
            }
            else
            {
                return RedirectToAction("Inicio", "Inicio");
            }
        }

        [HttpPost]
        public ActionResult CerrarSesion()
        {
            return RedirectToAction("Tarjeta", "Acceso");
        }
    }
}