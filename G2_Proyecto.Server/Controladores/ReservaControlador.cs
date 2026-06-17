using Microsoft.AspNetCore.Mvc;
using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Servicios;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaControlador : ControllerBase
    {
        private readonly ReservaServicio _servicio;
        public ReservaControlador(ReservaServicio servicio) { _servicio = servicio; }

        [HttpPost("CrearReserva")]
        public async Task<IActionResult> CrearReserva([FromBody] Reserva reserva)
        {
            try
            {
                await _servicio.CrearReservaAsync(reserva);
                return Ok(new { message = "Reserva creada correctamente", id = reserva.Id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerReservas() => Ok(await _servicio.ObtenerReservasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var reserva = await _servicio.ObtenerReservaPorIdAsync(id);
            if (reserva == null) return NotFound(new { message = "Reserva no encontrada" });
            return Ok(reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Reserva reserva)
        {
            try
            {
                reserva.Id = id;
                await _servicio.ActualizarReservaAsync(reserva);
                return Ok(new { message = "Reserva actualizada correctamente" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _servicio.EliminarReservaAsync(id);
                return Ok(new { message = "Reserva eliminada correctamente" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
