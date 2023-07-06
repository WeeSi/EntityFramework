using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EFCore.Common.EntityModels;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    public ActionResult<IList<Blogs>> Get()
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };
        List<Blogs> blogs = _context.Blogs.Include(b => b.User).Include(b => b.Comments).Include(b => b.Category).ToList();
        var jsonString = JsonSerializer.Serialize(blogs, options);
         return Ok(new { blogs = JsonSerializer.Deserialize<JsonElement>(jsonString)});
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromQuery(Name = "Title")] string Title,
        [FromQuery(Name = "Subtitle")] string? Subtitle,
        [FromQuery(Name = "Description")] string? Description,
        [FromQuery(Name = "Content")] string Content,
        [FromQuery(Name = "BlogDate")] DateTime BlogDate,
        [FromQuery(Name = "UserId")] int UserId,
        [FromQuery(Name = "CategoryId")] int CategoryId,
        [FromQuery(Name = "TagIds")] List<int> Tags)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu
        var TagsList = new List<Tags>();
        foreach (int tagId in Tags)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag == null)
            {
                return NotFound($"Tag introuvable pour l'ID {tagId}");
            }
            else TagsList.Add(tag);
        };
        var user = await _context.Users.FindAsync(UserId);
        if (user == null)
        {
            return NotFound($"Utilisateur introuvable pour l'ID {UserId}");
        }

        // Récupérer le blog associé à l'ID spécifié
        if(CategoryId==0) return NotFound("category obligatoire");
        var category = await _context.Categories.FindAsync(CategoryId);
        if (category == null)
        {
            return NotFound($"category introuvable pour l'ID {CategoryId}");
        }

        var blogEntity = new Blogs
        {
            Title = Title,
            Description = Description,
            Subtitle = Subtitle,
            BlogDate = BlogDate,
            Content = Content,
            Category = category,
            User = user,
            Tags = TagsList
        };

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            // Autres options de sérialisation si nécessaire
        };

        var jsonString = JsonSerializer.Serialize(blogEntity, options);
        // Ajouter l'entité à votre contexte de base de données
        _context.Blogs.Add(blogEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(jsonString);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(
        int id,
        [FromQuery(Name = "Title")] string? Title,
        [FromQuery(Name = "Subtitle")] string? Subtitle,
        [FromQuery(Name = "Description")] string? Description,
        [FromQuery(Name = "Content")] string? Content,
        [FromQuery(Name = "BlogDate")] DateTime? BlogDate,
        [FromQuery(Name = "CategoryId")] int? CategoryId,
        [FromQuery(Name = "TagIds")] List<int>? Tags)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            // Créer une instance de votre contexte de base de données (DbContext)
            // Créer une entité Blog à partir du modèle reçu
            var TagsList = new List<Tags>();
            foreach (int tagId in Tags)
            {
                var tag = await _context.Tags.FindAsync(tagId);
                if (tag == null)
                {
                    return NotFound($"Tag introuvable pour l'ID {tagId}");
                }
                else TagsList.Add(tag);
            };

            Categories? category = null;
            // Récupérer le blog associé à l'ID spécifié
            if(CategoryId != null)
            { 
                category = await _context.Categories.FindAsync(CategoryId);
                if (category == null )
                {
                    return NotFound($"category introuvable pour l'ID {CategoryId}");
                }
            }

            var existingBlogs = _context.Blogs.Where(s => s.BlogId == id)
                                                    .FirstOrDefault<Blogs>();

            if (existingBlogs != null)
            {
                if(Title != null)
                    existingBlogs.Title = Title;
                if(Description != null)
                    existingBlogs.Description = Description;
                if(Subtitle != null)
                    existingBlogs.Subtitle = Subtitle;
                if(category != null)
                    existingBlogs.Category = category;
                if(TagsList != null)
                    existingBlogs.Tags = TagsList;

                _context.SaveChanges();
                return Ok(existingBlogs);
            }
            else
            {
                return NotFound($"Blog introuvable pour l'ID {id}");
            }

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
     [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int? id){
        try
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound($"Blog avec l'id {id} introuvable!");
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return Ok(new {blog=blog, msg=$"Blog avec l'id {id} supprimé!"});
        }
        catch (Exception err)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error: {err}");
        }
    }
}
