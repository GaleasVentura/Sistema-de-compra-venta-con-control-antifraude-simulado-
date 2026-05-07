using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class Compra
    {
        [Key]
        public int id_compra { get; set; }

        public int id_usuario { get; set; }

        public DateTime fecha_compra { get; set; }

        public decimal total { get; set; }

        public required string estado { get; set; }

    }
}