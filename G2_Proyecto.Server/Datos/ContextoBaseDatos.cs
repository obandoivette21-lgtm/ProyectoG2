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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Datos semilla para Productos
            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Hamburguesa Sabor Express", Descripcion = "Deliciosa hamburguesa con carne de res de 150g, queso cheddar, lechuga y salsa especial.", Precio = 5.50m, Imagen = "hamburguesa.jpg" },
                new Producto { Id = 2, Nombre = "Pizza Pepperoni Grande", Descripcion = "Pizza artesanal con salsa de tomate de la casa, abundante queso mozzarella y pepperoni.", Precio = 12.00m, Imagen = "pizza.jpg" },
                new Producto { Id = 3, Nombre = "Pasta Alfredo con Pollo", Descripcion = "Fettuccine en salsa cremosa Alfredo con pechuga de pollo a la plancha y parmesano.", Precio = 8.50m, Imagen = "pasta.jpg" },
                new Producto { Id = 4, Nombre = "Papas Fritas Crujientes", Descripcion = "Papas fritas sazonadas con sal y un toque de pimentón, ideales para acompañar.", Precio = 2.50m, Imagen = "papas.jpg" },
                new Producto { Id = 5, Nombre = "Tarta de Tres Leches", Descripcion = "Postre tradicional húmedo con crema chantilly y canela espolvoreada.", Precio = 3.50m, Imagen = "tres_leches.jpg" },
                new Producto { Id = 6, Nombre = "Refresco de la Casa 500ml", Descripcion = "Bebida fría refrescante de sabores variados.", Precio = 1.50m, Imagen = "refresco.jpg" }
            );
        }
    }
}
