using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Domain.Dtos;
using Pokedex.Domain.Payloads;
using Pokedex.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [SwaggerTag("Pokemon Trainers")]
    [Route("[controller]/api/v1")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [SwaggerOperation(
            Summary = "Get a list of pokemons."
        )]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(List<PokemonDto>))]
        [HttpGet]
        [Route("Get/{Limit}")]
        async public Task<ActionResult> Get([SwaggerParameter][FromRoute] GetPokemonsByLimitPayload pokemonsByLimitPayload)
        {
            var pokemons = await _pokemonService.GetAsync(pokemonsByLimitPayload);

            return Ok(pokemons);
        }

        [SwaggerOperation(
            Summary = "Get a pokemon by name."
        )]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(PokemonDto))]
        [HttpGet]
        [Route("GetByName/{Name}")]
        async public Task<ActionResult> GetByName([SwaggerParameter][FromRoute] GetPokemonByNamePayload pokemonByNamePayload)
        {
            var pokemon = await _pokemonService.GetByNameAsync(pokemonByNamePayload);

            if (pokemon == null) return NotFound($"Could not found the pokemon with name {pokemonByNamePayload.Name}");

            return Ok(pokemon);
        }

        [SwaggerOperation(
            Summary = "Get a pokemon by id."
        )]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(PokemonDetailsDto))]
        [HttpGet]
        [Route("GetById/{Id}")]
        async public Task<ActionResult> GetById([SwaggerParameter][FromRoute] GetPokemonByIdPayload pokemonByIdPayload)
        {
            var pokemon = await _pokemonService.GetByIdAsync(pokemonByIdPayload);

            if (pokemon == null) return NotFound($"Could not found the pokemon with id: '{pokemonByIdPayload.Id}'");

            return Ok(pokemon);
        }
    }
}