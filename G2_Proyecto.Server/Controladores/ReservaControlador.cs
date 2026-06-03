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
            await _servicio.CrearReservaAsync(reserva);
            return Ok(new { message = "Reserva creada" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerReservas() => Ok(await _servicio.ObtenerReservasAsync());
    }
}
