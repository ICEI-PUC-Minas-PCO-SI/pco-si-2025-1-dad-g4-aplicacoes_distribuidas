using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Cart
{
    public class Cart
    {
        public List<CartItem> Items { get; } = new List<CartItem>();

        // Adiciona ou atualiza item no carrinho
        public void AddItem(int productId, string name, decimal price, int quantity = 1)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = name,
                    Price = price,
                    Quantity = quantity
                });
            }
        }

        // Remove item do carrinho
        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
        }

        // Limpa todo o carrinho
        public void Clear() => Items.Clear();

        // Calcula o total
        public decimal Total => Items.Sum(i => i.Price * i.Quantity);
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
    }
}