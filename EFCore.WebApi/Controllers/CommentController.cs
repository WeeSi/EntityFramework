
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using EFCore.Common.EntityModels;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostGresAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly AppDbContext _context;

    public CommentController(ILogger<CommentController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetComments")]
    public IEnumerable<Comments> Get()
    {
        return _context.Comments.ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromQuery(Name = "Comment")] string Comment,
        [FromQuery(Name = "UserId")] int UserId,
        [FromQuery(Name = "BlogId")] int BlogId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FindAsync(UserId);
        if (user == null)
        {
            return NotFound($"Utilisateur introuvable pour l'ID {UserId}");
        }

        // Récupérer le blog associé à l'ID spécifié
        var blog = await _context.Blogs.FindAsync(BlogId);
        if (blog == null)
        {
            return NotFound($"Blog introuvable pour l'ID {BlogId}");
        }
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            // Autres options de sérialisation si nécessaire
        };

        var userJson = JsonSerializer.Serialize(user, options);
        var blogJson = JsonSerializer.Serialize(blog, options);
        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu
        var commentEntity = new Comments
        {
            Comment = Comment,
            BlogId = BlogId,
            UserId = UserId
        };

        // Ajouter l'entité à votre contexte de base de données
        _context.Comments.Add(commentEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(new {Comment= Comment, User= userJson, Blog= blogJson});
    }
}
