using GameShelf.Api.Models;
using GameShelf.Api.Services;

namespace GameShelf.Api.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        var games = app.MapGroup("/games");

        games.MapGet("/", (IGameService service) =>
        {
            return service.GetAll();
        });

        games.MapGet("/{id}", (int id, IGameService service) =>
        {
            var game = service.GetById(id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });

        games.MapPost("/", (IGameService service, CreateGameRequest request) =>
        {
            var error = ValidateRequest(request);

            if (error is not null)
            {
                return Results.BadRequest(error);
            }

            var game = service.Create(request);
            return Results.Created($"/games/{game.Id}", game);
        });

        games.MapPut("/{id}", (int id, CreateGameRequest request, IGameService service) =>
        {
            var error = ValidateRequest(request);
            if (error is not null)
            {
                return Results.BadRequest(error);
            }

            var game = service.Update(id, request);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });

        games.MapDelete("/{id}", (int id, IGameService service) =>
        {
            return service.Delete(id) ? Results.NoContent() : Results.NotFound();
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