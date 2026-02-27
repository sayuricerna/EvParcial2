import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CarritoService } from '../../../../services/carrito.service';
import { ClienteService } from '../../../../services/cliente.service';
import { Carrito } from '../../../../interfaces/carrito.interface';
import { Cliente } from '../../../../interfaces/cliente.interface';

@Component({
  selector: 'app-lista',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {
  carritos: Carrito[] = [];
  clientes: Cliente[] = [];
  clienteId = 0;

  constructor(private svc: CarritoService, private cliSvc: ClienteService) {}

  ngOnInit() {
    this.cargar();
    this.cliSvc.getAll().subscribe(d => this.clientes = d);
  }

  cargar() { this.svc.getAll().subscribe(d => this.carritos = d); }

  crear() {
    if (!this.clienteId) return;
    this.svc.create(this.clienteId).subscribe(() => { this.cargar(); this.clienteId = 0; });
  }

  eliminar(id: number) {
    if (confirm('¿Eliminar este carrito?'))
      this.svc.delete(id).subscribe(() => this.cargar());
  }
}