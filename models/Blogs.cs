using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSQL.Data
{
    public class Blogs: BaseModel
    {
        [Required, Key]
        public int BlogId { get; set; }
        public DateTime BlogDate { get; set; }
        [ MaxLength(40)]
        public string Title { get; set; } = String.Empty;
        [ MaxLength(60)]
        public string Subtitle { get; set; } = String.Empty;
        [ MaxLength(150)]
        public string Description { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public int CustomerId { get; }
        [ForeignKey(nameof(CustomerId))]
        public Customers Customer { get; set; } = null!;
        public int CategoryId { get; }
        [ForeignKey(nameof(CategoryId))]
        public Categories Category { get; set; } = null!;
        public ICollection<Comments> Comments { get; } = new List<Comments>();
        public List<BlogTag> BlogTags { get; } = new();
    }
}