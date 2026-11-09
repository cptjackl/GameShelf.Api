using GameShelf.Api.Models;
using GameShelf.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IGameService, GameService>();
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

app.MapGet("/games", (IGameService service)=> service.GetAll());

app.MapGet("/games/{id}", (int id, IGameService service) =>
{
    var game = service.GetById(id);
    return game is null ? Results.NotFound() : Results.Ok(game);
}
);

app.MapPost("/games", (CreateGameRequest request, IGameService service) =>
{
    var game = service.Create(request);
    return Results.Created($"/games/{game.Id}", game);
});

app.MapPut("/games/{id}", (int id, CreateGameRequest request, IGameService service) =>
{
   var game = service.Update(id,request);
   return game is null ? Results.NotFound() : Results.Ok(game); 
});

app.MapDelete("/games/{id}", (int id, IGameService service)=>
{
    return service.Delete(id);
});
app.Run();

