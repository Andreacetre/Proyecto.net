using Microsoft.EntityFrameworkCore;
using TeoSoft.Models;

namespace TeoSoft.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }
        public DbSet<DetalleVenta> DetallesVentas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<AutenticacionModel> Autenticaciones { get; set; }
        public object DetallesPedido { get; internal set; }
        public object Pedido { get; internal set; }
        public object Producto { get; internal set; }
        public object DetallePedido { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación entre Cliente y Ventas
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Cliente y Pedidos
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            // Nueva relación entre Venta y Producto
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Producto)
                .WithMany()
                .HasForeignKey(v => v.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Nueva relación entre Pedido y Producto
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Producto)
                .WithMany()
                .HasForeignKey(p => p.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Venta y DetalleVenta
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(dv => dv.Venta)
                .WithMany(v => v.DetalleVenta)
                .HasForeignKey(dv => dv.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Pedido y DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.DetallesPedido)
                .HasForeignKey(dp => dp.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre Producto y DetalleVenta
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(dv => dv.Producto)
                .WithMany(p => p.DetalleVentas)
                .HasForeignKey(dv => dv.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Producto y DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Producto)
                .WithMany(p => p.DetallePedidos)
                .HasForeignKey(dp => dp.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Producto y CategoriaProducto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.CategoriaProducto)
                .WithMany(cp => cp.Productos)
                .HasForeignKey(p => p.CategoriaProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Venta y Devolucion
            modelBuilder.Entity<Devolucion>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Devoluciones)
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Producto y Devolucion
            modelBuilder.Entity<Devolucion>()
                .HasOne(d => d.Producto)
                .WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configuración para AutenticacionModel
            modelBuilder.Entity<AutenticacionModel>()
                .HasIndex(a => a.CorreoElectronico)
                .IsUnique();

            modelBuilder.Entity<AutenticacionModel>()
                .Ignore(a => a.ConfirmarContrasena);
        }
    }
}

