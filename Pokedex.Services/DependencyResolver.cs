using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Domain;
using Pokedex.Domain.Context;
using Pokedex.Domain.Payloads;
using Pokedex.Domain.Validators;

namespace Pokedex.Services
{
    public static class DependencyResolver
    {
        public static void ConfigDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PokedexContext>(opts => opts.UseInMemoryDatabase(databaseName: "PokedexDb"));
        }

        public static void ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
        }

        public static void ConfigServices(this IServiceCollection services)
        {
            services.AddScoped<IPokemonTrainerService, PokemonTrainerService>();
            services.AddScoped<IPokemonService, PokemonService>();
        }

        public static void ConfigValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreatePokemonTrainerPayload>, CreatePokemonTrainerPayloadValidator>();
            services.AddTransient<IValidator<GetPokemonByIdPayload>, GetPokemonByIdPayloadValidator>();
            services.AddTransient<IValidator<GetPokemonByNamePayload>, GetPokemonByNamePayloadValidator>();
            services.AddTransient<IValidator<GetPokemonsByLimitPayload>, GetPokemonsByLimitPayloadValidator>();
        }
    }
}