using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace G2_Proyecto.Server.Repositorios
{
    public class ReservaRepositorio
    {
        private readonly ContextoBaseDatos _context;
        public ReservaRepositorio(ContextoBaseDatos context) { _context = context; }
        
        public async Task<List<Reserva>> ObtenerTodasAsync() => await _context.Reservas.Include(r => r.Cliente).ToListAsync();
        public async Task<Reserva?> ObtenerPorIdAsync(int id) => await _context.Reservas.Include(r => r.Cliente).FirstOrDefaultAsync(r => r.Id == id);
        public async Task AgregarAsync(Reserva reserva) { _context.Reservas.Add(reserva); await _context.SaveChangesAsync(); }
        public async Task ActualizarAsync(Reserva reserva) { _context.Reservas.Update(reserva); await _context.SaveChangesAsync(); }
        public async Task EliminarAsync(Reserva reserva) { _context.Reservas.Remove(reserva); await _context.SaveChangesAsync(); }
    }
}
