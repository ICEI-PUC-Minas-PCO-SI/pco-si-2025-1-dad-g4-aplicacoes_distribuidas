using API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Cart;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Cart.CartItem>> AddItemToCart([FromBody] Cart.CartItem item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = await _context.CartItems.FirstOrDefaultAsync(i => i.ProdutoId == item.ProdutoId);

            if (existingItem != null)
            {
                existingItem.Quantidade += item.Quantidade;
                _context.Entry(existingItem).State = EntityState.Modified;
            }
            else
            {
                _context.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartItemById), new { produtoId = item.ProdutoId }, item);
        }


        [HttpDelete("{produtoId}")]
        public async Task<IActionResult> RemoveItemFromCart(int produtoId)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.ProdutoId == produtoId);

            if (item == null)
            {
                return NotFound("Produto não encontrado no carrinho");
            }

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{produtoId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int produtoId, [FromQuery] int novaQuantidade)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.ProdutoId == produtoId);

            if (item == null)
            {
                return NotFound("Produto não encontrado no carrinho");
            }

            item.Quantidade = novaQuantidade;
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(produtoId))
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart.CartItem>>> GetCart()
        {
            var items = await _context.CartItems.ToListAsync();

            return Ok(items);
        }

        [HttpGet("{produtoId}")]
        public async Task<ActionResult<Cart.CartItem>> GetCartItemById(int produtoId)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.ProdutoId == produtoId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        private bool CartItemExists(int produtoId)
        {
            return _context.CartItems.Any(e => e.ProdutoId == produtoId);