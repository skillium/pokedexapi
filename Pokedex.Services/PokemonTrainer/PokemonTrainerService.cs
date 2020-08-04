using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Context;
using Pokedex.Domain.Models;
using Pokedex.Domain.Payloads;

namespace Pokedex.Services
{
    public class PokemonTrainerService : IPokemonTrainerService
    {
        private readonly DbSet<PokemonTrainer> _dbSet;
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        public PokemonTrainerService(PokedexContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.PokemonTrainers;
            _mapper = mapper;
        }
        async public Task<PokemonTrainer> CreateAsync(CreatePokemonTrainerPayload createPokemonTrainerPayload)
        {
            var trainer = _mapper.Map<PokemonTrainer>(createPokemonTrainerPayload);

            await _dbSet.AddAsync(trainer);
            await _context.SaveChangesAsync();

            return trainer;
        }

        async public Task<PokemonTrainer> GetByEmailAsync(GetPokemonTrainerByEmailPayload getPokemonTrainerByEmailPayload)
        {
            var trainer = await _dbSet.FirstOrDefaultAsync(p => p.Email.Equals(getPokemonTrainerByEmailPayload.Email));

            return trainer;
        }

        async public Task<PokemonTrainer> GetByIdAsync(GetPokemonTrainerByIdPayload getPokemonTrainerByIdPayload)
        {
            var trainer = await _dbSet.FirstOrDefaultAsync(p => p.Id.Equals(getPokemonTrainerByIdPayload.Id));

            return trainer;
        }
    }
}