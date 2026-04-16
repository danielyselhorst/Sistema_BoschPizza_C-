using Microsoft.EntityFrameworkCore;
using BoschPizza.Models;

namespace BoschPizza.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

         //Representa a tabela Pizza no banco de dados
    public DbSet<Pizza> Pizzas { get; set; }

    public DbSet<UserLogin> UserLogins { get; set; }

    public DbSet<Cliente> Clientes { get; set; }

    }
}
   
