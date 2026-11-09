using GameShelf.Api.Models;

namespace GameShelf.Api.Services;

public interface IGameService
{
    IReadOnlyList<Game> GetAll();
    Game? GetById(int id);
    Game Create(CreateGameRequest request);
    Game? Update(int id, CreateGameRequest request);
    bool Delete(int id);
}