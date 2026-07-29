import { Component, OnInit } from '@angular/core';
import { ProductoService } from '../../servicios/producto.service';
import { PedidoService } from '../../servicios/pedido.service';
import { ReservaService } from '../../servicios/reserva.service';
import { Producto, Pedido, Reserva } from '../../modelos/modelos';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  standalone: false
})
export class AdminComponent implements OnInit {
  tabActiva: 'productos' | 'pedidos' | 'reservas' = 'productos';
  
  productos: Producto[] = [];
  pedidos: Pedido[] = [];
  reservas: Reserva[] = [];
  
  cargando = false;
  mensajeExito = '';
  mensajeError = '';

  productoForm: Producto = {
    id: 0,
    nombre: '',
    descripcion: '',
    precio: 0,
    imagen: ''
  };
  editandoProducto = false;

  constructor(
    private productoService: ProductoService,
    private pedidoService: PedidoService,
    private reservaService: ReservaService
  ) {}

  ngOnInit(): void {
    this.cargarDatos();
  }

  setTab(tab: 'productos' | 'pedidos' | 'reservas'): void {
    this.tabActiva = tab;
    this.limpiarMensajes();
  }

  limpiarMensajes(): void {
    this.mensajeExito = '';
    this.mensajeError = '';
  }

  cargarDatos(): void {
    this.cargando = true;
    this.productoService.obtenerProductos().subscribe({
      next: (prods) => {
        this.productos = prods;
        this.cargando = false;
      },
      error: (err) => console.error(err)
    });

    this.pedidoService.obtenerPedidos().subscribe({
      next: (peds) => this.pedidos = peds,
      error: (err) => console.error(err)
    });

    this.reservaService.obtenerReservas().subscribe({
      next: (res) => this.reservas = res,
      error: (err) => console.error(err)
    });
  }

  guardarProducto(): void {
    this.limpiarMensajes();
    if (!this.productoForm.nombre || this.productoForm.precio <= 0) {
      this.mensajeError = 'Por favor, ingresa un nombre y precio válidos.';
      return;
    }

    if (this.editandoProducto && this.productoForm.id) {
      this.productoService.actualizarProducto(this.productoForm.id, this.productoForm).subscribe({
        next: () => {
          this.mensajeExito = 'Producto actualizado correctamente.';
          this.resetProductoForm();
          this.cargarDatos();
        },
        error: (err) => this.mensajeError = err.error?.message || 'Error al actualizar el producto.'
      });
    } else {
      const nuevo: Producto = {
        nombre: this.productoForm.nombre,
        descripcion: this.productoForm.descripcion || '',
        precio: this.productoForm.precio,
        imagen: this.productoForm.imagen || 'plato.jpg'
      };
      this.productoService.crearProducto(nuevo).subscribe({
        next: () => {
          this.mensajeExito = 'Producto creado correctamente.';
          this.resetProductoForm();
          this.cargarDatos();
        },
        error: (err) => this.mensajeError = err.error?.message || 'Error al crear el producto.'
      });
    }
  }

  editarProducto(prod: Producto): void {
    this.productoForm = { ...prod };
    this.editandoProducto = true;
    this.limpiarMensajes();
  }

  resetProductoForm(): void {
    this.productoForm = { id: 0, nombre: '', descripcion: '', precio: 0, imagen: '' };
    this.editandoProducto = false;
  }

  eliminarProducto(id: number): void {
    if (confirm('¿Estás seguro de eliminar este producto?')) {
      this.limpiarMensajes();
      this.productoService.eliminarProducto(id).subscribe({
        next: () => {
          this.mensajeExito = 'Producto eliminado.';
          this.cargarDatos();
        },
        error: (err) => this.mensajeError = err.error?.message || 'Error al eliminar producto.'
      });
    }
  }

  cambiarEstadoPedido(pedido: Pedido, nuevoEstado: string): void {
    this.limpiarMensajes();
    const pedidoActualizado: Pedido = { ...pedido, estado: nuevoEstado };
    this.pedidoService.actualizarPedido(pedido.id!, pedidoActualizado).subscribe({
      next: () => {
        pedido.estado = nuevoEstado;
        this.mensajeExito = `Estado del pedido #${pedido.id} actualizado a "${nuevoEstado}".`;
      },
      error: (err) => this.mensajeError = err.error?.message || 'Error al cambiar estado.'
    });
  }

  eliminarReserva(id: number): void {
    if (confirm('¿Estás seguro de cancelar/eliminar esta reserva?')) {
      this.limpiarMensajes();
      this.reservaService.eliminarReserva(id).subscribe({
        next: () => {
          this.mensajeExito = 'Reserva eliminada correctamente.';
          this.cargarDatos();
        },
        error: (err) => this.mensajeError = err.error?.message || 'Error al eliminar reserva.'
      });
    }
  }
}
