using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.Data
{
    public class Tags: BaseModel
    {
        [Key]
        public int TagId { get; set; }
        [Required, MaxLength(40)]
        public string TagName { get; set; } = String.Empty;
    }
}
