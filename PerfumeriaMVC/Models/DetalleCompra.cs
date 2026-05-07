using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class DetalleCompra
    {
        [Key]
        public int id_detalle { get; set; }

        public int id_compra { get; set; }

        public int id_productos { get; set; }

        public int cantidad { get; set; }

        public decimal precio { get; set; }

        public decimal subtotal { get; set; }
    }
}