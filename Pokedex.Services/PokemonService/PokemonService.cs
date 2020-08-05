using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Pokedex.Domain.Dtos;
using Pokedex.Domain.Payloads;

namespace Pokedex.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private const string POKEAPI_URL = "https://pokeapi.co/api/v2";

        public PokemonService(IMemoryCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
        }
        async public Task<List<PokemonDto>> GetAsync(GetPokemonsByLimitPayload pokemonsByLimitPayload)
        {
            var cacheKey = $"limit={pokemonsByLimitPayload.Limit}";

            if (!_cache.TryGetValue(cacheKey, out List<PokemonDto> pokemons))
            {
                pokemons = new List<PokemonDto>();
                HttpClient client = new HttpClient();
                var httpResponse = await client.GetAsync($"{POKEAPI_URL}/pokemon?{cacheKey}");
                var jsonString = await httpResponse.Content.ReadAsStringAsync();
                var pokemonListResponse = JsonConvert.DeserializeAnonymousType(jsonString, new { results = new[] { new { name = "", url = "" } } });

                foreach (var item in pokemonListResponse.results)
                {
                    httpResponse = await client.GetAsync(item.url);
                    jsonString = await httpResponse.Content.ReadAsStringAsync();
                    var pokemonResponse = JsonConvert.DeserializeObject<PokemonResponseDto>(jsonString);
                    var pokemonDto = _mapper.Map<PokemonDto>(pokemonResponse);
                    pokemons.Add(pokemonDto);
                };

                _cache.Set(cacheKey, pokemons, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    SlidingExpiration = TimeSpan.FromMinutes(15),
                    Priority = CacheItemPriority.Normal
                });
            }

            return pokemons;
        }

        async public Task<PokemonDetailsDto> GetByIdAsync(GetPokemonByIdPayload pokemonsByIdPayload)
        {
            var cacheKey = $"pokemon:{pokemonsByIdPayload.Id}";

            if (!_cache.TryGetValue(cacheKey, out PokemonDetailsDto pokemon))
            {
                HttpClient client = new HttpClient();
                var httpResponse = await client.GetAsync($"{POKEAPI_URL}/pokemon/{pokemonsByIdPayload.Id}");

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

                var jsonString = await httpResponse.Content.ReadAsStringAsync();
                var pokemonResponse = JsonConvert.DeserializeObject<PokemonResponseDto>(jsonString);
                pokemon = _mapper.Map<PokemonDetailsDto>(pokemonResponse);
                pokemon.Evolutions = new List<PokemonDto>();

                await GetPokemonEvolutionsAsync(pokemon);

                _cache.Set(cacheKey, pokemon, new MemoryCacheEntryOptions
                { 
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    SlidingExpiration = TimeSpan.FromMinutes(15),
                    Priority = CacheItemPriority.Normal
                });
            }

            return pokemon;
        }

        async public Task<PokemonDto> GetByNameAsync(GetPokemonByNamePayload pokemonsByNamePayload)
        {
            var cacheKey = $"pokemon:{pokemonsByNamePayload.Name}";

            if (!_cache.TryGetValue(cacheKey, out PokemonDto pokemon))
            {
                HttpClient client = new HttpClient();
                var httpResponse = await client.GetAsync($"{POKEAPI_URL}/pokemon/{pokemonsByNamePayload.Name}");

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

                var jsonString = await httpResponse.Content.ReadAsStringAsync();
                var pokemonResponse = JsonConvert.DeserializeObject<PokemonResponseDto>(jsonString);
                pokemon = _mapper.Map<PokemonDto>(pokemonResponse);

                _cache.Set(cacheKey, pokemon, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    SlidingExpiration = TimeSpan.FromMinutes(15),
                    Priority = CacheItemPriority.Normal
                });
            }

            return pokemon;
        }

        #region Private methods

        private async Task GetPokemonEvolutionsAsync(PokemonDetailsDto pokemon)
        {
            HttpClient client = new HttpClient();
            var httpResponse = await client.GetAsync($"{POKEAPI_URL}/pokemon-species/{pokemon.Id}");

            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            var pokemonSpecieResponse = JsonConvert.DeserializeAnonymousType(jsonString, new { evolution_chain = new { url = "" } });

            httpResponse = await client.GetAsync(pokemonSpecieResponse.evolution_chain.url);
            jsonString = await httpResponse.Content.ReadAsStringAsync();
            var pokemonEvolutionChainResponse = JsonConvert.DeserializeObject<PokemonEvolutionChainReponseDto>(jsonString);

            await AddEvolutions(pokemon, pokemonEvolutionChainResponse.chain);
        }

        async private Task AddEvolutions(PokemonDetailsDto pokemonDetails, ChaindDto pokemonEvolutionChainReponseDto)
        {
            var pokemon = await GetByNameAsync(new GetPokemonByNamePayload { Name = pokemonEvolutionChainReponseDto.species.name });

            pokemonDetails.Evolutions.Add(pokemon);

            if (pokemonEvolutionChainReponseDto.evolves_to.Length > 0)
            {
                await AddEvolutions(pokemonDetails, pokemonEvolutionChainReponseDto.evolves_to[0]);
            }
        }

        #endregion
    }
}