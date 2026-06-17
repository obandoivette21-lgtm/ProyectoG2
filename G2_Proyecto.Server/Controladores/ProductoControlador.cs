using Microsoft.AspNetCore.Mvc;
using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Servicios;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoControlador : ControllerBase
    {
        private readonly ProductoServicio _servicio;
        public ProductoControlador(ProductoServicio servicio) { _servicio = servicio; }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos() => Ok(await _servicio.ObtenerProductosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var producto = await _servicio.ObtenerProductoPorIdAsync(id);
            if (producto == null) return NotFound(new { message = "Producto no encontrado" });
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Producto producto)
        {
            try
            {
                await _servicio.RegistrarProductoAsync(producto);
                return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.Id }, producto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Producto producto)
        {
            try
            {
                producto.Id = id;
                await _servicio.ActualizarProductoAsync(producto);
                return Ok(new { message = "Producto actualizado correctamente" });
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
                await _servicio.EliminarProductoAsync(id);
                return Ok(new { message = "Producto eliminado correctamente" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
