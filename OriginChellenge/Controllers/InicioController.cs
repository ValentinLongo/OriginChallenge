using OriginChellenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OriginChellenge.Controllers
{
    public class InicioController : Controller
    {
        public static Operacion operacionActual;
        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Balance()
        {
            Tarjeta mTarjeta = datosBalance();
            ViewBag.numeroTarjeta = mTarjeta.NumeroTarjeta;
            ViewBag.fechaVencimiento = mTarjeta.FechaVencimiento;
            ViewBag.cantidad = mTarjeta.Balance;
            return View();
        }

        public ActionResult ReporteOperacion()
        {
            Operacion mOperacion = operacionActual;
            Tarjeta mTarjeta = datosBalance();
            ViewBag.numeroTarjeta = mTarjeta.NumeroTarjeta;
            ViewBag.hora = mOperacion.Fecha.ToString();
            ViewBag.cantidadRetirada = mOperacion.Monto;
            ViewBag.balance = mTarjeta.Balance;
            return View();
        }

        private Tarjeta datosBalance()
        {
            using(dbOriginEntities2 db = new dbOriginEntities2())
            {
                int idTarjeta = AccesoController.IdTarjetaLogin;
                Tarjeta mTarjeta = db.Tarjeta.Where(t => t.IdTarjeta == idTarjeta).FirstOrDefault();

                //Guardo operacion
                var mOperacion = new Operacion
                {
                    IdTipoOperacion = 2,
                    IdTarjeta = idTarjeta,
                    Fecha = Convert.ToDateTime(DateTime.Now.ToString("G"))
                };
                db.Operacion.Add(mOperacion);
                db.SaveChanges();

                return mTarjeta;
            }     
        }

        public ActionResult Retiro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Retiro(Operacion mOperacion)
        {
            using(dbOriginEntities2 db = new dbOriginEntities2())
            {
                //Modificaciones en tabla Operacion
                int idTajeta = AccesoController.IdTarjetaLogin;
                decimal total = Convert.ToDecimal(mOperacion.Monto);
                var operacion = new Operacion
                {
                    IdTipoOperacion = 1,
                    IdTarjeta = idTajeta,
                    Monto = Convert.ToDecimal(mOperacion.Monto),
                    Fecha = Convert.ToDateTime(DateTime.Now.ToString("G"))
                };
                db.Operacion.Add(operacion);

                operacionActual = operacion;
                //Modificaciones en tabla Tajeta
                Tarjeta mTarjeta = db.Tarjeta.Where(t => t.IdTarjeta == idTajeta).FirstOrDefault();
                mTarjeta.Balance = mTarjeta.Balance - Convert.ToDecimal(operacion.Monto);

                db.SaveChanges();

                return RedirectToAction("ReporteOperacion", "Inicio");
            }
        }
    }
}