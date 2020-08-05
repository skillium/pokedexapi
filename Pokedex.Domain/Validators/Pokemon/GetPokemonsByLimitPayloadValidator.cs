using System;
using FluentValidation;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain.Validators
{
    public class GetPokemonsByLimitPayloadValidator : AbstractValidator<GetPokemonsByLimitPayload>
    {
        public GetPokemonsByLimitPayloadValidator()
        {
            RuleFor(p => p.Limit).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
