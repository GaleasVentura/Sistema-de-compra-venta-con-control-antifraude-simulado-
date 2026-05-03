using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Data;
using PerfumeriaMVC.Models;

namespace PerfumeriaMVC.Controllers
{
    public class productosController : Controller
    {
        private readonly Perfumeriadb _context;

        public productosController(Perfumeriadb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.productos.ToList();
            return View(lista);
        }
    }
}