using System;
using FluentValidation;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain.Validators
{
    public class GetPokemonByNamePayloadValidator : AbstractValidator<GetPokemonByNamePayload>
    {
        public GetPokemonByNamePayloadValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
        }
    }
}
