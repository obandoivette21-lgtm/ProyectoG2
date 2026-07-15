import { Component, OnInit } from '@angular/core';
import { ProductoService } from '../../servicios/producto.service';
import { PedidoService } from '../../servicios/pedido.service';
import { ClienteService } from '../../servicios/cliente.service';
import { Producto, Pedido, DetallePedido } from '../../modelos/modelos';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  standalone: false
})
export class MenuComponent implements OnInit {
  productos: Producto[] = [];
  cargando = true;
  error = '';
  
  carrito: { producto: Producto; cantidad: number }[] = [];
  direccionEntrega = '';
  pedidoExito = '';
  pedidoError = '';
  enviandoPedido = false;

  constructor(
    private productoService: ProductoService,
    private pedidoService: PedidoService,
    public clienteService: ClienteService
  ) {}

  ngOnInit(): void {
    this.productoService.obtenerProductos().subscribe({
      next: (data) => {
        this.productos = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Error al cargar el menú. Por favor, intenta de nuevo.';
        this.cargando = false;
      }
    });
  }

  agregarAlCarrito(producto: Producto): void {
    this.pedidoExito = '';
    this.pedidoError = '';
    const item = this.carrito.find(i => i.producto.id === producto.id);
    if (item) {
      item.cantidad++;
    } else {
      this.carrito.push({ producto, cantidad: 1 });
    }
  }

  eliminarDelCarrito(index: number): void {
    this.carrito.splice(index, 1);
  }

  obtenerTotal(): number {
    return this.carrito.reduce((sum, item) => sum + (item.producto.precio * item.cantidad), 0);
  }

  confirmarPedido(): void {
    if (!this.clienteService.estaAutenticado()) {
      this.pedidoError = 'Debes iniciar sesión para realizar un pedido.';
      return;
    }

    if (!this.direccionEntrega.trim()) {
      this.pedidoError = 'Por favor, ingresa una dirección de entrega.';
      return;
    }

    if (this.carrito.length === 0) {
      this.pedidoError = 'El carrito está vacío.';
      return;
    }

    const usuario = this.clienteService.usuarioActual();
    if (!usuario || !usuario.id) {
      this.pedidoError = 'Error al obtener los datos del usuario.';
      return;
    }

    this.enviandoPedido = true;
    this.pedidoError = '';
    this.pedidoExito = '';

    const detalles: DetallePedido[] = this.carrito.map(item => ({
      productoId: item.producto.id!,
      cantidad: item.cantidad,
      precioUnitario: item.producto.precio
    }));

    const nuevoPedido: Pedido = {
      clienteId: usuario.id,
      direccionEntrega: this.direccionEntrega,
      total: this.obtenerTotal(),
      estado: 'Pendiente',
      detalles: detalles
    };

    this.pedidoService.crearPedido(nuevoPedido).subscribe({
      next: () => {
        this.pedidoExito = '¡Pedido realizado con éxito!';
        this.carrito = [];
        this.direccionEntrega = '';
        this.enviandoPedido = false;
      },
      error: (err) => {
        console.error(err);
        this.pedidoError = err.error?.message || 'Error al procesar el pedido.';
        this.enviandoPedido = false;
      }
    });
  }
}

