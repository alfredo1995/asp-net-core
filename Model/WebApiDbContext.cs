using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Produto> Produtos { get; set; }
    }
}
