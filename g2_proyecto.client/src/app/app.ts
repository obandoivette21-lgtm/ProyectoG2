import { Component } from '@angular/core';
import { ClienteService } from './servicios/cliente.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  constructor(public clienteService: ClienteService) {}

  logout(): void {
    this.clienteService.logout();
  }
}
