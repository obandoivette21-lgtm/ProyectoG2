export interface Cliente {
  id?: number;
  nombre: string;
  correo: string;
  contrasena?: string;
}

export interface Producto {
  id?: number;
  nombre: string;
  descripcion: string;
  precio: number;
  imagen: string;
}

export interface Reserva {
  id?: number;
  fechaReserva: string;
  cantidadPersonas: number;
  clienteId: number;
  cliente?: Cliente;
}

export interface DetallePedido {
  id?: number;
  pedidoId?: number;
  productoId: number;
  producto?: Producto;
  cantidad: number;
  precioUnitario: number;
}

export interface Pedido {
  id?: number;
  fechaPedido?: string;
  total: number;
  estado: string;
  direccionEntrega: string;
  clienteId: number;
  cliente?: Cliente;
  detalles: DetallePedido[];
}
