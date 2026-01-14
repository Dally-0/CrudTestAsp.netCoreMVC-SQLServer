using EmbutidosTarea3.Models;
using Microsoft.EntityFrameworkCore;

namespace EmbutidosTarea3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aquí registramos las tablas
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
