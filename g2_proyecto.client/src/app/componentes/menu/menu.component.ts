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
  productosFiltrados: Producto[] = [];
  busqueda = '';
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
        this.productosFiltrados = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Error al cargar el menú. Por favor, intenta de nuevo.';
        this.cargando = false;
      }
    });
  }

  filtrarProductos(): void {
    const q = this.busqueda.toLowerCase().trim();
    if (!q) {
      this.productosFiltrados = this.productos;
    } else {
      this.productosFiltrados = this.productos.filter(p =>
        p.nombre.toLowerCase().includes(q) || p.descripcion.toLowerCase().includes(q)
      );
    }
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

  modificarCantidad(index: number, delta: number): void {
    const item = this.carrito[index];
    if (item) {
      item.cantidad += delta;
      if (item.cantidad <= 0) {
        this.carrito.splice(index, 1);
      }
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
        this.pedidoExito = '¡Pedido realizado con éxito! Puedes darle seguimiento en "Mis Pedidos".';
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
