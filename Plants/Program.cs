using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPlantService, PlantService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();



public enum Care
{
    Easy,
    Medium,
    Hard
}



public class Plant
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Height { get; set; }
    public Care Care { get; set; }
    public string Supplier { get; set; } = "";
}



public class CreatePlantRequest
{
    public string Name { get; set; } = "";
    public int Height { get; set; }
    public Care Care { get; set; }
}



public class PlantResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Height { get; set; }
    public Care Care { get; set; }
}



public interface IPlantService
{
    List<Plant> GetAll();
    Plant? GetById(int id);
    Plant? Add(CreatePlantRequest request);
}



public class PlantService : IPlantService
{
    private readonly List<Plant> _plants = new List<Plant>
    {
        new Plant { Id = 1, Name = "Кактус", Height = 25, Care = Care.Easy, Supplier = "Зеленый мир" },
        new Plant { Id = 2, Name = "Фикус", Height = 120, Care = Care.Medium, Supplier = "Цветочный дом" },
        new Plant { Id = 3, Name = "Орхидея", Height = 40, Care = Care.Hard, Supplier = "Орхидея плюс" },
        new Plant { Id = 4, Name = "Алоэ", Height = 35, Care = Care.Easy, Supplier = "Зеленый мир" },
        new Plant { Id = 5, Name = "Монстера", Height = 90, Care = Care.Medium, Supplier = "Тропик" },
        new Plant { Id = 6, Name = "Роза", Height = 50, Care = Care.Medium, Supplier = "Цветочный дом" },
        new Plant { Id = 7, Name = "Папоротник", Height = 60, Care = Care.Hard, Supplier = "Тропик" },
        new Plant { Id = 8, Name = "Сансевиерия", Height = 70, Care = Care.Easy, Supplier = "Зеленый мир" }
    };

    private int _lastId = 8;

    public List<Plant> GetAll()
    {
        return _plants;
    }

    public Plant? GetById(int id)
    {
        return _plants.FirstOrDefault(p => p.Id == id);
    }

    public Plant? Add(CreatePlantRequest request)
    {
        
        bool exists = _plants.Any(p =>
            p.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

        if (exists)
        {
            return null;
        }

        var plant = new Plant
        {
            Id = ++_lastId,
            Name = request.Name,
            Height = request.Height,
            Care = request.Care,
            Supplier = "Неизвестный поставщик"
        };

        _plants.Add(plant);
        return plant;
    }
}


[ApiController]
[Route("api/[controller]")]
public class PlantsController : ControllerBase
{
    private readonly IPlantService _plantService;

    public PlantsController(IPlantService plantService)
    {
        _plantService = plantService;
    }

    
    [HttpGet]
    public ActionResult<List<PlantResponse>> GetAll()
    {
        var plants = _plantService.GetAll();

        var response = plants.Select(p => ToResponse(p)).ToList();

        return Ok(response);
    }

    
    [HttpGet("{id:int}")]
    public ActionResult<PlantResponse> GetById(int id)
    {
        var plant = _plantService.GetById(id);

        if (plant is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(plant));
    }

    
    [HttpPost]
    public ActionResult<PlantResponse> Create(CreatePlantRequest request)
    {
        var plant = _plantService.Add(request);

        if (plant is null)
        {
            return Conflict($"Растение с названием уже существует: {request.Name}");
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = plant.Id },
            ToResponse(plant));
    }

    private PlantResponse ToResponse(Plant plant)
    {
        return new PlantResponse
        {
            Id = plant.Id,
            Name = plant.Name,
            Height = plant.Height,
            Care = plant.Care
        };
    }
}
