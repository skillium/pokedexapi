using FluentValidation;
using Pokedex.Domain.Payloads;

namespace Pokedex.Domain.Validators
{
    public class CreatePokemonTrainerPayloadValidator : AbstractValidator<CreatePokemonTrainerPayload>
    {
        public CreatePokemonTrainerPayloadValidator()
        {
            RuleFor(p => p.Fullname).NotEmpty().NotNull();
            RuleFor(p => p.Email).NotEmpty().NotNull();
        }
    }
}