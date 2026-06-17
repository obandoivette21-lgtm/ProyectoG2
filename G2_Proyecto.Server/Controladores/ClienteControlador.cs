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
            try
            {
                await _servicio.RegistrarClienteAsync(cliente);
                return Ok(new { message = "Cliente registrado correctamente" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var cliente = await _servicio.IniciarSesionAsync(request.Correo, request.Contrasena);
            if (cliente == null)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos" });
            }
            return Ok(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerClientesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _servicio.ObtenerClientePorIdAsync(id);
            if (cliente == null) return NotFound(new { message = "Cliente no encontrado" });
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Cliente cliente)
        {
            try
            {
                cliente.Id = id;
                await _servicio.ActualizarClienteAsync(cliente);
                return Ok(new { message = "Cliente actualizado correctamente" });
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
                await _servicio.EliminarClienteAsync(id);
                return Ok(new { message = "Cliente eliminado correctamente" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
