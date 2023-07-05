
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using EFCore.Common.EntityModels;
namespace PostGresAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly AppDbContext _context;

    public TagController(ILogger<TagController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetTags")]
    public IEnumerable<Tags> Get()
    {
        return _context.Tags.ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromQuery(Name = "TagName")] string TagName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu
        var tagEntity = new Tags
        {
            TagName = TagName,
        };

        // Ajouter l'entité à votre contexte de base de données
        _context.Tags.Add(tagEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(tagEntity);
    }
}
