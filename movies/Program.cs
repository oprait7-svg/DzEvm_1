using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(o =>
    o.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter(allowIntegerValues: false)));

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "v1"));


var movies = new List<Movie>
{
    new(1, "Семь", Genre.Detective, new DateOnly(1995, 9, 22), 8, "куплен 2020"),
    new(2, "Однажды в… Голливуде", Genre.Comedy, new DateOnly(2009, 8, 8), 8, "куплен 2019"),
    new(3, "Назад в будущее 2", Genre.Comedy, new DateOnly(1985, 6, 3), 8, "куплен 2021")
};
int lastId = 3;

MovieResponse ToResponse(Movie m) => new(m.Id, m.Title, m.Genre, m.Released, m.Rating);

app.MapPost("/movies", (CreateMovieRequest request) =>
{
    if (movies.Any(m => m.Title == request.Title))
        return Results.Conflict($"уже есть: {request.Title}");
    var movie = new Movie(++lastId, request.Title, request.Genre,
        request.Released, request.Rating, "только для своих");
    movies.Add(movie);
    return Results.Created($"/movies/{movie.Id}", ToResponse(movie));
});

app.MapGet("/movies/{id:int}", (int id) =>
{
    var movie = movies.FirstOrDefault(m => m.Id == id);
    if (movie is null)
        return Results.NotFound();
    return Results.Ok(ToResponse(movie));
});

app.MapGet("/movies", (int page = 1, int size = 5) =>
{
    if (page < 1 || size < 1 || size > 20)
        return Results.BadRequest("page от 1, size от 1 до 20");
    var items = movies.Skip((page - 1) * size)
                      .Take(size)
                      .Select(ToResponse).ToList();
    return Results.Ok(new PageResponse(items, page, size, movies.Count));
});

app.MapPut("/movies/{id:int}", (int id, CreateMovieRequest request) =>
{
    int index = movies.FindIndex(m => m.Id == id);
    if (index < 0) return Results.NotFound();
    if (movies.Any(m => m.Id != id && m.Title == request.Title))
        return Results.Conflict($"уже есть: {request.Title}");
    movies[index] = movies[index] with
    {
        Title = request.Title,
        Genre = request.Genre,
        Released = request.Released,
        Rating = request.Rating
    };
    return Results.Ok(ToResponse(movies[index]));
});

app.MapPatch("/movies/{id:int}", (int id, PatchMovieRequest request) =>
{
    int index = movies.FindIndex(m => m.Id == id);
    if (index < 0) return Results.NotFound();
    if (movies.Any(m => m.Id != id && m.Title == request.Title))
        return Results.Conflict($"уже есть: {request.Title}");
    var old = movies[index];
    movies[index] = old with
    {
        Title = request.Title ?? old.Title,
        Genre = request.Genre ?? old.Genre,
        Released = request.Released ?? old.Released,
        Rating = request.Rating ?? old.Rating
    };
    return Results.Ok(ToResponse(movies[index]));
});

app.MapDelete("/movies/{id:int}", (int id) =>
{
    if (movies.RemoveAll(m => m.Id == id) == 0)
        return Results.NotFound();
    return Results.NoContent();
});

app.Run();

enum Genre { Drama, Detective, Action }
record Movie(int Id, string Title, Genre Genre, DateOnly Released, int Rating, string InternalNote);
record CreateMovieRequest(string Title, Genre Genre, DateOnly Released, int Rating);
record MovieResponse(int Id, string Title, Genre Genre, DateOnly Released, int Rating);
record PatchMovieRequest(string? Title = null, Genre? Genre = null,
    DateOnly? Released = null, int? Rating = null);
record PageResponse(List<MovieResponse> Items, int Page, int Size, int Total);
