using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class Productos
    {
        [Key]
        public int id_producto { get; set; }

        public string? nombre { get; set; }

        public string? marca { get; set; }

        public decimal precio { get; set; }

        public int stock { get; set; }

        public bool? tiene_oferta { get; set; }
        public string? imagen_url { get; set; }

         public int? id_descuento { get; set; }

    }
}