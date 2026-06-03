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
        public async Task<Producto> ObtenerProductoPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);
    }
}
