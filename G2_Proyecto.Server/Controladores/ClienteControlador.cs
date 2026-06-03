using Microsoft.AspNetCore.Mvc;
using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Servicios;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Controladores
{
    // Controlador REST para manejar las peticiones HTTP de los clientes
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteControlador : ControllerBase
    {
        private readonly ClienteServicio _servicio;
        public ClienteControlador(ClienteServicio servicio) { _servicio = servicio; }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Cliente cliente)
        {
            await _servicio.RegistrarClienteAsync(cliente);
            return Ok(new { message = "Cliente registrado correctamente" });
        }
    }
}
