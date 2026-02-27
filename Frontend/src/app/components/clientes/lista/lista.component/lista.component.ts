import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ClienteService } from '../../../../services/cliente.service';
import { Cliente } from '../../../../interfaces/cliente.interface';

@Component({
  selector: 'app-lista',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {
  clientes: Cliente[] = [];

  constructor(private svc: ClienteService) {}

  ngOnInit() { this.cargar(); }

  cargar() { this.svc.getAll().subscribe(d => this.clientes = d); }

  eliminar(id: number) {
    if (confirm('¿Eliminar este cliente?'))
      this.svc.delete(id).subscribe(() => this.cargar());
  }
}