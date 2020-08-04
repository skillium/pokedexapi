using System.Threading.Tasks;
using Pokedex.Domain.Models;
using Pokedex.Domain.Payloads;

namespace Pokedex.Services
{
    public interface IPokemonTrainerService
    {
        Task<PokemonTrainer> CreateAsync(CreatePokemonTrainerPayload createPokemonTrainerPayload);
        Task<PokemonTrainer> GetByIdAsync(GetPokemonTrainerByIdPayload getPokemonTrainerByIdPayload);
        Task<PokemonTrainer> GetByEmailAsync(GetPokemonTrainerByEmailPayload getPokemonTrainerByEmailPayload);
    }
}