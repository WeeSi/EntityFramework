using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Common.EntityModels
{
    public class Blogs : BaseModel
    {
        [Key]
        public int BlogId { get; set; }
        public DateTime BlogDate { get; set; }
        [MaxLength(40)]
        public string Title { get; set; } = null!;
        [MaxLength(60)]
        public string? Subtitle { get; set; }
        [MaxLength(150)]
        public string? Description { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Users User { get; set; } = null!;
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Categories Category { get; set; } = null!;
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public ICollection<Tags> Tags { get; set; } = new List<Tags>();
        
    }
}