using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PostGresAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly AppDbContext _context;

    private readonly ILogger<BlogController> _logger;

    public BlogController(ILogger<BlogController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
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
    public async Task<IActionResult> Post(
        [FromQuery(Name = "Title")] string Title,
        [FromQuery(Name = "Subtitle")] string Subtitle,
        [FromQuery(Name = "Description")] string Description,
        [FromQuery(Name = "Content")] string Content,
        [FromQuery(Name = "BlogDate")] DateTime BlogDate,
        [FromQuery(Name = "UserId")] int UserId,
        [FromQuery(Name = "CategoryId")] int CategoryId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu

        var user = await _context.Users.FindAsync(UserId);
        if (user == null)
        {
            return NotFound($"Utilisateur introuvable pour l'ID {UserId}");
        }

        // Récupérer le blog associé à l'ID spécifié
        var category = await _context.Categories.FindAsync(CategoryId);
        // if (category == null)
        // {
        //     return NotFound($"category introuvable pour l'ID {CategoryId}");
        // }

        var blogEntity = new Blogs
        {
            Title = Title,
            Description = Description,
            Subtitle = Subtitle,
            BlogDate = BlogDate,
            Content = Content,
            Category = category,
            User = user,
        };

        // Ajouter l'entité à votre contexte de base de données
        _context.Blogs.Add(blogEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(blogEntity);
    }
}
