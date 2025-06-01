using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private static List<CartItem> _cart = new();
    public record CartItem(int Id, string Nome, decimal Preco, int Quantidade);

    // POST api/cart - Adicionar item
    [HttpPost]
    public IActionResult Adicionar([FromBody] CartItem item)
    {
        _cart.Add(item);
        return Ok(item);
    }

    // DELETE api/cart/{id} - Remover item
    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {
        var item = _cart.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();

        _cart.Remove(item);
        return NoContent();
    }

    // PUT api/cart/{id} - Atualizar item
    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, [FromBody] CartItem itemAtualizado)
    {
        var index = _cart.FindIndex(i => i.Id == id);
        if (index == -1) return NotFound();

        _cart[index] = itemAtualizado;
        return NoContent();
    }

    // GET api/cart - Listar todos
    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_cart);
    }

    // GET api/cart/{id} - Buscar específico
    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var item = _cart.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound();

        return Ok(item);
    }
}