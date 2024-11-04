using Microsoft.AspNetCore.Mvc;
using workHome.Models;

namespace workHome.Services
{
    public interface IPokemonControlers 
    {
        public  Task<ActionResult<object>> GetPokemonId(int id);
        public  Task<ActionResult<object>> GetPokemonName(string name);
        public  Task<ActionResult<PokemonDto>> CreatePokemon([FromBody] PokemonDto pokemon);

    }
}
