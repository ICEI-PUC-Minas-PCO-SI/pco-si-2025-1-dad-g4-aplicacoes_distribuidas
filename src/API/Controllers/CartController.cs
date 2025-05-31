using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        // Simulando um "banco de dados" em memória
        private static List<CartItem> _cartItems = new List<CartItem>();
        private static int _nextId = 1;

        // Modelo para o item do carrinho
        public class CartItem
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        // POST api/cart - Adicionar item ao carrinho
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CartItem> AddToCart([FromBody] CartItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.ProductName) || item.Price <= 0)
            {
                return BadRequest("Dados do produto inválidos");
            }

            item.Id = _nextId++;
            _cartItems.Add(item);

            return CreatedAtAction(nameof(GetCartItem), new { id = item.Id }, item);
        }

        // DELETE api/cart/{id} - Remover item do carrinho
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveFromCart(int id)
        {
            var item = _cartItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _cartItems.Remove(item);
            return NoContent();
        }

        // PUT api/cart/{id} - Atualizar item no carrinho
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCartItem(int id, [FromBody] CartItem updatedItem)
        {
            if (updatedItem == null || id != updatedItem.Id)
            {
                return BadRequest();
            }

            var existingItem = _cartItems.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.ProductName = updatedItem.ProductName;
            existingItem.Price = updatedItem.Price;
            existingItem.Quantity = updatedItem.Quantity;

            return NoContent();
        }

        // GET api/cart - Buscar todos os itens do carrinho
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CartItem>> GetCart()
        {
            return Ok(_cartItems);
        }

        // GET api/cart/{id} - Buscar um item específico do carrinho
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CartItem> GetCartItem(int id)
        {
            var item = _cartItems.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}