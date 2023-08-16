using System.ComponentModel.DataAnnotations;

namespace PokeApiConsumer.Models
{
    public class PokemonMaster
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade {  get; set; }
        public string Cpf { get; set; }
    }
}
