using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
<<<<<<< felipeOficial
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
        }
=======

[ApiController]
[Route("api/carrinho")]
public class CarrinhoController : ControllerBase
{
    private static List<ItemCarrinho> _itens = new List<ItemCarrinho>();

    // Modelo do item do carrinho
    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    // 1. Adicionar produto ao carrinho
    [HttpPost("adicionar")]
    public IActionResult Adicionar([FromBody] ItemCarrinho item)
    {
        var existente = _itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);

        if (existente != null)
        {
            existente.Quantidade += item.Quantidade;
        }
        else
        {
            _itens.Add(item);
        }

        return Ok(new { Mensagem = "Produto adicionado ao carrinho" });
    }

    // 2. Remover produto do carrinho
    [HttpDelete("remover/{produtoId}")]
    public IActionResult Remover(int produtoId)
    {
        var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);

        if (item == null)
            return NotFound("Produto não encontrado no carrinho");

        _itens.Remove(item);
        return Ok(new { Mensagem = "Produto removido do carrinho" });
    }

    // 3. Atualizar quantidade no carrinho
    [HttpPut("atualizar/{produtoId}")]
    public IActionResult Atualizar(int produtoId, [FromQuery] int novaQuantidade)
    {
        var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);

        if (item == null)
            return NotFound("Produto não encontrado no carrinho");

        item.Quantidade = novaQuantidade;
        return Ok(new { Mensagem = "Quantidade atualizada" });
    }

    // 4. Buscar carrinho
    [HttpGet]
    public IActionResult ObterCarrinho()
    {
        return Ok(new
        {
            Itens = _itens,
            TotalItens = _itens.Sum(i => i.Quantidade),
            ValorTotal = _itens.Sum(i => i.Preco * i.Quantidade)
        });
>>>>>>> master
    }
}