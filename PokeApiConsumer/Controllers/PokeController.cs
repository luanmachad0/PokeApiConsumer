using Microsoft.AspNetCore.Mvc;
using PokeApiConsumer.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PokeConsumer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokeController : ControllerBase
    { 
        private static readonly HttpClient client;

        public PokeController()
        {
        }

        static PokeController()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://pokeapi.co")
            };
        }

        [HttpGet("/pokemon/{value}")]
        public async Task<Pokemon> GetByNameOrId(string value)
        {
            var result = new Pokemon();
            HttpResponseMessage response = client.GetAsync($"api/v2/pokemon/{value}").Result;

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<Pokemon>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                var evolution = await GetEvolution(result.Id);
                result.evolution = evolution;

                if (result.sprites?.back_default is not null)
                {
                    byte[] spriteAsBytes = Encoding.ASCII.GetBytes(result.sprites.back_default);
                    result.base64_sprite_default = Convert.ToBase64String(spriteAsBytes);
                }
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }


            return result;
        }

        [HttpGet("/pokemon/fullinfo")]
        public async Task<List<Pokemon>> GetTenRandomFullInfo()
        {
            var pokemons = new List<Pokemon>();
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                pokemons.Add(await GetByNameOrId(rnd.Next(1000).ToString()));
            }


            return pokemons;
        }

        [HttpGet("/pokemon")]
        public IActionResult GetTenRandom()
        {
            try
            {
                Random rnd = new Random();

                HttpResponseMessage response = client.GetAsync($"api/v2/pokemon/?limit=10&offset={rnd.Next(100)}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            catch (Exception ex) { return Ok(ex.Message); }
            finally { }
        }

        private async Task<Evolution?> GetEvolution(int id)
        {
            var pokemonSpecie = new PokemonSpecie();
            HttpResponseMessage response = client.GetAsync($"api/v2/pokemon-species/{id}").Result;
            var result = new Evolution();
            var client2 = new HttpClient();

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                pokemonSpecie = JsonSerializer.Deserialize<PokemonSpecie>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                HttpResponseMessage response2 = client.GetAsync($"{pokemonSpecie?.evolution_chain.url}").Result;

                if (response2.IsSuccessStatusCode)
                {
                    var stringResponse2 = await response2.Content.ReadAsStringAsync();

                    result = JsonSerializer.Deserialize<Evolution>(stringResponse2,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}