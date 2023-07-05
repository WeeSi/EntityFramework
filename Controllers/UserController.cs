
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<Users> Get()
    {
        return _context.Users.ToList();
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
