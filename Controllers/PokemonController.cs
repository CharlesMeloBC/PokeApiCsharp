using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workHome.Data;
using workHome.Models;
using workHome.Services;

namespace workHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase, IPokemonControlers
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Id {id}")]
        public async Task<ActionResult<object>> GetPokemonId(int id)
        {
            var pokemon = await _context.pokemons.FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);

        }

        [HttpGet("Name {Name}")]
        public async Task<ActionResult<object>> GetPokemonName(string Name)
        {
            var pokemon = await _context.pokemons.FirstOrDefaultAsync(p => p.Name == Name);

            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);

        }

        [HttpPost]
        public async Task<ActionResult<PokemonDto>> CreatePokemon([FromBody] PokemonDto pokemonDto)
        {
            if (pokemonDto == null ||
                string.IsNullOrEmpty(pokemonDto.Name) ||
                pokemonDto.Skills == null || !pokemonDto.Skills.Any() ||
                string.IsNullOrEmpty(pokemonDto.Url))
            {
                return BadRequest("Dados do Pokémon estão incompletos.");
            }

            // Mapeamento de PokemonDto para PokemonModel
            var pokemon = new PokemonModel(
                pokemonDto.Name,
                pokemonDto.Skills,
                pokemonDto.Url
                );

            _context.pokemons.Add(pokemon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPokemonId), new { id = pokemon.Id }, pokemonDto);
        }


    }
}
