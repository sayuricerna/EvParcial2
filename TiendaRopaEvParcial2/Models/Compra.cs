namespace TiendaRopaEvParcial2.Models
{
    public class Compra
    {
        public int CompraId { get; set; }
        public int ClienteId { get; set; }
        public int CarritoId { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }

        public Cliente Cliente { get; set; }
        public Carrito Carrito { get; set; }
    }
}