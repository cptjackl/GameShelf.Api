using GameShelf.Api.Models;
using GameShelf.Api.Services;

namespace GameShelf.Api.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        var games = app.MapGroup("/games");

        games.MapGet("/", async (IGameService service) =>
        {
            return await service.GetAllAsync();
        });

        games.MapGet("/{id}", async (int id, IGameService service) =>
        {
            var game = await service.GetByIdAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });

        games.MapPost("/", async (IGameService service, CreateGameRequest request) =>
        {
            var error = ValidateRequest(request);

            if (error is not null)
            {
                return Results.BadRequest(error);
            }

            var game = await service.CreateAsync(request);
            return Results.Created($"/games/{game.Id}", game);
        });

        games.MapPut("/{id}", async (int id, CreateGameRequest request, IGameService service) =>
        {
            var error = ValidateRequest(request);
            if (error is not null)
            {
                return Results.BadRequest(error);
            }

            var game = await service.UpdateAsync(id, request);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });

        games.MapDelete("/{id}", async (int id, IGameService service) =>
        {
            return await service.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        });
    }

    private static string? ValidateRequest(CreateGameRequest request)
    {
        if (request.Title == "")
        {
            return "You need to provide a title for the game";
        }

        if (request.ReleaseYear > DateTime.Now.Year || request.ReleaseYear < 1950)
        {
            return "Invalid year";
        }

        return null;
    }
}