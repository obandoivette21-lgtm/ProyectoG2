using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace G2_Proyecto.Server.Repositorios
{
    public class PedidoRepositorio
    {
        private readonly ContextoBaseDatos _context;
        public PedidoRepositorio(ContextoBaseDatos context) { _context = context; }
        
        public async Task<List<Pedido>> ObtenerTodosAsync() => await _context.Pedidos.ToListAsync();
        public async Task AgregarAsync(Pedido pedido) { _context.Pedidos.Add(pedido); await _context.SaveChangesAsync(); }
    }
}
