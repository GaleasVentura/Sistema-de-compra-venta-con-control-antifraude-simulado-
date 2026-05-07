using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Data;

namespace PerfumeriaMVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Perfumeriadb _context;

        public ProductosController(Perfumeriadb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();

            ViewBag.Descuentos = _context.Descuentos.ToList();

            return View(productos);
        }

        public IActionResult EliminarProducto(int id)
        {
            var p = _context.Productos.Find(id);
            if (p != null)
            {
                _context.Productos.Remove(p);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}