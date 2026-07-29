using System;
using System.Collections.Generic;

namespace G2_Proyecto.Server.Modelos
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string DireccionEntrega { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new();
    }
}
