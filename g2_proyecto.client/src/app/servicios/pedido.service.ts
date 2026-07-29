import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pedido } from '../modelos/modelos';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  private apiUrl = '/api/PedidoControlador';

  constructor(private http: HttpClient) {}

  crearPedido(pedido: Pedido): Observable<any> {
    return this.http.post(`${this.apiUrl}/CrearPedido`, pedido);
  }

  obtenerPedidos(): Observable<Pedido[]> {
    return this.http.get<Pedido[]>(this.apiUrl);
  }

  obtenerPedidosPorCliente(clienteId: number): Observable<Pedido[]> {
    return this.http.get<Pedido[]>(`${this.apiUrl}/cliente/${clienteId}`);
  }

  actualizarPedido(id: number, pedido: Pedido): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, pedido);
  }

  eliminarPedido(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
