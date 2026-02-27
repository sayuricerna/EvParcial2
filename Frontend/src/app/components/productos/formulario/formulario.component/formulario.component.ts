import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductoService } from '../../../../services/producto.service';
import { Producto } from '../../../../interfaces/producto.interface';

@Component({
  selector: 'app-formulario',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './formulario.component.html'
})
export class FormularioComponent implements OnInit {
  producto: Producto = { nombre: '', codigo: '', talla: '', color: '', precio: 0, foto: '' };
  esEdicion = false;
  id?: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private svc: ProductoService
  ) {}

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.esEdicion = true;
      this.svc.getById(this.id).subscribe(p => this.producto = p);
    }
  }

  guardar() {
    if (this.esEdicion && this.id)
      this.svc.update(this.id, this.producto).subscribe(() => this.router.navigate(['/productos']));
    else
      this.svc.create(this.producto).subscribe(() => this.router.navigate(['/productos']));
  }
}