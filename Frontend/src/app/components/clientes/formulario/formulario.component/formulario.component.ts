import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClienteService } from '../../../../services/cliente.service';
import { Cliente } from '../../../../interfaces/cliente.interface';

@Component({
  selector: 'app-formulario',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './formulario.component.html'
})
export class FormularioComponent implements OnInit {
  cliente: Cliente = { nombre: '', apellido: '', email: '', telefono: '', cedula: '', direccion: '', genero: '' };
  esEdicion = false;
  id?: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private svc: ClienteService
  ) {}

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.esEdicion = true;
      this.svc.getById(this.id).subscribe(c => this.cliente = c);
    }
  }

  guardar() {
    if (this.esEdicion && this.id)
      this.svc.update(this.id, this.cliente).subscribe(() => this.router.navigate(['/clientes']));
    else
      this.svc.create(this.cliente).subscribe(() => this.router.navigate(['/clientes']));
  }
}