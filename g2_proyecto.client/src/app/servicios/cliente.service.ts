import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Cliente } from '../modelos/modelos';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiUrl = '/api/ClienteControlador';
  public usuarioActual = signal<Cliente | null>(null);

  constructor(private http: HttpClient) {
    const userJson = localStorage.getItem('usuario_sabor_express');
    if (userJson) {
      try {
        this.usuarioActual.set(JSON.parse(userJson));
      } catch {
        localStorage.removeItem('usuario_sabor_express');
      }
    }
  }

  registrar(cliente: Cliente): Observable<any> {
    return this.http.post(`${this.apiUrl}/Registrar`, cliente);
  }

  login(correo: string, contrasena: string): Observable<Cliente> {
    return this.http.post<Cliente>(`${this.apiUrl}/Login`, { correo, contrasena }).pipe(
      tap(user => {
        localStorage.setItem('usuario_sabor_express', JSON.stringify(user));
        this.usuarioActual.set(user);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('usuario_sabor_express');
    this.usuarioActual.set(null);
  }

  estaAutenticado(): boolean {
    return this.usuarioActual() !== null;
  }
}
