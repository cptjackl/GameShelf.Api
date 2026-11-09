using GameShelf.Api.Models;

namespace GameShelf.Api.Services;

public class GameService : IGameService
{
    private readonly List<Game> _games = new(){
        new(1,"hades","pc",2020),
        new(2,"hades","playstation",2020),
        new(3,"assassins creed","switch",2021),
    };

    public IReadOnlyList<Game> GetAll() => _games;

    public Game? GetById(int id)
    {
        var game = _games.FirstOrDefault(g=>g.Id==id);
        return game; 
    }
    
    public Game Create(CreateGameRequest request)
    {
        var nextId = _games.Count == 0 ? 1 : _games.Max(g=> g.Id) +1;
        var game = new Game(nextId, request.Title, request.Platform, request.ReleaseYear);
        _games.Add(game);
        return game;
    }
    
    public Game? Update(int id, CreateGameRequest request)
    {
        var index = _games.FindIndex(g => g.Id == id);
        if (index == -1) return null;

        _games[index] = new Game(id, request.Title, request.Platform, request.ReleaseYear);
        return _games[index];  
    }
    
    public bool Delete(int id)
    {
        var game = _games.FirstOrDefault(g=>g.Id==id);
        
        if (game is null) {
            return false;
        }
        else
        {
            _games.Remove(game);
            return true;   
        }
    }
}