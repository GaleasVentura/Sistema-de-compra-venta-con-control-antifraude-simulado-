using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class Revision
    {
        [Key]
        public int id_revision { get; set; }

        public int id_compra { get; set; }

        public int id_admin { get; set; }

        public string? respuesta { get; set; }

        public DateTime? fecha_respuesta { get; set; }

        public string? comentarios { get; set; }
    }
}