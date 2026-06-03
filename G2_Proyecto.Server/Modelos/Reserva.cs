using System;

namespace G2_Proyecto.Server.Modelos
{
    // Representa una reserva de mesa realizada por un cliente
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadPersonas { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
