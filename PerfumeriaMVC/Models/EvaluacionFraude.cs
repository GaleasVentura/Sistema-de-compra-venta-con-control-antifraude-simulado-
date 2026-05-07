using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeriaMVC.Models
{
    [Table("evaluacion_fraude")] // 👈 CLAVE
    public class EvaluacionFraude
    {
        [Key]
        public int id_evaluacion { get; set; }

        public int id_compra { get; set; }

        public string? resultado { get; set; }

        public string? motivo { get; set; }

        public DateTime fecha_evaluacion { get; set; }
    }
}