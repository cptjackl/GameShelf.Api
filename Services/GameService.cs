using GameShelf.Api.Data;
using GameShelf.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Api.Services;

public class GameService : IGameService
{
    private readonly GameDbContext _database;

    public GameService(GameDbContext database)
    {
        _database = database;
    }

    public async Task<IReadOnlyList<Game>> GetAllAsync() => await _database.Games.ToListAsync();

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await _database.Games.FindAsync(id);
    }

    public async Task<Game> CreateAsync(CreateGameRequest request)
    {
        var game = new Game(0, request.Title, request.Platform, request.ReleaseYear);
        _database.Games.Add(game);
        await _database.SaveChangesAsync();
        return game;

    }

    public async Task<Game?> UpdateAsync(int id, CreateGameRequest request)
    {
        var game = await _database.Games.FindAsync(id);
        if (game is null) return null;

        game.Platform = request.Platform;
        game.Title = request.Title;
        game.ReleaseYear = request.ReleaseYear;

        await _database.SaveChangesAsync();
        return game;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await _database.Games.FindAsync(id);
        if (game is null) return false;

        _database.Games.Remove(game);
        await _database.SaveChangesAsync();
        return true;
    }
}