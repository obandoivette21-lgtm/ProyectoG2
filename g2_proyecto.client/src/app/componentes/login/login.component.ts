import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../servicios/cliente.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: false
})
export class LoginComponent {
  correo = '';
  contrasena = '';
  error = '';
  exito = '';

  constructor(private clienteService: ClienteService, private router: Router) {}

  onSubmit(): void {
    if (!this.correo || !this.contrasena) {
      this.error = 'Por favor, completa todos los campos.';
      return;
    }

    this.clienteService.login(this.correo, this.contrasena).subscribe({
      next: () => {
        this.exito = '¡Inicio de sesión exitoso! Redirigiendo...';
        this.error = '';
        setTimeout(() => {
          this.router.navigate(['/']);
        }, 1500);
      },
      error: (err) => {
        console.error(err);
        this.error = err.error?.message || 'Correo o contraseña incorrectos.';
      }
    });
  }
}
