
namespace PerfumeriaMVC.Models
{
    public class TransaccionSospechosaViewModel
    {
        public int id_compra { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_compra { get; set; }
        public decimal total { get; set; }
        public string? resultado { get; set; }
        public string? motivo { get; set; }
    }
}