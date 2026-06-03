using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace G2_Proyecto.Server.Repositorios
{
    public class ProductoRepositorio
    {
        private readonly ContextoBaseDatos _context;
        public ProductoRepositorio(ContextoBaseDatos context) { _context = context; }
        
        public async Task<List<Producto>> ObtenerTodosAsync() => await _context.Productos.ToListAsync();
        public async Task<Producto> ObtenerPorIdAsync(int id) => await _context.Productos.FindAsync(id);
    }
}
