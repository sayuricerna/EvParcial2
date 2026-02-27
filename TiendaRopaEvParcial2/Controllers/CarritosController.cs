using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopaEvParcial2.Models;

namespace TiendaRopaEvParcial2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Carritos
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        //{
        //    return await _context.Carritos.ToListAsync();
        //}

        //// GET: api/Carritos/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Carrito>> GetCarrito(int id)
        //{
        //    var carrito = await _context.Carritos.FindAsync(id);

        //    if (carrito == null)
        //    {
        //        return NotFound();
        //    }

        //    return carrito;
        //}

        // GET: api/Carritos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCarritos()
        {
            return await _context.Carritos
                .Include(c => c.Cliente)
                .Include(c => c.CarritoProductos).ThenInclude(cp => cp.Producto)
                .Select(c => new {
                    c.CarritoId,
                    c.ClienteId,
                    ClienteNombre = c.Cliente.Nombre + " " + c.Cliente.Apellido,
                    c.FechaCreacion,
                    c.Estado,
                    Productos = c.CarritoProductos.Select(cp => new {
                        cp.Id,
                        cp.ProductoId,
                        Nombre = cp.Producto.Nombre,
                        cp.Cantidad,
                        cp.PrecioUnitario,
                        Subtotal = cp.Cantidad * cp.PrecioUnitario
                    }),
                    Subtotal = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario),
                    Iva = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario) * 0.15m,
                    Total = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario) * 1.15m
                })
                .ToListAsync();
        }

        // GET: api/Carritos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCarrito(int id)
        {
            var carrito = await _context.Carritos
                .Include(c => c.Cliente)
                .Include(c => c.CarritoProductos).ThenInclude(cp => cp.Producto)
                .Where(c => c.CarritoId == id)
                .Select(c => new {
                    c.CarritoId,
                    c.ClienteId,
                    ClienteNombre = c.Cliente.Nombre + " " + c.Cliente.Apellido,
                    c.FechaCreacion,
                    c.Estado,
                    Productos = c.CarritoProductos.Select(cp => new {
                        cp.Id,
                        cp.ProductoId,
                        Nombre = cp.Producto.Nombre,
                        cp.Cantidad,
                        cp.PrecioUnitario,
                        Subtotal = cp.Cantidad * cp.PrecioUnitario
                    }),
                    Subtotal = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario),
                    Iva = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario) * 0.15m,
                    Total = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario) * 1.15m
                })
                .FirstOrDefaultAsync();

            if (carrito == null) return NotFound();
            return carrito;
        }
        // PUT: api/Carritos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.CarritoId)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carritos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrito", new { id = carrito.CarritoId }, carrito);
        }

        // DELETE: api/Carritos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET con detalle de productos y totales
        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<object>> GetDetalle(int id)
        {
            var carrito = await _context.Carritos
                .Include(c => c.Cliente)
                .Include(c => c.CarritoProductos).ThenInclude(cp => cp.Producto)
                .Where(c => c.CarritoId == id)
                .Select(c => new {
                    c.CarritoId,
                    c.ClienteId,
                    ClienteNombre = c.Cliente.Nombre + " " + c.Cliente.Apellido,
                    c.FechaCreacion,
                    c.Estado,
                    Productos = c.CarritoProductos.Select(cp => new {
                        cp.Id,
                        cp.ProductoId,
                        Nombre = cp.Producto.Nombre,
                        cp.Cantidad,
                        cp.PrecioUnitario,
                        Subtotal = cp.Cantidad * cp.PrecioUnitario
                    }),
                    Subtotal = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario),
                    Iva = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario) * 0.15m,
                    Total = c.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario) * 1.15m
                })
                .FirstOrDefaultAsync();

            if (carrito == null) return NotFound();
            return carrito;
        }

        // POST agregar producto al carrito
        [HttpPost("{id}/agregar-producto")]
        public async Task<IActionResult> AgregarProducto(int id, [FromBody] CarritoProducto item)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null) return NotFound("Carrito no encontrado");

            var producto = await _context.Productos.FindAsync(item.ProductoId);
            if (producto == null) return NotFound("Producto no encontrado");

            item.CarritoId = id;
            item.PrecioUnitario = producto.PrecioDescuento ?? producto.Precio;
            _context.CarritoProductos.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.CarritoId == id);
        }
    }
}
