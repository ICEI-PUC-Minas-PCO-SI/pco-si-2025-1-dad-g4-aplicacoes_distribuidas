using API.Controllers;
using Microsoft.EntityFrameworkCore;
using API.Model.Autentication;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<City> Cities { get; set; }
    public DbSet<Autentication> Autenticacoes { get; set; }
}
