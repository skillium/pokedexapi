using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Models;

namespace Pokedex.Domain.Context
{
    public class PokedexContext : DbContext
    {
        public DbSet<PokemonTrainer> PokemonTrainers { get; set; }
        public PokedexContext(DbContextOptions<PokedexContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PokemonTrainer>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.Email).IsRequired();
                entity.HasIndex(p => p.Email).IsUnique();
            });
        }
    }
}