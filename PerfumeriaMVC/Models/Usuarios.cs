using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        public string nombre { get; set; } = "";

        public string correo { get; set; } = "";

        public string password { get; set; } = "";

        public int intentos_fallidos { get; set; } = 0;

        public DateTime fecha_registro { get; set; } = DateTime.Now;

        public string? rol { get; set; }
    }
}