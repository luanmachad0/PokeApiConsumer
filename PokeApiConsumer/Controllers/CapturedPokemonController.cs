using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeApiConsumer.Data;
using PokeApiConsumer.Models;
using System.Numerics;

namespace PokeApiConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapturedPokemonController : ControllerBase
    {
        private readonly DataContext Context;

        public CapturedPokemonController(DataContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CapturedPokemon>> Post([FromBody] CapturedPokemon capturedPokemonBody)
        {
            CapturedPokemon capturedPokemon = new()
            {
                PokemonId = capturedPokemonBody.PokemonId,
                PokemonName = capturedPokemonBody.PokemonName
            };

            Context.CapturedPokemons.Add(capturedPokemon);
            await Context.SaveChangesAsync();
            return Ok(capturedPokemon);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CapturedPokemon>>> GetAll()
        {
            return await Context.CapturedPokemons.ToListAsync();
        }
    }
}
