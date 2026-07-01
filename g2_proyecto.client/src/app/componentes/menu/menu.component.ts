import { Component, OnInit } from '@angular/core';
import { ProductoService } from '../../servicios/producto.service';
import { Producto } from '../../modelos/modelos';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  standalone: false
})
export class MenuComponent implements OnInit {
  productos: Producto[] = [];
  cargando = true;
  error = '';

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.productoService.obtenerProductos().subscribe({
      next: (data) => {
        this.productos = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Error al cargar el menú. Por favor, intenta de nuevo.';
        this.cargando = false;
      }
    });
  }
}
