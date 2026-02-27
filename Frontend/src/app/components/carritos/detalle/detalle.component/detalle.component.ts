import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CarritoService } from '../../../../services/carrito.service';
import { CompraService } from '../../../../services/compra.service';
import { ProductoService } from '../../../../services/producto.service';
import { Carrito } from '../../../../interfaces/carrito.interface';
import { Producto } from '../../../../interfaces/producto.interface';

@Component({
  selector: 'app-detalle',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './detalle.component.html'
})
export class DetalleComponent implements OnInit {
  carrito?: Carrito;
  productos: Producto[] = [];
  productoId = 0;
  cantidad = 1;
  id!: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private carritoSvc: CarritoService,
    private compraSvc: CompraService,
    private prodSvc: ProductoService
  ) {}

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.cargarCarrito();
    this.prodSvc.getAll().subscribe(p => this.productos = p);
  }

  cargarCarrito() {
    this.carritoSvc.getById(this.id).subscribe(c => this.carrito = c);
  }

  agregar() {
    if (!this.productoId) return;
    this.carritoSvc.agregarProducto(this.id, { productoId: this.productoId, cantidad: this.cantidad })
      .subscribe(() => { this.cargarCarrito(); this.productoId = 0; this.cantidad = 1; });
  }

  finalizar() {
    if (confirm('¿Finalizar la compra?')) {
      this.compraSvc.finalizarCompra(this.id)
        .subscribe(() => this.router.navigate(['/compras']));
    }
  }
}