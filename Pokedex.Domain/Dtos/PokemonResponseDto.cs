using System.Collections.Generic;

namespace Pokedex.Domain.Dtos
{
    public class PokemonResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public Sprites sprites { get; set; }
        public Moves[] moves { get; set; }
        public TypeObjectDto[] types { get; set; }
    }

    public class TypeObjectDto
    {
        public TypeDto type { get; set; }
    }

    public class TypeDto
    {
        public string name { get; set; }
    }

    public class Sprites
    {
        public string front_default { get; set; }
    }

    public class Moves
    {
        public MoveDto move { get; set; }
    }

    public class MoveDto
    {
        public string name { get; set; }
    }
}