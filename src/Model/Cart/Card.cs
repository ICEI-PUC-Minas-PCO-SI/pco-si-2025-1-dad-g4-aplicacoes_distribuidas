using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations; 

namespace Model.Cart
{
    public class Cart
    {
        

      
        public class CartItem
        {
            [Key] 
            public int ProdutoId { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
            public int Quantidade { get; set; }
        }

       
        private readonly List<CartItem> _items = new List<CartItem>();

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

        public void RemoverItem(int produtoId)
        {
            _items.RemoveAll(i => i.ProdutoId == produtoId);
        }

        public void AtualizarQuantidade(int produtoId, int novaQuantidade)
        {
            var item = _items.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                item.Quantidade = novaQuantidade;
            }
        }

        public List<CartItem> ObterItens() => _items.ToList();
        public int TotalItens => _items.Sum(i => i.Quantidade);
        public decimal ValorTotal => _items.Sum(i => i.Preco * i.Quantidade);
        public void Limpar() => _items.Clear();
    }
}