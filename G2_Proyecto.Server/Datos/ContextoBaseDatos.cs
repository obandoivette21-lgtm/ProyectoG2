using Microsoft.EntityFrameworkCore;
using G2_Proyecto.Server.Modelos;

namespace G2_Proyecto.Server.Datos
{
    // Contexto principal de la base de datos para Entity Framework
    public class ContextoBaseDatos : DbContext
    {
        public ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }
    }
}
