namespace GameShelf.Api.Models;

public record CreateGameRequest(string Title, string Platform, int? ReleaseYear);