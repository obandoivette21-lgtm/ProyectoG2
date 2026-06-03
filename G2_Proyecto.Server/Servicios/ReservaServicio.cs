using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Servicios
{
    public class ReservaServicio
    {
        private readonly ReservaRepositorio _repositorio;
        public ReservaServicio(ReservaRepositorio repositorio) { _repositorio = repositorio; }
        public async Task<List<Reserva>> ObtenerReservasAsync() => await _repositorio.ObtenerTodasAsync();
        public async Task CrearReservaAsync(Reserva reserva) => await _repositorio.AgregarAsync(reserva);
    }
}
