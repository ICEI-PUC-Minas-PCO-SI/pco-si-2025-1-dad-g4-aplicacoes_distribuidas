using API.Controllers;
using Microsoft.EntityFrameworkCore;
using Model.Cart;
using Model.Payments; 
using Model.Products;


namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        private DbSet<Cart.CartItem> cartItems;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Payments> Payments { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Notification> Notification { get; set; }
        private object cartItems1;

        public object GetCartItems()
        {
            return cartItems1;
        }

        internal void SetCartItems(object value)
        {
            cartItems1 = value;
        }

        public DbSet<Cart.CartItem> CartItems { get => cartItems; set => cartItems = value; }
    }
}
