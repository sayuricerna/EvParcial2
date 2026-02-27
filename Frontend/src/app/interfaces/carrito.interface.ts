export interface CarritoProducto {
  id?: number;
  productoId: number;
  nombre?: string;
  cantidad: number;
  precioUnitario?: number;
  subtotal?: number;
}

export interface Carrito {
  carritoId?: number;
  clienteId: number;
  clienteNombre?: string;
  fechaCreacion?: string;
  estado?: string;
  productos?: CarritoProducto[];
  subtotal?: number;
  iva?: number;
  total?: number;
}