using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Data;
using PerfumeriaMVC.Models;

namespace PerfumeriaMVC.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly Perfumeriadb _context;

        public EmpleadoController(Perfumeriadb context)
        {
            _context = context;
        }

        private bool EsEmpleado()
        {
            return HttpContext.Session.GetString("rol") == "EMPLEADO";
        }

        public IActionResult Index()
        {
            if (!EsEmpleado())
                return RedirectToAction("Login", "Account");

            return View();
        }

        //  PRODUCTOS
        public IActionResult Productos()
        {
            if (!EsEmpleado())
                return RedirectToAction("Login", "Account");

            var lista = _context.Productos.ToList();
            return View(lista);
        }

        //  COMPRAS
        public IActionResult Compras()
        {
            if (!EsEmpleado())
                return RedirectToAction("Login", "Account");

            var lista = _context.Compra.ToList();
            return View(lista);
        }

        // DETALLE COMPRA
        public IActionResult DetalleCompra(int id)
        {
            if (!EsEmpleado())
                return RedirectToAction("Login", "Account");

            var detalle = (from d in _context.DetalleCompra
                           join p in _context.Productos
                           on d.id_productos equals p.id_producto
                           join c in _context.Compra
                           on d.id_compra equals c.id_compra
                           where d.id_compra == id
                           select new DetalleTransaccionViewModel
                           {
                               producto = p.nombre,
                               cantidad = d.cantidad,
                               precio = d.precio,
                               subtotal = d.subtotal,
                               estado = c.estado
                           }).ToList();

            return View(detalle);
        }
    }
}