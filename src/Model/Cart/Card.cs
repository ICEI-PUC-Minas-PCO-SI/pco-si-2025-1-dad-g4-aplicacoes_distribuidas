using System;
using System.Collections.Generic;
using System.Linq;
namespace Model.Cart
{
    public class Cart
    {
        // Propriedades básicas do carrinho
        public int Id { get; set; }
        public string UserId { get; set; }  // Para associar o carrinho a um usuário
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Itens do carrinho
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Propriedades calculadas
        public int TotalItems => Items.Sum(item => item.Quantity);
        public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Price);

        // Métodos para manipulação do carrinho
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveItem(int productId)
        {
            var itemToRemove = Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void UpdateQuantity(int productId, int newQuantity)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void ClearCart()
        {
            Items.Clear();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Data de adição ao carrinho
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}