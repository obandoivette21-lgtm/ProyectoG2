import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reserva } from '../modelos/modelos';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {
  private apiUrl = '/api/ReservaControlador';

  constructor(private http: HttpClient) {}

  crearReserva(reserva: Reserva): Observable<any> {
    return this.http.post(`${this.apiUrl}/CrearReserva`, reserva);
  }

  obtenerReservas(): Observable<Reserva[]> {
    return this.http.get<Reserva[]>(this.apiUrl);
  }
}
