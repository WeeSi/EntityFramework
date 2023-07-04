using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
namespace PostGresAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<BlogController> _logger;

    public BlogController(ILogger<BlogController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetBlogs")]
    public IEnumerable<Blogs> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Blogs
        {
            Title = DateOnly.FromDateTime(DateTime.Now.AddDays(index)).ToString(),
            Description = Summaries[Random.Shared.Next(Summaries.Length)],
        })
        .ToArray();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Blogs model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        using (AppDbContext dbContext = new())
        {
            // Créer une entité Blog à partir du modèle reçu
            var blogEntity = new Blogs
            {
                Title = model.Title,
                Description = model.Description
            };

            // Ajouter l'entité à votre contexte de base de données
            dbContext.Blogs.Add(blogEntity);

            // Enregistrer les modifications dans la base de données
            dbContext.SaveChangesAsync();
        }

        return Ok(model);
    }
}
