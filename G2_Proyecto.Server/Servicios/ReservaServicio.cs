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
        
        public async Task<Reserva?> ObtenerReservaPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task CrearReservaAsync(Reserva reserva)
        {
            if (reserva.FechaReserva <= System.DateTime.Now)
            {
                throw new System.Exception("La fecha de la reserva debe ser en el futuro.");
            }
            if (reserva.CantidadPersonas <= 0)
            {
                throw new System.Exception("La cantidad de personas debe ser mayor a cero.");
            }
            if (reserva.ClienteId <= 0)
            {
                throw new System.Exception("Debe especificar un Cliente válido.");
            }

            await _repositorio.AgregarAsync(reserva);
        }

        public async Task ActualizarReservaAsync(Reserva reserva)
        {
            var existente = await _repositorio.ObtenerPorIdAsync(reserva.Id);
            if (existente == null)
            {
                throw new System.Exception("Reserva no encontrada.");
            }

            if (reserva.FechaReserva <= System.DateTime.Now)
            {
                throw new System.Exception("La fecha de la reserva debe ser en el futuro.");
            }
            if (reserva.CantidadPersonas <= 0)
            {
                throw new System.Exception("La cantidad de personas debe ser mayor a cero.");
            }

            existente.FechaReserva = reserva.FechaReserva;
            existente.CantidadPersonas = reserva.CantidadPersonas;

            await _repositorio.ActualizarAsync(existente);
        }

        public async Task EliminarReservaAsync(int id)
        {
            var reserva = await _repositorio.ObtenerPorIdAsync(id);
            if (reserva == null)
            {
                throw new System.Exception("Reserva no encontrada.");
            }

            await _repositorio.EliminarAsync(reserva);
        }
    }
}
