namespace workHome.Models
{
    public class PokemonModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public PokemonSkills Skills { get; set; }
    }
}
