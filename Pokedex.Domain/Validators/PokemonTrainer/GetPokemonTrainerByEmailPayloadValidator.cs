using FluentValidation;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain.Validators
{
    public class GetPokemonTrainerByEmailPayloadValidator : AbstractValidator<GetPokemonTrainerByEmailPayload>
    {
        public GetPokemonTrainerByEmailPayloadValidator()
        {
            RuleFor(p => p.Email).NotEmpty().NotNull();
        }
    }
}