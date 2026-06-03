namespace G2_Proyecto.Server.Modelos
{
    // Representa un producto ofrecido en el menú
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
    }
}
