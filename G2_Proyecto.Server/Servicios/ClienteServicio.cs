using G2_Proyecto.Server.Modelos;
using G2_Proyecto.Server.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G2_Proyecto.Server.Servicios
{
    // Contiene la lógica de negocio relacionada con los Clientes
    public class ClienteServicio
    {
        private readonly ClienteRepositorio _repositorio;
        public ClienteServicio(ClienteRepositorio repositorio) { _repositorio = repositorio; }
        public async Task<List<Cliente>> ObtenerClientesAsync() => await _repositorio.ObtenerTodosAsync();
        public async Task RegistrarClienteAsync(Cliente cliente) => await _repositorio.AgregarAsync(cliente);
    }
}
