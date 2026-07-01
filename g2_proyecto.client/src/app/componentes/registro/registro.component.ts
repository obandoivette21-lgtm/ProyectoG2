import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../servicios/cliente.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  standalone: false
})
export class RegistroComponent {
  nombre = '';
  correo = '';
  contrasena = '';
  error = '';
  exito = '';

  constructor(private clienteService: ClienteService, private router: Router) {}

  onSubmit(): void {
    if (!this.nombre || !this.correo || !this.contrasena) {
      this.error = 'Todos los campos son requeridos.';
      return;
    }

    const nuevoCliente = {
      nombre: this.nombre,
      correo: this.correo,
      contrasena: this.contrasena
    };

    this.clienteService.registrar(nuevoCliente).subscribe({
      next: () => {
        this.exito = '¡Registro completado! Redirigiendo a Iniciar Sesión...';
        this.error = '';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (err) => {
        console.error(err);
        this.error = err.error?.message || 'Ocurrió un error al registrar la cuenta.';
      }
    });
  }
}
