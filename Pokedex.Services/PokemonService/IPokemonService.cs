using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain.Dtos;
using Pokedex.Domain.Payloads;

namespace Pokedex.Services
{
    public interface IPokemonService
    {
        Task<List<PokemonDto>> GetAsync(GetPokemonsByLimitPayload pokemonsByLimitPayload);
        Task<PokemonDto> GetByNameAsync(GetPokemonByNamePayload pokemonsByNamePayload);
        Task<PokemonDetailsDto> GetByIdAsync(GetPokemonByIdPayload pokemonsByIdPayload);
    }
}