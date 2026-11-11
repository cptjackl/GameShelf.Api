using GameShelf.Api.Models;

namespace GameShelf.Api.Services;

public interface IGameService
{
    Task<IReadOnlyList<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<Game> CreateAsync(CreateGameRequest request);
    Task<Game?> UpdateAsync(int id, CreateGameRequest request);
    Task<bool> DeleteAsync(int id);
}