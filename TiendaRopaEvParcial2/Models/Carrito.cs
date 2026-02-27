namespace TiendaRopaEvParcial2.Models
{
    public class Carrito
    {
        public int CarritoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Activo";

        public Cliente Cliente { get; set; }
        public ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}