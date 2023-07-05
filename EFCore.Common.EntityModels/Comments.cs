using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Common.EntityModels
{
    public class Comments : BaseModel
    {
        [Key]
        public int CommentId { get; set; }
        [MaxLength(40)]
        public string Comment { get; set; } = String.Empty;
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        // public Users User { get; set; } = null!;
        [ForeignKey(nameof(BlogId))]
        public int BlogId { get; set; }
        // public Blogs Blog { get; set; } = null!;
    }
}
