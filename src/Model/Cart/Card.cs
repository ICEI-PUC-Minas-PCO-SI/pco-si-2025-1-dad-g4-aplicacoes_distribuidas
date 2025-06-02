using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Cart
{
    public class Cart
    {
        private readonly List<CartItem> _items = new List<CartItem>();

        // Versão simplificada do item do carrinho
        public class CartItem
        {
            public int ProdutoId { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public int Quantidade { get; set; }
        }

        // Adiciona ou atualiza item no carrinho
        public void AdicionarItem(CartItem item)
        {
            var existente = _items.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);

            if (existente != null)
            {
                existente.Quantidade += item.Quantidade;
            }
            else
            {
                _items.Add(item);
            }
        }

        // Remove item do carrinho
        public void RemoverItem(int produtoId)
        {
            _items.RemoveAll(i => i.ProdutoId == produtoId);
        }

        // Atualiza quantidade de um item
        public void AtualizarQuantidade(int produtoId, int novaQuantidade)
        {
            var item = _items.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                item.Quantidade = novaQuantidade;
            }
        }

        // Obtém todos os itens do carrinho
        public List<CartItem> ObterItens() => _items.ToList();

        // Calcula o total de itens
        public int TotalItens => _items.Sum(i => i.Quantidade);

        // Calcula o valor total
        public decimal ValorTotal => _items.Sum(i => i.Preco * i.Quantidade);

        // Limpa todo o carrinho
        public void Limpar() => _items.Clear();
    }
}