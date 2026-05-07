using PerfumeriaMVC.Models;
using PerfumeriaMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;



namespace PerfumeriaMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly Perfumeriadb _context;

        public AccountController(Perfumeriadb context)
        {
            _context = context;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public IActionResult Register(string nombre, string correo, string password)
        {
            // Verificar si ya existe
            var existe = _context.Usuarios
                .Any(u => u.correo == correo);

            if (existe)
            {
                ViewBag.Error = "Este correo ya está registrado";
                return View();
            }

            var usuario = new Usuario
            {
                nombre = nombre,
                correo = correo,
                password = password,
                intentos_fallidos = 0,
                fecha_registro = DateTime.Now,
                rol = "CLIENTE" // 🔥 AQUÍ ESTÁ LA CLAVE
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.correo == correo && u.password == password);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }

            // Guardar sesión
            HttpContext.Session.SetString("usuario", usuario.nombre);
            HttpContext.Session.SetString("rol", usuario.rol ?? "CLIENTE");

            // 🔥 REDIRECCIÓN POR ROL
            if (usuario.rol == "ADMIN")
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (usuario.rol == "EMPLEADO")
            {
                return RedirectToAction("Index", "Empleado");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}