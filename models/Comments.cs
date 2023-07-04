using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSQL.Data
{
    public class Comments: BaseModel
    {
        [Key]
        public int CommentId { get; set; }
        [Required, MaxLength(40)]
        public string CategoryName { get; set; } = String.Empty;
        public int UserId { get; }
        [ForeignKey(nameof(UserId))]
        public Users User { get; set; } = null!;
        public int BlogId { get; }
        [ForeignKey(nameof(BlogId))]
        public Blogs Blog { get; set; } = null!;
    }
}
