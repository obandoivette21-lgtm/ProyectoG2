import { Component, OnInit } from '@angular/core';
import { ReservaService } from '../../servicios/reserva.service';
import { ClienteService } from '../../servicios/cliente.service';
import { Reserva } from '../../modelos/modelos';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  standalone: false
})
export class ReservasComponent implements OnInit {
  fechaReserva = '';
  cantidadPersonas = 2;
  error = '';
  exito = '';
  estaAutenticado = false;
  misReservas: Reserva[] = [];
  cargandoReservas = false;

  constructor(
    private reservaService: ReservaService,
    public clienteService: ClienteService
  ) {}

  ngOnInit(): void {
    this.estaAutenticado = this.clienteService.estaAutenticado();
    if (this.estaAutenticado) {
      this.cargarMisReservas();
    }
  }

  cargarMisReservas(): void {
    const usuario = this.clienteService.usuarioActual();
    if (usuario && usuario.id) {
      this.cargandoReservas = true;
      this.reservaService.obtenerReservasPorCliente(usuario.id).subscribe({
        next: (data) => {
          this.misReservas = data;
          this.cargandoReservas = false;
        },
        error: (err) => {
          console.error(err);
          this.cargandoReservas = false;
        }
      });
    }
  }

  onSubmit(): void {
    if (!this.fechaReserva || !this.cantidadPersonas) {
      this.error = 'Por favor, completa todos los campos.';
      return;
    }

    const usuario = this.clienteService.usuarioActual();
    if (!usuario || !usuario.id) {
      this.error = 'Debes iniciar sesión para agendar una reserva.';
      return;
    }

    const nuevaReserva = {
      fechaReserva: this.fechaReserva,
      cantidadPersonas: this.cantidadPersonas,
      clienteId: usuario.id
    };

    this.reservaService.crearReserva(nuevaReserva).subscribe({
      next: () => {
        this.exito = '¡Reserva creada exitosamente! Te esperamos.';
        this.error = '';
        this.fechaReserva = '';
        this.cantidadPersonas = 2;
        this.cargarMisReservas();
      },
      error: (err) => {
        console.error(err);
        this.error = err.error?.message || 'Error al crear la reserva.';
      }
    });
  }

  cancelarReserva(id: number): void {
    if (confirm('¿Deseas cancelar esta reserva?')) {
      this.reservaService.eliminarReserva(id).subscribe({
        next: () => {
          this.exito = 'Reserva cancelada con éxito.';
          this.cargarMisReservas();
        },
        error: (err) => console.error(err)
      });
    }
  }
}
