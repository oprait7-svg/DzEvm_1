var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "v1"));


var readings = new List<Reading>
{
    new(1, "Москва", -5),
    new(2, "Москва", 3),
    new(3, "Сочи", 12),
    new(4, "Сочи", 0),
    new(5, "Казань", -11),
    new(6, "Казань", 7)
};

app.MapGet("/readings", (string? city, int? min) =>
{
    IEnumerable<Reading> result = readings;
    if (city != null)
        result = result.Where(r => r.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    if (min != null)
        result = result.Where(r => r.Temp >= min);
    return result;
});

app.MapGet("/readings/{id:int}", (int id) =>
{
    var reading = readings.FirstOrDefault(r => r.Id == id);
    if (reading is null)
        return Results.NotFound();
    return Results.Ok(reading);
});


var notes = new List<Note>();
int lastId = 0;

app.MapPost("/notes", (NoteInput input) =>
{
    var note = new Note(++lastId, input.Text);
    notes.Add(note);
    return note;
});

app.MapGet("/notes", () => notes);

app.MapGet("/notes/{id:int}", (int id) =>
{
    var note = notes.FirstOrDefault(n => n.Id == id);
    if (note is null)
        return Results.NotFound();
    return Results.Ok(note);
});

app.Run();

record Reading(int Id, string City, int Temp);
record NoteInput(string Text);
record Note(int Id, string Text);

