using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Products;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Products>> CreateProduct(Products product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // GET: api/Products?nome=caneca&precoMin=10&precoMax=100
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts(
            string nome = null,
            decimal? precoMin = null,
            decimal? precoMax = null,
            string categoria = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));

            if (precoMin.HasValue)
                query = query.Where(p => p.Preco >= precoMin.Value);

            if (precoMax.HasValue)
                query = query.Where(p => p.Preco <= precoMax.Value);

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(p => p.Categoria.ToLower() == categoria.ToLower());

            return await query.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.Id)
                return BadRequest("ID do produto não corresponde.");

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
