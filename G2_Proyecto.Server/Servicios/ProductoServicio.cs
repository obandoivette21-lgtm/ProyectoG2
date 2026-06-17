using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Servicios
{
    public class ProductoServicio
    {
        private readonly ProductoRepositorio _repositorio;
        public ProductoServicio(ProductoRepositorio repositorio) { _repositorio = repositorio; }
        public async Task<List<Producto>> ObtenerProductosAsync() => await _repositorio.ObtenerTodosAsync();
        public async Task<Producto?> ObtenerProductoPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task RegistrarProductoAsync(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                throw new System.Exception("El nombre del producto es obligatorio.");
            }
            if (producto.Precio <= 0)
            {
                throw new System.Exception("El precio del producto debe ser mayor a cero.");
            }

            await _repositorio.AgregarAsync(producto);
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            var existente = await _repositorio.ObtenerPorIdAsync(producto.Id);
            if (existente == null)
            {
                throw new System.Exception("Producto no encontrado.");
            }

            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Precio = producto.Precio;
            existente.Imagen = producto.Imagen;

            await _repositorio.ActualizarAsync(existente);
        }

        public async Task EliminarProductoAsync(int id)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);
            if (producto == null)
            {
                throw new System.Exception("Producto no encontrado.");
            }

            await _repositorio.EliminarAsync(producto);
        }
    }
}
