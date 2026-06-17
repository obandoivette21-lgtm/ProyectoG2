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
        public async Task<Cliente?> ObtenerPorIdAsync(int id) => await _context.Clientes.FindAsync(id);
        public async Task<Cliente?> ObtenerPorCorreoAsync(string correo) => await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correo);
        public async Task AgregarAsync(Cliente cliente) { _context.Clientes.Add(cliente); await _context.SaveChangesAsync(); }
        public async Task ActualizarAsync(Cliente cliente) { _context.Clientes.Update(cliente); await _context.SaveChangesAsync(); }
        public async Task EliminarAsync(Cliente cliente) { _context.Clientes.Remove(cliente); await _context.SaveChangesAsync(); }
    }
}
