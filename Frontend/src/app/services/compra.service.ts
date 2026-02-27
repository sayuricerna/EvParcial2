import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Compra } from '../interfaces/compra.interface';

@Injectable({ providedIn: 'root' })
export class CompraService {
  private url = 'http://localhost:5116/api/Compras';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Compra[]> {
    return this.http.get<Compra[]>(this.url);
  }

  finalizarCompra(carritoId: number): Observable<Compra> {
    return this.http.post<Compra>(this.url, { carritoId });
  }
}