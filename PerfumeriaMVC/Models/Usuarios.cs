using System.ComponentModel.DataAnnotations;

namespace PerfumeriaMVC.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        public string nombre { get; set; }

        public string correo { get; set; }

        public string password { get; set; }

        public string estado { get; set; }

        public int intentos_fallidos { get; set; }

        public DateTime fecha_registro { get; set; }
    }
}