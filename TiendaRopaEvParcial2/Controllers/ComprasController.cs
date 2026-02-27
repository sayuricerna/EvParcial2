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
    public class ComprasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            return await _context.Compras.ToListAsync();
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, Compra compra)
        {
            if (id != compra.CompraId)
            {
                return BadRequest();
            }

            _context.Entry(compra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        //{
        //    _context.Compras.Add(compra);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCompra", new { id = compra.CompraId }, compra);
        //}
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FinalizarCompraDto dto)
        {
            var carrito = await _context.Carritos
                .Include(c => c.CarritoProductos)
                .FirstOrDefaultAsync(c => c.CarritoId == dto.CarritoId);

            if (carrito == null) return NotFound("Carrito no encontrado");

            var subtotal = carrito.CarritoProductos.Sum(cp => cp.Cantidad * cp.PrecioUnitario);

            var compra = new Compra
            {
                ClienteId = carrito.ClienteId,
                CarritoId = carrito.CarritoId,
                Subtotal = subtotal,
                FechaCompra = DateTime.Now
            };

            carrito.Estado = "Completado";
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
            return Ok(compra);
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.CompraId == id);
        }
        public class FinalizarCompraDto
        {
            public int CarritoId { get; set; }
        }
    }
}
