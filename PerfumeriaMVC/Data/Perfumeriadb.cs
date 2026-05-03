using Microsoft.EntityFrameworkCore;
using PerfumeriaMVC.Models;

namespace PerfumeriaMVC.Data
{
    public class Perfumeriadb : DbContext
    {
        public Perfumeriadb(DbContextOptions<Perfumeriadb> options)
            : base(options)
        {
        }

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Productos> productos { get; set; }
        public DbSet<Compra> compra { get; set; }
        public DbSet<DetalleCompra> detalle_compra { get; set; }
    }
}