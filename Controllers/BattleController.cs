using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workHome.Data;
using workHome.Models;
using workHome.Services;

namespace workHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase, IBattleControlers
    {
        private readonly AppDbContext _context;

        public BattleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPokemonWithRandomSkill(int id)
        {
            var pokemon = await _context.pokemons.FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            var random = new Random();
            var skills = Enum.GetValues(typeof(PokemonSkills));
            var randomSkill = (PokemonSkills)skills.GetValue(random.Next(skills.Length));

            return Ok(new
            {
                Name = pokemon.Name,
                Skill = randomSkill.ToString()
            });

        }

        [HttpPost]
        public async Task<ActionResult<PokemonModel>> CreatePokemon([FromBody] PokemonModel pokemon)
        {
            if (pokemon == null)
            {
                return BadRequest("Pokemon data is null.");
            }

            pokemon.Skills = PokemonSkills.basic | PokemonSkills.normal | PokemonSkills.special;
            
            _context.pokemons.Add(pokemon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPokemonWithRandomSkill), new { id = pokemon.Id }, pokemon);
        }
    }
}
