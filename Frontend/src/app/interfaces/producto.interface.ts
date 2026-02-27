export interface Producto {
  productoId?: number;
  nombre: string;
  codigo: string;
  talla: string;
  color: string;
  precio: number;
  precioDescuento?: number;
  foto?: string;
}