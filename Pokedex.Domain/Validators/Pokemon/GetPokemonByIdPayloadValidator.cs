using System;
using FluentValidation;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain.Validators
{
    public class GetPokemonByIdPayloadValidator : AbstractValidator<GetPokemonByIdPayload>
    {
        public GetPokemonByIdPayloadValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
