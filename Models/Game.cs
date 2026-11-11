namespace GameShelf.Api.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Platform { get; set; }
    public int? ReleaseYear { get; set; }

    public Game(int id, string title, string platform, int? releaseYear)
    {
        Id = id;
        Title = title;
        Platform = platform;
        ReleaseYear = releaseYear;

    }
}


