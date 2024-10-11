using ITISHub.Application.Interfaces.Repositories;
using ITISHub.Application.Services;
using ITISHub.Persistence;
using ITISHub.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

services.AddDbContext<SocialNetworkDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(SocialNetworkDbContext)));
});

services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<UsersService>();

builder.Services.AddAutoMapper(typeof(DataBaseMappings));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
