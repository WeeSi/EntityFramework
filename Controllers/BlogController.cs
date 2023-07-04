using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<Blog> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Blog
        {
            CategoryName = DateOnly.FromDateTime(DateTime.Now.AddDays(index)).ToString(),
            Description = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Blog model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok();
    }
}
