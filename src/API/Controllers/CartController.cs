using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
    }
}