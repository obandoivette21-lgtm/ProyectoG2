using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace G2_Proyecto.Server.Repositorios
{
    // Maneja las operaciones de base de datos para Clientes
    public class ClienteRepositorio
    {
        private readonly ContextoBaseDatos _context;
        public ClienteRepositorio(ContextoBaseDatos context) { _context = context; }
        
        public async Task<List<Cliente>> ObtenerTodosAsync() => await _context.Clientes.ToListAsync();
        public async Task AgregarAsync(Cliente cliente) { _context.Clientes.Add(cliente); await _context.SaveChangesAsync(); }
    }
}
