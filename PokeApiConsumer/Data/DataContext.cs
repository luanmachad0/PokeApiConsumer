using Microsoft.EntityFrameworkCore;
using PokeApiConsumer.Models;

namespace PokeApiConsumer.Data
{
    public class DataContext : DbContext
    {
        public DbSet<PokemonMaster> PokemonMasters { get; set; }
        public DbSet<CapturedPokemon> CapturedPokemons { get; set; }
        public string DbPath { get; set; }

        //public DataContext() 
        //{
        //    DbPath = "pokeapi.db";
        //}

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
             : base(options) { }
    }
}
