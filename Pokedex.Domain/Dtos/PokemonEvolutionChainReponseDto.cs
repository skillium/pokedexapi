using System;
namespace Pokedex.Domain.Dtos
{
    public class PokemonEvolutionChainReponseDto
    {
        public ChaindDto chain { get; set; }
    }

    public class ChaindDto
    {
        public SpeciesDto species { get; set; }
        public ChaindDto[] evolves_to { get; set; }
    }
}
