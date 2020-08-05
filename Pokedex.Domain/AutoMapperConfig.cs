using System.Linq;
using AutoMapper;
using Pokedex.Domain.Dtos;
using Pokedex.Domain.Models;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreatePokemonTrainerPayload, PokemonTrainer>();
            CreateMap<PokemonResponseDto, PokemonDto>()
                .ForMember(p => p.image, opts => opts.MapFrom(pr => pr.sprites.front_default))
                .ForMember(p => p.Types, opts => opts.MapFrom(pr => pr.types.Select(type => type.type.name)));

            CreateMap<PokemonResponseDto, PokemonDetailsDto>()
                .ForMember(p => p.Evolutions, opts => opts.Ignore())
                .ForMember(p => p.image, opts => opts.MapFrom(pr => pr.sprites.front_default))
                .ForMember(p => p.Types, opts => opts.MapFrom(pr => pr.types.Select(type => type.type.name)));
        }
    }
}