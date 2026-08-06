using Microsoft.EntityFrameworkCore;
using G2_Proyecto.Server.Modelos;

namespace G2_Proyecto.Server.Datos
{
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

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nombre = "Cliente Demo", Correo = "cliente@saborexpress.com", Contrasena = "123456" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Hamburguesa Artesanal Sabor Express", Descripcion = "Carne de res de 180g a la parrilla, queso cheddar fundido, tocineta crocante, lechuga y salsa de la casa.", Precio = 6.50m, Imagen = "hamburguesa.jpg" },
                new Producto { Id = 2, Nombre = "Pizza Pepperoni Supreme (8 rebanadas)", Descripcion = "Masa madre artesanal, abundante queso mozzarella, pepperoni italiano y orégano fresco.", Precio = 12.50m, Imagen = "pizza.jpg" },
                new Producto { Id = 3, Nombre = "Pasta Alfredo con Pollo y Champiñones", Descripcion = "Fettuccine en crema Alfredo casera, pechuga a la plancha, champiñones frescos y queso parmesano.", Precio = 9.00m, Imagen = "pasta.jpg" },
                new Producto { Id = 4, Nombre = "Papas Rústicas sazonadas", Descripcion = "Papas rústicas doradas con finas hierbas, sal marina y dip de mayonesa de ajo casera.", Precio = 3.00m, Imagen = "papas.jpg" },
                new Producto { Id = 5, Nombre = "Tarta de Tres Leches Tradicional", Descripcion = "Bizcocho esponjoso bañado en tres leches, decorado con canela e hilos de caramelo.", Precio = 3.75m, Imagen = "tres_leches.jpg" },
                new Producto { Id = 6, Nombre = "Limonada Natural con Menta", Descripcion = "Bebida artesanal súper refrescante elaborada con limones frescos y menta de nuestro huerto.", Precio = 2.00m, Imagen = "refresco.jpg" },
                new Producto { Id = 7, Nombre = "Tacos al Pastor Especiales (3 uds)", Descripcion = "Tortillas de maíz hechas a mano, carne de cerdo marinada, piña asada, cilantro y cebolla.", Precio = 7.00m, Imagen = "tacos.jpg" },
                new Producto { Id = 8, Nombre = "Ensalada César con Pollo a la Parrilla", Descripcion = "Mezcla de lechugas frescas, crotones crujientes, aderezo César cremoso y lonjas de pollo.", Precio = 7.50m, Imagen = "ensalada.jpg" }
            );
        }
    }
}
