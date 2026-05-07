using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class Descuento
    {
        [Key]
        public int id_descuento { get; set; }

        [Required]
        public string nombre { get; set; } = "";

        public decimal porcentaje { get; set; }

        public bool activo { get; set; }
    }
}