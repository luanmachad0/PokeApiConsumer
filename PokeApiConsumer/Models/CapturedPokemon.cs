using System.ComponentModel.DataAnnotations;

namespace PokeApiConsumer.Models
{
    public class CapturedPokemon
    {
        [Key]
        public int PokemonId { get; set; }
        public string? PokemonName { get; set;}
    }
}
