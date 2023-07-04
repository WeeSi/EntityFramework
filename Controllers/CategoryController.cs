using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace PostGresAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly AppDbContext _context;

    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetCategories")]
    public IEnumerable<Categories> Get()
    {
        return _context.Categories.ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Categories model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu
        var categoryEntity = new Categories
        {
            CategoryName = model.CategoryName,
        };

        // Ajouter l'entité à votre contexte de base de données
        _context.Categories.Add(categoryEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(model);
    }
}
