using System.Collections.Generic;

namespace Pokedex.Domain.Dtos
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public List<string> Moves { get; set; }
        public string Image { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}