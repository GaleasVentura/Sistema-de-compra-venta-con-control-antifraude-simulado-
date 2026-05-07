
namespace PerfumeriaMVC.Models
{
    public class DetalleTransaccionViewModel
    {
        public int id_detalle { get; set; }

        public string? producto { get; set; }

        public int cantidad { get; set; }

        public decimal precio { get; set; }

        public decimal subtotal { get; set; }

         public required string estado { get; set; }
    }
}