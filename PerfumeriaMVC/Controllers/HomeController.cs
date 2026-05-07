using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Data;

namespace PerfumeriaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Perfumeriadb _context;

        public HomeController(Perfumeriadb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var usuario = HttpContext.Session.GetString("usuario");

            if (usuario == null)
                return RedirectToAction("Login", "Account");

            var productos = _context.Productos.ToList();
            return View(productos);
        }

    }
}