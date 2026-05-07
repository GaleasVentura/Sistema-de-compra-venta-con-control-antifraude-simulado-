using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Data;
using PerfumeriaMVC.Models;
using System.Linq;

namespace PerfumeriaMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly Perfumeriadb _context;

        public AdminController(Perfumeriadb context)
        {
            _context = context;
        }

        // 🔐 VALIDACIÓN CENTRAL
        private bool EsAdmin()
        {
            return HttpContext.Session.GetString("rol") == "ADMIN";
        }

        // 📊 PANEL PRINCIPAL
        public IActionResult Index()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.TotalProductos = _context.Productos.Count();
            ViewBag.TotalUsuarios = _context.Usuarios.Count();
            ViewBag.TotalVentas = _context.Compra.Count();
            ViewBag.Sospechosas = _context.EvaluacionFraude.Count(x => x.resultado == "Sospechosa");

            return View();
        }

        // 👥 LISTAR USUARIOS
        public IActionResult Usuarios()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var lista = _context.Usuarios.ToList();
            return View(lista);
        }

        // ❌ ELIMINAR USUARIO
        public IActionResult Eliminar(int id)
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var user = _context.Usuarios.Find(id);

            if (user != null)
            {
                _context.Usuarios.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Usuarios");
        }

        // 🟢 FORMULARIO CREAR EMPLEADO
        [HttpGet]
        public IActionResult CrearEmpleado()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        // 🔥 GUARDAR EMPLEADO
        [HttpPost]
        public IActionResult CrearEmpleado(string nombre, string correo, string password)
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var existe = _context.Usuarios.Any(u => u.correo == correo);

            if (existe)
            {
                ViewBag.Error = "El correo ya existe";
                return View();
            }

            var empleado = new Usuario
            {
                nombre = nombre,
                correo = correo,
                password = password,
                intentos_fallidos = 0,
                fecha_registro = DateTime.Now,
                rol = "EMPLEADO"
            };

            _context.Usuarios.Add(empleado);
            _context.SaveChanges();

            return RedirectToAction("Usuarios");
        }

        // 🛍️ PRODUCTOS
        public IActionResult Productos()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var lista = _context.Productos.ToList();
            return View(lista);
        }

        // 💳 TRANSACCIONES GENERALES
        public IActionResult Transacciones()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var lista = _context.Compra.ToList();
            return View(lista);
        }

        // 🚨 TRANSACCIONES SOSPECHOSAS
        public IActionResult TransaccionesSospechosas()
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var lista = (from c in _context.Compra
                         join e in _context.EvaluacionFraude
                         on c.id_compra equals e.id_compra
                         where e.resultado == "Sospechosa"
                         select new TransaccionSospechosaViewModel
                         {
                             id_compra = c.id_compra,
                             id_usuario = c.id_usuario,
                             fecha_compra = c.fecha_compra,
                             total = c.total,
                             resultado = e.resultado,
                             motivo = e.motivo
                         }).ToList();

            return View(lista);
        }

        // 🔍 DETALLE (MODAL AJAX - SIN ROMPER VISTAS)
        public IActionResult DetalleTransaccion(int id)
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var detalle = (from d in _context.DetalleCompra
                           join p in _context.Productos on d.id_productos equals p.id_producto
                           join c in _context.Compra on d.id_compra equals c.id_compra
                           where d.id_compra == id
                           select new DetalleTransaccionViewModel
                           {
                               id_detalle = d.id_detalle,
                               producto = p.nombre,
                               cantidad = d.cantidad,
                               precio = d.precio,
                               subtotal = d.subtotal,
                               estado = c.estado
                           }).ToList();

            return Json(detalle);
        }

        // ⬆ ESCALAR CASO
        public IActionResult EscalarCaso(int id)
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var existe = _context.Revision.Any(r => r.id_compra == id);

            if (!existe)
            {
                var revision = new Revision
                {
                    id_compra = id,
                    id_admin = 1,
                    respuesta = "Pendiente",
                    fecha_respuesta = DateTime.Now,
                    comentarios = "Escalado por sospecha"
                };

                _context.Revision.Add(revision);
                _context.SaveChanges();
            }

            return RedirectToAction("TransaccionesSospechosas");
        }

        // ✔ APROBAR
        public IActionResult AprobarCaso(int id)
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var revision = _context.Revision.FirstOrDefault(r => r.id_compra == id);

            if (revision != null)
            {
                revision.respuesta = "Aprobado";
                revision.fecha_respuesta = DateTime.Now;

                var compra = _context.Compra.Find(id);
                if (compra != null)
                    compra.estado = "Aprobada";

                _context.SaveChanges();
            }

            return RedirectToAction("TransaccionesSospechosas");
        }

        // ❌ RECHAZAR
        public IActionResult RechazarCaso(int id)
        {
            if (!EsAdmin())
                return RedirectToAction("Login", "Account");

            var revision = _context.Revision.FirstOrDefault(r => r.id_compra == id);

            if (revision != null)
            {
                revision.respuesta = "Rechazado";
                revision.fecha_respuesta = DateTime.Now;

                var compra = _context.Compra.Find(id);
                if (compra != null)
                    compra.estado = "Rechazada";

                _context.SaveChanges();
            }

            return RedirectToAction("TransaccionesSospechosas");
        }

        // ➕ CREAR PRODUCTO (CONSISTENTE)
        public IActionResult CrearProducto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearProducto(Productos p)
        {
            _context.Productos.Add(p);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}