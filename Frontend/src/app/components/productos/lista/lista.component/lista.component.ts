import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProductoService } from '../../../../services/producto.service';
import { Producto } from '../../../../interfaces/producto.interface'

@Component({
  selector: 'app-lista',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {
  productos: Producto[] = [];

  constructor(private svc: ProductoService) {}

  ngOnInit() { this.cargar(); }

  cargar() { this.svc.getAll().subscribe(d => this.productos = d); }

  eliminar(id: number) {
    if (confirm('¿Eliminar este producto?'))
      this.svc.delete(id).subscribe(() => this.cargar());
  }
}