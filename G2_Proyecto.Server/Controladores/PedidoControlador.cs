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
            await _servicio.CrearPedidoAsync(pedido);
            return Ok(new { message = "Pedido creado" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPedidos() => Ok(await _servicio.ObtenerPedidosAsync());
    }
}
