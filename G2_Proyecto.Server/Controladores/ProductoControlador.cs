using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> ObtenerProducto(int id) => Ok(await _servicio.ObtenerProductoPorIdAsync(id));
    }
}
