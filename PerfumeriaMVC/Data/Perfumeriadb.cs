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

        // 🔥 IMPORTANTE: NOMBRES EN MAYÚSCULA
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Productos> Productos { get; set; }

        public DbSet<Compra> Compra { get; set; }

        public DbSet<DetalleCompra> DetalleCompra { get; set; }

        public DbSet<Revision> Revision { get; set; }
        public DbSet<EvaluacionFraude> EvaluacionFraude { get; set; }

         public DbSet<Descuento> Descuentos { get; set; }


    }
}