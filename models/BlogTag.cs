using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Data
{
    [PrimaryKey(nameof(BlogId), nameof(TagId))]
    public class BlogTag
    {
        [Required]
        [Column(Order=1)]
        public int BlogId { get; set; }
        [Required]
        [Column(Order = 2)]
        public int TagId { get; set; }
        public Blogs Post { get; set; } = null!;
        public Tags Tag { get; set; } = null!;
    }
}
