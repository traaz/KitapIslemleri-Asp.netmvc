using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.NewFolder
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }
        public DbSet<KitapTuru> KitapTurleri { get; set; }    
    }
}
