using ITISHub.API.Extensions;
using ITISHub.Application.Interfaces.Auth;
using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Application.Services;
using ITISHub.Core.Enums;
using ITISHub.Infrastructure.Auth;
using ITISHub.Persistence;
using ITISHub.Persistence.Repositories;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Configuration
    .AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

var configuration = builder.Configuration;
var services = builder.Services;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
services.AddApiAuthentication(configuration);

services.AddDbContext<SocialNetworkDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(SocialNetworkDbContext))); });

services.AddAuthorizationPolicy("RequireAdmin", new[] { Permission.Delete });
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<UsersService>();
services.AddScoped<ErrorResponseFactory>();

services.AddAutoMapper(typeof(DataBaseMappings));

services.AddValidators();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
