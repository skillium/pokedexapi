using System.Collections.Generic;

namespace Pokedex.Domain.Dtos
{
    public class PokemonDetailsDto : PokemonDto
    {
        public List<PokemonDto> Evolutions { get; set; }
    }
}