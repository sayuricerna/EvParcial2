export interface Compra {
  compraId?: number;
  clienteId: number;
  clienteNombre?: string;
  carritoId: number;
  fechaCompra?: string;
  subtotal?: number;
  iva?: number;
  total?: number;
}