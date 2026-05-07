using Microsoft.AspNetCore.Mvc;
using PerfumeriaMVC.Models;
using PerfumeriaMVC.Helpers;
using System.Collections.Generic;
using System.Linq;
using PerfumeriaMVC.Data;

namespace PerfumeriaMVC.Controllers
{
    public class CarritoController :Controller
    {
        private readonly Perfumeriadb _context;

        public CarritoController(Perfumeriadb context)
        {
            _context = context;
        }

        // Obtener carrito desde sesión
        private List<CarritoItem> ObtenerCarrito()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("carrito");
            if (carrito == null)
            {
                carrito = new List<CarritoItem>();
            }
            return carrito;
        }

        // Guardar carrito
        private void GuardarCarrito(List<CarritoItem> carrito)
        {
            HttpContext.Session.SetObjectAsJson("carrito", carrito);
        }

        // AGREGAR
        public IActionResult Agregar(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.id_producto == id);

            if (producto == null)
                return RedirectToAction("Index", "Producto");

            var carrito = ObtenerCarrito();

            var item = carrito.FirstOrDefault(p => p.ProductoId == id);

            if (item == null)
            {
                carrito.Add(new CarritoItem
                {
                    ProductoId = producto.id_producto,
                    Nombre = producto.nombre,
                    Precio = producto.precio,
                    Cantidad = 1
                });
            }
            else
            {
                item.Cantidad++;
            }

            GuardarCarrito(carrito);

            return RedirectToAction("Ver");
        }

        // VER CARRITO
        public IActionResult Ver()
        {
            var carrito = ObtenerCarrito();
            return View(carrito);
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            var carrito = ObtenerCarrito();

            var item = carrito.FirstOrDefault(p => p.ProductoId == id);

            if (item != null)
            {
                carrito.Remove(item);
            }

            GuardarCarrito(carrito);

            return RedirectToAction("Ver");
        }
    }
}