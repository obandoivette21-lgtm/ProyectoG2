using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Servicios
{
    public class PedidoServicio
    {
        private readonly PedidoRepositorio _repositorio;
        private readonly ProductoRepositorio _productoRepositorio;

        public PedidoServicio(PedidoRepositorio repositorio, ProductoRepositorio productoRepositorio)
        {
            _repositorio = repositorio;
            _productoRepositorio = productoRepositorio;
        }

        public async Task<List<Pedido>> ObtenerPedidosAsync() => await _repositorio.ObtenerTodosAsync();

        public async Task<Pedido?> ObtenerPedidoPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task CrearPedidoAsync(Pedido pedido)
        {
            pedido.FechaPedido = System.DateTime.Now;
            pedido.Estado = "Pendiente";
            decimal total = 0;

            if (pedido.Detalles == null || pedido.Detalles.Count == 0)
            {
                throw new System.Exception("El pedido debe contener al menos un detalle de producto.");
            }

            foreach (var detalle in pedido.Detalles)
            {
                var producto = await _productoRepositorio.ObtenerPorIdAsync(detalle.ProductoId);
                if (producto == null)
                {
                    throw new System.Exception($"El producto con ID {detalle.ProductoId} no existe.");
                }
                detalle.PrecioUnitario = producto.Precio;
                total += detalle.Cantidad * detalle.PrecioUnitario;
            }

            pedido.Total = total;
            await _repositorio.AgregarAsync(pedido);
        }

        public async Task ActualizarPedidoAsync(Pedido pedido)
        {
            var existente = await _repositorio.ObtenerPorIdAsync(pedido.Id);
            if (existente == null)
            {
                throw new System.Exception("Pedido no encontrado.");
            }

            // Permitir actualizar el estado y la dirección
            existente.Estado = pedido.Estado;
            existente.DireccionEntrega = pedido.DireccionEntrega;

            await _repositorio.ActualizarAsync(existente);
        }

        public async Task EliminarPedidoAsync(int id)
        {
            var pedido = await _repositorio.ObtenerPorIdAsync(id);
            if (pedido == null)
            {
                throw new System.Exception("Pedido no encontrado.");
            }

            await _repositorio.EliminarAsync(pedido);
        }
    }
}
