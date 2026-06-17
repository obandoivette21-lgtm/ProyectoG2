using Microsoft.AspNetCore.Mvc;
using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Servicios;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoControlador : ControllerBase
    {
        private readonly PedidoServicio _servicio;
        public PedidoControlador(PedidoServicio servicio) { _servicio = servicio; }

        [HttpPost("CrearPedido")]
        public async Task<IActionResult> CrearPedido([FromBody] Pedido pedido)
        {
            try
            {
                await _servicio.CrearPedidoAsync(pedido);
                return Ok(new { message = "Pedido creado correctamente", id = pedido.Id, total = pedido.Total });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPedidos() => Ok(await _servicio.ObtenerPedidosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var pedido = await _servicio.ObtenerPedidoPorIdAsync(id);
            if (pedido == null) return NotFound(new { message = "Pedido no encontrado" });
            return Ok(pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Pedido pedido)
        {
            try
            {
                pedido.Id = id;
                await _servicio.ActualizarPedidoAsync(pedido);
                return Ok(new { message = "Pedido actualizado correctamente" });
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
                await _servicio.EliminarPedidoAsync(id);
                return Ok(new { message = "Pedido eliminado correctamente" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
