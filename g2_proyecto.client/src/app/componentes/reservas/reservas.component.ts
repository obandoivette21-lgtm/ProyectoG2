import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservaService } from '../../servicios/reserva.service';
import { ClienteService } from '../../servicios/cliente.service';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  standalone: false
})
export class ReservasComponent implements OnInit {
  fechaReserva = '';
  cantidadPersonas = 1;
  error = '';
  exito = '';
  estaAutenticado = false;

  constructor(
    private reservaService: ReservaService,
    public clienteService: ClienteService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.estaAutenticado = this.clienteService.estaAutenticado();
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
        this.cantidadPersonas = 1;
      },
      error: (err) => {
        console.error(err);
        this.error = err.error?.message || 'Error al crear la reserva.';
      }
    });
  }
}
