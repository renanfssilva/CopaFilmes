using Microsoft.EntityFrameworkCore;
using CopaFilmes.API.Models;

namespace CopaFilmes.API
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Filme> Filmes { get; set; }
    }
}
