using API.Controllers;
using Microsoft.EntityFrameworkCore;
using Model.Autentication;
using Model.Order;
using Model.Payments;
using Model.Products;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Autentication> Autentication { get; set; }

    }
}
