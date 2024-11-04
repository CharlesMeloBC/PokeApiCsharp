using System;

namespace workHome.Models
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string[]? Skills { get; set;}
        public string? Url { get; set; }
    }
}
