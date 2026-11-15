using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(o =>
    o.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter(allowIntegerValues: false)));

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "v1"));



var games = new List<Game>
{
    new(1, "Каркассон", 5, Kind.Family, new DateOnly(2000, 1, 1), "склад"),
    new(2, "Колонизаторы", 4, Kind.Strategy, new DateOnly(1995, 1, 1), "склад"),
    new(3, "Имаджинариум", 7, Kind.Party, new DateOnly(2010, 1, 1), "склад"),
    new(4, "Бэнг", 7, Kind.Party, new DateOnly(2002, 1, 1), "склад"),
    new(5, "Билет на поезд", 5, Kind.Family, new DateOnly(2004, 1, 1), "склад"),
    new(6, "Кодовые имена", 8, Kind.Party, new DateOnly(2015, 1, 1), "склад"),
    new(7, "Терраформирование Марса", 5, Kind.Strategy, new DateOnly(2016, 1, 1), "склад"),
};
int lastId = 7;


GameResponse ToResponse(Game g) => new(g.Id, g.Title, g.Players, g.Kind, g.Released);

app.MapPost("/games", (CreateGameRequest request) =>
{
    if (games.Any(g => g.Title == request.Title))
        return Results.Conflict($"уже есть: {request.Title}");
    var game = new Game(++lastId, request.Title, request.Players,
        request.Kind, request.Released, "склад");
    games.Add(game);
    return Results.Created($"/games/{game.Id}", ToResponse(game));
});

app.MapGet("/games/{id:int}", (int id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    if (game is null)
        return Results.NotFound();
    return Results.Ok(ToResponse(game));
});

app.MapGet("/games", (int page = 1, int size = 5) =>
{
    if (page < 1 || size < 1 || size > 20)
        return Results.BadRequest("page от 1, size от 1 до 20");
    var items = games.Skip((page - 1) * size)
                     .Take(size)
                     .Select(ToResponse).ToList();
    int pages = (games.Count + size - 1) / size;
    return Results.Ok(new PageResponse(items, page, size, games.Count, pages));
});

app.MapPut("/games/{id:int}", (int id, CreateGameRequest request) =>
{
    int index = games.FindIndex(g => g.Id == id);
    if (index < 0) return Results.NotFound();
    if (games.Any(g => g.Id != id && g.Title == request.Title))
        return Results.Conflict($"уже есть: {request.Title}");
    games[index] = games[index] with
    {
        Title = request.Title,
        Players = request.Players,
        Kind = request.Kind,
        Released = request.Released
    };
    return Results.Ok(ToResponse(games[index]));
});

app.MapDelete("/games/{id:int}", (int id) =>
{
    if (games.RemoveAll(g => g.Id == id) == 0)
        return Results.NotFound();
    return Results.NoContent();
});

app.MapPatch("/games/{id:int}", (int id, PatchGameRequest request) =>
{
    int index = games.FindIndex(g => g.Id == id);
    if (index < 0) return Results.NotFound();
    if (games.Any(g => g.Id != id && g.Title == request.Title))
        return Results.Conflict($"уже есть: {request.Title}");
    var old = games[index];
    games[index] = old with
    {
        Title = request.Title ?? old.Title,
        Players = request.Players ?? old.Players,
        Kind = request.Kind ?? old.Kind,
        Released = request.Released ?? old.Released
    };
    return Results.Ok(ToResponse(games[index]));
});

app.Run();

enum Kind { Strategy, Party, Family }
record Game(int Id, string Title, int Players, Kind Kind, DateOnly Released, string Shelf);
record CreateGameRequest(string Title, int Players, Kind Kind, DateOnly Released);
record GameResponse(int Id, string Title, int Players, Kind Kind, DateOnly Released);
record PatchGameRequest(string? Title = null, int? Players = null,
    Kind? Kind = null, DateOnly? Released = null);
record PageResponse(List<GameResponse> Items, int Page, int Size, int Total, int Pages);
