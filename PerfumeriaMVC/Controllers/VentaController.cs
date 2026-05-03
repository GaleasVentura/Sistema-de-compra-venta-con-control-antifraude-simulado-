using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Data;
using PerfumeriaMVC.Models;

namespace PerfumeriaMVC.Controllers
{
    public class VentaController : Controller
    {
        private readonly Perfumeriadb _context;

        public VentaController(Perfumeriadb context)
        {
            _context = context;
        }

        // lista de productos para comprar 
        public IActionResult Index()
        {
            var productos = _context.productos.ToList();
            return View(productos);
        }
        // metodo para la compra
        public IActionResult Comprar(int id)
        {
          var productos = _context.productos.Find(id);

          if (productos == null)
          return NotFound();

          return View(productos);
        }
        
        [HttpPost]
public IActionResult ConfirmarCompra(int id_productos, int cantidad)
{
    var productos = _context.productos.Find(id_productos);

    if (productos == null)
        return NotFound();

    // 1. Crear compra
    var compra = new Compra
    {
        id_usuario = 1, // temporal (luego login)
        fecha_compra = DateTime.Now,
        total = productos.precio * cantidad,
        estado = "PENDIENTE"
    };

    _context.compra.Add(compra);
    _context.SaveChanges();

    // 2. Crear detalle
    var detalle = new DetalleCompra
    {
        id_compra = compra.id_compra,
        id_productos = productos.id_producto,
        cantidad = cantidad,
        precio = productos.precio,
        subtotal = productos.precio * cantidad
    };

    _context.detalle_compra.Add(detalle);

    // 3. Descontar stock
    productos.stock -= cantidad;

    _context.SaveChanges();

    return RedirectToAction("Index", "productos");
}
    }
}