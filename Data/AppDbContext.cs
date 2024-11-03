using Microsoft.EntityFrameworkCore;
using workHome.Models;

namespace workHome.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<PokemonModel> pokemons {  get; set; }
    };
}

