using GameShelf.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var games = new List<Game>
{
    new(1,"hades","pc",2020),
    new(2,"hades","playstation",2020),
    new(3,"assassins creed","switch",2021),
};

app.MapGet("/games", ()=>games);

app.MapGet("/games/{id}", (int id) =>
{
    var game = games.FirstOrDefault(g=>g.Id==id);
    return game is null ? Results.NotFound() : Results.Ok(game);
}
);

app.MapPost("/games", (CreateGameRequest request) =>
{
    var nextId = games.Count == 0 ? 1 : games.Max(g=> g.Id) +1;
    var game = new Game(nextId, request.Title, request.Platform, request.ReleaseYear);
    games.Add(game);
    return Results.Created($"/games/{game.Id}", game);
});

app.MapPut("/games/{id}", (int id, CreateGameRequest request) =>
{
   var index = games.FindIndex(g => g.Id == id);
   if (index == -1) return Results.NotFound();

   games[index] = new Game(id, request.Title, request.Platform, request.ReleaseYear);
   return Results.Ok(games[index]); 
});

app.MapDelete("/games/{id}", (int id)=>
{
    var game = games.FirstOrDefault(g=>g.Id==id);
    
    if (game is null) {
        return Results.NotFound();
    }
    else
    {
        games.Remove(game);
        return Results.NoContent();;   
    }
});
app.Run();

