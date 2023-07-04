using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.Data
{
    public class Blog
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(15)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
