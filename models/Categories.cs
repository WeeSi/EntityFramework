using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Data
{
    public class Categories
    {
        [Required, Key]
        public int CategoryId { get; set; }
        [Required, MaxLength(40)]
        public string CategoryName { get; set; } = String.Empty;
        public ICollection<Blogs> Blogs { get; } = new List<Blogs>();
    }
}
