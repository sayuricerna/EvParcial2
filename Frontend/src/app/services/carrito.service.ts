
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Carrito, CarritoProducto } from '../interfaces/carrito.interface';

@Injectable({ providedIn: 'root' })
export class CarritoService {
  private url = 'https://localhost:7044/api/carritos';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Carrito[]> {
    return this.http.get<Carrito[]>(this.url);
  }

  getById(id: number): Observable<Carrito> {
    return this.http.get<Carrito>(`${this.url}/${id}`);
  }

  create(clienteId: number): Observable<Carrito> {
    return this.http.post<Carrito>(this.url, {
      clienteId: clienteId,
      estado: 'Activo',
      fechaCreacion: new Date().toISOString()
    });
  }

  agregarProducto(carritoId: number, item: CarritoProducto): Observable<any> {
    return this.http.post(`${this.url}/${carritoId}/agregar-producto`, item);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}