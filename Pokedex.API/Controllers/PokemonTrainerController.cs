using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Domain.Models;
using Pokedex.Domain.Payloads;
using Pokedex.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [SwaggerTag("Pokemon Trainers")]
    [Route("[controller]/api/v1")]
    public class PokemonTrainerController : ControllerBase
    {
        private readonly IPokemonTrainerService _pokemonTrainerService;
        public PokemonTrainerController(IPokemonTrainerService pokemonTrainerService)
        {
            _pokemonTrainerService = pokemonTrainerService;
        }

        [SwaggerOperation(
            Summary = "Get a trainer by Id."
        )]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(PokemonTrainer))]
        [HttpGet]
        [Route("GetById/{Id}")]
        async public Task<ActionResult> GetById([SwaggerParameter][FromRoute] GetPokemonTrainerByIdPayload getPokemonTrainerByIdPayload)
        {
            var trainer = await _pokemonTrainerService.GetByIdAsync(getPokemonTrainerByIdPayload);

            if (trainer == null) return BadRequest("Could not find the trainer.");

            return Ok(trainer);
        }

        [SwaggerOperation(
            Summary = "Get a trainer by email."
        )]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(PokemonTrainer))]
        [HttpGet]
        [Route("GetByEmail/{Email}")]
        async public Task<ActionResult> GetByEmail([SwaggerParameter][FromRoute] GetPokemonTrainerByEmailPayload pokemonTrainerByEmailPayload)
        {
            var trainer = await _pokemonTrainerService.GetByEmailAsync(pokemonTrainerByEmailPayload);

            if (trainer == null) return BadRequest("Could not find the trainer.");

            return Ok(trainer);
        }

        [SwaggerOperation(
            Summary = "Create a trainer."
        )]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Created", typeof(PokemonTrainer))]
        [HttpPost]
        [Route("Create")]
        async public Task<ActionResult> Create([SwaggerRequestBody][FromBody] CreatePokemonTrainerPayload createPokemonTrainerPayload)
        {
            var patient = await _pokemonTrainerService.CreateAsync(createPokemonTrainerPayload);
            return Ok(patient);
        }
    }
}
