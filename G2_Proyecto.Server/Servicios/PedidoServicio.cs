using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Servicios
{
    public class PedidoServicio
    {
        private readonly PedidoRepositorio _repositorio;
        public PedidoServicio(PedidoRepositorio repositorio) { _repositorio = repositorio; }
        public async Task<List<Pedido>> ObtenerPedidosAsync() => await _repositorio.ObtenerTodosAsync();
        public async Task CrearPedidoAsync(Pedido pedido) => await _repositorio.AgregarAsync(pedido);
    }
}
