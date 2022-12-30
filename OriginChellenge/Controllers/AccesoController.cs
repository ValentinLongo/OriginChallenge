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
            if(tarj == null)
            {
                return Content("No se encontro nada");
            }
            else
            {
                return RedirectToAction("PIN","Acceso");
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
                return Content("Datos Incorrectos");
            }
            else
            {
                return RedirectToAction("Inicio", "Inicio");
            }
        }
    }
}