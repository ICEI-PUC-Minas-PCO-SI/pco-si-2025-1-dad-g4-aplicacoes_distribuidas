using API.Controllers;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Model.Payments; 
using Model.Products;
=======
using Model.Payments;


namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Payments> Payments { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Notification> Notification { get; set; }

    }
}
