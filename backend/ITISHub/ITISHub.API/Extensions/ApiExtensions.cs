using FluentValidation;
using ITISHub.API.Contracts;
using ITISHub.API.Validation;
using ITISHub.Application.Interfaces.Auth;
using ITISHub.Application.Services;
using ITISHub.Core.Enums;
using ITISHub.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ITISHub.API.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(
             this IServiceCollection services,
             IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        // схема как должна работать апи, когд к ней подключается какой то пользователь
        // конретно эта проверяет, что в заголовках передается вообще токен
        // Если будет схема куки, то будет через куки

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["tasty-cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization();
    }

    public static void AddAuthorizationPolicy(this IServiceCollection services, string policyName, Permission[] permissions)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(policyName, policy =>
                policy.Requirements.Add(new PermissionRequirement(permissions)));
        });
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        services.AddScoped<IValidator<ChangeUserRolesRequest>, ChangeUserRolesRequestValidator>();
        services.AddScoped<IValidator<GetUserByUserNameRequest>, GetUserByUserNameRequestValidator>();
    }
}
