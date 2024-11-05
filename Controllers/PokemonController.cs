using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workHome.Data;
using workHome.Models;

namespace workHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PokemonController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "TreinadorPolicy, ProfessorPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonModel>> GetPokemonId(int id)
        {
            var pokemon = await _context.pokemons.FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }
        [Authorize(Policy = "TreinadorPolicy, ProfessorPolicy")]
        [HttpGet("name/{name}")]
        public async Task<ActionResult<PokemonModel>> GetPokemonName(string name)
        {
            var pokemon = await _context.pokemons.FirstOrDefaultAsync(p => p.Name == name);

            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }
        [Authorize(Policy = "ProfessorPolicy")]
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
