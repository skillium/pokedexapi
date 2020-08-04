using System;
using FluentValidation;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain.Validators
{
    public class GetPokemonTrainerByIdPayloadValidator : AbstractValidator<GetPokemonTrainerByIdPayload>
    {
        public GetPokemonTrainerByIdPayloadValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}