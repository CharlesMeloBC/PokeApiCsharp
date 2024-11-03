using Microsoft.AspNetCore.Mvc;
using workHome.Models;

namespace workHome.Services
{
    public interface IBattleControlers 
    {
        public  Task<ActionResult<object>> GetPokemonWithRandomSkill(int id);
        public  Task<ActionResult<PokemonModel>> CreatePokemon([FromBody] PokemonModel pokemon);

    }
}
