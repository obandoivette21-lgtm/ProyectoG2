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
        
        public async Task<Cliente?> ObtenerClientePorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task RegistrarClienteAsync(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Correo) || string.IsNullOrWhiteSpace(cliente.Contrasena))
            {
                throw new System.Exception("Todos los campos (Nombre, Correo, Contraseña) son obligatorios.");
            }

            var existe = await _repositorio.ObtenerPorCorreoAsync(cliente.Correo);
            if (existe != null)
            {
                throw new System.Exception("El correo ya se encuentra registrado.");
            }

            await _repositorio.AgregarAsync(cliente);
        }

        public async Task ActualizarClienteAsync(Cliente cliente)
        {
            var existente = await _repositorio.ObtenerPorIdAsync(cliente.Id);
            if (existente == null)
            {
                throw new System.Exception("Cliente no encontrado.");
            }

            existente.Nombre = cliente.Nombre;
            existente.Correo = cliente.Correo;
            if (!string.IsNullOrWhiteSpace(cliente.Contrasena))
            {
                existente.Contrasena = cliente.Contrasena;
            }

            await _repositorio.ActualizarAsync(existente);
        }

        public async Task EliminarClienteAsync(int id)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                throw new System.Exception("Cliente no encontrado.");
            }

            await _repositorio.EliminarAsync(cliente);
        }

        public async Task<Cliente?> IniciarSesionAsync(string correo, string contrasena)
        {
            var cliente = await _repositorio.ObtenerPorCorreoAsync(correo);
            if (cliente == null || cliente.Contrasena != contrasena)
            {
                return null;
            }
            return cliente;
        }
    }
}
