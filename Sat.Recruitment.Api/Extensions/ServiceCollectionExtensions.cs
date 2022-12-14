using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Contracts.Engine;
using Sat.Recruitment.DataAccess;
using Sat.Recruitment.DataAccess.Interfaces;
using Sat.Recruitment.DataAccess.Repositories;
using Sat.Recruitment.Api.Validator;
using Sat.Recruitment.Models;
using Sat.Recruitment.Models.Configuration;
using System.Diagnostics.CodeAnalysis;
using Sat.Recruitment.Engine;

namespace Sat.Recruitment.Api.Extensions
{

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void RegisterDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(ConnectionStringSettings.KEY).Get<ConnectionStringSettings>();
            services.AddDbContext<UserContext>(options => options.UseSqlServer(settings.DefaultConnectionString), ServiceLifetime.Transient);
        }

        public static void RegisterValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<User>, UserValidation>();
            services.AddTransient<IValidator<int>, IntegerValidation>();
        }

        public static void RegisterEngines(this IServiceCollection services)
        {
            services.AddScoped<IUserEngine, UserEngine>();
        }
    }
}
