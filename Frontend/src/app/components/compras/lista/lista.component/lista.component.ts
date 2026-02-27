import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompraService } from '../../../../services/compra.service';
import { Compra } from '../../../../interfaces/compra.interface';

@Component({
  selector: 'app-lista',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lista.component.html'
})
export class ListaComponent implements OnInit {
  compras: Compra[] = [];

  constructor(private svc: CompraService) {}

  ngOnInit() { this.svc.getAll().subscribe(d => this.compras = d); }
}