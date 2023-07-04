
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Post([Bind("Comment,UserId,BlogId")] Comments model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FindAsync(model.UserId);
        if (user == null)
        {
            return NotFound($"Utilisateur introuvable pour l'ID {model.UserId}");
        }

        // Récupérer le blog associé à l'ID spécifié
        var blog = await _context.Blogs.FindAsync(model.BlogId);
        if (blog == null)
        {
            return NotFound($"Blog introuvable pour l'ID {model.BlogId}");
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu
        var commentEntity = new Comments
        {
            Comment = model.Comment,
            Blog = blog,
            User = user
        };

        // Ajouter l'entité à votre contexte de base de données
        _context.Comments.Add(commentEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(model);
    }
}
