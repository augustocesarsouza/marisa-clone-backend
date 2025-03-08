using Marisa.Application.Mappings;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Marisa.Domain.Authentication;
using Marisa.Infra.Data.Authentication;
using Marisa.Infra.Data.UtilityExternal;
using Marisa.Infra.Data.UtilityExternal.Interface;
using Marisa.Application.Services.Interfaces;
using Marisa.Application.Services;
using brevo_csharp.Api;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.DTOs.Validations.UserValidator;

namespace Marisa.Infra.IoC
{
    public static class DependectyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //var connectionString = configuration.GetConnectionString("Default");
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddAutoMapper(typeof(DtoToDomainMapping));

            services.AddDbContext<ApplicationDbContext>(
                  options => options.UseNpgsql(connectionString)); // Server=ms-sql-server; quando depender dele no Docker-Compose

            services.AddStackExchangeRedisCache(redis =>
            {
                redis.Configuration = "localhost:7006";
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));

            services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IUserCreateAccountFunction, UserCreateAccountFunction>();
            services.AddScoped<ITokenGeneratorUser, TokenGeneratorUser>();
            services.AddScoped<ICloudinaryUti, CloudinaryUti>();
            services.AddScoped<ICacheRedisUti, CacheRedisUti>();
            services.AddScoped<ISendEmailBrevo, SendEmailBrevo>();
            services.AddScoped<ISendEmailUser, SendEmailUser>();
            services.AddScoped<ITransactionalEmailApiUti, TransactionalEmailApiUti>();
            
            services.AddScoped<IUserSendCodeEmailDTOValidator, UserSendCodeEmailDTOValidator>();
            services.AddScoped<IUserCreateDTOValidator, UserCreateDTOValidator>();

            return services;
        }
    }
}
