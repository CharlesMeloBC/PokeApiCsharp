using System;

namespace workHome.Models
{
    public class PokemonModel
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string[] Skills { get; private set; } 
        public string? Url { get; private set; }


        public PokemonModel(string name, string[] skills, string url) 
        { 
            Name = name;
            Skills = skills;
            Url = url;

        }
    }

}
