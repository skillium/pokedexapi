using System;
namespace Pokedex.Domain.Dtos
{
    public class PokemonDetailsResponseDto : PokemonResponseDto
    {
        public SpeciesDto species { get; set; }
    }

    public class SpeciesDto
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
