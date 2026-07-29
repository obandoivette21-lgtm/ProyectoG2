import { Component, OnInit } from '@angular/core';
import { PedidoService } from '../../servicios/pedido.service';
import { ClienteService } from '../../servicios/cliente.service';
import { Pedido } from '../../modelos/modelos';

@Component({
  selector: 'app-mis-pedidos',
  templateUrl: './mis-pedidos.component.html',
  standalone: false
})
export class MisPedidosComponent implements OnInit {
  pedidos: Pedido[] = [];
  cargando = true;
  error = '';

  constructor(
    private pedidoService: PedidoService,
    public clienteService: ClienteService
  ) {}

  ngOnInit(): void {
    this.cargarPedidos();
  }

  cargarPedidos(): void {
    const usuario = this.clienteService.usuarioActual();
    if (!usuario || !usuario.id) {
      this.error = 'Debes iniciar sesión para consultar tus pedidos.';
      this.cargando = false;
      return;
    }

    this.pedidoService.obtenerPedidosPorCliente(usuario.id).subscribe({
      next: (data) => {
        this.pedidos = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Error al obtener tus pedidos.';
        this.cargando = false;
      }
    });
  }
}
