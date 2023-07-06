
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
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _context;

    public UserController(ILogger<UserController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetUsers")]
    public ActionResult<IList<Users>> Get()
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };
        List<Users> users = _context.Users.Include(b => b.Comments).Include(b => b.Blogs).ToList();
        var jsonString = JsonSerializer.Serialize(users, options);
         return Ok(new { users = JsonSerializer.Deserialize<JsonElement>(jsonString)});
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromQuery(Name = "UserName")] string UserName,
        [FromQuery(Name = "UserMail")] string UserMail)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une instance de votre contexte de base de données (DbContext)
        // Créer une entité Blog à partir du modèle reçu
        var userEntity = new Users
        {
            UserName = UserName,
            UserMail = UserMail,
        };

        // Ajouter l'entité à votre contexte de base de données
        _context.Users.Add(userEntity);

        // Enregistrer les modifications dans la base de données
        await _context.SaveChangesAsync();

        return Ok(userEntity);
    }
}
