using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.Data
{
    public class Tags
    {
        [Required, Key]
        public int TagId { get; set; }
        [Required, MaxLength(40)]
        public string TagName { get; set; } = String.Empty;
        public List<BlogTag> BlogTags { get; } = new();
    }
}
