using GameShelf.Api.Models;
using GameShelf.Api.Services;
using GameShelf.Api.Endpoints;
using GameShelf.Api.Data;
using Microsoft.EntityFrameworkCore;
using YamlDotNet.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "GameShelf";
    config.Title = "GameShelf v1";
    config.Version = "v1";
});

var connectionString = builder.Configuration.GetConnectionString("GameShelf");

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.AddScoped<IGameService, GameService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "GameShelf";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();

app.MapGameEndpoints();

app.Run();

