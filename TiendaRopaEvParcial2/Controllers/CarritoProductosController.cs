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
    public class CarritoProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritoProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarritoProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarritoProducto>>> GetCarritoProductos()
        {
            return await _context.CarritoProductos.ToListAsync();
        }

        // GET: api/CarritoProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoProducto>> GetCarritoProducto(int id)
        {
            var carritoProducto = await _context.CarritoProductos.FindAsync(id);

            if (carritoProducto == null)
            {
                return NotFound();
            }

            return carritoProducto;
        }

        // PUT: api/CarritoProductos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarritoProducto(int id, CarritoProducto carritoProducto)
        {
            if (id != carritoProducto.Id)
            {
                return BadRequest();
            }

            _context.Entry(carritoProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoProductoExists(id))
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

        // POST: api/CarritoProductos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarritoProducto>> PostCarritoProducto(CarritoProducto carritoProducto)
        {
            _context.CarritoProductos.Add(carritoProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarritoProducto", new { id = carritoProducto.Id }, carritoProducto);
        }

        // DELETE: api/CarritoProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritoProducto(int id)
        {
            var carritoProducto = await _context.CarritoProductos.FindAsync(id);
            if (carritoProducto == null)
            {
                return NotFound();
            }

            _context.CarritoProductos.Remove(carritoProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoProductoExists(int id)
        {
            return _context.CarritoProductos.Any(e => e.Id == id);
        }
    }
}
