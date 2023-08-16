using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeApiConsumer.Data;
using PokeApiConsumer.Models;
using System.Numerics;

namespace PokeApiConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonMasterController : ControllerBase
    {
        private readonly DataContext Context;

        public PokemonMasterController(DataContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PokemonMaster>> Post([FromBody] PokemonMaster pokemonMasterBody)
        {
            PokemonMaster pokemonMaster = new()
            {
                Nome = pokemonMasterBody.Nome,
                Idade = pokemonMasterBody.Idade,
                Cpf = pokemonMasterBody.Cpf
            };

            Context.PokemonMasters.Add(pokemonMaster);
            await Context.SaveChangesAsync();
            return Ok(pokemonMaster);
        }
    }
}
