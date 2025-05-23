using API.Controllers;
using Microsoft.EntityFrameworkCore;
using Model.Payments; 

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Payments> Payments { get; set; }
    }
namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
    public DbSet<City> Cities { get; set; }
    public DbSet<Notifications> Notifications { get; set; }
}
