using System.Drawing;
using System.Xml.Linq;

namespace PokeApiConsumer.Models
{
    public class PokemonSpecie
    {
        public EvolutionChain? evolution_chain { get; set; }

        public class EvolutionChain
        {
            public string? url { get; set; }
        }
    }
}
