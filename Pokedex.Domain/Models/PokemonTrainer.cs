using System;

namespace Pokedex.Domain.Models
{
    public class PokemonTrainer
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}