using Microsoft.EntityFrameworkCore;
using TiendaRopaEvParcial2.Models;
namespace TiendaRopaEvParcial2.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoProducto> CarritoProductos { get; set; }
        public DbSet<Compra> Compras { get; set; }
    }
}
