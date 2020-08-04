using AutoMapper;
using Pokedex.Domain.Models;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreatePokemonTrainerPayload, PokemonTrainer>();
        }
    }
}